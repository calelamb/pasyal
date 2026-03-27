# PASYAL — AI Coding Reference

## What Is This Project?

Pasyal is a cozy pixel-art open-world exploration game set in the Philippines, built in Godot 4 (.NET/C#). Players wander freely through a Filipino coastal village — riding trikes, fishing, bargaining at the palengke, talking to NPCs in Tagalog, and learning the language through immersion. No combat, no fail states, no pressure.

## Tech Stack

- **Engine:** Godot 4.x (.NET version)
- **Language:** C# (all game logic in C#, no GDScript)
- **Target resolution:** 320×180 base, scaled up (pixel-perfect)
- **Tile size:** 16×16
- **Character size:** 16×32 (1 tile wide, 2 tall)
- **Target platforms:** Web (primary), Desktop, Mobile (future)

## Project Structure

```
pasyal/
├── CLAUDE.md
├── project.godot
├── assets/
│   ├── sprites/
│   │   ├── characters/        # Player + NPC spritesheets
│   │   ├── tilesets/          # Terrain, buildings, props
│   │   ├── ui/               # Dialogue box, portraits, journal, icons
│   │   └── effects/          # Particles, overlays
│   ├── audio/
│   │   ├── music/            # Chiptune tracks (.ogg)
│   │   ├── sfx/              # Sound effects (.ogg)
│   │   └── ambient/          # Loops per zone/time (.ogg)
│   └── fonts/                # Pixel fonts (must support Filipino chars: ñ, ng)
├── scenes/
│   ├── main/                 # Main.tscn, TitleScreen.tscn
│   ├── zones/                # BahayKubo.tscn, Sentro.tscn, Dalampasigan.tscn, TrikeStop.tscn, BundokTrail.tscn
│   ├── minigames/            # Fishing.tscn, Tawad.tscn, TrikeRide.tscn
│   └── ui/                   # DialogueBox.tscn, Salitaan.tscn, Inventory.tscn, PauseMenu.tscn
├── scripts/
│   ├── Player/
│   │   ├── Player.cs          # Movement, interaction, animation
│   │   └── PlayerData.cs      # Inventory, vocab progress, save state (singleton/autoload)
│   ├── NPC/
│   │   ├── NPCBase.cs         # Base NPC: interact, face player, dialogue trigger
│   │   ├── NPCWander.cs       # Random idle movement
│   │   └── NPCSchedule.cs     # Time-based position/dialogue changes
│   ├── Systems/
│   │   ├── DialogueManager.cs  # Load JSON, run dialogue sequences, handle choices
│   │   ├── VocabManager.cs     # Track discovered words, categories, notify UI
│   │   ├── TimeManager.cs      # 4-period day cycle, palette overlays, signals
│   │   ├── AudioManager.cs     # Music/SFX/ambient layering, crossfade
│   │   ├── SaveManager.cs      # JSON serialization of game state
│   │   ├── ZoneManager.cs      # Scene transitions with fade
│   │   └── InventoryManager.cs # 12-slot bag, add/remove/check
│   ├── Minigames/
│   │   ├── FishingGame.cs      # Cast → wait → catch timing game
│   │   └── TawadGame.cs        # Bargaining dialogue game
│   └── UI/
│       ├── DialogueUI.cs       # Text box, typewriter effect, portraits, choices
│       ├── SalitaanUI.cs       # Vocab journal with categories/tabs
│       └── InventoryUI.cs      # Grid inventory display
└── data/
    ├── dialogue/              # One JSON per NPC
    ├── vocab/
    │   ├── words.json         # All vocab entries
    │   └── untranslatables.json
    ├── items/
    │   └── items.json
    └── fish/
        └── fish.json
```

## C# Conventions

- Use PascalCase for classes, methods, properties, signals
- Use camelCase for local variables and parameters
- Use `_camelCase` for private fields
- One class per file, filename matches class name
- Use C# signals via `[Signal] public delegate void EventNameEventHandler();`
- Use `[Export]` for inspector-exposed properties
- Use nullable reference types where appropriate
- Autoloads (singletons): PlayerData, DialogueManager, VocabManager, TimeManager, AudioManager, SaveManager
- Prefer composition over inheritance for NPC behaviors
- All data files are JSON, deserialized with `System.Text.Json`

## Godot C# Patterns

### Node access
```csharp
// Get child nodes in _Ready
private AnimatedSprite2D _sprite;
public override void _Ready()
{
    _sprite = GetNode<AnimatedSprite2D>("Sprite");
}
```

### Signals
```csharp
// Define
[Signal] public delegate void VocabDiscoveredEventHandler(string word);

// Emit
EmitSignal(SignalName.VocabDiscovered, "kumain");

// Connect
manager.VocabDiscovered += OnVocabDiscovered;
```

### Autoload access
```csharp
var playerData = GetNode<PlayerData>("/root/PlayerData");
var vocabManager = GetNode<VocabManager>("/root/VocabManager");
```

### Scene transitions
```csharp
GetTree().ChangeSceneToFile("res://scenes/zones/Sentro.tscn");
```

## Core Data Schemas

### Dialogue (JSON per NPC)
```json
{
  "aling_nena_morning_1": {
    "lines": [
      {
        "speaker": "aling_nena",
        "tagalog": "Oy! Magandang umaga, anak!",
        "english": "Hey! Good morning, child!",
        "vocab": ["magandang umaga"],
        "emotion": "happy"
      }
    ],
    "choices": [
      {
        "tagalog": "Opo, kumain na po ako.",
        "english": "Yes, I've already eaten.",
        "next": "aling_nena_morning_1a"
      }
    ],
    "conditions": {
      "time": "umaga",
      "minVisits": 0
    }
  }
}
```

### Vocab Entry (words.json)
```json
{
  "tagalog": "tawad",
  "english": "to bargain / haggle",
  "pronunciation": "ta-WAD",
  "example": "Marunong ka bang tumawad?",
  "exampleEnglish": "Do you know how to bargain?",
  "category": "palengke",
  "locationLearned": "Palengke — Kuya Jojo"
}
```

### Fish (fish.json)
```json
{
  "id": "bangus",
  "tagalog": "bangus",
  "english": "milkfish",
  "time": "umaga",
  "rarity": "common",
  "catchDifficulty": 0.3
}
```

### Item (items.json)
```json
{
  "id": "kape",
  "tagalog": "kape",
  "english": "coffee",
  "price": 15,
  "icon": "res://assets/sprites/ui/icons/kape.png",
  "description": "Mainit na kape. Pampagising."
}
```

## Game Systems Detail

### Player Movement
- 4-directional grid-aligned movement (up/down/left/right)
- Walk: 3 tiles/sec, Run (hold shift): 5 tiles/sec
- Smooth interpolation between tiles (lerp, not snap)
- Interaction: spacebar/E key, context-sensitive
- Small bounce indicator above interactive objects
- Camera follows with lerp, clamps at zone edges

### Dialogue System
- Bottom-screen text box with NPC 48×48 portrait
- Typewriter text effect (configurable speed)
- Tagalog text primary (larger), English below (smaller, dimmer)
- English toggle in settings (hide for advanced learners)
- New vocab words highlighted with accent color on first encounter
- Choices shown as 2-3 Tagalog buttons with English subtitle
- DialogueManager loads JSON, tracks conversation state, emits signals

### Time System (TimeManager)
- 4 periods: Umaga (morning), Tanghali (midday), Hapon (afternoon), Gabi (night)
- Time advances ONLY by: sleeping, resting in hammock, completing activities
- Time does NOT auto-advance while walking (keeps it chill)
- Each period applies a CanvasModulate color overlay
- Signals: TimeChanged(string period) — NPCs and audio respond

### Vocab System (VocabManager)
- Loads all vocab from words.json at startup
- Tracks discovered vs undiscovered per word
- Categories: Bahay, Palengke, Dagat, Bayan, Pagkain, Tao, Bilang, Panahon, Paglalakbay, Damdamin
- Special "Untranslatable" entries with long cultural descriptions
- Signal: VocabDiscovered(string tagalog) — triggers journal notification
- Total target: ~200 words in V1

### Inventory (InventoryManager)
- 12 slots (3×4 grid)
- Items: fish, food, market goods
- Items display Tagalog name as primary label
- Used for: selling at palengke, delivering task items, future cooking

### Save System (SaveManager)
- Auto-save on zone transition and sleep
- JSON serialization to user://save.json
- Saves: player position, current zone, time period, inventory, discovered vocab list, NPC relationship values, completed tasks

## Mini-Games

### Fishing (FishingGame.cs)
1. Enter bangka at dock → scene transitions to side-view fishing scene
2. Press action to cast (timing = distance)
3. Wait: bobber floats, random bite timer (3-10 sec)
4. Bite: visual + audio cue, moving indicator appears
5. Catch: press action when indicator is in target zone
6. Success: fish name in Tagalog + English, added to inventory
7. Fish vary by time of day and rarity
8. Tatay Andoy comments on catches

### Tawad / Bargaining (TawadGame.cs)
1. Interact with palengke vendor → select item
2. Vendor quotes price in Tagalog: "Limang daan!"
3. Player picks from 3 Tagalog counter-offers
4. 2-3 rounds of back-and-forth
5. Outcomes: great deal, fair deal, overpaid, no sale
6. Using "po" and "na lang" improves offers
7. Teaches numbers, polite language, cultural bargaining norms

## Zones (V1: Baryo Alon)

5 zones, each a separate scene:

1. **Bahay Kubo** (~30×25 tiles) — Home base. Player's hut, Aling Nena, animals, mango tree. Domestic vocab.
2. **Sentro** (~40×35 tiles) — Town center. Sari-sari store (Ate Merly), palengke (Kuya Jojo + tawad game), plaza (Padre Miguel), kapitan hall (Kapitan Rody). Social/commerce vocab.
3. **Dalampasigan** (~35×20 tiles) — Beach. Fishing (Tatay Andoy), bangka dock, hammock, bonfire, coconut trees. Nature/sea vocab.
4. **Trike Stop** (~15×10 tiles) — Travel hub. Manong Boy, trike ride animation to one destination. Travel vocab.
5. **Bundok Trail** (~10×15 tiles) — Teaser. Blocked path hinting at V2 content.

## NPCs (V1)

| NPC | Zone | Role | Personality |
|-----|------|------|-------------|
| Aling Nena | Bahay Kubo | Tutorial neighbor, lola | Warm, nosy, always cooking |
| Ate Merly | Sentro (sari-sari) | Shopkeeper | Efficient, gossipy |
| Kuya Jojo | Sentro (palengke) | Fish vendor, tawad opponent | Loud, theatrical, funny |
| Kapitan Rody | Sentro (hall) | Barangay captain, task-giver | Busy, practical, proud |
| Padre Miguel | Sentro (plaza) | Elder, philosopher | Slow-speaking, wise |
| Tatay Andoy | Dalampasigan | Fisherman, storyteller | Quiet, patient |
| Manong Boy | Trike Stop | Trike driver | Chatty, drives fast |

## Art Direction

- Pixel art, 16×16 tiles, Stardew Valley-inspired
- Warm tropical palette: terracotta #C4684A, ocean #3A7CA5, green #6B8F3C, gold #E8A838, sand #E8D5B7
- Accents: jeepney red #D4382C, jeepney yellow #F5C518, bougainvillea pink #D94F8C
- 4 time-of-day palette overlays via CanvasModulate
- Environmental storytelling: tsinelas outside doors, sampayan, stray animals, hand-painted signs, tangled wires

## Build Phases

Phase 0: Project scaffold, folder structure, autoloads, test scene with placeholder tiles
Phase 1: Player movement, camera, zone transitions, dialogue system, vocab manager, time system, save/load, inventory
Phase 2: Bahay Kubo zone — Aling Nena, animals, interactive objects, dialogue
Phase 3: Sentro — sari-sari store, palengke, tawad mini-game, 4 NPCs
Phase 4: Dalampasigan — fishing mini-game, Tatay Andoy, beach interactions
Phase 5: Trike Stop — travel system, Manong Boy, ride animation, Bundok teaser
Phase 6: Salitaan journal UI — categories, word tracking, untranslatables, discovery notifications
Phase 7: Polish — lighting, audio, particles, NPC schedules, title screen, settings
Phase 8: Content — all dialogue written, 200 vocab words populated, playtesting, itch.io release

## Current Phase

Phase 8 — All phases complete. Full game built: 5 zones, 7 NPCs, 3 mini-games, 200 vocab words, journal, inventory, title screen, settings, pause menu, save/load, delivery tasks, shop system.

## Important Notes

- This is a CHILL game. No combat, no death, no fail states. Keep everything warm and welcoming.
- Tagalog is the primary language of the game world. English is secondary/toggle.
- All C#, no GDScript. Use Godot's .NET APIs.
- JSON for all data (dialogue, vocab, items, fish). No hardcoded content.
- Pixel-perfect rendering: set project display to 320×180 with integer scaling.
- Audio: .ogg format for all audio in Godot.
