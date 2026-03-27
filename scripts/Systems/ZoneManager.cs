using Godot;

namespace Pasyal.Systems;

public partial class ZoneManager : Node
{
    [Signal] public delegate void ZoneChangedEventHandler(string zoneName);

    private ColorRect _fadeOverlay = null!;
    private CanvasLayer _overlayLayer = null!;
    private string _currentZone = "";
    private Vector2 _pendingSpawnPosition;

    public string CurrentZone => _currentZone;

    public override void _Ready()
    {
        _overlayLayer = new CanvasLayer();
        _overlayLayer.Layer = 100;
        AddChild(_overlayLayer);

        _fadeOverlay = new ColorRect();
        _fadeOverlay.Color = new Color(0, 0, 0, 0);
        _fadeOverlay.MouseFilter = Control.MouseFilterEnum.Ignore;
        _fadeOverlay.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        _overlayLayer.AddChild(_fadeOverlay);
    }

    public void TransitionToZone(string zoneName, Vector2 spawnPosition)
    {
        _pendingSpawnPosition = spawnPosition;
        string scenePath = $"res://scenes/zones/{zoneName}.tscn";

        // Fade out
        var tween = CreateTween();
        tween.TweenProperty(_fadeOverlay, "color:a", 1.0f, 0.3f);
        tween.TweenCallback(Callable.From(() =>
        {
            var error = GetTree().ChangeSceneToFile(scenePath);
            if (error != Error.Ok)
            {
                GD.PrintErr($"ZoneManager: Failed to load zone '{zoneName}' at '{scenePath}'");
                // Fade back in on failure
                var fadeTween = CreateTween();
                fadeTween.TweenProperty(_fadeOverlay, "color:a", 0.0f, 0.3f);
                return;
            }

            _currentZone = zoneName;

            // Wait one frame for the scene to initialize, then set position and fade in
            CallDeferred(MethodName.OnSceneLoaded);
        }));
    }

    private void OnSceneLoaded()
    {
        // Set player position if player exists in new scene
        var player = GetTree().CurrentScene?.GetNodeOrNull<Node2D>("Player");
        if (player is not null)
        {
            player.Position = _pendingSpawnPosition;
        }

        // Fade in
        var tween = CreateTween();
        tween.TweenProperty(_fadeOverlay, "color:a", 0.0f, 0.3f);

        EmitSignal(SignalName.ZoneChanged, _currentZone);
    }

    // Used by SaveManager to set zone without transition animation
    public void SetCurrentZone(string zoneName)
    {
        _currentZone = zoneName;
    }
}
