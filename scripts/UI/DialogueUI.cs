using Godot;
using System.Collections.Generic;
using System.Text.Json;
using Pasyal.Systems;

namespace Pasyal.UI;

public partial class DialogueUI : Control
{
    [Export] public float TypewriterSpeed = 30f;

    private PanelContainer _panel = null!;
    private RichTextLabel _tagalogLabel = null!;
    private Label _englishLabel = null!;
    private Label _speakerLabel = null!;
    private VBoxContainer _choicesContainer = null!;
    private TextureRect _portrait = null!;

    private DialogueManager _dialogueManager = null!;
    private VocabManager _vocabManager = null!;

    private bool _isTypewriting;
    private string _fullBbcodeText = "";
    private int _totalVisibleChars;
    private float _visibleCharProgress;

    public override void _Ready()
    {
        _panel = GetNode<PanelContainer>("Panel");
        _tagalogLabel = GetNode<RichTextLabel>("Panel/MarginContainer/VBoxContainer/TagalogLabel");
        _englishLabel = GetNode<Label>("Panel/MarginContainer/VBoxContainer/EnglishLabel");
        _speakerLabel = GetNode<Label>("Panel/MarginContainer/VBoxContainer/SpeakerLabel");
        _choicesContainer = GetNode<VBoxContainer>("ChoicesContainer");
        _portrait = GetNode<TextureRect>("Portrait");

        _dialogueManager = GetNode<DialogueManager>("/root/DialogueManager");
        _vocabManager = GetNode<VocabManager>("/root/VocabManager");

        _dialogueManager.DialogueStarted += OnDialogueStarted;
        _dialogueManager.DialogueEnded += OnDialogueEnded;
        _dialogueManager.LineAdvanced += OnLineAdvanced;
        _dialogueManager.ChoicesPresented += OnChoicesPresented;

        Hide();
    }

    public override void _ExitTree()
    {
        _dialogueManager.DialogueStarted -= OnDialogueStarted;
        _dialogueManager.DialogueEnded -= OnDialogueEnded;
        _dialogueManager.LineAdvanced -= OnLineAdvanced;
        _dialogueManager.ChoicesPresented -= OnChoicesPresented;
    }

    public override void _Process(double delta)
    {
        if (!_isTypewriting)
            return;

        _visibleCharProgress += TypewriterSpeed * (float)delta;
        int charsToShow = (int)_visibleCharProgress;

        if (charsToShow >= _totalVisibleChars)
        {
            CompleteTypewriter();
        }
        else
        {
            _tagalogLabel.VisibleCharacters = charsToShow;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!Visible || !_dialogueManager.IsActive)
            return;

        if (@event.IsActionPressed("interact"))
        {
            if (_isTypewriting)
            {
                CompleteTypewriter();
            }
            else
            {
                _dialogueManager.AdvanceLine();
            }

            GetViewport().SetInputAsHandled();
        }
    }

    private void OnDialogueStarted()
    {
        Show();
        _panel.Show();
        ClearChoices();
    }

    private void OnDialogueEnded()
    {
        Hide();
    }

    private void OnLineAdvanced()
    {
        JsonElement? lineElement = _dialogueManager.GetCurrentLine();
        if (lineElement is null)
            return;

        JsonElement line = lineElement.Value;

        string tagalog = line.TryGetProperty("tagalog", out var t) ? t.GetString() ?? "" : "";
        string english = line.TryGetProperty("english", out var e) ? e.GetString() ?? "" : "";
        string speaker = line.TryGetProperty("speaker", out var s) ? s.GetString() ?? "" : "";

        List<string> vocabWords = new();
        if (line.TryGetProperty("vocab", out var vocab))
        {
            foreach (var word in vocab.EnumerateArray())
            {
                string? w = word.GetString();
                if (w is not null)
                    vocabWords.Add(w);
            }
        }

        _speakerLabel.Text = FormatSpeakerName(speaker);
        _englishLabel.Text = english;

        _fullBbcodeText = ApplyVocabHighlights(tagalog, vocabWords);
        _tagalogLabel.BbcodeEnabled = true;
        _tagalogLabel.Text = _fullBbcodeText;

        _totalVisibleChars = _tagalogLabel.GetTotalCharacterCount();
        _tagalogLabel.VisibleCharacters = 0;
        _visibleCharProgress = 0f;
        _isTypewriting = true;

        ClearChoices();
        LoadPortrait(speaker);

        foreach (string word in vocabWords)
        {
            _vocabManager.DiscoverWord(word);
        }
    }

    private void OnChoicesPresented()
    {
        ClearChoices();

        var choices = _dialogueManager.GetChoices();
        for (int i = 0; i < choices.Count; i++)
        {
            var choice = choices[i];
            string tagalog = choice.TryGetProperty("tagalog", out var t) ? t.GetString() ?? "" : "";
            string english = choice.TryGetProperty("english", out var e) ? e.GetString() ?? "" : "";

            var button = new Button();
            button.Text = $"{tagalog}\n{english}";
            button.CustomMinimumSize = new Vector2(0, 32);

            int choiceIndex = i;
            button.Pressed += () => OnChoiceSelected(choiceIndex);

            _choicesContainer.AddChild(button);
        }

        _choicesContainer.Show();
    }

    private void OnChoiceSelected(int index)
    {
        ClearChoices();
        _dialogueManager.SelectChoice(index);
    }

    private void ClearChoices()
    {
        foreach (Node child in _choicesContainer.GetChildren())
        {
            child.QueueFree();
        }

        _choicesContainer.Hide();
    }

    private void CompleteTypewriter()
    {
        _isTypewriting = false;
        _tagalogLabel.VisibleCharacters = -1;
    }

    private string ApplyVocabHighlights(string text, List<string> vocabWords)
    {
        string result = text;

        foreach (string word in vocabWords)
        {
            string highlighted = $"[color=#E8A838]{word}[/color]";
            result = result.Replace(word, highlighted, System.StringComparison.OrdinalIgnoreCase);
        }

        return result;
    }

    private static string FormatSpeakerName(string speakerId)
    {
        if (string.IsNullOrEmpty(speakerId))
            return "";

        return speakerId.Replace("_", " ")
            .ToUpper()[0] + speakerId.Replace("_", " ")[1..];
    }

    private void LoadPortrait(string speakerId)
    {
        if (string.IsNullOrEmpty(speakerId))
        {
            _portrait.Texture = null;
            return;
        }

        string portraitPath = $"res://assets/sprites/ui/portraits/{speakerId}.png";
        if (ResourceLoader.Exists(portraitPath))
        {
            _portrait.Texture = GD.Load<Texture2D>(portraitPath);
        }
        else
        {
            _portrait.Texture = null;
        }
    }
}
