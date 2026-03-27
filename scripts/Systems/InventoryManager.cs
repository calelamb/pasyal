using Godot;
using System.Collections.Generic;

namespace Pasyal.Systems;

public partial class InventoryManager : Node
{
    [Signal] public delegate void InventoryChangedEventHandler();
    [Signal] public delegate void ItemAddedEventHandler(string itemId);
    [Signal] public delegate void ItemRemovedEventHandler(string itemId);

    private const int MaxSlots = 12;
    private readonly List<string> _items = new();

    public bool AddItem(string itemId)
    {
        if (_items.Count >= MaxSlots)
            return false;

        _items.Add(itemId);
        EmitSignal(SignalName.ItemAdded, itemId);
        EmitSignal(SignalName.InventoryChanged);
        return true;
    }

    public bool RemoveItem(string itemId)
    {
        if (!_items.Remove(itemId))
            return false;

        EmitSignal(SignalName.ItemRemoved, itemId);
        EmitSignal(SignalName.InventoryChanged);
        return true;
    }

    public bool HasItem(string itemId)
    {
        return _items.Contains(itemId);
    }

    public List<string> GetItems()
    {
        return new List<string>(_items);
    }

    public int GetItemCount()
    {
        return _items.Count;
    }

    public bool IsFull()
    {
        return _items.Count >= MaxSlots;
    }

    public void Clear()
    {
        _items.Clear();
        EmitSignal(SignalName.InventoryChanged);
    }
}
