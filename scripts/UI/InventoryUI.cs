using Godot;
using System.Collections.Generic;
using System.Text.Json;
using Pasyal.Systems;

namespace Pasyal.UI;

public partial class InventoryUI : Control
{
    [Signal]
    public delegate void InventoryClosedEventHandler();

    private const int SlotCount = 12;
    private const int Columns = 4;

    private GridContainer _grid = null!;
    private Label _pesosLabel = null!;
    private Button _closeButton = null!;

    private InventoryManager _inventoryManager = null!;
    private PlayerData _playerData = null!;

    private Dictionary<string, string> _itemDisplayNames = new();

    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Always;

        _grid = GetNode<GridContainer>("Panel/VBoxContainer/Grid");
        _pesosLabel = GetNode<Label>("Panel/VBoxContainer/PesosLabel");
        _closeButton = GetNode<Button>("CloseButton");

        _grid.Columns = Columns;

        _inventoryManager = GetNode<InventoryManager>("/root/InventoryManager");
        _playerData = GetNode<PlayerData>("/root/PlayerData");

        _inventoryManager.InventoryChanged += Refresh;
        _playerData.PesosChanged += OnPesosChanged;

        _closeButton.Pressed += Close;

        LoadItemData();
        Visible = false;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("inventory"))
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
        if (_inventoryManager is not null)
            _inventoryManager.InventoryChanged -= Refresh;
        if (_playerData is not null)
            _playerData.PesosChanged -= OnPesosChanged;
    }

    public void Open()
    {
        GetTree().Paused = true;
        Visible = true;
        Refresh();
    }

    public void Close()
    {
        Visible = false;
        GetTree().Paused = false;
        EmitSignal(SignalName.InventoryClosed);
    }

    private void LoadItemData()
    {
        string path = "res://data/items/items.json";
        if (!FileAccess.FileExists(path))
            return;

        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        if (file is null)
            return;

        string json = file.GetAsText();
        using var doc = JsonDocument.Parse(json);

        foreach (var item in doc.RootElement.EnumerateArray())
        {
            string id = item.GetProperty("id").GetString() ?? "";
            string tagalog = item.GetProperty("tagalog").GetString() ?? id;
            if (!string.IsNullOrEmpty(id))
            {
                _itemDisplayNames[id] = tagalog;
            }
        }
    }

    private void Refresh()
    {
        foreach (var child in _grid.GetChildren())
        {
            child.QueueFree();
        }

        var items = _inventoryManager.GetItems();

        for (int i = 0; i < SlotCount; i++)
        {
            var slot = new PanelContainer
            {
                CustomMinimumSize = new Vector2(36, 28)
            };

            var label = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            if (i < items.Count)
            {
                string itemId = items[i];
                label.Text = _itemDisplayNames.TryGetValue(itemId, out string? displayName)
                    ? displayName
                    : itemId;
            }
            else
            {
                label.Text = "---";
            }

            slot.AddChild(label);
            _grid.AddChild(slot);
        }

        UpdatePesos();
    }

    private void UpdatePesos()
    {
        int pesos = _playerData.Pesos;
        _pesosLabel.Text = $"\u20b1{pesos}";
    }

    private void OnPesosChanged(int _)
    {
        UpdatePesos();
    }
}
