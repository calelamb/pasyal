using Godot;

namespace Pasyal.World;

/// <summary>
/// An Area2D that crossfades ambient sound and optionally changes music
/// when the player enters or exits the region.
/// </summary>
public partial class AmbientZone : Area2D
{
    [Export] public string AmbientPath { get; set; } = "";
    [Export] public string MusicPath { get; set; } = "";

    public override void _Ready()
    {
        CollisionLayer = 0;
        CollisionMask = 1; // player layer

        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (!body.IsInGroup("player"))
            return;

        var audioManager = GetNode<Node>("/root/AudioManager");

        if (!string.IsNullOrEmpty(AmbientPath))
        {
            audioManager.Call("PlayAmbient", AmbientPath);
        }

        if (!string.IsNullOrEmpty(MusicPath))
        {
            audioManager.Call("CrossfadeMusic", MusicPath);
        }
    }

    private void OnBodyExited(Node2D body)
    {
        if (!body.IsInGroup("player"))
            return;

        var audioManager = GetNode<Node>("/root/AudioManager");

        if (!string.IsNullOrEmpty(AmbientPath))
        {
            audioManager.Call("StopAmbient");
        }
    }
}
