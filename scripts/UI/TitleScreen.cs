using Godot;
using Pasyal.Systems;

namespace Pasyal.UI;

public partial class TitleScreen : Control
{
    private Label _titleLabel = null!;
    private Label _taglineLabel = null!;
    private Button _startButton = null!;
    private Button _settingsButton = null!;

    private SaveManager _saveManager = null!;
    private ZoneManager _zoneManager = null!;

    [Export]
    public PackedScene? SettingsMenuScene { get; set; }

    public override void _Ready()
    {
        _titleLabel = GetNode<Label>("CenterContainer/VBoxContainer/TitleLabel");
        _taglineLabel = GetNode<Label>("CenterContainer/VBoxContainer/TaglineLabel");
        _startButton = GetNode<Button>("CenterContainer/VBoxContainer/StartButton");
        _settingsButton = GetNode<Button>("CenterContainer/VBoxContainer/SettingsButton");

        _saveManager = GetNode<SaveManager>("/root/SaveManager");
        _zoneManager = GetNode<ZoneManager>("/root/ZoneManager");

        _titleLabel.Text = "PASYAL";
        _taglineLabel.Text = "Tara, magpasyal tayo.";

        _startButton.Text = "Maglaro";
        _settingsButton.Text = "Mga Setting";

        _startButton.Pressed += OnStartPressed;
        _settingsButton.Pressed += OnSettingsPressed;
    }

    private void OnStartPressed()
    {
        if (_saveManager.HasSave())
        {
            if (!_saveManager.LoadGame())
            {
                _zoneManager.TransitionToZone("BahayKubo", new Vector2(128, 160));
            }
        }
        else
        {
            _zoneManager.TransitionToZone("BahayKubo", new Vector2(128, 160));
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
