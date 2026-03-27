using Godot;
using System;
using System.Collections.Generic;

namespace Pasyal.Minigames;

public partial class TrikeRide : Node2D
{
    [Signal]
    public delegate void RideCompleteEventHandler();

    [Export] public string DestinationZone { get; set; } = "";
    [Export] public Vector2 DestinationSpawn { get; set; }
    [Export] public float RideDuration { get; set; } = 3.0f;

    private Sprite2D _trike = null!;
    private Label _chatterLabel = null!;
    private RandomNumberGenerator _rng = new();

    private static readonly string[] ManongBoyPhrases =
    {
        "Ingat ha! / Be careful!",
        "Malapit na tayo! / We're almost there!",
        "Ang traffic ngayon! / So much traffic today!",
        "Saan ka pupunta? / Where are you headed?",
        "Masaya ka ba? / Are you happy?",
        "Ang ganda ng panahon! / The weather is beautiful!"
    };

    private static readonly string[] TravelVocab = { "sakay", "biyahe", "malapit", "malayo" };

    public override void _Ready()
    {
        _trike = GetNode<Sprite2D>("Trike");
        _chatterLabel = GetNode<Label>("ChatterLabel");
        _rng.Randomize();

        DiscoverTravelVocab();
        StartRide();
    }

    private void StartRide()
    {
        float screenWidth = GetViewportRect().Size.X;
        float startX = -80f;
        float endX = screenWidth + 80f;

        _trike.Position = new Vector2(startX, _trike.Position.Y);
        _chatterLabel.Text = "";

        var tween = CreateTween();
        tween.TweenProperty(_trike, "position:x", endX, RideDuration)
            .SetEase(Tween.EaseType.InOut)
            .SetTrans(Tween.TransitionType.Sine);

        // Schedule 2-3 random chatter lines during the ride
        int chatterCount = _rng.RandiRange(2, 3);
        float interval = RideDuration / (chatterCount + 1);

        for (int i = 0; i < chatterCount; i++)
        {
            float delay = interval * (i + 1);
            string phrase = ManongBoyPhrases[_rng.RandiRange(0, ManongBoyPhrases.Length - 1)];

            var chatterTween = CreateTween();
            chatterTween.TweenInterval(delay);
            chatterTween.TweenCallback(Callable.From(() => ShowChatter(phrase)));
            chatterTween.TweenInterval(1.5);
            chatterTween.TweenCallback(Callable.From(() => _chatterLabel.Text = ""));
        }

        // On ride complete, transition
        tween.TweenCallback(Callable.From(OnRideComplete));
    }

    private void ShowChatter(string phrase)
    {
        _chatterLabel.Text = phrase;
    }

    private void DiscoverTravelVocab()
    {
        var vocabManager = GetNode<Node>("/root/VocabManager");
        foreach (string word in TravelVocab)
        {
            vocabManager.Call("DiscoverWord", word);
        }
    }

    private void OnRideComplete()
    {
        EmitSignal(SignalName.RideComplete);

        var zoneManager = GetNode<Node>("/root/ZoneManager");
        zoneManager.Call("TransitionToZone", DestinationZone, DestinationSpawn);
    }
}
