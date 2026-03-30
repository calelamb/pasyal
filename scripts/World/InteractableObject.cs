using Godot;
using Pasyal.Systems;

namespace Pasyal.World;

/// <summary>
/// A world object the player can interact with (bed, mango tree, sampayan, etc.).
/// Performs a game action based on <see cref="InteractAction"/> and emits a signal
/// so UI layers can display the accompanying flavor text.
/// </summary>
public partial class InteractableObject : StaticBody2D
{
    [Signal]
    public delegate void ObjectInteractedEventHandler(string tagalog, string english);

    [Export] public string ObjectId { get; set; } = "";

    [Export(PropertyHint.Enum, "sleep,shake_tree,rest,bonfire_story,toggle_music,learn_animal,show_text,start_fishing")]
    public string InteractAction { get; set; } = "";

    [Export] public string ItemReward { get; set; } = "";
    [Export] public string VocabWord { get; set; } = "";
    [Export] public string InteractTextTagalog { get; set; } = "";
    [Export] public string InteractTextEnglish { get; set; } = "";

    private bool _musicPlaying;

    private static readonly System.Collections.Generic.Dictionary<string, Color> ActionColors = new()
    {
        { "sleep", new Color(0.4f, 0.3f, 0.6f) },       // purple - bed
        { "shake_tree", new Color(0.2f, 0.6f, 0.2f) },   // green - tree
        { "rest", new Color(0.6f, 0.5f, 0.3f) },         // tan - hammock
        { "bonfire_story", new Color(0.8f, 0.4f, 0.1f) }, // orange - fire
        { "toggle_music", new Color(0.5f, 0.5f, 0.5f) },  // gray - radio
        { "learn_animal", new Color(0.7f, 0.6f, 0.4f) },  // brown - animal
        { "show_text", new Color(0.5f, 0.4f, 0.3f) },     // brown - sign/object
        { "start_fishing", new Color(0.2f, 0.4f, 0.7f) }, // blue - boat
    };

    public override void _Ready()
    {
        AddToGroup("interactable");

        // Create visible placeholder so objects aren't invisible
        var sprite = GetNodeOrNull<Sprite2D>("Sprite2D");
        if (sprite is not null && sprite.Texture is null)
        {
            var color = ActionColors.TryGetValue(InteractAction, out var c) ? c : new Color(0.5f, 0.4f, 0.3f);
            var rect = new ColorRect();
            rect.Size = new Vector2(16, 16);
            rect.Position = new Vector2(-8, -16);
            rect.Color = color;
            AddChild(rect);

            // Add a small label so player knows what the object is
            var label = new Label();
            label.Text = string.IsNullOrEmpty(VocabWord) ? ObjectId : VocabWord;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.Position = new Vector2(-20, -26);
            label.Size = new Vector2(40, 10);
            label.AddThemeFontSizeOverride("font_size", 6);
            label.AddThemeColorOverride("font_color", new Color(1, 1, 1, 0.8f));
            AddChild(label);
        }
    }

    /// <summary>
    /// Called by the player interaction system when this object is interacted with.
    /// </summary>
    public void Interact()
    {
        switch (InteractAction)
        {
            case "sleep":
                HandleSleep();
                break;
            case "shake_tree":
                HandleShakeTree();
                break;
            case "rest":
                HandleRest();
                break;
            case "bonfire_story":
                HandleBonfireStory();
                break;
            case "toggle_music":
                HandleToggleMusic();
                break;
            case "learn_animal":
                HandleLearnAnimal();
                break;
            case "show_text":
                HandleShowText();
                break;
            case "start_fishing":
                HandleStartFishing();
                break;
            default:
                GD.PushWarning($"InteractableObject '{ObjectId}': unknown action '{InteractAction}'");
                break;
        }
    }

    private void HandleSleep()
    {
        var timeManager = GetNode<TimeManager>("/root/TimeManager");
        timeManager.SetTime("umaga");

        var saveManager = GetNode<SaveManager>("/root/SaveManager");
        saveManager.SaveGame();

        EmitSignal(SignalName.ObjectInteracted,
            "Tulog... Magandang umaga!",
            "Sleep... Good morning!");
    }

    private void HandleShakeTree()
    {
        if (!string.IsNullOrEmpty(ItemReward))
        {
            var inventoryManager = GetNode<InventoryManager>("/root/InventoryManager");
            inventoryManager.AddItem(ItemReward);
        }

        // Play a quick shake animation via tween.
        var sprite = GetNodeOrNull<Node2D>("Sprite");
        if (sprite is not null)
        {
            var tween = CreateTween();
            Vector2 origin = sprite.Position;
            tween.TweenProperty(sprite, "position", origin + new Vector2(2, 0), 0.05f);
            tween.TweenProperty(sprite, "position", origin + new Vector2(-2, 0), 0.05f);
            tween.TweenProperty(sprite, "position", origin + new Vector2(1, 0), 0.05f);
            tween.TweenProperty(sprite, "position", origin, 0.05f);
        }

        EmitSignal(SignalName.ObjectInteracted,
            InteractTextTagalog,
            InteractTextEnglish);
    }

    private void HandleRest()
    {
        var timeManager = GetNode<TimeManager>("/root/TimeManager");
        timeManager.AdvanceTime();

        EmitSignal(SignalName.ObjectInteracted,
            "Pahinga muna...",
            "Rest for a while...");
    }

    private void HandleBonfireStory()
    {
        var timeManager = GetNode<TimeManager>("/root/TimeManager");
        string period = timeManager.CurrentPeriod;

        if (period.ToLower() != "gabi")
        {
            EmitSignal(SignalName.ObjectInteracted,
                "Mas maganda 'to sa gabi...",
                "This is better at night...");
            return;
        }

        var currentScene = GetTree().CurrentScene;
        if (currentScene?.HasMethod("StartBonfireStory") == true)
        {
            currentScene.Call("StartBonfireStory");
        }
    }

    private void HandleToggleMusic()
    {
        _musicPlaying = !_musicPlaying;
        if (_musicPlaying)
        {
            EmitSignal(SignalName.ObjectInteracted,
                "Tumutugtog ang radyo.",
                "The radio is playing.");
        }
        else
        {
            EmitSignal(SignalName.ObjectInteracted,
                "Pinatay ang radyo.",
                "Turned off the radio.");
        }
    }

    private void HandleLearnAnimal()
    {
        if (!string.IsNullOrEmpty(VocabWord))
        {
            var vocabManager = GetNode<VocabManager>("/root/VocabManager");
            vocabManager.DiscoverWord(VocabWord);
        }

        EmitSignal(SignalName.ObjectInteracted,
            InteractTextTagalog,
            InteractTextEnglish);
    }

    private void HandleShowText()
    {
        EmitSignal(SignalName.ObjectInteracted,
            InteractTextTagalog,
            InteractTextEnglish);
    }

    private void HandleStartFishing()
    {
        GetTree().ChangeSceneToFile("res://scenes/minigames/Fishing.tscn");
    }
}
