using Godot;
using Pasyal.Systems;

namespace Pasyal.UI;

public partial class SettingsMenu : Control
{
    public static bool ShowEnglish { get; private set; } = true;

    private CheckButton _englishToggle = null!;
    private HSlider _musicSlider = null!;
    private HSlider _sfxSlider = null!;
    private HSlider _ambientSlider = null!;
    private Button _backButton = null!;

    private AudioManager _audioManager = null!;

    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Always;
        GetTree().Paused = true;

        _englishToggle = GetNode<CheckButton>("Panel/MarginContainer/VBoxContainer/EnglishToggle");
        _musicSlider = GetNode<HSlider>("Panel/MarginContainer/VBoxContainer/MusicSlider");
        _sfxSlider = GetNode<HSlider>("Panel/MarginContainer/VBoxContainer/SfxSlider");
        _ambientSlider = GetNode<HSlider>("Panel/MarginContainer/VBoxContainer/AmbientSlider");
        _backButton = GetNode<Button>("Panel/MarginContainer/VBoxContainer/BackButton");

        _audioManager = GetNode<AudioManager>("/root/AudioManager");

        _englishToggle.Text = "Ipakita ang English";
        _englishToggle.ButtonPressed = ShowEnglish;

        ConfigureSlider(_musicSlider, "Musika");
        ConfigureSlider(_sfxSlider, "SFX");
        ConfigureSlider(_ambientSlider, "Ambient");

        _backButton.Text = "Bumalik";

        _englishToggle.Toggled += OnEnglishToggled;
        _musicSlider.ValueChanged += OnMusicChanged;
        _sfxSlider.ValueChanged += OnSfxChanged;
        _ambientSlider.ValueChanged += OnAmbientChanged;
        _backButton.Pressed += OnBackPressed;
    }

    private static void ConfigureSlider(HSlider slider, string label)
    {
        slider.MinValue = 0;
        slider.MaxValue = 100;
        slider.Value = 80;
        slider.Step = 1;
    }

    private void OnEnglishToggled(bool toggled)
    {
        ShowEnglish = toggled;
    }

    private void OnMusicChanged(double value)
    {
        float db = LinearToDb((float)value / 100.0f);
        _audioManager.SetMusicVolume(db);
    }

    private void OnSfxChanged(double value)
    {
        float db = LinearToDb((float)value / 100.0f);
        _audioManager.SetSfxVolume(db);
    }

    private void OnAmbientChanged(double value)
    {
        float db = LinearToDb((float)value / 100.0f);
        _audioManager.SetAmbientVolume(db);
    }

    private static float LinearToDb(float linear)
    {
        if (linear <= 0.0f)
            return -80.0f;

        return Mathf.Log(linear) * 20.0f / Mathf.Log(10.0f);
    }

    private void OnBackPressed()
    {
        GetTree().Paused = false;
        QueueFree();
    }
}
