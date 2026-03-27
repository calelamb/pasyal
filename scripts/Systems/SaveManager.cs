using Godot;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pasyal.Systems;

public partial class SaveManager : Node
{
    private const string SavePath = "user://save.json";

    private class SaveData
    {
        [JsonPropertyName("zone")]
        public string Zone { get; set; } = "";

        [JsonPropertyName("vocab")]
        public List<string> Vocab { get; set; } = new();

        [JsonPropertyName("inventory")]
        public List<string> Inventory { get; set; } = new();

        [JsonPropertyName("visits")]
        public Dictionary<string, int> Visits { get; set; } = new();

        [JsonPropertyName("tasks")]
        public List<string> Tasks { get; set; } = new();

        [JsonPropertyName("pesos")]
        public int Pesos { get; set; }

        [JsonPropertyName("timePeriod")]
        public string TimePeriod { get; set; } = "umaga";

        [JsonPropertyName("playerPositionX")]
        public float PlayerPositionX { get; set; }

        [JsonPropertyName("playerPositionY")]
        public float PlayerPositionY { get; set; }
    }

    public void SaveGame()
    {
        var playerData = GetNode<PlayerData>("/root/PlayerData");
        var timeManager = GetNode<TimeManager>("/root/TimeManager");
        var zoneManager = GetNode<ZoneManager>("/root/ZoneManager");
        var vocabManager = GetNode<VocabManager>("/root/VocabManager");
        var inventoryManager = GetNode<InventoryManager>("/root/InventoryManager");

        var saveData = new SaveData
        {
            Zone = string.IsNullOrEmpty(zoneManager.CurrentZone) ? "BahayKubo" : zoneManager.CurrentZone,
            TimePeriod = timeManager.CurrentPeriod,
            Vocab = new List<string>(vocabManager.GetAllDiscovered()),
            Inventory = inventoryManager.GetItems(),
            Pesos = playerData.Pesos,
            Tasks = new List<string>(playerData.CompletedTasks),
        };

        foreach (var kvp in playerData.NpcVisitCounts)
        {
            saveData.Visits[kvp.Key] = kvp.Value;
        }

        var player = GetTree().CurrentScene?.GetNodeOrNull<Node2D>("Player");
        if (player is not null)
        {
            saveData.PlayerPositionX = player.Position.X;
            saveData.PlayerPositionY = player.Position.Y;
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(saveData, options);

        using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
        if (file is null)
        {
            GD.PrintErr("SaveManager: Could not open save file for writing");
            return;
        }

        file.StoreString(json);
        GD.Print("SaveManager: Game saved");
    }

    public bool LoadGame()
    {
        if (!HasSave())
            return false;

        using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Read);
        if (file is null)
            return false;

        string json = file.GetAsText();
        SaveData? saveData;

        try
        {
            saveData = JsonSerializer.Deserialize<SaveData>(json);
        }
        catch (JsonException e)
        {
            GD.PrintErr($"SaveManager: Failed to parse save file: {e.Message}");
            return false;
        }

        if (saveData is null)
            return false;

        var playerData = GetNode<PlayerData>("/root/PlayerData");
        var timeManager = GetNode<TimeManager>("/root/TimeManager");
        var vocabManager = GetNode<VocabManager>("/root/VocabManager");
        var inventoryManager = GetNode<InventoryManager>("/root/InventoryManager");
        var zoneManager = GetNode<ZoneManager>("/root/ZoneManager");

        timeManager.SetTime(saveData.TimePeriod);
        vocabManager.RestoreDiscovered(saveData.Vocab);
        playerData.SetPesos(saveData.Pesos);
        playerData.RestoreVisits(saveData.Visits);
        playerData.RestoreTasks(saveData.Tasks);

        inventoryManager.Clear();
        foreach (string item in saveData.Inventory)
        {
            inventoryManager.AddItem(item);
        }

        string zoneName = string.IsNullOrEmpty(saveData.Zone) ? "BahayKubo" : saveData.Zone;
        Vector2 spawnPos = saveData.PlayerPositionX == 0f && saveData.PlayerPositionY == 0f
            ? GetDefaultSpawn(zoneName)
            : new Vector2(saveData.PlayerPositionX, saveData.PlayerPositionY);
        zoneManager.TransitionToZone(zoneName, spawnPos);

        GD.Print("SaveManager: Game loaded");
        return true;
    }

    public bool HasSave()
    {
        return FileAccess.FileExists(SavePath);
    }

    public void DeleteSave()
    {
        if (HasSave())
        {
            DirAccess.RemoveAbsolute(SavePath);
            GD.Print("SaveManager: Save deleted");
        }
    }

    private static Vector2 GetDefaultSpawn(string zoneName)
    {
        return zoneName switch
        {
            "Sentro" => new Vector2(32, 280),
            "Dalampasigan" => new Vector2(32, 160),
            "TrikeStop" => new Vector2(120, 32),
            "BundokTrail" => new Vector2(80, 224),
            _ => new Vector2(128, 160)
        };
    }
}
