using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Pasyal.Systems;

public partial class ShopSystem : Node
{
    [Signal]
    public delegate void ShopOpenedEventHandler();

    [Signal]
    public delegate void ShopClosedEventHandler();

    [Signal]
    public delegate void ItemPurchasedEventHandler(string itemId);

    [Signal]
    public delegate void ItemSoldEventHandler(string itemId);

    [Export] public string[] AvailableItems { get; set; } = new[] { "yelo", "kape", "pandesal", "skyflakes", "c2" };

    private Dictionary<string, ShopItemData> _allItems = new();
    private bool _isOpen;

    private static readonly string[] TransactionVocab = { "bili", "bayad", "presyo", "sukli" };

    public override void _Ready()
    {
        LoadItemData();
    }

    public void OpenShop()
    {
        _isOpen = true;
        EmitSignal(SignalName.ShopOpened);
    }

    public void CloseShop()
    {
        _isOpen = false;
        EmitSignal(SignalName.ShopClosed);
    }

    public void SetAvailableItems(string[] items)
    {
        AvailableItems = items;
    }

    public bool BuyItem(string itemId)
    {
        if (!_allItems.TryGetValue(itemId, out var item))
        {
            GD.PrintErr($"ShopSystem: Unknown item '{itemId}'");
            return false;
        }

        if (!AvailableItems.Contains(itemId))
        {
            GD.PrintErr($"ShopSystem: Item '{itemId}' not available in this shop");
            return false;
        }

        var playerData = GetNode<Node>("/root/PlayerData");
        int pesos = (int)playerData.Get("Pesos");

        if (pesos < item.Price)
        {
            GD.Print("ShopSystem: Not enough pesos");
            return false;
        }

        var inventoryManager = GetNode<Node>("/root/InventoryManager");
        if ((bool)inventoryManager.Call("IsFull"))
        {
            GD.Print("ShopSystem: Inventory is full");
            return false;
        }

        playerData.Call("SpendPesos", item.Price);
        inventoryManager.Call("AddItem", itemId);

        // Discover transactional vocab
        var vocabManager = GetNode<Node>("/root/VocabManager");
        foreach (string word in TransactionVocab)
        {
            vocabManager.Call("DiscoverWord", word);
        }

        EmitSignal(SignalName.ItemPurchased, itemId);
        return true;
    }

    public bool SellItem(string itemId)
    {
        var inventoryManager = GetNode<Node>("/root/InventoryManager");
        if (!(bool)inventoryManager.Call("HasItem", itemId))
        {
            GD.Print($"ShopSystem: Player doesn't have item '{itemId}'");
            return false;
        }

        if (!_allItems.TryGetValue(itemId, out var item))
        {
            GD.PrintErr($"ShopSystem: Unknown item '{itemId}'");
            return false;
        }

        int sellPrice = item.Price / 2;
        inventoryManager.Call("RemoveItem", itemId);

        var playerData = GetNode<Node>("/root/PlayerData");
        playerData.Call("AddPesos", sellPrice);

        EmitSignal(SignalName.ItemSold, itemId);
        return true;
    }

    public List<ShopItemData> GetShopItems()
    {
        var result = new List<ShopItemData>();
        foreach (string itemId in AvailableItems)
        {
            if (_allItems.TryGetValue(itemId, out var item))
            {
                result.Add(item);
            }
        }
        return result;
    }

    private void LoadItemData()
    {
        var file = FileAccess.Open("res://data/items/items.json", FileAccess.ModeFlags.Read);
        if (file is null)
        {
            GD.PrintErr("ShopSystem: Could not load items.json");
            return;
        }

        string json = file.GetAsText();
        file.Close();

        var items = JsonSerializer.Deserialize<List<ShopItemData>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (items is null) return;

        _allItems.Clear();
        foreach (var item in items)
        {
            _allItems[item.Id] = item;
        }
    }

    public class ShopItemData
    {
        public string Id { get; set; } = "";
        [System.Text.Json.Serialization.JsonPropertyName("tagalog")]
        public string TagalogName { get; set; } = "";
        [System.Text.Json.Serialization.JsonPropertyName("english")]
        public string EnglishName { get; set; } = "";
        public int Price { get; set; }
        public string Description { get; set; } = "";
    }
}
