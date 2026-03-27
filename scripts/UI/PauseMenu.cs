using Godot;

namespace Pasyal.UI;

public partial class PauseMenu : Control
{
    [Signal]
    public delegate void OpenJournalRequestedEventHandler();

    [Signal]
    public delegate void OpenInventoryRequestedEventHandler();

    [Signal]
    public delegate void OpenSettingsRequestedEventHandler();

    private PanelContainer _panel = null!;
    private Button _resumeButton = null!;
    private Button _journalButton = null!;
    private Button _inventoryButton = null!;
    private Button _settingsButton = null!;
    private Button _saveQuitButton = null!;

    private Node _saveManager = null!;

    private bool _isPaused;

    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Always;

        _panel = GetNode<PanelContainer>("Panel");
        _resumeButton = GetNode<Button>("Panel/VBox/ResumeButton");
        _journalButton = GetNode<Button>("Panel/VBox/JournalButton");
        _inventoryButton = GetNode<Button>("Panel/VBox/InventoryButton");
        _settingsButton = GetNode<Button>("Panel/VBox/SettingsButton");
        _saveQuitButton = GetNode<Button>("Panel/VBox/SaveQuitButton");

        _saveManager = GetNode("/root/SaveManager");

        _resumeButton.Text = "Ituloy";
        _journalButton.Text = "Salitaan";
        _inventoryButton.Text = "Gamit";
        _settingsButton.Text = "Mga Setting";
        _saveQuitButton.Text = "I-save at Umalis";

        _resumeButton.Pressed += Resume;
        _journalButton.Pressed += OnJournalPressed;
        _inventoryButton.Pressed += OnInventoryPressed;
        _settingsButton.Pressed += OnSettingsPressed;
        _saveQuitButton.Pressed += OnSaveQuitPressed;

        Visible = false;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("pause"))
        {
            if (_isPaused)
                Resume();
            else
                Pause();

            GetViewport().SetInputAsHandled();
        }
    }

    private void Pause()
    {
        _isPaused = true;
        Visible = true;
        GetTree().Paused = true;
    }

    private void Resume()
    {
        _isPaused = false;
        Visible = false;
        GetTree().Paused = false;
    }

    private void OnJournalPressed()
    {
        Resume();
        EmitSignal(SignalName.OpenJournalRequested);
    }

    private void OnInventoryPressed()
    {
        Resume();
        EmitSignal(SignalName.OpenInventoryRequested);
    }

    private void OnSettingsPressed()
    {
        EmitSignal(SignalName.OpenSettingsRequested);
    }

    private void OnSaveQuitPressed()
    {
        _saveManager.Call("SaveGame");
        GetTree().Paused = false;
        GetTree().Quit();
    }
}
