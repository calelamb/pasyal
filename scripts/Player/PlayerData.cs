using Godot;
using System.Collections.Generic;

namespace Pasyal;

public partial class PlayerData : Node
{
    [Signal]
    public delegate void PesosChangedEventHandler(int newAmount);

    public string CurrentZone { get; set; } = string.Empty;
    public List<string> DiscoveredVocab { get; private set; } = new();
    public List<string> Inventory { get; private set; } = new();
    public Dictionary<string, int> NpcVisitCounts { get; private set; } = new();
    public List<string> CompletedTasks { get; private set; } = new();
    public int Pesos { get; private set; } = 100;

    public void AddVocab(string word)
    {
        if (!DiscoveredVocab.Contains(word))
        {
            DiscoveredVocab.Add(word);
        }
    }

    public bool HasVocab(string word)
    {
        return DiscoveredVocab.Contains(word);
    }

    public void AddVisit(string npcId)
    {
        if (NpcVisitCounts.ContainsKey(npcId))
        {
            NpcVisitCounts[npcId]++;
        }
        else
        {
            NpcVisitCounts[npcId] = 1;
        }
    }

    public int GetVisits(string npcId)
    {
        return NpcVisitCounts.TryGetValue(npcId, out int count) ? count : 0;
    }

    public void CompleteTask(string taskId)
    {
        if (!CompletedTasks.Contains(taskId))
        {
            CompletedTasks.Add(taskId);
        }
    }

    public bool IsTaskCompleted(string taskId)
    {
        return CompletedTasks.Contains(taskId);
    }

    public void AddPesos(int amount)
    {
        Pesos += amount;
        EmitSignal(SignalName.PesosChanged, Pesos);
    }

    public bool SpendPesos(int amount)
    {
        if (amount > Pesos)
        {
            return false;
        }

        Pesos -= amount;
        EmitSignal(SignalName.PesosChanged, Pesos);
        return true;
    }

    public void SetPesos(int amount)
    {
        Pesos = amount;
        EmitSignal(SignalName.PesosChanged, Pesos);
    }

    public void RestoreVisits(Dictionary<string, int> visits)
    {
        NpcVisitCounts.Clear();
        foreach (var kvp in visits)
        {
            NpcVisitCounts[kvp.Key] = kvp.Value;
        }
    }

    public void RestoreTasks(List<string> tasks)
    {
        CompletedTasks.Clear();
        CompletedTasks.AddRange(tasks);
    }

    public void Reset()
    {
        CurrentZone = string.Empty;
        DiscoveredVocab.Clear();
        Inventory.Clear();
        NpcVisitCounts.Clear();
        CompletedTasks.Clear();
        Pesos = 100;
        EmitSignal(SignalName.PesosChanged, Pesos);
    }
}
