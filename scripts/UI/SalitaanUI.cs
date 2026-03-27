using Godot;
using System.Collections.Generic;
using System.Text.Json;
using Pasyal.Systems;

namespace Pasyal.UI;

public partial class SalitaanUI : Control
{
    [Signal]
    public delegate void JournalClosedEventHandler();

    private static readonly string[] Categories =
    {
        "Bahay", "Palengke", "Dagat", "Bayan", "Pagkain",
        "Tao", "Bilang", "Panahon", "Paglalakbay", "Damdamin"
    };

    private VBoxContainer _categoryList = null!;
    private VBoxContainer _wordList = null!;
    private PanelContainer _detailPanel = null!;
    private Label _counterLabel = null!;
    private Button _closeButton = null!;

    private Label _detailTagalog = null!;
    private Label _detailEnglish = null!;
    private Label _detailPronunciation = null!;
    private Label _detailExample = null!;
    private Label _detailExampleEnglish = null!;
    private Label _detailLocation = null!;

    private VocabManager _vocabManager = null!;
    private string _selectedCategory = "Bahay";

    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Always;

        _categoryList = GetNode<VBoxContainer>("HBoxContainer/CategoryList");
        _wordList = GetNode<ScrollContainer>("HBoxContainer/ScrollContainer").GetNode<VBoxContainer>("WordList");
        _detailPanel = GetNode<PanelContainer>("DetailPanel");
        _counterLabel = GetNode<Label>("CounterLabel");
        _closeButton = GetNode<Button>("CloseButton");

        SetupDetailPanel();

        _vocabManager = GetNode<VocabManager>("/root/VocabManager");
        _vocabManager.VocabDiscovered += OnVocabDiscovered;

        _closeButton.Pressed += Close;

        BuildCategoryTabs();
        _detailPanel.Visible = false;
        Visible = false;

        RefreshUI();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("journal"))
        {
            var currentScene = GetTree().CurrentScene;
            bool blockingUiOpen = currentScene?.HasMethod("IsBlockingUiOpen") == true
                && (bool)currentScene.Call("IsBlockingUiOpen");

            if (!Visible && blockingUiOpen)
                return;

            if (Visible)
                Close();
            else
                Open();

            GetViewport().SetInputAsHandled();
        }
    }

    public override void _ExitTree()
    {
        if (_vocabManager is not null)
            _vocabManager.VocabDiscovered -= OnVocabDiscovered;
    }

    public void Open()
    {
        GetTree().Paused = true;
        Visible = true;
        _detailPanel.Visible = false;
        RefreshUI();
    }

    public void Close()
    {
        Visible = false;
        GetTree().Paused = false;
        EmitSignal(SignalName.JournalClosed);
    }

    private void SetupDetailPanel()
    {
        foreach (Node child in _detailPanel.GetChildren())
        {
            child.QueueFree();
        }

        var vbox = new VBoxContainer();
        vbox.Name = "DetailVBox";

        _detailTagalog = new Label { Name = "DetailTagalog" };
        _detailEnglish = new Label { Name = "DetailEnglish" };
        _detailPronunciation = new Label { Name = "DetailPronunciation" };
        _detailExample = new Label { Name = "DetailExample", AutowrapMode = TextServer.AutowrapMode.WordSmart };
        _detailExampleEnglish = new Label { Name = "DetailExampleEnglish", AutowrapMode = TextServer.AutowrapMode.WordSmart };
        _detailLocation = new Label { Name = "DetailLocation" };

        vbox.AddChild(_detailTagalog);
        vbox.AddChild(_detailPronunciation);
        vbox.AddChild(_detailEnglish);
        vbox.AddChild(new HSeparator());
        vbox.AddChild(_detailExample);
        vbox.AddChild(_detailExampleEnglish);
        vbox.AddChild(new HSeparator());
        vbox.AddChild(_detailLocation);

        _detailPanel.AddChild(vbox);
    }

    private void BuildCategoryTabs()
    {
        foreach (var child in _categoryList.GetChildren())
        {
            child.QueueFree();
        }

        foreach (var category in Categories)
        {
            var button = new Button
            {
                Text = category,
                ToggleMode = true,
                CustomMinimumSize = new Vector2(60, 14),
                ClipText = true
            };

            button.Pressed += () => SelectCategory(category, button);
            _categoryList.AddChild(button);

            if (category == _selectedCategory)
            {
                button.ButtonPressed = true;
            }
        }
    }

    private void SelectCategory(string category, Button pressed)
    {
        _selectedCategory = category;
        _detailPanel.Visible = false;

        foreach (var child in _categoryList.GetChildren())
        {
            if (child is Button btn && btn != pressed)
            {
                btn.ButtonPressed = false;
            }
        }

        PopulateWordList();
    }

    private void RefreshUI()
    {
        int discovered = _vocabManager.GetDiscoveryCount();
        int total = _vocabManager.GetTotalCount();
        _counterLabel.Text = $"{discovered} / {total} salita";

        PopulateWordList();
    }

    private void PopulateWordList()
    {
        foreach (var child in _wordList.GetChildren())
        {
            child.QueueFree();
        }

        var words = _vocabManager.GetWordsByCategory(_selectedCategory);

        foreach (var wordVariant in words)
        {
            string jsonStr = wordVariant.GetRawText();
            using var doc = JsonDocument.Parse(jsonStr);
            var word = doc.RootElement;

            string tagalog = word.GetProperty("tagalog").GetString() ?? "???";
            string english = word.GetProperty("english").GetString() ?? "";
            string pronunciation = word.GetProperty("pronunciation").GetString() ?? "";
            bool isDiscovered = _vocabManager.IsDiscovered(tagalog);

            var entry = new Button
            {
                CustomMinimumSize = new Vector2(0, 16),
                ClipText = true,
                Alignment = HorizontalAlignment.Left
            };

            if (isDiscovered)
            {
                entry.Text = $"{tagalog}  -  {english}";
                string capturedJson = jsonStr;
                entry.Pressed += () => ShowDetail(capturedJson);
            }
            else
            {
                entry.Text = "???";
                entry.Disabled = true;
            }

            _wordList.AddChild(entry);
        }
    }

    private void ShowDetail(string jsonStr)
    {
        using var doc = JsonDocument.Parse(jsonStr);
        var word = doc.RootElement;

        _detailTagalog.Text = word.GetProperty("tagalog").GetString() ?? "";
        _detailEnglish.Text = word.GetProperty("english").GetString() ?? "";
        _detailPronunciation.Text = word.GetProperty("pronunciation").GetString() ?? "";

        _detailExample.Text = word.TryGetProperty("example", out var ex)
            ? ex.GetString() ?? ""
            : "";

        _detailExampleEnglish.Text = word.TryGetProperty("exampleEnglish", out var exEn)
            ? exEn.GetString() ?? ""
            : "";

        _detailLocation.Text = word.TryGetProperty("locationLearned", out var loc)
            ? $"Natutunan sa: {loc.GetString()}"
            : "";

        _detailPanel.Visible = true;
    }

    private void OnVocabDiscovered(string _)
    {
        RefreshUI();
    }
}
