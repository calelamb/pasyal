using Godot;
using System.Linq;
using Pasyal.Systems;
using Pasyal.UI;

namespace Pasyal.World;

public partial class ZoneSceneController : Node2D
{
    private DialogueUI? _dialogueUi;
    private PauseMenu? _pauseMenu;
    private InventoryUI? _inventoryUi;
    private SalitaanUI? _journalUi;
    private ShopMenu? _shopMenu;
    private SettingsMenu? _settingsMenu;
    private CanvasModulate? _canvasModulate;

    private TimeManager _timeManager = null!;
    private ZoneManager _zoneManager = null!;
    private TaskManager _taskManager = null!;
    private ShopSystem _shopSystem = null!;

    public override void _Ready()
    {
        _dialogueUi = GetNodeOrNull<DialogueUI>("DialogueUI");
        _canvasModulate = GetNodeOrNull<CanvasModulate>("CanvasModulate");

        _timeManager = GetNode<TimeManager>("/root/TimeManager");
        _zoneManager = GetNode<ZoneManager>("/root/ZoneManager");
        _taskManager = GetNode<TaskManager>("/root/TaskManager");
        _shopSystem = GetNode<ShopSystem>("/root/ShopSystem");

        _zoneManager.SetCurrentZone(Name);
        ApplyTimeOverlay();
        _timeManager.TimeChanged += OnTimeChanged;
        _taskManager.TaskAccepted += OnTaskAccepted;
        _taskManager.TaskCompleted += OnTaskCompleted;

        ConfigureShopInventory();
        CreateSharedUi();
        ConnectInteractables();
    }

    public override void _ExitTree()
    {
        if (_timeManager is not null)
            _timeManager.TimeChanged -= OnTimeChanged;
        if (_taskManager is not null)
        {
            _taskManager.TaskAccepted -= OnTaskAccepted;
            _taskManager.TaskCompleted -= OnTaskCompleted;
        }
    }

    public void HandleNpcPostDialogue(string npcId, string interactMode)
    {
        if (TryDeliverActiveTask(npcId))
            return;

        switch (interactMode)
        {
            case "shop":
                OpenShopMenu();
                break;
            case "tawad":
                StartTawad();
                break;
            case "task":
                HandleTaskFlow(npcId);
                break;
            case "trike":
                StartTrikeRide();
                break;
        }
    }

    public void StartBonfireStory()
    {
        var dialogueManager = GetNode<DialogueManager>("/root/DialogueManager");
        dialogueManager.StartDialogue("tatay_andoy", "tatay_andoy_gabi_bonfire");
    }

    public void StartTawad()
    {
        GetTree().ChangeSceneToFile("res://scenes/minigames/Tawad.tscn");
    }

    public void StartTrikeRide()
    {
        GetTree().ChangeSceneToFile("res://scenes/minigames/TrikeRide.tscn");
    }

    public void ShowWorldMessage(string tagalog, string english)
    {
        _dialogueUi?.ShowSystemMessage(tagalog, english);
    }

    public bool IsBlockingUiOpen()
    {
        return (_inventoryUi?.Visible ?? false)
            || (_journalUi?.Visible ?? false)
            || (_shopMenu?.Visible ?? false)
            || (_settingsMenu?.Visible ?? false);
    }

    private void CreateSharedUi()
    {
        _pauseMenu = LoadUiScene<PauseMenu>("res://scenes/ui/PauseMenu.tscn");
        _inventoryUi = LoadUiScene<InventoryUI>("res://scenes/ui/InventoryUI.tscn");
        _journalUi = LoadUiScene<SalitaanUI>("res://scenes/ui/Salitaan.tscn");
        _shopMenu = LoadUiScene<ShopMenu>("res://scenes/ui/ShopMenu.tscn");

        if (_pauseMenu is not null)
        {
            _pauseMenu.OpenJournalRequested += () => _journalUi?.Open();
            _pauseMenu.OpenInventoryRequested += () => _inventoryUi?.Open();
            _pauseMenu.OpenSettingsRequested += OpenSettings;
        }
    }

    private T? LoadUiScene<T>(string path) where T : Node
    {
        var scene = GD.Load<PackedScene>(path);
        if (scene is null)
            return null;

        var instance = scene.Instantiate<T>();
        AddChild(instance);
        return instance;
    }

    private void OpenSettings()
    {
        if (_settingsMenu is not null && IsInstanceValid(_settingsMenu))
            return;

        var scene = GD.Load<PackedScene>("res://scenes/ui/SettingsMenu.tscn");
        if (scene is null)
            return;

        _settingsMenu = scene.Instantiate<SettingsMenu>();
        AddChild(_settingsMenu);
        _settingsMenu.TreeExited += () => _settingsMenu = null;
    }

    private void OpenShopMenu()
    {
        _shopMenu?.Open();
    }

    private void ConfigureShopInventory()
    {
        var sceneShop = GetNodeOrNull<ShopSystem>("ShopNode");
        if (sceneShop is not null && sceneShop.AvailableItems.Length > 0)
        {
            _shopSystem.SetAvailableItems(sceneShop.AvailableItems);
        }
    }

    private void ConnectInteractables()
    {
        foreach (Node node in FindChildren("*", "", true, false))
        {
            if (node is InteractableObject interactable)
            {
                interactable.ObjectInteracted += OnObjectInteracted;
            }
        }
    }

    private void OnObjectInteracted(string tagalog, string english)
    {
        ShowWorldMessage(tagalog, english);
    }

    private void OnTimeChanged(string _period)
    {
        ApplyTimeOverlay();
    }

    private void ApplyTimeOverlay()
    {
        if (_canvasModulate is not null)
        {
            _canvasModulate.Color = _timeManager.GetOverlayColor();
        }
    }

    private void HandleTaskFlow(string npcId)
    {
        if (_taskManager.IsTaskActive())
        {
            var activeTask = _taskManager.GetActiveTask();
            if (activeTask is not null)
            {
                ShowWorldMessage(activeTask.TagalogDescription, activeTask.EnglishDescription);
            }
            return;
        }

        var nextTask = _taskManager.GetAvailableTasks().FirstOrDefault(task => task.GiverNpcId == npcId);
        if (nextTask is null)
        {
            ShowWorldMessage("Wala pang bagong gawain.", "No new delivery right now.");
            return;
        }

        _taskManager.AcceptTask(nextTask.Id);
    }

    private bool TryDeliverActiveTask(string npcId)
    {
        var activeTask = _taskManager.GetActiveTask();
        if (activeTask is null || activeTask.TargetNpcId != npcId)
            return false;

        return _taskManager.TryDeliverTask(npcId);
    }

    private void OnTaskAccepted(string taskId)
    {
        var task = _taskManager.GetTaskById(taskId);
        if (task is null)
            return;

        ShowWorldMessage(
            $"Tinanggap mo ang gawain: {task.TagalogDescription}",
            $"You accepted the task: {task.EnglishDescription}");
    }

    private void OnTaskCompleted(string taskId)
    {
        var task = _taskManager.GetTaskById(taskId);
        if (task is null)
            return;

        ShowWorldMessage(
            $"Natapos mo ang gawain! +\u20b1{task.RewardPesos}",
            $"Task complete! +\u20b1{task.RewardPesos}");
    }
}
