using Godot;

namespace Pasyal.World;

/// <summary>
/// An Area2D that triggers a zone transition when the player enters it.
/// Place at zone edges and configure <see cref="TargetZone"/> and <see cref="SpawnPosition"/>.
/// </summary>
public partial class ZoneTransitionArea : Area2D
{
    [Export] public string TargetZone { get; set; } = "";
    [Export] public Vector2 SpawnPosition { get; set; }

    public override void _Ready()
    {
        // Collision layer 0 (no layer), mask on layer 1 (player layer).
        CollisionLayer = 0;
        CollisionMask = 1;

        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (!body.IsInGroup("player"))
            return;

        var zoneManager = GetNode<Node>("/root/ZoneManager");
        zoneManager.Call("TransitionToZone", TargetZone, SpawnPosition);
    }
}
