using Godot;
using System.Collections.Generic;
using System.Text.Json;

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

    private Node _inventoryManager = null!;
    private Node _playerData = null!;

    private Dictionary<string, string> _itemDisplayNames = new();

    public override void _Ready()
    {
        _grid = GetNode<GridContainer>("Grid");
        _pesosLabel = GetNode<Label>("PesosLabel");
        _closeButton = GetNode<Button>("CloseButton");

        _grid.Columns = Columns;

        _inventoryManager = GetNode("/root/InventoryManager");
        _playerData = GetNode("/root/PlayerData");

        _inventoryManager.Connect("InventoryChanged", Callable.From(() => Refresh()));
        _playerData.Connect("PesosChanged", Callable.From(() => UpdatePesos()));

        _closeButton.Pressed += Close;

        LoadItemData();
        Visible = false;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("inventory"))
        {
            if (Visible)
                Close();
            else
                Open();

            GetViewport().SetInputAsHandled();
        }
    }

    public void Open()
    {
        Visible = true;
        Refresh();
    }

    public void Close()
    {
        Visible = false;
        EmitSignal(SignalName.InventoryClosed);
    }

    private void LoadItemData()
    {
        string path = "res://data/items.json";
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

        var itemsVariant = _inventoryManager.Call("GetItems");
        var items = itemsVariant.AsGodotArray();

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
                string itemId = items[i].AsString();
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
        int pesos = (int)_playerData.Get("Pesos");
        _pesosLabel.Text = $"\u20b1{pesos}";
    }
}
