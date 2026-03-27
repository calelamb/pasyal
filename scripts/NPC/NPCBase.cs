using Godot;

namespace Pasyal.NPC;

/// <summary>
/// Core NPC component. Attach to a CharacterBody2D with an AnimatedSprite2D child named "Sprite".
/// Provides interaction, facing logic, and identification for the dialogue system.
/// </summary>
public partial class NPCBase : CharacterBody2D
{
    [Export] public string NpcId { get; set; } = "";
    [Export] public string DisplayName { get; set; } = "";

    private AnimatedSprite2D _sprite = null!;
    private Vector2I _facingDirection = Vector2I.Down;

    public override void _Ready()
    {
        AddToGroup("interactable");
        _sprite = GetNode<AnimatedSprite2D>("Sprite");
        _sprite.Play("idle_down");
    }

    /// <summary>
    /// Called by the player interaction system when this NPC is interacted with.
    /// </summary>
    public void Interact()
    {
        FacePlayer();

        var playerData = GetNode<Node>("/root/PlayerData");
        playerData.Call("AddVisit", NpcId);

        var dialogueManager = GetNode<Node>("/root/DialogueManager");
        string dialogueId = dialogueManager.Call("GetBestDialogue", NpcId).AsString();
        dialogueManager.Call("StartDialogue", NpcId, dialogueId);
    }

    /// <summary>
    /// Plays the idle animation for the given cardinal direction.
    /// </summary>
    public void FaceDirection(Vector2I direction)
    {
        _facingDirection = direction;

        string animation = direction switch
        {
            { X: 0, Y: -1 } => "idle_up",
            { X: 0, Y: 1 }  => "idle_down",
            { X: -1, Y: 0 } => "idle_left",
            { X: 1, Y: 0 }  => "idle_right",
            _ => "idle_down"
        };

        _sprite.Play(animation);
    }

    /// <summary>
    /// Faces the NPC toward the player character.
    /// </summary>
    private void FacePlayer()
    {
        var player = GetTree().GetFirstNodeInGroup("player");
        if (player is not Node2D playerNode)
            return;

        Vector2 toPlayer = (playerNode.GlobalPosition - GlobalPosition).Normalized();

        // Pick the dominant axis to determine a cardinal direction.
        Vector2I dir;
        if (Mathf.Abs(toPlayer.X) > Mathf.Abs(toPlayer.Y))
            dir = toPlayer.X > 0 ? Vector2I.Right : Vector2I.Left;
        else
            dir = toPlayer.Y > 0 ? Vector2I.Down : Vector2I.Up;

        FaceDirection(dir);
    }
}
