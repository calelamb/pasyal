using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Pasyal.Systems;

public partial class TaskManager : Node
{
    [Signal]
    public delegate void TaskAcceptedEventHandler(string taskId);

    [Signal]
    public delegate void TaskCompletedEventHandler(string taskId);

    private List<TaskData> _allTasks = new();
    private string _activeTaskId = "";

    public override void _Ready()
    {
        LoadTaskData();
    }

    public List<TaskData> GetAvailableTasks()
    {
        var playerData = GetNode<Node>("/root/PlayerData");
        var available = new List<TaskData>();

        foreach (var task in _allTasks)
        {
            // Skip completed tasks
            if ((bool)playerData.Call("IsTaskCompleted", task.Id))
                continue;

            // Skip if prerequisite not met
            if (!string.IsNullOrEmpty(task.RequiredPreviousTask)
                && !(bool)playerData.Call("IsTaskCompleted", task.RequiredPreviousTask))
                continue;

            available.Add(task);
        }

        return available;
    }

    public TaskData? GetActiveTask()
    {
        if (string.IsNullOrEmpty(_activeTaskId))
            return null;

        return _allTasks.Find(t => t.Id == _activeTaskId);
    }

    public TaskData? GetTaskById(string taskId)
    {
        return _allTasks.Find(t => t.Id == taskId);
    }

    public bool IsTaskActive()
    {
        return !string.IsNullOrEmpty(_activeTaskId);
    }

    public bool AcceptTask(string taskId)
    {
        if (IsTaskActive())
        {
            GD.Print("TaskManager: Already have an active task");
            return false;
        }

        var task = _allTasks.Find(t => t.Id == taskId);
        if (task is null)
        {
            GD.PrintErr($"TaskManager: Unknown task '{taskId}'");
            return false;
        }

        // Check prerequisites
        var playerData = GetNode<Node>("/root/PlayerData");
        if ((bool)playerData.Call("IsTaskCompleted", taskId))
        {
            GD.Print($"TaskManager: Task '{taskId}' already completed");
            return false;
        }

        if (!string.IsNullOrEmpty(task.RequiredPreviousTask)
            && !(bool)playerData.Call("IsTaskCompleted", task.RequiredPreviousTask))
        {
            GD.Print($"TaskManager: Prerequisite '{task.RequiredPreviousTask}' not met");
            return false;
        }

        _activeTaskId = taskId;

        // Add the delivery item to inventory
        if (!string.IsNullOrEmpty(task.ItemId))
        {
            var inventoryManager = GetNode<InventoryManager>("/root/InventoryManager");
            if (inventoryManager.IsFull())
            {
                GD.Print("TaskManager: Inventory is full");
                _activeTaskId = "";
                return false;
            }

            inventoryManager.AddItem(task.ItemId);
        }

        EmitSignal(SignalName.TaskAccepted, taskId);
        return true;
    }

    public bool TryDeliverTask(string npcId)
    {
        if (!IsTaskActive())
            return false;

        var task = GetActiveTask();
        if (task is null || task.TargetNpcId != npcId)
            return false;

        // Remove delivery item from inventory
        if (!string.IsNullOrEmpty(task.ItemId))
        {
            var inventoryManager = GetNode<Node>("/root/InventoryManager");
            inventoryManager.Call("RemoveItem", task.ItemId);
        }

        // Give reward
        var playerData = GetNode<Node>("/root/PlayerData");
        playerData.Call("AddPesos", task.RewardPesos);
        playerData.Call("CompleteTask", task.Id);

        string completedTaskId = _activeTaskId;
        _activeTaskId = "";

        EmitSignal(SignalName.TaskCompleted, completedTaskId);
        return true;
    }

    private void LoadTaskData()
    {
        var file = FileAccess.Open("res://data/tasks/tasks.json", FileAccess.ModeFlags.Read);
        if (file is null)
        {
            GD.PrintErr("TaskManager: Could not load tasks.json");
            return;
        }

        string json = file.GetAsText();
        file.Close();

        var tasks = JsonSerializer.Deserialize<List<TaskData>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (tasks is not null)
            _allTasks = tasks;
    }

    public class TaskData
    {
        public string Id { get; set; } = "";
        public string GiverNpcId { get; set; } = "";
        public string TargetNpcId { get; set; } = "";
        public string ItemId { get; set; } = "";
        public int RewardPesos { get; set; }
        public string TagalogDescription { get; set; } = "";
        public string EnglishDescription { get; set; } = "";
        public string? RequiredPreviousTask { get; set; }
    }
}
