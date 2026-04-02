using Godot;

namespace Pasyal.Systems;

public partial class TimeManager : Node
{
    [Signal] public delegate void TimeChangedEventHandler(string period);

    private static readonly string[] Periods = { "umaga", "tanghali", "hapon", "gabi" };
    private int _periodIndex;

    public string CurrentPeriod => Periods[_periodIndex];

    public override void _Ready()
    {
        _periodIndex = 0;

        var timer = new Timer();
        timer.WaitTime = 120.0f; // 2 minutes
        timer.Autostart = true;
        timer.Timeout += AdvanceTime;
        AddChild(timer);
    }

    public void AdvanceTime()
    {
        _periodIndex = (_periodIndex + 1) % Periods.Length;
        EmitSignal(SignalName.TimeChanged, CurrentPeriod);
    }

    public void SetTime(string period)
    {
        for (int i = 0; i < Periods.Length; i++)
        {
            if (Periods[i] == period)
            {
                _periodIndex = i;
                EmitSignal(SignalName.TimeChanged, CurrentPeriod);
                return;
            }
        }

        GD.PrintErr($"TimeManager: Unknown period '{period}'");
    }

    public Color GetOverlayColor()
    {
        return CurrentPeriod switch
        {
            "umaga" => new Color(0.8f, 0.9f, 1.0f, 1.0f),
            "tanghali" => new Color(1.0f, 1.0f, 1.0f, 1.0f),
            "hapon" => new Color(1.0f, 0.9f, 0.7f, 1.0f),
            "gabi" => new Color(0.4f, 0.4f, 0.7f, 1.0f),
            _ => new Color(1.0f, 1.0f, 1.0f, 1.0f)
        };
    }
}
