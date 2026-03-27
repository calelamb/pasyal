using Godot;
using System;

namespace Pasyal;

public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void InteractedWithEventHandler(Node target);

    [Export] public int TileSize { get; set; } = 16;
    [Export] public float WalkSpeed { get; set; } = 3.0f;
    [Export] public float RunSpeed { get; set; } = 5.0f;
    [Export] public Vector2 ZoneBoundsMin { get; set; } = new(-10000, -10000);
    [Export] public Vector2 ZoneBoundsMax { get; set; } = new(10000, 10000);
    [Export] public float CameraSmoothSpeed { get; set; } = 5.0f;

    private AnimatedSprite2D _sprite = null!;
    private Camera2D _camera = null!;
    private RayCast2D _interactRay = null!;

    private Vector2I _facingDirection = Vector2I.Down;
    private Vector2 _startPosition;
    private Vector2 _targetPosition;
    private bool _isMoving;
    private float _moveLerp;

    public Vector2I FacingDirection => _facingDirection;

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("Sprite");
        _camera = GetNode<Camera2D>("Camera");
        _interactRay = GetNode<RayCast2D>("InteractRay");

        _targetPosition = GlobalPosition;
        _startPosition = GlobalPosition;
        _isMoving = false;
        _moveLerp = 1.0f;

        if (_sprite.SpriteFrames == null)
        {
            // Create a visible placeholder so the player isn't invisible
            var rect = new ColorRect();
            rect.Size = new Vector2(16, 32);
            rect.Position = new Vector2(-8, -32);
            rect.Color = new Color(0.2f, 0.4f, 0.9f); // Blue player
            AddChild(rect);
        }

        UpdateAnimation();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (IsDialogueActive())
        {
            return;
        }

        if (_isMoving)
        {
            ProcessMovement(delta);
        }
        else
        {
            HandleInput();
        }

        UpdateCameraClamp(delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("interact") && !IsDialogueActive())
        {
            TryInteract();
        }
    }

    private void HandleInput()
    {
        var direction = Vector2I.Zero;

        if (Input.IsActionPressed("move_up"))
        {
            direction = Vector2I.Up;
        }
        else if (Input.IsActionPressed("move_down"))
        {
            direction = Vector2I.Down;
        }
        else if (Input.IsActionPressed("move_left"))
        {
            direction = Vector2I.Left;
        }
        else if (Input.IsActionPressed("move_right"))
        {
            direction = Vector2I.Right;
        }

        if (direction != Vector2I.Zero)
        {
            _facingDirection = direction;
            UpdateInteractRay();

            if (CanMoveInDirection(direction))
            {
                StartMove(direction);
            }
            else
            {
                UpdateAnimation();
            }
        }
        else
        {
            UpdateAnimation();
        }
    }

    private void StartMove(Vector2I direction)
    {
        _startPosition = GlobalPosition;
        _targetPosition = GlobalPosition + (Vector2)direction * TileSize;
        _isMoving = true;
        _moveLerp = 0.0f;
        UpdateAnimation();
    }

    private void ProcessMovement(double delta)
    {
        float speed = Input.IsActionPressed("run") ? RunSpeed : WalkSpeed;
        float pixelsPerSecond = speed * TileSize;
        float distance = _startPosition.DistanceTo(_targetPosition);

        if (distance <= 0.0f)
        {
            FinishMove();
            return;
        }

        _moveLerp += (float)(pixelsPerSecond * delta) / distance;

        if (_moveLerp >= 1.0f)
        {
            FinishMove();
        }
        else
        {
            GlobalPosition = _startPosition.Lerp(_targetPosition, _moveLerp);
        }
    }

    private void FinishMove()
    {
        GlobalPosition = _targetPosition;
        _moveLerp = 1.0f;
        _isMoving = false;

        // Allow immediate chaining into the next step
        HandleInput();
    }

    private bool CanMoveInDirection(Vector2I direction)
    {
        var spaceState = GetWorld2D().DirectSpaceState;
        var from = GlobalPosition;
        var to = from + (Vector2)direction * TileSize;

        var query = PhysicsRayQueryParameters2D.Create(from, to);
        query.Exclude = new Godot.Collections.Array<Rid> { GetRid() };
        query.CollideWithBodies = true;

        var result = spaceState.IntersectRay(query);
        return result.Count == 0;
    }

    private void TryInteract()
    {
        _interactRay.ForceRaycastUpdate();

        if (!_interactRay.IsColliding())
        {
            return;
        }

        var collider = _interactRay.GetCollider();

        if (collider is not Node target)
        {
            return;
        }

        if (target.IsInGroup("interactable") || target.HasMethod("Interact"))
        {
            if (target.HasMethod("Interact"))
            {
                target.Call("Interact");
            }

            EmitSignal(SignalName.InteractedWith, target);
        }
    }

    private void UpdateInteractRay()
    {
        _interactRay.TargetPosition = (Vector2)_facingDirection * TileSize;
    }

    private void UpdateAnimation()
    {
        string directionSuffix = _facingDirection switch
        {
            { X: 0, Y: -1 } => "up",
            { X: 0, Y: 1 } => "down",
            { X: -1, Y: 0 } => "left",
            { X: 1, Y: 0 } => "right",
            _ => "down"
        };

        string prefix = _isMoving ? "walk" : "idle";
        string animationName = $"{prefix}_{directionSuffix}";

        if (_sprite.SpriteFrames != null && _sprite.SpriteFrames.HasAnimation(animationName))
        {
            if (_sprite.Animation != animationName)
                _sprite.Play(animationName);
        }
    }

    private void UpdateCameraClamp(double delta)
    {
        var viewport = GetViewportRect().Size;
        var halfViewport = viewport / (_camera.Zoom * 2.0f);

        float clampedX = Mathf.Clamp(GlobalPosition.X, ZoneBoundsMin.X + halfViewport.X, ZoneBoundsMax.X - halfViewport.X);
        float clampedY = Mathf.Clamp(GlobalPosition.Y, ZoneBoundsMin.Y + halfViewport.Y, ZoneBoundsMax.Y - halfViewport.Y);

        var targetOffset = new Vector2(clampedX, clampedY) - GlobalPosition;
        _camera.Offset = _camera.Offset.Lerp(targetOffset, (float)(CameraSmoothSpeed * delta));
    }

    private bool IsDialogueActive()
    {
        var dialogueManager = GetNodeOrNull<Node>("/root/DialogueManager");

        if (dialogueManager is null)
        {
            return false;
        }

        if (dialogueManager.HasMethod("IsActive"))
        {
            return (bool)dialogueManager.Call("IsActive");
        }

        return false;
    }
}
