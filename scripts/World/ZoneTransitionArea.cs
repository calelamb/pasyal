using Godot;
using Pasyal.Systems;

namespace Pasyal.World;

public partial class ZoneTransitionArea : Area2D
{
    [Export] public string TargetZone { get; set; } = "";
    [Export] public Vector2 SpawnPosition { get; set; }

    public override void _Ready()
    {
        CollisionLayer = 0;
        CollisionMask = 1;

        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (!body.IsInGroup("player"))
            return;

        var zoneManager = GetNode<ZoneManager>("/root/ZoneManager");
        zoneManager.TransitionToZone(TargetZone, SpawnPosition);
    }
}
