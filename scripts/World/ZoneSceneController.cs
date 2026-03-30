using Godot;
using System.Collections.Generic;
using System.Linq;
using Pasyal.Systems;
using Pasyal.UI;

namespace Pasyal.World;

public partial class ZoneSceneController : Node2D
{
    [Export] public Vector2 ZoneSize { get; set; } = new(480, 360);
    private DialogueUI? _dialogueUi;
    private PauseMenu? _pauseMenu;
    private InventoryUI? _inventoryUi;
    private SalitaanUI? _journalUi;
    private ShopMenu? _shopMenu;
    private SettingsMenu? _settingsMenu;
    private CanvasModulate? _canvasModulate;
    private CanvasLayer? _uiLayer;

    private TimeManager _timeManager = null!;
    private ZoneManager _zoneManager = null!;
    private TaskManager _taskManager = null!;
    private ShopSystem _shopSystem = null!;

    public override void _Ready()
    {
        _canvasModulate = GetNodeOrNull<CanvasModulate>("CanvasModulate");

        // Move DialogueUI and VocabNotification into a CanvasLayer so they stay fixed on screen
        _uiLayer = GetNodeOrNull<CanvasLayer>("UILayer");
        if (_uiLayer is null)
        {
            _uiLayer = new CanvasLayer();
            _uiLayer.Name = "UILayer";
            _uiLayer.Layer = 10;
            AddChild(_uiLayer);
        }

        var dialogueUiNode = GetNodeOrNull<DialogueUI>("DialogueUI");
        if (dialogueUiNode is not null)
        {
            dialogueUiNode.GetParent().RemoveChild(dialogueUiNode);
            _uiLayer.AddChild(dialogueUiNode);
        }
        _dialogueUi = dialogueUiNode;

        var vocabNotif = GetNodeOrNull<Control>("VocabNotification");
        if (vocabNotif is not null)
        {
            vocabNotif.GetParent().RemoveChild(vocabNotif);
            _uiLayer.AddChild(vocabNotif);
        }

        _timeManager = GetNode<TimeManager>("/root/TimeManager");
        _zoneManager = GetNode<ZoneManager>("/root/ZoneManager");
        _taskManager = GetNode<TaskManager>("/root/TaskManager");
        _shopSystem = GetNode<ShopSystem>("/root/ShopSystem");

        _zoneManager.SetCurrentZone(Name);
        ApplyTimeOverlay();
        _timeManager.TimeChanged += OnTimeChanged;
        _taskManager.TaskAccepted += OnTaskAccepted;
        _taskManager.TaskCompleted += OnTaskCompleted;

        BuildZoneVisuals();
        BuildZoneBoundaries();

        // Set player camera bounds to match zone
        var player = GetNodeOrNull<CharacterBody2D>("Player");
        if (player is not null)
        {
            player.Set("ZoneBoundsMin", Vector2.Zero);
            player.Set("ZoneBoundsMax", ZoneSize);
        }

        ConfigureShopInventory();
        CreateSharedUi();
        ConnectInteractables();
    }

    private readonly RandomNumberGenerator _rng = new();

    private void BuildZoneVisuals()
    {
        _rng.Seed = (ulong)Name.GetHashCode(); // deterministic per zone
        float w = ZoneSize.X;
        float h = ZoneSize.Y;

        switch (Name)
        {
            case "BahayKubo": BuildBahayKubo(w, h); break;
            case "Sentro": BuildSentro(w, h); break;
            case "Dalampasigan": BuildDalampasigan(w, h); break;
            case "TrikeStop": BuildTrikeStop(w, h); break;
            case "BundokTrail": BuildBundokTrail(w, h); break;
            default: BuildGenericZone(w, h); break;
        }
    }

    // ── BahayKubo: rural home, lush grass, dirt paths, garden, fence ──
    private void BuildBahayKubo(float w, float h)
    {
        // Base grass
        Bg(0, 0, w, h, new Color("4a7a2a"), -10);
        // Lighter grass patches
        for (int i = 0; i < 30; i++)
            Bg(_rng.RandfRange(0, w - 24), _rng.RandfRange(0, h - 16), _rng.RandfRange(16, 48), _rng.RandfRange(8, 24), new Color("5a8a3a"), -9);
        // Dirt path from center to exits
        Bg(100, 140, 360, 16, new Color("9a7a4a"), -8); // horizontal
        Bg(200, 0, 16, h, new Color("9a7a4a"), -8);     // vertical
        // Path edges (slightly darker)
        Bg(100, 138, 360, 2, new Color("7a5a2a"), -7);
        Bg(100, 156, 360, 2, new Color("7a5a2a"), -7);
        // Nipa hut floor (under the house area)
        Bg(72, 48, 64, 48, new Color("8a6a3a"), -8);
        Bg(70, 46, 68, 2, new Color("6a4a1a"), -7); // shadow
        // Hut roof triangle approximation
        Bg(64, 28, 80, 8, new Color("7a5520"), -6);
        Bg(68, 36, 72, 6, new Color("8a6530"), -6);
        Bg(72, 42, 64, 6, new Color("9a7540"), -6);
        // Garden plots
        Bg(40, 200, 80, 40, new Color("3a5a1a"), -8);
        for (int i = 0; i < 6; i++)
            Bg(44 + i * 12, 204, 8, 32, new Color("4a6a2a"), -7);
        // Flowers in garden
        Color[] flowerColors = { new("d44"), new("dd4"), new("d4d"), new("4dd") };
        for (int i = 0; i < 12; i++)
            Bg(_rng.RandfRange(42, 116), _rng.RandfRange(202, 236), 2, 2, flowerColors[i % 4], -6);
        // Fence posts along bottom garden
        for (float fx = 36; fx < 124; fx += 12)
        {
            Bg(fx, 196, 2, 8, new Color("6a4a2a"), -6);
            Bg(fx, 242, 2, 8, new Color("6a4a2a"), -6);
        }
        Bg(36, 198, 88, 2, new Color("7a5a3a"), -6); // fence rail top
        Bg(36, 244, 88, 2, new Color("7a5a3a"), -6); // fence rail bottom
        // Grass tufts scattered
        for (int i = 0; i < 40; i++)
        {
            float gx = _rng.RandfRange(0, w);
            float gy = _rng.RandfRange(0, h);
            Bg(gx, gy, 3, 5, new Color("3a6a1a"), -5);
            Bg(gx + 2, gy + 1, 2, 4, new Color("4a7a2a"), -5);
        }
        // Small stones
        for (int i = 0; i < 8; i++)
            Bg(_rng.RandfRange(0, w), _rng.RandfRange(0, h), _rng.RandfRange(2, 5), _rng.RandfRange(2, 4), new Color("8a8a7a"), -5);
        // Zone exit markers
        ExitMarker(w - 16, 184, "Sentro →", false);
        ExitMarker(224, h - 16, "↓ Trike Stop", true);
        ZoneTitle("Bahay Kubo", 4, 2);
    }

    // ── Sentro: town center, stone ground, market stalls, plaza ──
    private void BuildSentro(float w, float h)
    {
        Bg(0, 0, w, h, new Color("8a7a5a"), -10); // dusty ground
        // Stone tile pattern
        for (float tx = 0; tx < w; tx += 16)
            for (float ty = 0; ty < h; ty += 16)
            {
                float shade = _rng.RandfRange(0.0f, 0.06f);
                Bg(tx, ty, 15, 15, new Color(0.53f + shade, 0.47f + shade, 0.35f + shade), -9);
            }
        // Main road (horizontal)
        Bg(0, 260, w, 24, new Color("6a6050"), -8);
        Bg(0, 258, w, 2, new Color("5a5040"), -7);
        Bg(0, 284, w, 2, new Color("5a5040"), -7);
        // Plaza area (center)
        Bg(200, 160, 120, 80, new Color("b0a070"), -8);
        Bg(204, 164, 112, 72, new Color("c0b080"), -7);
        // Fountain in plaza
        Bg(244, 188, 32, 24, new Color("5a8aba"), -6);
        Bg(248, 192, 24, 16, new Color("6a9aca"), -5);
        Bg(258, 186, 4, 8, new Color("7aaadd"), -4); // spout
        // Market stalls (tarp roofs)
        Color[] tarpColors = { new("c44a3a"), new("3a6ac4"), new("c4a43a") };
        for (int i = 0; i < 3; i++)
        {
            float sx = 80 + i * 80;
            Bg(sx, 80, 48, 32, new Color("9a7a4a"), -8); // stall base
            Bg(sx - 4, 72, 56, 10, tarpColors[i], -6); // tarp roof
            Bg(sx - 4, 71, 56, 2, tarpColors[i].Darkened(0.2f), -5);
        }
        // Church area (top right)
        Bg(460, 60, 80, 100, new Color("d0c8b0"), -8);
        Bg(464, 64, 72, 92, new Color("e0d8c0"), -7);
        Bg(492, 40, 16, 24, new Color("d0c8b0"), -6); // bell tower
        Bg(494, 36, 12, 8, new Color("8a7a5a"), -5);   // tower top
        Bg(498, 30, 4, 8, new Color("daa520"), -4);     // cross
        // Scattered people-life details
        for (int i = 0; i < 15; i++)
            Bg(_rng.RandfRange(0, w), _rng.RandfRange(0, h), 2, 2, new Color("7a6a4a"), -5);
        // Trees along road
        for (float tx = 40; tx < w; tx += 100)
        {
            Bg(tx, 240, 4, 20, new Color("5a3a1a"), -6);     // trunk
            Bg(tx - 6, 228, 16, 14, new Color("3a7a2a"), -5); // canopy
            Bg(tx - 4, 232, 12, 10, new Color("4a8a3a"), -4);
        }
        ExitMarker(0, 264, "← Bahay Kubo", false);
        ExitMarker(w - 16, 264, "Dalampasigan →", false);
        ExitMarker(304, 0, "↑ Bundok", true);
        ZoneTitle("Sentro ng Bayan", 4, 2);
    }

    // ── Dalampasigan: beach, sand, ocean, palm trees, waves ──
    private void BuildDalampasigan(float w, float h)
    {
        // Sky gradient (upper portion)
        Bg(0, 0, w, h * 0.4f, new Color("87ceeb"), -10);
        // Sand
        Bg(0, h * 0.2f, w, h * 0.5f, new Color("e8d8a0"), -9);
        // Wet sand near water
        Bg(0, h * 0.55f, w, h * 0.1f, new Color("c8b880"), -8);
        // Ocean
        Bg(0, h * 0.65f, w, h * 0.35f, new Color("2a6aaa"), -9);
        // Shallow water
        Bg(0, h * 0.62f, w, h * 0.06f, new Color("4a9acc"), -8);
        // Wave foam lines
        for (int i = 0; i < 4; i++)
        {
            float wy = h * 0.63f + i * 20;
            for (float fx = _rng.RandfRange(0, 20); fx < w; fx += _rng.RandfRange(12, 32))
                Bg(fx, wy, _rng.RandfRange(8, 24), 2, new Color("eeffff"), -7);
        }
        // Sand texture variation
        for (int i = 0; i < 60; i++)
        {
            float sx = _rng.RandfRange(0, w);
            float sy = _rng.RandfRange(h * 0.22f, h * 0.58f);
            Bg(sx, sy, _rng.RandfRange(2, 6), 1, new Color("d8c890"), -8);
        }
        // Shells and pebbles
        Color[] shellColors = { new("f0e0d0"), new("e0d0c0"), new("d0c0a0"), new("f0d0d0") };
        for (int i = 0; i < 10; i++)
            Bg(_rng.RandfRange(20, w - 20), _rng.RandfRange(h * 0.3f, h * 0.55f), 3, 2, shellColors[i % 4], -6);
        // Palm trees
        float[] palmX = { 60, 180, 380, 480 };
        foreach (float px in palmX)
        {
            float py = h * 0.18f;
            // Trunk
            Bg(px, py, 4, 40, new Color("6a4a2a"), -5);
            Bg(px + 1, py, 2, 40, new Color("7a5a3a"), -4);
            // Fronds (fan of leaves)
            Bg(px - 12, py - 8, 28, 6, new Color("2a7a1a"), -4);
            Bg(px - 10, py - 14, 24, 8, new Color("3a8a2a"), -3);
            Bg(px - 8, py - 4, 20, 4, new Color("2a6a1a"), -4);
            // Coconuts
            Bg(px, py - 2, 3, 3, new Color("6a5020"), -3);
            Bg(px + 3, py, 3, 3, new Color("7a6030"), -3);
        }
        // Boat near shore
        Bg(260, h * 0.52f, 40, 12, new Color("8a3a1a"), -6);
        Bg(264, h * 0.52f - 4, 32, 6, new Color("9a4a2a"), -5);
        Bg(296, h * 0.52f - 8, 2, 20, new Color("7a5a3a"), -5); // mast
        ExitMarker(0, 144, "← Sentro", false);
        ZoneTitle("Dalampasigan", 4, 2);
    }

    // ── TrikeStop: small roadside area, concrete, parked trikes ──
    private void BuildTrikeStop(float w, float h)
    {
        Bg(0, 0, w, h, new Color("7a7a6a"), -10); // concrete
        // Concrete tile grid
        for (float tx = 0; tx < w; tx += 16)
            for (float ty = 0; ty < h; ty += 16)
                Bg(tx, ty, 15, 15, new Color(0.48f + _rng.RandfRange(-0.02f, 0.02f), 0.48f + _rng.RandfRange(-0.02f, 0.02f), 0.42f + _rng.RandfRange(-0.02f, 0.02f)), -9);
        // Road
        Bg(0, 60, w, 40, new Color("4a4a4a"), -8);
        Bg(0, 58, w, 2, new Color("9a9a2a"), -7); // yellow line
        Bg(0, 100, w, 2, new Color("9a9a2a"), -7);
        // Road dashes
        for (float dx = 10; dx < w; dx += 24)
            Bg(dx, 78, 12, 2, new Color("eeeedd"), -7);
        // Waiting shed
        Bg(20, 20, 60, 36, new Color("8a8a9a"), -8); // floor
        Bg(20, 16, 60, 6, new Color("5a8aba"), -6);   // roof
        Bg(18, 14, 64, 4, new Color("4a7aaa"), -5);
        Bg(22, 22, 4, 30, new Color("6a6a6a"), -6);    // post left
        Bg(74, 22, 4, 30, new Color("6a6a6a"), -6);    // post right
        // Bench
        Bg(34, 40, 32, 6, new Color("7a5a2a"), -5);
        Bg(36, 46, 4, 8, new Color("6a4a1a"), -5);
        Bg(58, 46, 4, 8, new Color("6a4a1a"), -5);
        // Trike parked
        Bg(140, 64, 24, 16, new Color("cc4444"), -6); // body
        Bg(164, 68, 16, 12, new Color("ddaa44"), -6); // sidecar
        Bg(136, 76, 8, 8, new Color("333333"), -5);   // front wheel
        Bg(168, 76, 8, 8, new Color("333333"), -5);   // rear wheel
        ExitMarker(104, 0, "↑ Bahay Kubo", true);
        ZoneTitle("Trike Stop", 4, 2);
    }

    // ── BundokTrail: mountain trail, dense vegetation, rocks, misty ──
    private void BuildBundokTrail(float w, float h)
    {
        Bg(0, 0, w, h, new Color("2a4a1a"), -10); // deep forest green
        // Dense undergrowth patches
        for (int i = 0; i < 50; i++)
        {
            float shade = _rng.RandfRange(-0.04f, 0.04f);
            Bg(_rng.RandfRange(0, w - 16), _rng.RandfRange(0, h - 8),
               _rng.RandfRange(8, 32), _rng.RandfRange(6, 16),
               new Color(0.18f + shade, 0.32f + shade, 0.1f + shade), -9);
        }
        // Rocky trail path
        Bg(50, 80, 32, 160, new Color("6a5a3a"), -8);
        Bg(48, 80, 2, 160, new Color("5a4a2a"), -7);
        Bg(82, 80, 2, 160, new Color("5a4a2a"), -7);
        // Trail stones
        for (int i = 0; i < 8; i++)
        {
            float sy = 88 + i * 20;
            Bg(_rng.RandfRange(52, 76), sy, _rng.RandfRange(4, 10), _rng.RandfRange(3, 6), new Color("8a7a6a"), -7);
        }
        // Large boulders
        Bg(140, 40, 32, 24, new Color("6a6a6a"), -7);
        Bg(144, 36, 24, 8, new Color("7a7a7a"), -6);
        Bg(160, 80, 20, 16, new Color("5a5a5a"), -7);
        // Dense tree trunks
        float[] treeX = { 16, 120, 180, 200, 100 };
        float[] treeY = { 30, 100, 60, 180, 200 };
        for (int i = 0; i < treeX.Length; i++)
        {
            Bg(treeX[i], treeY[i], 6, 32, new Color("3a2a1a"), -6);
            Bg(treeX[i] - 10, treeY[i] - 12, 26, 16, new Color("1a4a0a"), -5);
            Bg(treeX[i] - 8, treeY[i] - 8, 22, 12, new Color("2a5a1a"), -4);
        }
        // Ferns
        for (int i = 0; i < 20; i++)
        {
            float fx = _rng.RandfRange(0, w);
            float fy = _rng.RandfRange(0, h);
            Bg(fx, fy, 6, 3, new Color("2a6a1a"), -4);
            Bg(fx + 2, fy - 2, 4, 3, new Color("3a7a2a"), -4);
        }
        // Mist effect (semi-transparent white patches)
        for (int i = 0; i < 6; i++)
            Bg(_rng.RandfRange(0, w - 40), _rng.RandfRange(0, h * 0.4f),
               _rng.RandfRange(30, 60), _rng.RandfRange(8, 16),
               new Color(1, 1, 1, 0.08f), -2);
        // "Closed" barrier visual
        Bg(20, 56, 200, 4, new Color("8a4a2a"), -5);
        Bg(20, 52, 4, 12, new Color("6a3a1a"), -5);
        Bg(220, 52, 4, 12, new Color("6a3a1a"), -5);
        ExitMarker(64, h - 16, "↓ Sentro", true);
        ZoneTitle("Bundok Trail", 4, 2);
    }

    private void BuildGenericZone(float w, float h)
    {
        Bg(0, 0, w, h, new Color("5a7a3a"), -10);
        ZoneTitle(Name, 4, 2);
    }

    private void Bg(float x, float y, float width, float height, Color color, int z)
    {
        var rect = new ColorRect();
        rect.Position = new Vector2(x, y);
        rect.Size = new Vector2(width, height);
        rect.Color = color;
        rect.ZIndex = z;
        rect.MouseFilter = Control.MouseFilterEnum.Ignore;
        AddChild(rect);
    }

    private void ExitMarker(float x, float y, string text, bool vertical)
    {
        // Pulsing arrow label near zone exits
        var label = new Label();
        label.Text = text;
        label.Position = new Vector2(x - 20, y);
        label.Size = new Vector2(60, 12);
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.AddThemeFontSizeOverride("font_size", 5);
        label.AddThemeColorOverride("font_color", new Color(1, 1, 0.6f, 0.7f));
        label.AddThemeColorOverride("font_shadow_color", new Color(0, 0, 0, 0.5f));
        label.AddThemeConstantOverride("shadow_offset_x", 1);
        label.AddThemeConstantOverride("shadow_offset_y", 1);
        label.ZIndex = 50;
        AddChild(label);
    }

    private void ZoneTitle(string title, float x, float y)
    {
        var label = new Label();
        label.Text = title;
        label.Position = new Vector2(x, y);
        label.AddThemeFontSizeOverride("font_size", 7);
        label.AddThemeColorOverride("font_color", new Color(1, 1, 1, 0.4f));
        label.ZIndex = 100;
        AddChild(label);
    }

    private void BuildZoneBoundaries()
    {
        float w = ZoneSize.X;
        float h = ZoneSize.Y;
        float thick = 16f;

        // Top wall
        AddWall(new Vector2(w / 2, -thick / 2), new Vector2(w + thick * 2, thick));
        // Bottom wall
        AddWall(new Vector2(w / 2, h + thick / 2), new Vector2(w + thick * 2, thick));
        // Left wall
        AddWall(new Vector2(-thick / 2, h / 2), new Vector2(thick, h + thick * 2));
        // Right wall
        AddWall(new Vector2(w + thick / 2, h / 2), new Vector2(thick, h + thick * 2));
    }

    private void AddWall(Vector2 center, Vector2 size)
    {
        var body = new StaticBody2D();
        body.Position = center;
        var shape = new CollisionShape2D();
        var rect = new RectangleShape2D();
        rect.Size = size;
        shape.Shape = rect;
        body.AddChild(shape);
        AddChild(body);
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
        _uiLayer!.AddChild(instance);
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
        _uiLayer!.AddChild(_settingsMenu);
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
