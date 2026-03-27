using Godot;
using System.Collections.Generic;
using System.Text.Json;

namespace Pasyal.Systems;

public partial class DialogueManager : Node
{
    [Signal] public delegate void DialogueStartedEventHandler();
    [Signal] public delegate void DialogueEndedEventHandler();
    [Signal] public delegate void LineAdvancedEventHandler();
    [Signal] public delegate void ChoicesPresentedEventHandler();

    private bool _isActive;
    private List<JsonElement> _currentLines = new();
    private List<JsonElement> _currentChoices = new();
    private int _currentLineIndex;
    private string _currentDialogueId = "";
    private string _currentNpcId = "";
    private readonly Dictionary<string, JsonDocument> _dialogueCache = new();

    public bool IsActive => _isActive;

    public override void _Ready()
    {
    }

    private JsonDocument LoadDialogueFile(string npcId)
    {
        if (_dialogueCache.TryGetValue(npcId, out var cached))
            return cached;

        string path = $"res://data/dialogue/{npcId}.json";
        if (!FileAccess.FileExists(path))
        {
            GD.PrintErr($"DialogueManager: File not found: {path}");
            return JsonDocument.Parse("{}");
        }

        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        var doc = JsonDocument.Parse(json);
        _dialogueCache[npcId] = doc;
        return doc;
    }

    public string GetBestDialogue(string npcId)
    {
        var doc = LoadDialogueFile(npcId);
        var root = doc.RootElement;

        var timeManager = GetNode<TimeManager>("/root/TimeManager");
        var playerData = GetNode<PlayerData>("/root/PlayerData");

        string bestId = "";
        int bestScore = -1;

        foreach (var property in root.EnumerateObject())
        {
            string dialogueId = property.Name;
            var dialogue = property.Value;

            int score = 0;
            bool conditionsMet = true;

            if (dialogue.TryGetProperty("conditions", out var conditions))
            {
                if (conditions.TryGetProperty("time", out var timeCondition))
                {
                    string requiredTime = timeCondition.ValueKind == JsonValueKind.String
                        ? timeCondition.GetString() ?? ""
                        : "";
                    if (requiredTime != "" && timeManager.CurrentPeriod != requiredTime)
                    {
                        conditionsMet = false;
                    }
                    else if (requiredTime != "")
                    {
                        score += 10;
                    }
                }

                if (conditions.TryGetProperty("minVisits", out var minVisitsCondition))
                {
                    int minVisits = minVisitsCondition.GetInt32();
                    int currentVisits = playerData.GetVisits(npcId);

                    if (currentVisits < minVisits)
                    {
                        conditionsMet = false;
                    }
                    else
                    {
                        score += minVisits;
                    }
                }
            }

            if (conditionsMet && score > bestScore)
            {
                bestScore = score;
                bestId = dialogueId;
            }
        }

        if (bestId == "")
        {
            foreach (var property in root.EnumerateObject())
            {
                bestId = property.Name;
                break;
            }
        }

        return bestId;
    }

    public void StartDialogue(string npcId, string dialogueId)
    {
        var doc = LoadDialogueFile(npcId);
        var root = doc.RootElement;

        if (!root.TryGetProperty(dialogueId, out var dialogue))
        {
            GD.PrintErr($"DialogueManager: Dialogue '{dialogueId}' not found for NPC '{npcId}'");
            return;
        }

        _currentNpcId = npcId;
        _currentDialogueId = dialogueId;
        _currentLines.Clear();
        _currentChoices.Clear();
        _currentLineIndex = 0;

        if (dialogue.TryGetProperty("lines", out var lines))
        {
            foreach (var line in lines.EnumerateArray())
            {
                _currentLines.Add(line.Clone());
            }
        }

        if (dialogue.TryGetProperty("choices", out var choices))
        {
            foreach (var choice in choices.EnumerateArray())
            {
                _currentChoices.Add(choice.Clone());
            }
        }

        _isActive = true;
        EmitSignal(SignalName.DialogueStarted);

        if (_currentLines.Count > 0)
        {
            EmitSignal(SignalName.LineAdvanced);
        }
    }

    public void AdvanceLine()
    {
        if (!_isActive)
            return;

        _currentLineIndex++;

        if (_currentLineIndex >= _currentLines.Count)
        {
            if (_currentChoices.Count > 0)
            {
                EmitSignal(SignalName.ChoicesPresented);
            }
            else
            {
                EndDialogue();
            }
            return;
        }

        EmitSignal(SignalName.LineAdvanced);
    }

    public void SelectChoice(int index)
    {
        if (index < 0 || index >= _currentChoices.Count)
            return;

        var choice = _currentChoices[index];

        // Discover vocab from the chosen option
        if (choice.TryGetProperty("vocab", out var vocab))
        {
            var vocabManager = GetNode<VocabManager>("/root/VocabManager");
            foreach (var word in vocab.EnumerateArray())
            {
                string? w = word.GetString();
                if (w is not null)
                    vocabManager.DiscoverWord(w);
            }
        }

        if (choice.TryGetProperty("next", out var next) &&
            next.ValueKind == JsonValueKind.String)
        {
            string nextId = next.GetString() ?? "";
            if (nextId != "")
            {
                EndDialogue();
                StartDialogue(_currentNpcId, nextId);
                return;
            }
        }

        EndDialogue();
    }

    public JsonElement? GetCurrentLine()
    {
        if (!_isActive || _currentLineIndex < 0 || _currentLineIndex >= _currentLines.Count)
            return null;

        return _currentLines[_currentLineIndex];
    }

    public List<JsonElement> GetChoices()
    {
        return _currentChoices;
    }

    public string CurrentNpcId => _currentNpcId;

    private void EndDialogue()
    {
        _isActive = false;
        _currentLines.Clear();
        _currentChoices.Clear();
        _currentLineIndex = 0;
        _currentDialogueId = "";
        EmitSignal(SignalName.DialogueEnded);
    }
}
