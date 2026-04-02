using Godot;
using System;

namespace Pasyal;

public partial class Player : CharacterBody3D
{
    [Signal]
    public delegate void InteractedWithEventHandler(Node target);

    [Export] public float WalkSpeed { get; set; } = 5.0f;
    [Export] public float RunSpeed { get; set; } = 8.0f;
    [Export] public float JumpVelocity { get; set; } = 4.5f;
    [Export] public float MouseSensitivity { get; set; } = 0.002f;

    public float Gravity { get; set; } = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    private Camera3D _camera = null!;
    private RayCast3D _interactRay = null!;

    public override void _Ready()
    {
        AddToGroup("player");

        _camera = GetNodeOrNull<Camera3D>("Camera3D");
        _interactRay = GetNodeOrNull<RayCast3D>("Camera3D/InteractRay");

        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (IsDialogueActive())
        {
            Input.MouseMode = Input.MouseModeEnum.Visible;
            return;
        }

        Input.MouseMode = Input.MouseModeEnum.Captured;

        if (@event is InputEventMouseMotion mouseMotion && _camera != null)
        {
            RotateY(-mouseMotion.Relative.X * MouseSensitivity);
            _camera.RotateX(-mouseMotion.Relative.Y * MouseSensitivity);
            
            var rotation = _camera.Rotation;
            rotation.X = Mathf.Clamp(rotation.X, -Mathf.Pi / 2, Mathf.Pi / 2);
            _camera.Rotation = rotation;
        }

        if (@event.IsActionPressed("interact"))
        {
            TryInteract();
        }
        
        // Escape to free mouse (temporary feature)
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && keyEvent.Keycode == Key.Escape)
        {
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (IsDialogueActive()) return;

        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity.Y -= Gravity * (float)delta;
        }

        // Handle Jump (fallback to standard ui_accept which is Space usually, or custom "jump" if defined in InputMap)
        if ((Input.IsActionJustPressed("ui_accept") || Input.IsActionJustPressed("jump")) && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
        }

        // Get the input direction and handle the movement/deceleration.
        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        float speed = Input.IsActionPressed("run") ? RunSpeed : WalkSpeed;

        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * speed;
            velocity.Z = direction.Z * speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    private void TryInteract()
    {
        if (_interactRay == null) return;

        _interactRay.ForceRaycastUpdate();

        if (!_interactRay.IsColliding()) return;

        var collider = _interactRay.GetCollider();

        if (collider is not Node target) return;

        if (target.IsInGroup("interactable") || target.HasMethod("Interact"))
        {
            if (target.HasMethod("Interact"))
            {
                target.Call("Interact");
            }

            EmitSignal(SignalName.InteractedWith, target);
        }
    }

    private bool IsDialogueActive()
    {
        var dialogueManager = GetNodeOrNull<Pasyal.Systems.DialogueManager>("/root/DialogueManager");
        return dialogueManager?.IsActive ?? false;
    }
}
