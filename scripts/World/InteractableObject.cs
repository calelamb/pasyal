using Godot;

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

    public override void _Ready()
    {
        AddToGroup("interactable");
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
        var timeManager = GetNode<Node>("/root/TimeManager");
        timeManager.Call("SetTime", "umaga");

        var saveManager = GetNode<Node>("/root/SaveManager");
        saveManager.Call("SaveGame");

        EmitSignal(SignalName.ObjectInteracted,
            "Tulog... Magandang umaga!",
            "Sleep... Good morning!");
    }

    private void HandleShakeTree()
    {
        if (!string.IsNullOrEmpty(ItemReward))
        {
            var inventoryManager = GetNode<Node>("/root/InventoryManager");
            inventoryManager.Call("AddItem", ItemReward);
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
        var timeManager = GetNode<Node>("/root/TimeManager");
        timeManager.Call("AdvanceTime");

        EmitSignal(SignalName.ObjectInteracted,
            "Pahinga muna...",
            "Rest for a while...");
    }

    private void HandleBonfireStory()
    {
        var timeManager = GetNode<Node>("/root/TimeManager");
        string period = timeManager.Get("CurrentPeriod").AsString();

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
            var vocabManager = GetNode<Node>("/root/VocabManager");
            vocabManager.Call("DiscoverWord", VocabWord);
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
