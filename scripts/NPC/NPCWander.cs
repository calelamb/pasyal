using Godot;
using Pasyal.Systems;

namespace Pasyal.NPC;

/// <summary>
/// Random idle wandering behavior for animals and ambient NPCs.
/// Add as a child of an NPCBase node.
/// </summary>
public partial class NPCWander : Node
{
    [Export] public float WanderRadius { get; set; } = 32f; // 2 tiles
    [Export] public float WanderInterval { get; set; } = 3f; // seconds between moves
    [Export] public float MoveSpeed { get; set; } = 1f;      // tiles per second

    private const float TileSize = 16f;

    private NPCBase _npc = null!;
    private Timer _wanderTimer = null!;
    private Vector2 _origin;
    private Vector2 _moveTarget;
    private bool _isMoving;

    public override void _Ready()
    {
        _npc = GetParent<NPCBase>();
        _origin = _npc.GlobalPosition;
        _moveTarget = _origin;

        _wanderTimer = new Timer();
        _wanderTimer.WaitTime = WanderInterval;
        _wanderTimer.OneShot = false;
        _wanderTimer.Timeout += OnWanderTimeout;
        AddChild(_wanderTimer);
        _wanderTimer.Start();
    }

    public override void _PhysicsProcess(double delta)
    {
        // Pause movement while a dialogue is active.
        var dialogueManager = GetNodeOrNull<DialogueManager>("/root/DialogueManager");
        bool dialogueActive = dialogueManager?.IsActive ?? false;
        if (dialogueActive)
        {
            _isMoving = false;
            _npc.Velocity = Vector2.Zero;
            return;
        }

        if (!_isMoving)
        {
            _npc.Velocity = Vector2.Zero;
            _npc.MoveAndSlide();
            return;
        }

        Vector2 direction = (_moveTarget - _npc.GlobalPosition);
        if (direction.Length() < 1f)
        {
            _isMoving = false;
            _npc.Velocity = Vector2.Zero;
            _npc.MoveAndSlide();
            return;
        }

        Vector2 velocity = direction.Normalized() * MoveSpeed * TileSize;
        _npc.Velocity = velocity;
        _npc.MoveAndSlide();
    }

    private void OnWanderTimeout()
    {
        // Pause wandering while a dialogue is active.
        var dialogueManager = GetNodeOrNull<DialogueManager>("/root/DialogueManager");
        if (dialogueManager?.IsActive ?? false)
            return;

        // 40% chance to stay idle instead of moving.
        if (GD.Randf() < 0.4f)
            return;

        // Pick a random cardinal direction and attempt to move one tile.
        Vector2I[] directions = { Vector2I.Up, Vector2I.Down, Vector2I.Left, Vector2I.Right };
        Vector2I chosen = directions[GD.Randi() % directions.Length];

        Vector2 candidate = _npc.GlobalPosition + new Vector2(chosen.X, chosen.Y) * TileSize;

        // Stay within the wander radius from the origin.
        if (candidate.DistanceTo(_origin) > WanderRadius)
            return;

        _moveTarget = candidate;
        _isMoving = true;
        _npc.FaceDirection(chosen);
    }
}
