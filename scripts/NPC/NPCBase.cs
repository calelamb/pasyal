using Godot;
using Pasyal.Systems;

namespace Pasyal.NPC;

/// <summary>
/// Core NPC component. Attach to a CharacterBody2D with an AnimatedSprite2D child named "Sprite".
/// Provides interaction, facing logic, and identification for the dialogue system.
/// </summary>
public partial class NPCBase : CharacterBody2D
{
    [Export] public string NpcId { get; set; } = "";
    [Export] public string DisplayName { get; set; } = "";
    [Export(PropertyHint.Enum, "dialogue,shop,tawad,task,trike")]
    public string InteractMode { get; set; } = "dialogue";

    private AnimatedSprite2D _sprite = null!;
    private Vector2I _facingDirection = Vector2I.Down;

    public override void _Ready()
    {
        AddToGroup("interactable");
        _sprite = GetNode<AnimatedSprite2D>("Sprite");

        if (_sprite.SpriteFrames == null)
        {
            // Create a visible placeholder colored by NPC id hash
            var rect = new ColorRect();
            rect.Size = new Vector2(16, 32);
            rect.Position = new Vector2(-8, -32);
            // Derive a color from NpcId so different NPCs look distinct
            uint hash = (uint)(NpcId ?? "npc").GetHashCode();
            float h = (hash % 360) / 360f;
            rect.Color = Color.FromHsv(h, 0.6f, 0.8f);
            AddChild(rect);
        }

        if (_sprite.SpriteFrames != null && _sprite.SpriteFrames.HasAnimation("idle_down"))
        {
            _sprite.Play("idle_down");
        }
    }

    /// <summary>
    /// Called by the player interaction system when this NPC is interacted with.
    /// </summary>
    public void Interact()
    {
        FacePlayer();

        var playerData = GetNode<PlayerData>("/root/PlayerData");
        playerData.AddVisit(NpcId);

        var dialogueManager = GetNode<DialogueManager>("/root/DialogueManager");
        dialogueManager.DialogueEnded -= OnDialogueEnded;
        dialogueManager.DialogueEnded += OnDialogueEnded;

        string dialogueId = dialogueManager.GetBestDialogue(NpcId);
        if (!string.IsNullOrEmpty(dialogueId))
        {
            dialogueManager.StartDialogue(NpcId, dialogueId);
        }
        else
        {
            OnDialogueEnded();
        }
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

        if (_sprite.SpriteFrames != null && _sprite.SpriteFrames.HasAnimation(animation))
        {
            _sprite.Play(animation);
        }
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

    private void OnDialogueEnded()
    {
        var dialogueManager = GetNode<DialogueManager>("/root/DialogueManager");
        dialogueManager.DialogueEnded -= OnDialogueEnded;

        var currentScene = GetTree().CurrentScene;
        if (currentScene?.HasMethod("HandleNpcPostDialogue") == true)
        {
            currentScene.CallDeferred("HandleNpcPostDialogue", NpcId, InteractMode);
        }
    }
}
