using Godot;
using Pasyal.Systems;

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

    private SaveManager _saveManager = null!;

    private bool _isPaused;

    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Always;

        _panel = GetNode<PanelContainer>("Panel");
        _resumeButton = GetNode<Button>("Panel/MarginContainer/VBoxContainer/ResumeButton");
        _journalButton = GetNode<Button>("Panel/MarginContainer/VBoxContainer/JournalButton");
        _inventoryButton = GetNode<Button>("Panel/MarginContainer/VBoxContainer/InventoryButton");
        _settingsButton = GetNode<Button>("Panel/MarginContainer/VBoxContainer/SettingsButton");
        _saveQuitButton = GetNode<Button>("Panel/MarginContainer/VBoxContainer/SaveQuitButton");

        _saveManager = GetNode<SaveManager>("/root/SaveManager");

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
            var currentScene = GetTree().CurrentScene;
            if (currentScene?.HasMethod("IsBlockingUiOpen") == true
                && (bool)currentScene.Call("IsBlockingUiOpen"))
            {
                return;
            }

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
        Resume();
        EmitSignal(SignalName.OpenSettingsRequested);
    }

    private void OnSaveQuitPressed()
    {
        _saveManager.SaveGame();
        GetTree().Paused = false;
        GetTree().Quit();
    }
}
