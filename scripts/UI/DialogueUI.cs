using Godot;
using System.Collections.Generic;
using System.Text.Json;
using Pasyal.Systems;

namespace Pasyal.UI;

public partial class DialogueUI : Control
{
    [Export] public float TypewriterSpeed = 30f;

    private Control _panel = null!;
    private RichTextLabel? _tagalogRichLabel;
    private Label? _tagalogPlainLabel;
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
    private bool _showingSystemMessage;
    private float _systemMessageTimer;

    public override void _Ready()
    {
        _panel = GetNode<Control>("Panel");
        _tagalogRichLabel = GetNodeOrNull<RichTextLabel>("Panel/MarginContainer/VBoxContainer/TagalogLabel");
        if (_tagalogRichLabel is null)
        {
            _tagalogPlainLabel = GetNodeOrNull<Label>("Panel/MarginContainer/VBoxContainer/TagalogLabel");
        }
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
        if (_showingSystemMessage)
        {
            _systemMessageTimer -= (float)delta;
            if (_systemMessageTimer <= 0f)
            {
                Hide();
                _showingSystemMessage = false;
            }
        }

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
            SetVisibleCharacters(charsToShow);
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!Visible)
            return;

        if (@event.IsActionPressed("interact"))
        {
            if (_showingSystemMessage)
            {
                Hide();
                _showingSystemMessage = false;
                GetViewport().SetInputAsHandled();
                return;
            }

            if (!_dialogueManager.IsActive)
                return;

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
        _showingSystemMessage = false;
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
        _englishLabel.Text = SettingsMenu.ShowEnglish ? english : "";
        _englishLabel.Visible = SettingsMenu.ShowEnglish && !string.IsNullOrEmpty(english);

        _fullBbcodeText = ApplyVocabHighlights(tagalog, vocabWords);
        SetTagalogText(_fullBbcodeText, tagalog, true);

        _totalVisibleChars = GetTotalVisibleCharacters();
        SetVisibleCharacters(0);
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
        SetVisibleCharacters(-1);
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

    public void ShowSystemMessage(string tagalog, string english)
    {
        _showingSystemMessage = true;
        _systemMessageTimer = 2.25f;
        _isTypewriting = false;

        ClearChoices();
        _speakerLabel.Text = "";
        _portrait.Texture = null;
        _englishLabel.Text = SettingsMenu.ShowEnglish ? english : "";
        _englishLabel.Visible = SettingsMenu.ShowEnglish && !string.IsNullOrEmpty(english);

        SetTagalogText(tagalog, tagalog, false);
        SetVisibleCharacters(-1);

        Show();
        _panel.Show();
    }

    private void SetTagalogText(string richText, string plainText, bool useRichText)
    {
        if (_tagalogRichLabel is not null)
        {
            _tagalogRichLabel.BbcodeEnabled = true;
            _tagalogRichLabel.Text = useRichText ? richText : plainText;
        }

        if (_tagalogPlainLabel is not null)
        {
            _tagalogPlainLabel.Text = plainText;
        }
    }

    private int GetTotalVisibleCharacters()
    {
        if (_tagalogRichLabel is not null)
            return _tagalogRichLabel.GetTotalCharacterCount();

        return _tagalogPlainLabel?.Text.Length ?? 0;
    }

    private void SetVisibleCharacters(int count)
    {
        if (_tagalogRichLabel is not null)
        {
            _tagalogRichLabel.VisibleCharacters = count;
        }

        if (_tagalogPlainLabel is not null)
        {
            _tagalogPlainLabel.VisibleCharacters = count;
        }
    }
}
