using Godot;

namespace Pasyal.NPC;

/// <summary>
/// Time-based positioning for NPCs. Add as a child of an NPCBase node.
/// The NPC will smoothly move to a designated position for each time period
/// and optionally hide at night.
/// </summary>
public partial class NPCSchedule : Node
{
    [Export] public Vector2 UmagaPosition { get; set; }
    [Export] public Vector2 TanghaliPosition { get; set; }
    [Export] public Vector2 HaponPosition { get; set; }
    [Export] public Vector2 GabiPosition { get; set; }

    /// <summary>
    /// If true the NPC becomes invisible during the Gabi (night) period,
    /// simulating them going indoors.
    /// </summary>
    [Export] public bool HideAtNight { get; set; }

    private const float LerpDuration = 1f;

    private NPCBase _npc = null!;
    private Tween? _moveTween;

    public override void _Ready()
    {
        _npc = GetParent<NPCBase>();

        var timeManager = GetNode<Node>("/root/TimeManager");
        timeManager.Connect("TimeChanged", Callable.From<string>(OnTimeChanged));

        // Snap to correct position for the current period on load.
        string currentPeriod = timeManager.Get("CurrentPeriod").AsString();
        _npc.GlobalPosition = GetPositionForPeriod(currentPeriod);
        ApplyVisibility(currentPeriod);
    }

    private void OnTimeChanged(string newPeriod)
    {
        Vector2 target = GetPositionForPeriod(newPeriod);

        // Kill any running movement tween before starting a new one.
        _moveTween?.Kill();
        _moveTween = _npc.CreateTween();
        _moveTween.TweenProperty(_npc, "global_position", target, LerpDuration)
            .SetTrans(Tween.TransitionType.Sine)
            .SetEase(Tween.EaseType.InOut);

        ApplyVisibility(newPeriod);
    }

    private Vector2 GetPositionForPeriod(string period)
    {
        return period.ToLower() switch
        {
            "umaga"    => UmagaPosition,
            "tanghali" => TanghaliPosition,
            "hapon"    => HaponPosition,
            "gabi"     => GabiPosition,
            _          => UmagaPosition
        };
    }

    private void ApplyVisibility(string period)
    {
        if (HideAtNight)
        {
            _npc.Visible = period.ToLower() != "gabi";
            // Also disable collision so the player can't interact with a hidden NPC.
            _npc.ProcessMode = _npc.Visible
                ? ProcessModeEnum.Inherit
                : ProcessModeEnum.Disabled;
        }
    }
}
