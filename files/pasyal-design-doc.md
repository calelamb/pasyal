# PASYAL — Game Design Document

## Comprehensive Design & Build Plan

**Version:** 1.0
**Author:** Cale Lamb
**Date:** March 2026
**Status:** Pre-Production

---

## 1. Vision & Identity

### One-Line Pitch
A cozy, pixel-art open-world exploration game set in the Philippines where players wander freely through Filipino life — riding jeepneys, fishing, bargaining at the palengke, and learning Tagalog naturally through immersion.

### Core Fantasy
You are a traveler arriving in the Philippines for the first time. No quests, no urgency — just the joy of discovery. You explore a beautiful, living world and absorb the language and culture the way real immersion works: through context, repetition, and human connection.

### Design Pillars
1. **Chill exploration over achievement** — no fail states, no scores, no pressure
2. **Language through living** — Tagalog is learned by existing in the world, not through flashcards
3. **Cultural authenticity** — every detail is drawn from real Filipino life, not stereotypes
4. **Warmth** — the game should feel like being welcomed into someone's home

### Target Audience
- People learning Tagalog (missionaries, expats, language enthusiasts)
- Filipinos abroad (OFWs, diaspora) who miss home
- Tourists curious about the Philippines
- Cozy game enthusiasts (Stardew Valley, A Short Hike, Unpacking fans)
- Anyone who loves pixel art and chill exploration

### Comparable Games
- Stardew Valley (pixel art, activities, NPC relationships)
- A Short Hike (exploration, no pressure, discovering a world)
- Spiritfarer (warmth, emotional depth, beautiful 2D art)
- Unpacking (no combat, meditative, storytelling through environment)
- VA-11 Hall-A (dialogue-heavy, rich world, cultural specificity)

---

## 2. Art Direction

### Style: Pixel Art
Resolution: 320×180 base (16:9), scaled up. This gives a classic pixel-art feel with enough detail for expressive environments.

### Color Palette
Warm, tropical, grounded. Inspired by golden hour in the provinces.

**Primary palette:**
- Terracotta / warm clay: `#C4684A`
- Ocean blue: `#3A7CA5`
- Rice paddy green: `#6B8F3C`
- Sunset gold: `#E8A838`
- Deep wood: `#5C3D2E`
- Sand: `#E8D5B7`
- Sky blue: `#87CEEB`
- Night purple: `#2D1B4E`

**Accent colors:**
- Jeepney red: `#D4382C`
- Jeepney yellow: `#F5C518`
- Sari-sari store teal: `#2AA198`
- Bougainvillea pink: `#D94F8C`

### Tile Size
16×16 pixels per tile. Character sprites are 16×32 (1 tile wide, 2 tiles tall). This is the sweet spot for detail-to-charm ratio.

### Animation Principles
- Characters: 4-frame walk cycle, 2-frame idle breathing
- Environment: Subtle loops — waves (6 frames), palm sway (4 frames), smoke from cooking (8 frames), chickens pecking (4 frames)
- Transitions: Simple fade-to-black between zones, slide-up for UI panels

### Lighting & Time of Day
4 time periods with palette shifts applied as overlay:
- **Umaga (Morning):** Cool blue-white overlay, long shadows, rooster sounds
- **Tanghali (Midday):** Full brightness, harsh shadows, heat shimmer effect
- **Hapon (Afternoon):** Golden hour warm overlay, everything glows
- **Gabi (Night):** Deep blue-purple overlay, warm light from windows, fireflies

### Environmental Storytelling Details
- Tsinelas (slippers) outside every doorway
- Sampayan (clothesline) with laundry blowing
- Stray cats and dogs wandering
- Hand-painted signs on sari-sari stores
- Rust on tin roofs
- Santo Niño statues in windows
- Walis tambo (broom) leaning against walls
- Tangled electric wires overhead in town areas
- Random basketball hoop made from a ring and plywood

---

## 3. World Design — Baryo Alon (V1)

### Overview
"Baryo Alon" (Barangay Wave) is a small coastal fishing village. It's the player's first area and serves as a vertical slice of the full game. Inspired by real towns across Visayas and Luzon — San Juan (La Union), Oslob, Siquijor.

The barangay has 5 interconnected zones. The player can walk freely between them.

### Zone Map

```
                ┌──────────────────────┐
                │    BUNDOK TRAIL      │
                │  (locked in V1,      │
                │   teaser path)       │
                └──────────┬───────────┘
                           │
        ┌──────────────────┼──────────────────┐
        │            SENTRO (Town Center)      │
        │                                      │
        │   Sari-Sari ── Plaza ── Kapitan Hall │
        │                 │                    │
        │             Palengke                 │
        └───────┬─────────┴──────────┬─────────┘
                │                    │
     ┌──────────┴──────┐    ┌───────┴───────────┐
     │   BAHAY KUBO    │    │   DALAMPASIGAN    │
     │   (Home Base)   │    │   (Beach/Shore)   │
     │                 │    │                   │
     │  Player's hut   │    │  Bangka dock      │
     │  Neighbor huts  │    │  Fishing spot     │
     │  Garden area    │    │  Coconut grove    │
     │  Animals        │    │  Bonfire circle   │
     └────────┬────────┘    └───────────────────┘
              │
     ┌────────┴────────┐
     │   TRIKE STOP    │
     │  (Fast travel)  │
     └─────────────────┘
```

### Zone 1: Bahay Kubo Area (Home Base)

**Size:** ~30×25 tiles
**Purpose:** Safe base, tutorial area, domestic vocabulary
**Time-of-day feel:** Morning — roosters crow, Aling Nena sweeps her yard

**Layout:**
- 3-4 nipa huts on stilts with small yards
- Player's hut (can enter, has a sleeping mat, a small table, a radio)
- Aling Nena's hut next door (larger, with a small garden)
- Dirt path connecting huts to the main road
- Free-roaming animals: 2 chickens, 1 rooster, 1 dog, 1 cat
- A mango tree with shade underneath
- Sampayan with laundry between two huts

**Interactive elements:**
- Player's bed → sleep to advance time of day
- Radio → plays OPM-style chiptune music (toggleable)
- Aling Nena → primary tutorial NPC
- Mango tree → shake to collect mangga (future cooking ingredient)
- Animals → tap to learn their Tagalog names

**Vocab taught here:**
| Tagalog | English | Context |
|---------|---------|---------|
| bahay | house | Environmental label |
| kubo | hut (nipa) | Environmental label |
| umaga | morning | Aling Nena greeting |
| gabi | night | Time transition |
| manok | chicken | Tap animal |
| aso | dog | Tap animal |
| pusa | cat | Tap animal |
| mangga | mango | Shake tree |
| kumain | to eat | Aling Nena: "Kumain ka na ba?" |
| tulog | sleep | Bed interaction |
| magandang umaga | good morning | Aling Nena greeting |
| sampayan | clothesline | Environmental label |

**NPC: Aling Nena**
- Role: Neighbor, lola-type, tutorial guide
- Personality: Warm, slightly nosy, always cooking or sweeping
- Morning dialogue: "Oy! Magandang umaga! Kumain ka na ba?"
- Function: Teaches player basic interactions, gives first "task" (go to sari-sari store to buy kape)
- Recurring: Has different dialogue each time of day

### Zone 2: Sentro (Town Center)

**Size:** ~40×35 tiles (largest zone)
**Purpose:** Social hub, commerce, quest-giving, cultural immersion
**Time-of-day feel:** Afternoon — kids playing, busiest time

**Layout:**
- Central plaza with a large acacia tree and concrete benches
- Small simbahan (church) with a bell tower (background building, can peek inside)
- Kapitan's hall (small municipal building with a Philippine flag)
- Sari-sari store (front counter with items displayed behind wire mesh)
- Palengke area (4-5 market stalls in an open-air structure)
- A basketball half-court near the edge (V2 mini-game)
- Posters/signage in Tagalog on walls
- Jeepney decoration mural on one wall

**Sub-area: Sari-Sari Store**
- Run by Ate Merly
- Counter-based interaction: walk up, see items displayed
- Buy menu shows items with Tagalog names and prices
- Items: yelo, kape, pandesal, SkyFlakes, C2, Red Horse (flavor only), load (prepaid credits)
- Teaches transactional Tagalog

**Sari-sari vocab:**
| Tagalog | English | Context |
|---------|---------|---------|
| tindahan | store | Environmental |
| pabili | "I'd like to buy" | Buy interaction |
| magkano | how much | Buy interaction |
| piso | peso | Currency |
| yelo | ice | Item |
| kape | coffee | Item |
| pandesal | bread roll | Item |
| salamat | thank you | After purchase |
| walang anuman | you're welcome | Ate Merly response |
| sukli | change (money) | After purchase |

**NPC: Ate Merly**
- Role: Sari-sari store owner
- Personality: Efficient, friendly, knows everyone's business
- Function: Sell items, source of town gossip/tips
- Dialogue reveals what other NPCs are up to, hints at discoverable things

**Sub-area: Palengke**
- 4-5 stalls: isda (fish), gulay (vegetables), prutas (fruit), karne (meat), kakanin (sweets)
- Vibrant, noisy, colorful pixel art
- This is where the TAWAD MINI-GAME lives (see Section 5)
- Different vendors with different personalities

**NPC: Kuya Jojo** (Palengke)
- Role: Fish vendor, tawad opponent
- Personality: Loud, theatrical, always joking, secretly generous
- Function: Primary tawad mini-game partner
- "Uy! Fresh na fresh ang isda ko! Magkano? Para sa 'yo, special price!"

**NPC: Padre Miguel** (Plaza)
- Role: The town's thoughtful elder (not necessarily a priest, more of a respected lolo)
- Personality: Philosophical, tells stories, speaks slowly and clearly
- Function: Teaches deeper cultural concepts and untranslatable words
- Each conversation introduces one "untranslatable" (see Section 6)

**NPC: Kapitan Rody** (Kapitan Hall)
- Role: Barangay captain
- Personality: Busy, practical, proud of the community
- Function: Gives tasks that unlock progression
- Tasks are gentle nudges, not quests: "Puwede mo bang dalhin 'to kay Tatay Andoy sa beach?"

### Zone 3: Dalampasigan (Beach/Shore)

**Size:** ~35×20 tiles (wide, shallow — long coastline)
**Purpose:** Fishing, relaxation, nature vocabulary, atmosphere
**Time-of-day feel:** Evening — sunset on the water, bonfire

**Layout:**
- Sandy beach with coconut palms (parallax swaying)
- 3 bangka boats at a wooden dock (one is the player's fishing boat)
- Rocky area on one end with tide pools
- Bonfire circle with log seats (NPCs gather here at night)
- Fisherman's shack (Tatay Andoy's spot)
- Shallow water with visible fish swimming
- A hammock between two palms

**Interactive elements:**
- Bangka → enter to start FISHING MINI-GAME (see Section 5)
- Hammock → rest, listen to ambient ocean sounds, time passes
- Bonfire → at night, NPCs tell stories here
- Tide pools → discover small creatures (alimango, talangka, shells)
- Coconut tree → shake for buko (future cooking ingredient)

**Beach vocab:**
| Tagalog | English | Context |
|---------|---------|---------|
| dagat | sea/ocean | Environmental |
| buhangin | sand | Environmental |
| alon | wave | Environmental (and game title!) |
| bangka | boat | Dock interaction |
| isda | fish | Fishing |
| araw | sun | Environmental |
| hangin | wind | Environmental |
| buko | coconut | Shake tree |
| bituin | star | Night sky |
| buwan | moon | Night sky |
| gabi | night | Time |

**NPC: Tatay Andoy**
- Role: Old fisherman
- Personality: Quiet, patient, wise in a simple way
- Function: Teaches fishing, shares stories about the sea
- Sits on the dock mending nets, or in his shack
- Night bonfire dialogue: tells folk tales about the ocean
- "Dahan-dahan lang. Matuto kang maghintay."

### Zone 4: Trike Stop (Travel Hub)

**Size:** ~15×10 tiles (small, functional)
**Purpose:** Fast travel between areas, travel vocabulary
**Time-of-day feel:** Any time — always available

**Layout:**
- A dusty road with a hand-painted sign: "TRIKE — SAKAY DITO"
- One parked trike (colorful, with side car)
- A waiting bench
- A small handwritten fare list on a post

**Interactive elements:**
- Talk to Manong Boy → choose destination from menu
- V1: One destination (Bukid zone — rice paddies, small preview area)
- V2+: Multiple destinations, jeepney terminal upgrade

**Travel vocab:**
| Tagalog | English | Context |
|---------|---------|---------|
| saan | where | Manong Boy asks |
| kanan | right | During ride |
| kaliwa | left | During ride |
| diretso | straight | During ride |
| para | stop | Player can say |
| dito | here | Destination |
| sakay | ride/board | Sign |
| magkano ang pamasahe | how much is the fare | Interaction |

**NPC: Manong Boy**
- Role: Trike driver
- Personality: Chatty, drives too fast, plays music on a phone speaker
- Function: Fast travel + travel vocabulary
- During ride: animated trike sequence with scenery scrolling by
- Random commentary: "Hawak ka lang ha! Medyo baku-bako 'tong daan!"

### Zone 5: Bundok Trail (V1 Teaser)

**Size:** ~10×15 tiles (small, deliberately limited)
**Purpose:** Tease future content, create a sense of a larger world

**Layout:**
- A trail leading uphill from the town center
- Dense tropical vegetation
- After a short walk, a fallen tree blocks the path
- A hand-painted sign: "BAWAL PUMASOK — DELIKADO" (Do not enter — dangerous)
- Beyond the blockage, the player can glimpse mountains and a waterfall in the distance

**Function:**
- Tells the player there's more world to explore
- V2 opens this into a mountain/forest zone with new activities and NPCs

---

## 4. Core Systems

### 4.1 Movement & Interaction

**Player movement:**
- 4-directional grid-based movement (up/down/left/right)
- Walk speed: 3 tiles/second
- Optional run (hold shift/B button): 5 tiles/second
- Smooth sub-tile interpolation (character slides between tiles, not snapping)

**Interaction:**
- Single action button (Space / A button / tap)
- Context-sensitive: talk to NPCs, pick up items, interact with objects
- Small interaction indicator appears above interactive objects (subtle bounce animation)

**Camera:**
- Follows player with slight smoothing/lerp
- Stops at zone edges (no black void)
- Slight vertical offset (player in lower third, more world visible ahead)

### 4.2 Dialogue System

This is the heart of the game and where Tagalog learning happens.

**Dialogue display:**
- Bottom-of-screen text box with NPC portrait (pixel art face, ~48×48)
- NPC name displayed above text
- Text appears character-by-character (typewriter effect)
- Player can press action button to complete text instantly

**Bilingual display:**
- Primary line in Tagalog (larger font)
- English translation below (smaller, slightly dimmer)
- Player can toggle English on/off in settings (advanced learners can hide translations)
- New vocabulary words are highlighted in a distinct color on first encounter

**Dialogue data structure:**
```
DialogueLine {
  speaker: string           // NPC id
  tagalog: string           // "Magandang umaga! Kumain ka na ba?"
  english: string           // "Good morning! Have you eaten?"
  vocab_highlights: string[] // ["magandang umaga", "kumain"]
  emotion: string           // "happy" | "neutral" | "sad" | "excited"
  choices?: DialogueChoice[]
}

DialogueChoice {
  tagalog: string     // "Oo, kumain na ako."
  english: string     // "Yes, I already ate."
  next: string        // dialogue node id
  relationship?: int  // +1 or -1 to NPC affinity
}
```

**Dialogue choices:**
- When present, player picks from 2-3 Tagalog responses
- Each choice shows Tagalog with English underneath
- Choices affect NPC responses but never lock content (no "wrong" answers)
- Choosing more adventurous/Tagalog-forward options may unlock bonus dialogue

### 4.3 Time System

**4 time periods per day:**
1. Umaga (Morning) — 6:00 AM
2. Tanghali (Midday) — 12:00 PM
3. Hapon (Afternoon) — 4:00 PM
4. Gabi (Night) — 8:00 PM

**Time advances by:**
- Sleeping in bed (advance to next morning)
- Resting in hammock (advance one period)
- Completing certain activities (fishing session = one period passes)
- Time does NOT pass by just walking around (this keeps it chill)

**Time affects:**
- NPC locations and available dialogue
- Lighting/palette overlay
- Ambient sounds
- Which activities are available (fishing best in umaga, bonfire only at gabi)
- Palengke only open umaga through hapon

### 4.4 Inventory System

**Simple and small.** This is not an inventory management game.

- 12-slot bag (3×4 grid)
- Items are simple: isda, mangga, buko, pandesal, kape, etc.
- Items show their Tagalog name as the primary label
- Used for: delivering task items, selling at palengke, future cooking
- No equipment, no weapons, no armor

### 4.5 Salitaan (Vocab Journal)

**The player's personal Tagalog dictionary.** This is the "collectible" of the game.

**Accessed from:** Pause menu, always available

**Entry structure:**
```
VocabEntry {
  tagalog: string          // "tawad"
  english: string          // "to bargain / haggle"
  pronunciation: string    // "ta-WAD"
  example: string          // "Marunong ka bang tumawad?"
  example_english: string  // "Do you know how to bargain?"
  category: string         // "palengke" | "dagat" | "bahay" | etc.
  location_learned: string // "Palengke — Kuya Jojo"
  times_encountered: int   // increments each time word appears in dialogue
  discovered: bool         // true once first encountered
}
```

**Display:**
- Organized by category (tabs: Bahay, Palengke, Dagat, Bayan, Tao, etc.)
- Each category has a small icon
- Undiscovered words show as "???" (player knows how many remain per category)
- Total count displayed: "147 / 312 salita"
- Newest discoveries appear with a subtle glow

**No quizzing.** The journal is purely for reference and collection satisfaction. If the player wants to study, they can browse it. But the game never tests them.

---

## 5. Mini-Games & Activities

### 5.1 Fishing (Pangingisda)

**Location:** Dalampasigan, bangka dock
**Trigger:** Walk to bangka, interact → "Mangisda?" (Fish?) → Yes/No

**Gameplay:**
1. Scene transitions to a side-view of the boat on water
2. Player casts line (press action button — timing determines distance)
3. Wait phase: bobber floats, ambient ocean sounds
4. Bite: bobber dips + visual/audio cue
5. Catch: Quick minigame — press button at right moment as a moving indicator crosses a target zone (like Stardew Valley fishing but simpler)
6. Success: fish caught, name appears in Tagalog + English
7. Fish goes to inventory

**Fish variety (time-dependent):**
| Fish | Tagalog | Time | Rarity |
|------|---------|------|--------|
| Tilapia | tilapya | Any | Common |
| Milkfish | bangus | Umaga | Common |
| Grouper | lapu-lapu | Tanghali | Uncommon |
| Squid | pusit | Gabi | Uncommon |
| Blue Marlin | malasugi | Any | Rare |
| Shrimp | hipon | Hapon | Common |

**Vocab learned through fishing:**
- Fish names (above)
- "Huli!" (Caught it!)
- "Wala" (Nothing)
- "Malaki!" (Big one!)
- "Maliit" (Small)
- "Subukan ulit" (Try again)
- Tatay Andoy comments on catches

### 5.2 Tawad (Bargaining Mini-Game)

**Location:** Palengke, any vendor stall
**Trigger:** Interact with vendor → select item → bargaining begins

**Gameplay:**
1. Vendor states a price in Tagalog: "Limang daan!" (500!)
2. Player sees 3 response options, all in Tagalog:
   - High counter: "Apat na raan na lang po?" (400?)
   - Medium counter: "Tatlong daan?" (300?)
   - Low counter: "Dalawang daan!" (200!)
3. Vendor reacts — each vendor has a "base price" and a "walk-away" threshold
4. Back-and-forth for 2-3 rounds
5. Results:
   - Great deal → Vendor: "Sige na nga!" (Fine!) — vendor acts exasperated but amused
   - Fair deal → Vendor: "O sige, deal!" — both happy
   - Overpaid → Vendor grins too wide — player learns to negotiate harder
   - Too aggressive → Vendor waves you off: "Ay nako, hindi na!" — no sale

**Design notes:**
- The mini-game teaches Tagalog numbers, polite language ("po"), and cultural norms
- Using "po" or "opo" in responses improves your offers
- Learning to add "na lang" (just/only) is a key tactic
- Vendors have moods affected by time of day
- It's intentionally lighthearted and funny, never stressful

**Number vocab taught:**
| Tagalog | English |
|---------|---------|
| isa | one |
| dalawa | two |
| tatlo | three |
| apat | four |
| lima | five |
| sampu | ten |
| dalawampu | twenty |
| limampu | fifty |
| isang daan | one hundred |
| limang daan | five hundred |

### 5.3 Delivery Tasks (Pag-abot)

**Not a quest system** — more like neighborly favors.

**Trigger:** NPC asks you to bring something to another NPC
**Example:** Kapitan Rody: "Puwede mo bang dalhin itong isda kay Aling Nena?"

**Gameplay:**
- Receive item → walk to destination NPC → deliver → receive thanks + sometimes a reward item
- Reward is often food or a new vocab word
- Teaches directional language and NPC names

**Design notes:**
- Never mandatory
- Never timed
- Always simple (go from A to B)
- The point is the conversations at pickup and delivery

---

## 6. Tagalog Learning System

### Philosophy
The player should never feel like they're "studying." Language acquisition happens through:
1. **Context** — words appear when they make sense (fish words at the ocean)
2. **Repetition** — NPCs use common phrases repeatedly in natural ways
3. **Choice** — dialogue responses let the player practice constructing meaning
4. **Collection** — the Salitaan journal rewards discovery without pressure

### Word Categories & Target Counts (V1)

| Category | Icon | Words | Source |
|----------|------|-------|--------|
| Bahay (Home) | 🏠 | 25 | Bahay Kubo zone |
| Palengke (Market) | 🛒 | 30 | Palengke + Sari-sari |
| Dagat (Sea) | 🌊 | 20 | Dalampasigan |
| Bayan (Town) | 🏘️ | 20 | Sentro |
| Pagkain (Food) | 🍚 | 25 | All zones |
| Tao (People) | 👤 | 15 | NPC interactions |
| Bilang (Numbers) | 🔢 | 20 | Tawad mini-game |
| Panahon (Weather/Time) | ☀️ | 15 | Environmental |
| Paglalakbay (Travel) | 🛺 | 15 | Trike stop |
| Damdamin (Feelings) | 💛 | 15 | Deep NPC convos |
| **Total** | | **~200** | |

### Untranslatable Concepts (Special Collectibles)

These are cultural concepts with no direct English equivalent. Each is tied to a specific NPC/moment and unlocks a longer journal entry with cultural explanation.

| Tagalog | Rough Meaning | Source NPC |
|---------|--------------|------------|
| Kilig | Romantic excitement / butterflies | Ate Merly (gossip about town couple) |
| Diskarte | Resourcefulness / street smarts | Manong Boy (fixes trike with random parts) |
| Gigil | Overwhelming cuteness urge | Aling Nena (talking about her apo) |
| Bahala na | "Leave it to God" / come what may | Tatay Andoy (before a storm) |
| Utang na loob | Debt of gratitude (deep obligation) | Kapitan Rody (explaining community) |
| Pakikisama | Getting along / group harmony | Padre Miguel (philosophy) |
| Tampo | Silent treatment / sulking (not anger) | Aling Nena (after you don't visit) |
| Pasalubong | Gift brought home from a trip | Manong Boy (after trike ride) |
| Lihi | Pregnancy cravings (cultural) | Background NPC conversation |
| Basta | "Just because" / don't ask | Kuya Jojo (when you ask his recipe) |

### Phrase Progression

The game subtly increases language complexity over time:

**Early game (first encounters):**
- "Magandang umaga!" (Good morning!)
- "Kumain ka na ba?" (Have you eaten?)
- "Salamat po." (Thank you.)

**Mid game (after 30+ vocab):**
- "Saan ka pupunta?" (Where are you going?)
- "Gusto mo bang mangisda?" (Do you want to fish?)
- "Ang ganda ng sunset ngayon." (The sunset is beautiful today.)

**Late game (after 100+ vocab):**
- "Alam mo, noong bata pa ako, walang kuryente dito." (You know, when I was young, there was no electricity here.)
- "Minsan, mas maganda ang tahimik na buhay." (Sometimes, a quiet life is more beautiful.)

---

## 7. Technical Architecture

### Engine: Godot 4.x

**Why Godot:**
- Best-in-class 2D tilemap system
- GDScript is intuitive (Python-like)
- Exports to Web, Windows, Mac, Linux, Android, iOS
- Free, open source, no royalties
- Active community with extensive pixel-art game tutorials
- Built-in animation, audio, and particle systems

### Project Structure

```
pasyal/
├── project.godot
├── CLAUDE.md                    # AI coding reference
│
├── assets/
│   ├── sprites/
│   │   ├── characters/
│   │   │   ├── player/          # Walk cycles, idle, fishing pose
│   │   │   ├── aling_nena/
│   │   │   ├── ate_merly/
│   │   │   ├── kuya_jojo/
│   │   │   ├── tatay_andoy/
│   │   │   ├── kapitan_rody/
│   │   │   ├── padre_miguel/
│   │   │   ├── manong_boy/
│   │   │   └── animals/         # Manok, aso, pusa
│   │   ├── tilesets/
│   │   │   ├── terrain.png      # Ground tiles: grass, dirt, sand, water
│   │   │   ├── buildings.png    # Bahay kubo, sari-sari, simbahan
│   │   │   ├── props.png        # Trees, fences, signs, furniture
│   │   │   └── palengke.png     # Market stalls, produce, goods
│   │   ├── ui/
│   │   │   ├── dialogue_box.png
│   │   │   ├── portraits/       # 48×48 NPC faces
│   │   │   ├── salitaan/        # Journal UI elements
│   │   │   ├── inventory/       # Bag UI
│   │   │   └── icons/           # Item icons, category icons
│   │   └── effects/
│   │       ├── particles/       # Fireflies, cooking smoke, water splash
│   │       └── overlays/        # Time-of-day color overlays
│   │
│   ├── audio/
│   │   ├── music/
│   │   │   ├── baryo_day.ogg    # Main daytime theme (OPM-inspired chiptune)
│   │   │   ├── baryo_night.ogg  # Gentle nighttime theme
│   │   │   ├── palengke.ogg     # Busy market theme
│   │   │   ├── fishing.ogg      # Calm ocean theme
│   │   │   └── trike_ride.ogg   # Fun travel jingle
│   │   ├── sfx/
│   │   │   ├── footsteps_dirt.ogg
│   │   │   ├── footsteps_sand.ogg
│   │   │   ├── footsteps_wood.ogg
│   │   │   ├── rooster.ogg
│   │   │   ├── dog_bark.ogg
│   │   │   ├── waves.ogg
│   │   │   ├── trike_engine.ogg
│   │   │   ├── fishing_cast.ogg
│   │   │   ├── fishing_reel.ogg
│   │   │   ├── fish_caught.ogg
│   │   │   ├── coins.ogg
│   │   │   ├── dialogue_blip.ogg
│   │   │   ├── menu_select.ogg
│   │   │   └── karaoke_distant.ogg
│   │   └── ambient/
│   │       ├── morning_birds.ogg
│   │       ├── afternoon_cicadas.ogg
│   │       ├── night_crickets.ogg
│   │       ├── ocean_waves.ogg
│   │       └── market_bustle.ogg
│   │
│   └── fonts/
│       ├── pixel_main.ttf       # Primary pixel font (supports Filipino characters)
│       └── pixel_accent.ttf     # Secondary font for UI headers
│
├── scenes/
│   ├── main/
│   │   ├── main.tscn            # Main game scene (loads zones)
│   │   ├── title_screen.tscn
│   │   └── loading.tscn
│   ├── zones/
│   │   ├── bahay_kubo.tscn
│   │   ├── sentro.tscn
│   │   ├── dalampasigan.tscn
│   │   ├── trike_stop.tscn
│   │   └── bundok_trail.tscn
│   ├── minigames/
│   │   ├── fishing.tscn
│   │   ├── tawad.tscn
│   │   └── trike_ride.tscn      # Travel animation scene
│   └── ui/
│       ├── dialogue_box.tscn
│       ├── salitaan.tscn        # Vocab journal
│       ├── inventory.tscn
│       ├── pause_menu.tscn
│       └── settings.tscn
│
├── scripts/
│   ├── player/
│   │   ├── player.gd            # Movement, interaction
│   │   └── player_data.gd       # Inventory, discovered words, progress
│   ├── npc/
│   │   ├── npc_base.gd          # Base NPC behavior
│   │   ├── npc_wander.gd        # Random movement AI
│   │   └── npc_schedule.gd      # Time-based positioning
│   ├── systems/
│   │   ├── dialogue_manager.gd  # Load/play dialogue sequences
│   │   ├── vocab_manager.gd     # Track discovered words
│   │   ├── time_manager.gd      # Day/night cycle
│   │   ├── audio_manager.gd     # Music/SFX/ambient layering
│   │   ├── save_manager.gd      # Save/load game state
│   │   ├── zone_manager.gd      # Zone transitions
│   │   └── inventory_manager.gd
│   ├── minigames/
│   │   ├── fishing_game.gd
│   │   └── tawad_game.gd
│   └── ui/
│       ├── dialogue_ui.gd
│       ├── salitaan_ui.gd
│       └── inventory_ui.gd
│
├── data/
│   ├── dialogue/
│   │   ├── aling_nena.json
│   │   ├── ate_merly.json
│   │   ├── kuya_jojo.json
│   │   ├── tatay_andoy.json
│   │   ├── kapitan_rody.json
│   │   ├── padre_miguel.json
│   │   └── manong_boy.json
│   ├── vocab/
│   │   ├── words.json           # All vocab entries
│   │   └── untranslatables.json # Special cultural concepts
│   ├── items/
│   │   └── items.json           # Item definitions
│   └── fish/
│       └── fish.json            # Fish data (name, rarity, time)
│
└── addons/                      # Godot plugins if needed
```

### Key Technical Decisions

**Tilemap approach:**
- Use Godot's TileMap node with multiple layers:
  - Layer 0: Ground (grass, dirt, sand, water)
  - Layer 1: Ground detail (paths, shadows, puddles)
  - Layer 2: Objects (buildings, trees, furniture)
  - Layer 3: Overhead (tree canopy, roofs that fade when player walks under)

**Zone transitions:**
- Each zone is a separate scene
- Walking to zone edge triggers transition (fade to black, load new scene, fade in)
- Player position is set to corresponding entry point
- ~0.5 second transition

**Dialogue data format (JSON):**
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
      },
      {
        "speaker": "aling_nena",
        "tagalog": "Kumain ka na ba?",
        "english": "Have you eaten?",
        "vocab": ["kumain"],
        "emotion": "concerned"
      }
    ],
    "choices": [
      {
        "tagalog": "Opo, kumain na po ako.",
        "english": "Yes, I've already eaten.",
        "next": "aling_nena_morning_1a"
      },
      {
        "tagalog": "Hindi pa po...",
        "english": "Not yet...",
        "next": "aling_nena_morning_1b"
      }
    ],
    "conditions": {
      "time": "umaga",
      "min_visits": 0
    }
  }
}
```

**Save system:**
- Auto-save on zone transition and sleep
- Save data: player position, time of day, inventory, discovered vocab, NPC relationship values, completed tasks
- Use Godot's built-in `ConfigFile` or JSON for save data

---

## 8. Audio Design

### Music Direction
Chiptune / lo-fi with Filipino musical influences. Think: 8-bit OPM.

**Track list (V1):**
| Track | Mood | Tempo | Used In |
|-------|------|-------|---------|
| Baryo Day | Warm, hopeful, folk melody | 100 BPM | Bahay Kubo, Sentro (day) |
| Baryo Night | Gentle, contemplative | 70 BPM | All zones (night) |
| Palengke Hustle | Busy, playful, rhythmic | 120 BPM | Palengke area |
| Dagat | Calm, spacious, ocean feel | 80 BPM | Dalampasigan |
| Trike Ride | Fun, bouncy, road trip | 130 BPM | Travel sequences |
| Tawad Theme | Comedic tension, staccato | 110 BPM | Bargaining mini-game |
| Fishing Calm | Minimal, ambient, meditative | 60 BPM | Fishing mini-game |
| Title Theme | Grand, welcoming, "arrival" | 90 BPM | Title screen |

**Audio approach:**
- Compose or commission 8-bit/chiptune tracks
- Tools: FamiTracker, BeepBox, LMMS, or commission from a chiptune artist
- Alternative: use AI music generation (Suno, Udio) with chiptune/Filipino style prompts, then loop-edit

### Ambient Sound Layers
Each zone has ambient sound loops that blend together:
- Cross-fade between zone ambients on transition
- Time-of-day affects which ambient layers are active
- Volume ducking when dialogue is active

---

## 9. Asset Pipeline

### How to Create Pixel Art Assets

**Tools (pick one):**
- Aseprite ($20, industry standard for pixel art, best animation tools)
- LibreSprite (free, fork of older Aseprite)
- Pixelorama (free, Godot-based pixel editor)
- Piskel (free, web-based, good for quick sprites)

**AI-assisted workflow:**
- Use AI image generation to create concept art / reference images
- Manually pixel-art the final sprites (AI can't do clean pixel art well yet)
- Alternative: generate high-res art, then carefully downscale and clean up pixel-by-pixel

**Sprite specifications:**
| Asset | Size (px) | Frames | Notes |
|-------|-----------|--------|-------|
| Player walk | 16×32 | 4 per direction (16 total) | + idle (2 frames) |
| NPC walk | 16×32 | 4 per direction | Some NPCs only need 1-2 directions |
| NPC portrait | 48×48 | 3-4 emotions | happy, neutral, sad, excited |
| Animals | 16×16 | 2-4 frames | Idle loop |
| Terrain tiles | 16×16 | 1 (static) | Need auto-tile edges |
| Building tiles | 16×16 | 1 (static) | Modular pieces |
| Water tiles | 16×16 | 4 frames | Animated loop |
| Trees | 16×32 or 32×32 | 2 frames (sway) | Coconut, mango, acacia |
| Item icons | 16×16 | 1 | For inventory/journal |
| UI elements | Various | 1 | Dialogue box, buttons, panels |

**Estimated asset count (V1):**
- ~15 character sprites (7 NPCs + player + animals)
- ~7 NPC portraits × 4 emotions = ~28 portrait frames
- ~100 terrain/building tiles
- ~30 prop sprites (furniture, signs, market goods)
- ~20 item icons
- ~10 fish sprites
- UI elements: ~20 pieces
- **Total: ~250-300 individual sprite assets**

### Tileset Strategy

Use a **modular tileset approach:**
1. Create a master terrain tileset with auto-tile rules (Godot supports this)
2. Building pieces are modular (walls, roofs, doors as separate tiles)
3. Props are individual sprites placed on top of the tilemap
4. This means you can build varied environments from a relatively small set of tiles

---

## 10. Build Plan & Timeline

### Phase 0: Setup (Days 1-3)
- [ ] Install Godot 4.x
- [ ] Create project with folder structure
- [ ] Write CLAUDE.md for AI coding assistants
- [ ] Set up Git repo
- [ ] Create a test scene with player movement on a simple tilemap
- [ ] Verify export works (web build)

### Phase 1: Core Systems (Week 1-2)
- [ ] Player movement (4-dir, smooth interpolation)
- [ ] Camera system (follow with lerp, zone bounds)
- [ ] Zone transition system (fade, load, position)
- [ ] Dialogue system (text display, typewriter, portraits, choices)
- [ ] Vocab manager (discover, store, track)
- [ ] Time system (4 periods, palette overlays)
- [ ] Basic save/load
- [ ] Inventory system (simple grid)

### Phase 2: First Zone — Bahay Kubo (Week 2-3)
- [ ] Design tilemap for Bahay Kubo zone
- [ ] Place player hut interior (bed, radio, table)
- [ ] Create Aling Nena NPC (sprite, portrait, dialogue)
- [ ] Animal sprites (chicken, dog, cat) with idle animations
- [ ] Interactive objects (bed, mango tree, sampayan)
- [ ] Write all Aling Nena dialogue (morning, afternoon, evening variants)
- [ ] Ambient sounds (rooster, dog, birds)

### Phase 3: Sentro & Palengke (Week 3-4)
- [ ] Design tilemap for Sentro zone
- [ ] Build sari-sari store (counter interaction, buy menu)
- [ ] Build palengke (market stalls, vendor NPCs)
- [ ] Create NPCs: Ate Merly, Kuya Jojo, Kapitan Rody, Padre Miguel
- [ ] Write all Sentro NPC dialogue
- [ ] Implement tawad mini-game
- [ ] Implement sari-sari buy/sell system
- [ ] Palengke ambient audio

### Phase 4: Beach & Fishing (Week 4-5)
- [ ] Design tilemap for Dalampasigan zone
- [ ] Create Tatay Andoy NPC
- [ ] Implement fishing mini-game
- [ ] Fish data (6 types, time-dependent spawning)
- [ ] Beach interactive objects (hammock, bonfire, coconut tree, tide pools)
- [ ] Ocean ambient audio + wave animation
- [ ] Night bonfire scene with storytelling dialogue

### Phase 5: Travel & Trike (Week 5)
- [ ] Design Trike Stop zone
- [ ] Create Manong Boy NPC
- [ ] Trike ride animation sequence
- [ ] Implement fast travel system
- [ ] Create small destination zone (rice paddy preview)
- [ ] Bundok Trail teaser zone (blocked path)

### Phase 6: Salitaan Journal (Week 5-6)
- [ ] Design journal UI (categories, tabs, word entries)
- [ ] Implement word discovery tracking
- [ ] Untranslatable concepts (special entries with long descriptions)
- [ ] Word highlight system in dialogue
- [ ] Discovery notifications ("New salita discovered!")
- [ ] Progress counter

### Phase 7: Polish & Art (Week 6-7)
- [ ] Time-of-day lighting system (palette overlays)
- [ ] All ambient sound layers per zone + time
- [ ] Music tracks (compose or source)
- [ ] Particle effects (fireflies, cooking smoke, water splash)
- [ ] NPC schedule system (move NPCs based on time)
- [ ] Screen transitions polish
- [ ] Title screen + intro sequence
- [ ] Settings menu (language toggle, volume, controls)
- [ ] Controller support

### Phase 8: Content & Testing (Week 7-8)
- [ ] Write remaining dialogue for all NPCs (aim: 50+ unique conversations)
- [ ] Populate full vocab list (200 words)
- [ ] Playtest full loop: wake up → explore → fish → bargain → deliver → sleep
- [ ] Balance fishing timing, tawad difficulty
- [ ] Bug fixes
- [ ] Build for web (itch.io), consider mobile export
- [ ] Create itch.io page with screenshots + description

### Launch: itch.io Release
- Free to play
- Web build (playable in browser) + downloadable
- Share with Filipino communities, language learning communities, r/Philippines, cozy gaming communities

---

## 11. Post-V1 Roadmap

### V2: Expand the World (Months 2-3)
- Open Bundok Trail → forest/mountain zone with new NPCs
- Jeepney system replacing trike (multi-destination)
- Cooking mechanic (combine caught fish + market ingredients)
- Basketball mini-game
- Karaoke mini-game (rhythm game with Tagalog lyrics)
- 3-4 new zones
- 100+ additional vocab words

### V3: Community & Stories (Months 3-5)
- Full multi-island map (Luzon, Visayas, Mindanao)
- Fiesta/seasonal events
- Deeper NPC relationships (friendship levels unlock stories)
- Player housing customization (decorate your bahay kubo)
- Side stories / narrative arcs per region
- Mobile release (Android first, iOS later)

### V4: AI Integration (Month 5+)
- AI-powered NPC conversations (Anthropic API)
- Talk freely in Tagalog with any NPC
- AI evaluates your Tagalog and responds naturally
- Dynamic story generation based on player choices
- Voice input for Tagalog pronunciation practice

---

## 12. Naming & Branding

**Title:** PASYAL (means "journey" or "travel")
**Tagline:** "Tara, magpasyal tayo." (Come, let's go for a stroll.) (Come, let's explore the Philippines.)
**Logo concept:** The word PASYAL in pixel font, with the A replaced by a coconut palm, sitting on a pixel wave. Warm sunset gradient behind it.

---

## 13. References & Resources

### Godot Learning
- Godot official docs: https://docs.godotengine.org
- HeartBeast's Godot pixel RPG tutorial (YouTube)
- GDQuest Godot tutorials
- Brackeys Godot series

### Pixel Art Learning
- Pixel Pete (YouTube) — excellent tileset tutorials
- Brandon James Greer (YouTube) — character sprite tutorials
- Lospec.com — palette database, pixel art tools
- AdamCYounis (YouTube) — environment pixel art

### Filipino Folklore & Culture Reference
- "The Aswang Phenomenon" (documentary)
- "Philippine Mythology" by Jocano
- r/Philippines for cultural accuracy checking
- Tagalog.com for vocabulary verification

### Music Tools
- BeepBox (free, browser-based chiptune composer)
- FamiTracker (NES-style music)
- LMMS (free DAW for more complex tracks)

---

*This is a living document. Update as development progresses.*
*Pasyal — Tara na!*
