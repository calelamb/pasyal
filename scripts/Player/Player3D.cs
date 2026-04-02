using Godot;

namespace Pasyal;

public partial class Player3D : CharacterBody3D
{
    [Export] public float WalkSpeed = 5.0f;
    [Export] public float RunSpeed = 8.0f;
    [Export] public float JumpVelocity = 6.0f;
    [Export] public float MouseSensitivity = 0.003f;

    private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    private Camera3D _camera = null!;

    public override void _Ready()
    {
        // Assuming Camera3D is a child of a Node3D "Head"
        _camera = GetNode<Camera3D>("Head/Camera3D");
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            RotateY(-mouseMotion.Relative.X * MouseSensitivity);
            var head = GetNode<Node3D>("Head");
            head.RotateX(-mouseMotion.Relative.Y * MouseSensitivity);

            Vector3 cameraRot = head.Rotation;
            cameraRot.X = Mathf.Clamp(cameraRot.X, -Mathf.Pi / 2, Mathf.Pi / 2);
            head.Rotation = cameraRot;
        }

        // Toggle mouse trap
        if (@event.IsActionPressed("ui_cancel"))
        {
            Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured 
                ? Input.MouseModeEnum.Visible 
                : Input.MouseModeEnum.Captured;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y -= _gravity * (float)delta;

        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
            velocity.Y = JumpVelocity;

        float currentSpeed = Input.IsActionPressed("run") ? RunSpeed : WalkSpeed;

        // Get the input direction and handle the movement/deceleration.
        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        
        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * currentSpeed;
            velocity.Z = direction.Z * currentSpeed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, currentSpeed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, currentSpeed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
