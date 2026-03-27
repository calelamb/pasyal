using Godot;
using System.Collections.Generic;
using System.Text.Json;

namespace Pasyal.Systems;

public partial class VocabManager : Node
{
    [Signal] public delegate void VocabDiscoveredEventHandler(string tagalog);

    private readonly List<JsonElement> _allWords = new();
    private readonly HashSet<string> _discoveredWords = new();
    private readonly List<JsonElement> _untranslatables = new();

    public static readonly string[] Categories =
    {
        "Bahay", "Palengke", "Dagat", "Bayan", "Pagkain",
        "Tao", "Bilang", "Panahon", "Paglalakbay", "Damdamin"
    };

    public override void _Ready()
    {
        LoadWords();
        LoadUntranslatables();
    }

    private void LoadWords()
    {
        string path = "res://data/vocab/words.json";
        if (!FileAccess.FileExists(path))
        {
            GD.PrintErr("VocabManager: words.json not found");
            return;
        }

        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        using var doc = JsonDocument.Parse(json);

        foreach (var word in doc.RootElement.EnumerateArray())
        {
            _allWords.Add(word.Clone());
        }

        GD.Print($"VocabManager: Loaded {_allWords.Count} words");
    }

    private void LoadUntranslatables()
    {
        string path = "res://data/vocab/untranslatables.json";
        if (!FileAccess.FileExists(path))
        {
            GD.Print("VocabManager: untranslatables.json not found, skipping");
            return;
        }

        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        using var doc = JsonDocument.Parse(json);

        foreach (var word in doc.RootElement.EnumerateArray())
        {
            _untranslatables.Add(word.Clone());
        }

        GD.Print($"VocabManager: Loaded {_untranslatables.Count} untranslatables");
    }

    public void DiscoverWord(string tagalog)
    {
        if (_discoveredWords.Contains(tagalog))
            return;

        _discoveredWords.Add(tagalog);
        EmitSignal(SignalName.VocabDiscovered, tagalog);
    }

    public bool IsDiscovered(string tagalog)
    {
        return _discoveredWords.Contains(tagalog);
    }

    public List<JsonElement> GetWordsByCategory(string category)
    {
        var results = new List<JsonElement>();
        foreach (var word in _allWords)
        {
            if (word.TryGetProperty("category", out var cat) &&
                cat.GetString() == category)
            {
                results.Add(word);
            }
        }
        return results;
    }

    public HashSet<string> GetAllDiscovered()
    {
        return new HashSet<string>(_discoveredWords);
    }

    public int GetDiscoveryCount()
    {
        return _discoveredWords.Count;
    }

    public int GetTotalCount()
    {
        return _allWords.Count;
    }

    // Used by SaveManager to restore state
    public void RestoreDiscovered(IEnumerable<string> words)
    {
        _discoveredWords.Clear();
        foreach (string word in words)
        {
            _discoveredWords.Add(word);
        }
    }
}
