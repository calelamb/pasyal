using Godot;
using Pasyal.Systems;

namespace Pasyal.UI;

public partial class ShopMenu : Control
{
    private VBoxContainer _itemList = null!;
    private Label _statusLabel = null!;
    private Label _pesosLabel = null!;
    private Button _closeButton = null!;

    private ShopSystem _shopSystem = null!;
    private PlayerData _playerData = null!;

    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Always;

        _itemList = GetNode<VBoxContainer>("Panel/MarginContainer/VBoxContainer/ItemList");
        _statusLabel = GetNode<Label>("Panel/MarginContainer/VBoxContainer/StatusLabel");
        _pesosLabel = GetNode<Label>("Panel/MarginContainer/VBoxContainer/PesosLabel");
        _closeButton = GetNode<Button>("Panel/MarginContainer/VBoxContainer/CloseButton");

        _shopSystem = GetNode<ShopSystem>("/root/ShopSystem");
        _playerData = GetNode<PlayerData>("/root/PlayerData");

        _closeButton.Pressed += Close;
        _playerData.PesosChanged += OnPesosChanged;

        Visible = false;
    }

    public override void _ExitTree()
    {
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
    }

    private void Refresh()
    {
        foreach (Node child in _itemList.GetChildren())
        {
            child.QueueFree();
        }

        _statusLabel.Text = "Pili ka lang ng bibilhin.";
        _pesosLabel.Text = $"\u20b1{_playerData.Pesos}";

        foreach (var item in _shopSystem.GetShopItems())
        {
            var button = new Button
            {
                Text = $"{item.TagalogName} / {item.EnglishName} - \u20b1{item.Price}",
                CustomMinimumSize = new Vector2(0, 28),
                ClipText = true
            };

            string itemId = item.Id;
            button.Pressed += () => Purchase(itemId);
            _itemList.AddChild(button);
        }
    }

    private void Purchase(string itemId)
    {
        bool purchased = _shopSystem.BuyItem(itemId);
        Refresh();
        _statusLabel.Text = purchased
            ? "Salamat! Nasa bag mo na."
            : "Hindi puwede ngayon. Kulang ang pera o puno ang bag.";
    }

    private void OnPesosChanged(int _)
    {
        _pesosLabel.Text = $"\u20b1{_playerData.Pesos}";
    }
}
