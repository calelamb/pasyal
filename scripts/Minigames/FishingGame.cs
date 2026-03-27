using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Pasyal.Minigames;

public partial class FishingGame : Node2D
{
    private enum FishingState
    {
        Idle,
        Casting,
        Waiting,
        Bite,
        Reeling,
        CatchSuccess,
        CatchFail
    }

    [Signal]
    public delegate void FishingCompleteEventHandler(bool caught, string fishId);

    [Export] public float IndicatorSpeed { get; set; } = 120f;
    [Export] public float TargetZoneWidth { get; set; } = 40f;

    private FishingState _state = FishingState.Idle;
    private double _waitTimer;
    private double _biteTimer;
    private double _resultDisplayTimer;
    private float _powerBarDirection = 1f;
    private string _selectedFishId = "";
    private float _indicatorPosition;
    private float _indicatorDirection = 1f;

    private List<FishData> _fishPool = new();

    // Child node references
    private ProgressBar _powerBar = null!;
    private Sprite2D _bobber = null!;
    private ColorRect _indicator = null!;
    private ColorRect _targetZone = null!;
    private Label _resultLabel = null!;
    private RichTextLabel _fishNameLabel = null!;

    private RandomNumberGenerator _rng = new();

    public override void _Ready()
    {
        _powerBar = GetNode<ProgressBar>("PowerBar");
        _bobber = GetNode<Sprite2D>("Bobber");
        _indicator = GetNode<ColorRect>("Indicator");
        _targetZone = GetNode<ColorRect>("TargetZone");
        _resultLabel = GetNode<Label>("ResultLabel");
        _fishNameLabel = GetNode<RichTextLabel>("FishNameLabel");

        _rng.Randomize();
        LoadFishData();
        ResetUI();
        _state = FishingState.Idle;
        _resultLabel.Text = "Press interact to cast!";
    }

    public override void _Process(double delta)
    {
        switch (_state)
        {
            case FishingState.Idle:
                ProcessIdle();
                break;
            case FishingState.Casting:
                ProcessCasting(delta);
                break;
            case FishingState.Waiting:
                ProcessWaiting(delta);
                break;
            case FishingState.Bite:
                ProcessBite(delta);
                break;
            case FishingState.Reeling:
                ProcessReeling(delta);
                break;
            case FishingState.CatchSuccess:
                ProcessCatchSuccess(delta);
                break;
            case FishingState.CatchFail:
                ProcessCatchFail(delta);
                break;
        }
    }

    private void ProcessIdle()
    {
        if (Input.IsActionJustPressed("interact"))
        {
            _state = FishingState.Casting;
            _powerBar.Visible = true;
            _powerBar.Value = 0;
            _powerBarDirection = 1f;
            _resultLabel.Text = "Set your cast power!";
        }
    }

    private void ProcessCasting(double delta)
    {
        _powerBar.Value += _powerBarDirection * 100.0 * delta;
        if (_powerBar.Value >= 100.0)
            _powerBarDirection = -1f;
        else if (_powerBar.Value <= 0.0)
            _powerBarDirection = 1f;

        if (Input.IsActionJustPressed("interact"))
        {
            _powerBar.Visible = false;
            _bobber.Visible = true;

            // Cast distance affects bobber position (cosmetic)
            float castPower = (float)_powerBar.Value / 100f;
            var bobberTarget = _bobber.Position with { X = Mathf.Lerp(60f, 260f, castPower) };
            _bobber.Position = bobberTarget;

            _selectedFishId = SelectRandomFish();
            _waitTimer = _rng.RandfRange(3f, 10f);
            _state = FishingState.Waiting;
            _resultLabel.Text = "Waiting for a bite...";
        }
    }

    private void ProcessWaiting(double delta)
    {
        _waitTimer -= delta;
        if (_waitTimer <= 0)
        {
            _state = FishingState.Bite;
            _biteTimer = 1.5;
            _resultLabel.Text = "!";

            // Visual cue: bobber dips
            var tween = CreateTween();
            tween.TweenProperty(_bobber, "position:y", _bobber.Position.Y + 8f, 0.15);
            tween.TweenProperty(_bobber, "position:y", _bobber.Position.Y + 4f, 0.15);

            var audioManager = GetNode<Node>("/root/AudioManager");
            audioManager.Call("PlaySfx", "res://audio/sfx/bite.wav");
        }
    }

    private void ProcessBite(double delta)
    {
        _biteTimer -= delta;

        if (Input.IsActionJustPressed("interact"))
        {
            // Player reacted in time — start reeling
            StartReeling();
            return;
        }

        if (_biteTimer <= 0)
        {
            // Too slow
            _state = FishingState.CatchFail;
            _resultDisplayTimer = 2.5;
            ShowFailResult();
        }
    }

    private void StartReeling()
    {
        _state = FishingState.Reeling;
        _bobber.Visible = false;

        FishData? fish = GetFishById(_selectedFishId);
        float difficulty = fish?.CatchDifficulty ?? 1f;
        float adjustedZoneWidth = TargetZoneWidth / difficulty;

        _indicator.Visible = true;
        _targetZone.Visible = true;

        // Center the target zone
        float barWidth = 200f;
        float zoneX = (barWidth - adjustedZoneWidth) / 2f;
        _targetZone.Size = new Vector2(adjustedZoneWidth, _targetZone.Size.Y);
        _targetZone.Position = new Vector2(zoneX, _targetZone.Position.Y);

        _indicatorPosition = 0f;
        _indicatorDirection = 1f;
        _indicator.Position = new Vector2(0f, _indicator.Position.Y);
        _resultLabel.Text = "Press interact in the target zone!";
    }

    private void ProcessReeling(double delta)
    {
        float barWidth = 200f;
        _indicatorPosition += _indicatorDirection * IndicatorSpeed * (float)delta;
        if (_indicatorPosition >= barWidth)
            _indicatorDirection = -1f;
        else if (_indicatorPosition <= 0f)
            _indicatorDirection = 1f;

        _indicator.Position = new Vector2(_indicatorPosition, _indicator.Position.Y);

        if (Input.IsActionJustPressed("interact"))
        {
            float indicatorCenter = _indicatorPosition + _indicator.Size.X / 2f;
            float zoneLeft = _targetZone.Position.X;
            float zoneRight = _targetZone.Position.X + _targetZone.Size.X;

            if (indicatorCenter >= zoneLeft && indicatorCenter <= zoneRight)
            {
                _state = FishingState.CatchSuccess;
                _resultDisplayTimer = 3.0;
                ShowSuccessResult();
            }
            else
            {
                _state = FishingState.CatchFail;
                _resultDisplayTimer = 2.5;
                ShowFailResult();
            }

            _indicator.Visible = false;
            _targetZone.Visible = false;
        }
    }

    private void ProcessCatchSuccess(double delta)
    {
        _resultDisplayTimer -= delta;
        if (_resultDisplayTimer <= 0)
        {
            EmitSignal(SignalName.FishingComplete, true, _selectedFishId);
            ResetUI();
            _state = FishingState.Idle;
            _resultLabel.Text = "Press interact to cast!";
        }
    }

    private void ProcessCatchFail(double delta)
    {
        _resultDisplayTimer -= delta;
        if (_resultDisplayTimer <= 0)
        {
            EmitSignal(SignalName.FishingComplete, false, _selectedFishId);
            ResetUI();
            _state = FishingState.Idle;
            _resultLabel.Text = "Press interact to cast!";
        }
    }

    private void ShowSuccessResult()
    {
        FishData? fish = GetFishById(_selectedFishId);
        if (fish is null) return;

        _fishNameLabel.Visible = true;
        _fishNameLabel.Text = $"[center]{fish.TagalogName}\n[i]{fish.EnglishName}[/i][/center]";
        _resultLabel.Text = "Nahuli mo! / You caught it!";

        var inventoryManager = GetNode<Node>("/root/InventoryManager");
        inventoryManager.Call("AddItem", _selectedFishId);

        var vocabManager = GetNode<Node>("/root/VocabManager");
        vocabManager.Call("DiscoverWord", fish.TagalogName);

        var audioManager = GetNode<Node>("/root/AudioManager");
        audioManager.Call("PlaySfx", "res://audio/sfx/catch_success.wav");
    }

    private void ShowFailResult()
    {
        _fishNameLabel.Visible = false;
        _resultLabel.Text = "Naku, nakawala! / Oh no, it got away!";
        _bobber.Visible = false;
    }

    private void ResetUI()
    {
        _powerBar.Visible = false;
        _bobber.Visible = false;
        _indicator.Visible = false;
        _targetZone.Visible = false;
        _fishNameLabel.Visible = false;
        _fishNameLabel.Text = "";
        _resultLabel.Text = "";
    }

    public void ReturnToZone()
    {
        GetTree().ChangeSceneToFile("res://scenes/zones/Dalampasigan.tscn");
    }

    // --- Fish data loading and selection ---

    private void LoadFishData()
    {
        var file = FileAccess.Open("res://data/items/fish.json", FileAccess.ModeFlags.Read);
        if (file is null)
        {
            GD.PrintErr("FishingGame: Could not load fish.json");
            return;
        }

        string json = file.GetAsText();
        file.Close();

        var fishList = JsonSerializer.Deserialize<List<FishData>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (fishList is null) return;

        var timeManager = GetNode<Node>("/root/TimeManager");
        string currentPeriod = (string)timeManager.Get("CurrentPeriod");

        _fishPool.Clear();
        foreach (var fish in fishList)
        {
            if (fish.TimePeriods is null || fish.TimePeriods.Contains(currentPeriod))
            {
                _fishPool.Add(fish);
            }
        }
    }

    private string SelectRandomFish()
    {
        if (_fishPool.Count == 0) return "";

        // Weighted selection: common=70%, uncommon=25%, rare=5%
        float roll = _rng.Randf() * 100f;
        string targetRarity;
        if (roll < 70f)
            targetRarity = "common";
        else if (roll < 95f)
            targetRarity = "uncommon";
        else
            targetRarity = "rare";

        // Filter to matching rarity; fallback to full pool
        var candidates = _fishPool.FindAll(f =>
            string.Equals(f.Rarity, targetRarity, StringComparison.OrdinalIgnoreCase));

        if (candidates.Count == 0)
            candidates = _fishPool;

        int index = _rng.RandiRange(0, candidates.Count - 1);
        return candidates[index].Id;
    }

    private FishData? GetFishById(string id)
    {
        return _fishPool.Find(f => f.Id == id);
    }

    private class FishData
    {
        public string Id { get; set; } = "";
        public string TagalogName { get; set; } = "";
        public string EnglishName { get; set; } = "";
        public string Rarity { get; set; } = "common";
        public float CatchDifficulty { get; set; } = 1f;
        public List<string>? TimePeriods { get; set; }
    }
}
