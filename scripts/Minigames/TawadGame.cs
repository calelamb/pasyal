using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pasyal.Systems;

namespace Pasyal.Minigames;

public partial class TawadGame : Node2D
{
    private enum TawadState
    {
        SelectItem,
        VendorOffer,
        PlayerCounter,
        VendorResponse,
        Outcome
    }

    [Signal]
    public delegate void TawadCompleteEventHandler(bool purchased, string itemId, int finalPrice);

    private TawadState _state = TawadState.SelectItem;
    private int _currentPrice;
    private int _fairPrice;
    private int _rounds;
    private int _maxRounds;
    private int _politenessScore;
    private string _selectedItemId = "";

    private List<ShopItemData> _shopItems = new();
    private List<CounterOffer> _currentOffers = new();

    private RandomNumberGenerator _rng = new();

    // Child node references
    private RichTextLabel _vendorLabel = null!;
    private Label _priceLabel = null!;
    private Button _choiceButton1 = null!;
    private Button _choiceButton2 = null!;
    private Button _choiceButton3 = null!;
    private Label _roundLabel = null!;
    private Label _outcomeLabel = null!;
    private Button _returnButton = null!;

    // Number vocab to discover
    private static readonly Dictionary<int, string> NumberWords = new()
    {
        { 1, "isa" },
        { 2, "dalawa" },
        { 3, "tatlo" },
        { 4, "apat" },
        { 5, "lima" },
        { 10, "sampu" },
        { 20, "dalawampu" },
        { 50, "limampu" },
        { 100, "isang daan" },
        { 200, "dalawang daan" },
        { 500, "limang daan" },
        { 1000, "isang libo" }
    };

    public override void _Ready()
    {
        // Add dynamic time tinting
        var modulate = new CanvasModulate();
        var timeManager = GetNode<TimeManager>("/root/TimeManager");
        modulate.Color = timeManager.GetOverlayColor();
        timeManager.TimeChanged += (period) => modulate.Color = timeManager.GetOverlayColor();
        AddChild(modulate);

        _vendorLabel = GetNode<RichTextLabel>("VendorLabel");
        _priceLabel = GetNode<Label>("PriceLabel");
        _choiceButton1 = GetNode<Button>("ChoicesPanel/ChoiceButton1");
        _choiceButton2 = GetNode<Button>("ChoicesPanel/ChoiceButton2");
        _choiceButton3 = GetNode<Button>("ChoicesPanel/ChoiceButton3");
        _roundLabel = GetNode<Label>("RoundLabel");
        _outcomeLabel = GetNode<Label>("OutcomeLabel");
        _returnButton = GetNode<Button>("ReturnButton");

        _choiceButton1.Pressed += () => OnChoiceSelected(0);
        _choiceButton2.Pressed += () => OnChoiceSelected(1);
        _choiceButton3.Pressed += () => OnChoiceSelected(2);
        _returnButton.Pressed += ReturnToZone;

        _rng.Randomize();
        LoadItemData();
        SetState(TawadState.SelectItem);
    }

    private void SetState(TawadState newState)
    {
        _state = newState;
        switch (_state)
        {
            case TawadState.SelectItem:
                ShowSelectItem();
                break;
            case TawadState.VendorOffer:
                ShowVendorOffer();
                break;
            case TawadState.PlayerCounter:
                ShowPlayerCounter();
                break;
            case TawadState.VendorResponse:
                ShowVendorResponse();
                break;
            case TawadState.Outcome:
                ShowOutcome();
                break;
        }
    }

    private void ShowSelectItem()
    {
        _outcomeLabel.Visible = false;
        _returnButton.Visible = false;
        _vendorLabel.Text = "[center]Ano po ang gusto ninyo?\n[i]What would you like?[/i][/center]";
        _priceLabel.Text = "";
        _roundLabel.Text = "";
        SetButtonsVisible(true);

        // Show up to 3 items as choices
        int count = Mathf.Min(_shopItems.Count, 3);
        var buttons = new[] { _choiceButton1, _choiceButton2, _choiceButton3 };
        for (int i = 0; i < 3; i++)
        {
            if (i < count)
            {
                buttons[i].Visible = true;
                buttons[i].Text = $"{_shopItems[i].TagalogName} / {_shopItems[i].EnglishName}";
            }
            else
            {
                buttons[i].Visible = false;
            }
        }
    }

    private void ShowVendorOffer()
    {
        // Inflate price 2-3x
        float inflationMultiplier = _rng.RandfRange(2f, 3f);
        _currentPrice = Mathf.RoundToInt(_fairPrice * inflationMultiplier);
        _rounds = 0;
        _maxRounds = _rng.RandiRange(2, 3);
        _politenessScore = 0;

        _vendorLabel.Text = $"[center]Vendor: \"{_currentPrice} pesos po!\"\n[i]\"That'll be {_currentPrice} pesos!\"[/i][/center]";
        _priceLabel.Text = $"Asking: {_currentPrice} pesos | Fair: ???";
        _roundLabel.Text = $"Round {_rounds + 1} / {_maxRounds}";

        DiscoverNumberVocab(_currentPrice);
        SetState(TawadState.PlayerCounter);
    }

    private void ShowPlayerCounter()
    {
        _currentOffers = GenerateCounterOffers();
        SetButtonsVisible(true);

        var buttons = new[] { _choiceButton1, _choiceButton2, _choiceButton3 };
        for (int i = 0; i < 3; i++)
        {
            buttons[i].Visible = true;
            buttons[i].Text = $"{_currentOffers[i].TagalogText}\n({_currentOffers[i].EnglishText})";
        }
    }

    private void ShowVendorResponse()
    {
        _rounds++;
        _roundLabel.Text = $"Round {_rounds} / {_maxRounds}";

        SetButtonsVisible(false);

        if (_rounds >= _maxRounds)
        {
            // Final round — vendor gives final answer
            string vendorReaction;
            if (_politenessScore >= 2)
            {
                // Polite discount
                _currentPrice = Mathf.Max(_fairPrice - 5, Mathf.RoundToInt(_currentPrice * 0.85f));
                vendorReaction = "Sige na nga, dahil magalang ka naman.\n[i]Alright, since you're polite.[/i]";
            }
            else if (_politenessScore <= -1)
            {
                // Vendor offended, holds price
                vendorReaction = "Hmp. Ayan na presyo ko.\n[i]Hmp. That's my price.[/i]";
            }
            else
            {
                _currentPrice = Mathf.RoundToInt(_currentPrice * 0.9f);
                vendorReaction = "Sige, kaunti pa.\n[i]Okay, a little more off.[/i]";
            }

            _vendorLabel.Text = $"[center]{vendorReaction}[/center]";
            _priceLabel.Text = $"Final Price: {_currentPrice} pesos";
            SetState(TawadState.Outcome);
            return;
        }

        // Mid-round vendor response — slight concession
        int vendorDrop = _rng.RandiRange(5, 15);
        _currentPrice = Mathf.Max(_fairPrice, _currentPrice - vendorDrop);
        _vendorLabel.Text = $"[center]Vendor: \"Hmm... {_currentPrice} na lang?\"\n[i]\"How about {_currentPrice}?\"[/i][/center]";
        _priceLabel.Text = $"Current: {_currentPrice} pesos";
        DiscoverNumberVocab(_currentPrice);

        SetState(TawadState.PlayerCounter);
    }

    private void ShowOutcome()
    {
        SetButtonsVisible(true);
        _outcomeLabel.Visible = true;

        float ratio = (float)_currentPrice / _fairPrice;
        string outcomeText;

        if (ratio < 0.9f)
            outcomeText = "Magaling! Great deal!";
        else if (ratio <= 1.1f)
            outcomeText = "Ayos lang. Fair enough.";
        else
            outcomeText = "Mahal! You overpaid!";

        _outcomeLabel.Text = outcomeText;

        // Show buy / walk away choices
        _choiceButton1.Text = $"Sige, kukunin ko! / I'll take it! ({_currentPrice} pesos)";
        _choiceButton1.Visible = true;
        _choiceButton2.Text = "Sige, hindi na. / No thanks, I'll pass.";
        _choiceButton2.Visible = true;
        _choiceButton3.Visible = false;
    }

    private void OnChoiceSelected(int index)
    {
        switch (_state)
        {
            case TawadState.SelectItem:
                OnItemSelected(index);
                break;
            case TawadState.PlayerCounter:
                OnCounterOfferSelected(index);
                break;
            case TawadState.Outcome:
                OnOutcomeChoice(index);
                break;
        }
    }

    private void OnItemSelected(int index)
    {
        if (index >= _shopItems.Count) return;
        var item = _shopItems[index];
        _selectedItemId = item.Id;
        _fairPrice = item.Price;
        SetState(TawadState.VendorOffer);
    }

    private void OnCounterOfferSelected(int index)
    {
        if (index >= _currentOffers.Count) return;
        var offer = _currentOffers[index];

        // Apply the counter-offer
        _currentPrice = Mathf.RoundToInt(_currentPrice * offer.PriceModifier);
        _politenessScore += offer.PolitenessModifier;

        _vendorLabel.Text = $"[center]You: \"{offer.TagalogText}\"\n[i]{offer.EnglishText}[/i][/center]";
        SetButtonsVisible(false);

        // Brief pause then vendor responds (use a timer via tween)
        var tween = CreateTween();
        tween.TweenInterval(0.8);
        tween.TweenCallback(Callable.From(() => SetState(TawadState.VendorResponse)));
    }

    private void OnOutcomeChoice(int index)
    {
        if (index == 0)
        {
            // Purchase
            var playerData = GetNode<PlayerData>("/root/PlayerData");
            int pesos = playerData.Pesos;

            if (pesos < _currentPrice)
            {
                _outcomeLabel.Text = "Hindi sapat ang pera! / Not enough money!";
                return;
            }

            playerData.SpendPesos(_currentPrice);

            var inventoryManager = GetNode<InventoryManager>("/root/InventoryManager");
            inventoryManager.AddItem(_selectedItemId);

            // Discover transactional vocab
            var vocabManager = GetNode<VocabManager>("/root/VocabManager");
            vocabManager.DiscoverWord("bili");
            vocabManager.DiscoverWord("bayad");
            vocabManager.DiscoverWord("presyo");
            vocabManager.DiscoverWord("sukli");

            EmitSignal(SignalName.TawadComplete, true, _selectedItemId, _currentPrice);
            _vendorLabel.Text = "[center]Salamat sa pagbili!\n[i]Thanks for your purchase![/i][/center]";
        }
        else
        {
            // Walk away
            _outcomeLabel.Text = "Sige, hindi na. / I'll pass.";
            EmitSignal(SignalName.TawadComplete, false, _selectedItemId, 0);
        }

        SetButtonsVisible(false);
        _returnButton.Visible = true;
    }

    private List<CounterOffer> GenerateCounterOffers()
    {
        var offers = new List<CounterOffer>();

        // Polite low offer
        offers.Add(new CounterOffer
        {
            TagalogText = $"Pwede po bang {Mathf.RoundToInt(_currentPrice * 0.6f)} na lang po?",
            EnglishText = $"Could it be just {Mathf.RoundToInt(_currentPrice * 0.6f)} please?",
            PriceModifier = 0.7f,
            PolitenessModifier = 1
        });

        // Moderate offer
        offers.Add(new CounterOffer
        {
            TagalogText = $"Pahingi naman ng konting tawad, {Mathf.RoundToInt(_currentPrice * 0.75f)} na lang?",
            EnglishText = $"A small discount? How about {Mathf.RoundToInt(_currentPrice * 0.75f)}?",
            PriceModifier = 0.8f,
            PolitenessModifier = 0
        });

        // Aggressive offer
        offers.Add(new CounterOffer
        {
            TagalogText = $"Ang mahal naman! {Mathf.RoundToInt(_currentPrice * 0.45f)} lang!",
            EnglishText = $"That's too expensive! Only {Mathf.RoundToInt(_currentPrice * 0.45f)}!",
            PriceModifier = 0.6f,
            PolitenessModifier = -1
        });

        return offers;
    }

    private void DiscoverNumberVocab(int price)
    {
        var vocabManager = GetNode<VocabManager>("/root/VocabManager");
        foreach (var kvp in NumberWords)
        {
            if (price >= kvp.Key)
            {
                vocabManager.DiscoverWord(kvp.Value);
            }
        }
    }

    private void SetButtonsVisible(bool visible)
    {
        _choiceButton1.Visible = visible;
        _choiceButton2.Visible = visible;
        _choiceButton3.Visible = visible;
    }

    private void LoadItemData()
    {
        var file = FileAccess.Open("res://data/items/items.json", FileAccess.ModeFlags.Read);
        if (file is null)
        {
            GD.PrintErr("TawadGame: Could not load items.json");
            return;
        }

        string json = file.GetAsText();
        file.Close();

        var items = JsonSerializer.Deserialize<List<ShopItemData>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (items is not null)
            _shopItems = items;
    }

    private class CounterOffer
    {
        public string TagalogText { get; set; } = "";
        public string EnglishText { get; set; } = "";
        public float PriceModifier { get; set; } = 1f;
        public int PolitenessModifier { get; set; }
    }

    private class ShopItemData
    {
        public string Id { get; set; } = "";
        [JsonPropertyName("tagalog")]
        public string TagalogName { get; set; } = "";
        [JsonPropertyName("english")]
        public string EnglishName { get; set; } = "";
        public int Price { get; set; }
    }

    private void ReturnToZone()
    {
        var zoneManager = GetNode<ZoneManager>("/root/ZoneManager");
        zoneManager.RestorePreviousState();
    }
}
