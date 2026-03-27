using Godot;

namespace Pasyal.Systems;

public partial class AudioManager : Node
{
    private AudioStreamPlayer _musicPlayer = null!;
    private AudioStreamPlayer _sfxPlayer = null!;
    private AudioStreamPlayer _ambientPlayer = null!;

    public override void _Ready()
    {
        _musicPlayer = new AudioStreamPlayer();
        _musicPlayer.Name = "MusicPlayer";
        _musicPlayer.Bus = ResolveBus("Music");
        AddChild(_musicPlayer);

        _sfxPlayer = new AudioStreamPlayer();
        _sfxPlayer.Name = "SfxPlayer";
        _sfxPlayer.Bus = ResolveBus("SFX");
        AddChild(_sfxPlayer);

        _ambientPlayer = new AudioStreamPlayer();
        _ambientPlayer.Name = "AmbientPlayer";
        _ambientPlayer.Bus = ResolveBus("Ambient");
        AddChild(_ambientPlayer);
    }

    public void PlayMusic(string path)
    {
        if (!ResourceLoader.Exists(path))
            return;

        var stream = GD.Load<AudioStream>(path);
        if (stream is null)
            return;

        _musicPlayer.Stream = stream;
        _musicPlayer.Play();
    }

    public void PlaySfx(string path)
    {
        if (!ResourceLoader.Exists(path))
            return;

        var stream = GD.Load<AudioStream>(path);
        if (stream is null)
            return;

        _sfxPlayer.Stream = stream;
        _sfxPlayer.Play();
    }

    public void PlayAmbient(string path)
    {
        if (!ResourceLoader.Exists(path))
            return;

        var stream = GD.Load<AudioStream>(path);
        if (stream is null)
            return;

        _ambientPlayer.Stream = stream;
        _ambientPlayer.Play();
    }

    public void StopMusic()
    {
        _musicPlayer.Stop();
    }

    public void StopAmbient()
    {
        _ambientPlayer.Stop();
    }

    public void FadeMusic(float duration)
    {
        var tween = CreateTween();
        tween.TweenProperty(_musicPlayer, "volume_db", -80.0f, duration);
        tween.TweenCallback(Callable.From(() => _musicPlayer.Stop()));
    }

    public void CrossfadeMusic(string newPath, float duration = 1.0f)
    {
        float halfDuration = duration / 2.0f;

        var tween = CreateTween();
        tween.TweenProperty(_musicPlayer, "volume_db", -80.0f, halfDuration);
        tween.TweenCallback(Callable.From(() =>
        {
            PlayMusic(newPath);
            _musicPlayer.VolumeDb = -80.0f;
        }));
        tween.TweenProperty(_musicPlayer, "volume_db", 0.0f, halfDuration);
    }

    public void CrossfadeMusic(string newPath)
    {
        CrossfadeMusic(newPath, 1.0f);
    }

    public void SetMusicVolume(float db)
    {
        _musicPlayer.VolumeDb = db;
    }

    public void SetSfxVolume(float db)
    {
        _sfxPlayer.VolumeDb = db;
    }

    public void SetAmbientVolume(float db)
    {
        _ambientPlayer.VolumeDb = db;
    }

    private static string ResolveBus(string preferredBus)
    {
        return AudioServer.GetBusIndex(preferredBus) >= 0 ? preferredBus : "Master";
    }
}
