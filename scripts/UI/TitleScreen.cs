using Godot;

namespace Pasyal.UI;

public partial class TitleScreen : Control
{
    private Label _titleLabel = null!;
    private Label _taglineLabel = null!;
    private Button _startButton = null!;
    private Button _settingsButton = null!;

    private Node _saveManager = null!;
    private Node _zoneManager = null!;

    [Export]
    public PackedScene? SettingsMenuScene { get; set; }

    public override void _Ready()
    {
        _titleLabel = GetNode<Label>("CenterContainer/VBoxContainer/TitleLabel");
        _taglineLabel = GetNode<Label>("CenterContainer/VBoxContainer/TaglineLabel");
        _startButton = GetNode<Button>("CenterContainer/VBoxContainer/StartButton");
        _settingsButton = GetNode<Button>("CenterContainer/VBoxContainer/SettingsButton");

        _saveManager = GetNode("/root/SaveManager");
        _zoneManager = GetNode("/root/ZoneManager");

        _titleLabel.Text = "PASYAL";
        _taglineLabel.Text = "Tara, magpasyal tayo.";

        _startButton.Text = "Maglaro";
        _settingsButton.Text = "Mga Setting";

        _startButton.Pressed += OnStartPressed;
        _settingsButton.Pressed += OnSettingsPressed;
    }

    private void OnStartPressed()
    {
        bool hasSave = (bool)_saveManager.Call("HasSave");

        if (hasSave)
        {
            bool loaded = (bool)_saveManager.Call("LoadGame");
            if (!loaded)
            {
                _zoneManager.Call("TransitionToZone", "BahayKubo", new Vector2(128, 160));
            }
        }
        else
        {
            _zoneManager.Call("TransitionToZone", "BahayKubo", new Vector2(128, 160));
        }
    }

    private void OnSettingsPressed()
    {
        var settingsScene = SettingsMenuScene ?? GD.Load<PackedScene>("res://scenes/ui/SettingsMenu.tscn");
        if (settingsScene is not null)
        {
            var settings = settingsScene.Instantiate<Control>();
            AddChild(settings);
        }
    }
}
