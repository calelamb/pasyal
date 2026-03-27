// ─────────────────────────────────────────────
// PASYAL — Website Content & Pixel Art Data
// ─────────────────────────────────────────────

// Color palette shortcuts (used in grids)
const _ = null;        // transparent
const SKY = '#87CEEB'; // sky blue
const OCN = '#3A7CA5'; // ocean
const SND = '#E8D5B7'; // sand
const GRN = '#6B8F3C'; // green / grass
const DGN = '#4A6B28'; // dark green
const WOD = '#5C3D2E'; // wood / brown
const DWD = '#3D2B1F'; // dark wood
const GLD = '#E8A838'; // gold
const RED = '#D4382C'; // red
const YLW = '#F5C518'; // yellow
const TEA = '#2AA198'; // teal
const PNK = '#D94F8C'; // pink
const TER = '#C4684A'; // terracotta
const DRK = '#2D1B4E'; // dark purple
const SKN = '#DEB887'; // skin
const DSK = '#8B6914'; // dark skin
const FBL = '#0038A8'; // flag blue
const FRD = '#CE1126'; // flag red
const FGL = '#FCD116'; // flag gold
const WHT = '#FFFFFF'; // white
const GRY = '#808080'; // gray
const DGR = '#555555'; // dark gray
const LGR = '#AAAAAA'; // light gray
const BLK = '#222222'; // near-black
const LSK = '#A8D8EA'; // light sky
const ORG = '#E8751A'; // orange
const LGN = '#A8C256'; // light green
const TAN = '#C4A876'; // tan / dry sand
const DSN = '#D4C4A0'; // dry sand light

// ─────────────────────────────────────────────
// PIXEL ART GRIDS
// ─────────────────────────────────────────────

// Hero scene: 32x12 — panoramic coastal village
export const heroScene = [
  // Row 0 — sky
  [SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,FGL,FGL,SKY,SKY,SKY],
  // Row 1 — sky + sun
  [SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,FGL,GLD,GLD,FGL,SKY,SKY],
  // Row 2 — sky + sun + palm canopy
  [SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,GRN,DGN,GRN,SKY,SKY,SKY,SKY,FGL,GLD,GLD,FGL,SKY,SKY],
  // Row 3 — roof of hut + palm canopy
  [SKY,SKY,SKY,SND,TAN,TAN,TAN,SND,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,DGN,GRN,GRN,GRN,DGN,SKY,SKY,SKY,SKY,FGL,FGL,SKY,SKY,SKY],
  // Row 4 — hut roof lower + palm trunk
  [SKY,SKY,TAN,SND,TAN,TAN,TAN,SND,TAN,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,GRN,DGN,GRN,LGN,GRN,DGN,GRN,SKY,SKY,SKY,SKY,SKY,SKY,OCN,OCN],
  // Row 5 — hut walls + character + palm trunk + ocean
  [SKY,SKY,_,WOD,WOD,DWD,WOD,WOD,_,SKY,SKY,SKY,SKY,SKY,BLK,BLK,SKY,SKY,SKY,SKY,WOD,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,OCN,OCN,OCN],
  // Row 6 — hut walls + character body + palm trunk + ocean
  [SKY,SKY,_,WOD,WOD,DWD,WOD,WOD,_,SKY,SKY,SKY,SKY,SKN,FBL,FBL,SKN,SKY,SKY,SKY,WOD,SKY,SKY,SKY,SKY,SKY,SKY,SKY,OCN,OCN,OCN,OCN],
  // Row 7 — hut base + character legs + ground starts
  [GRN,GRN,GRN,WOD,WOD,DWD,WOD,WOD,GRN,GRN,GRN,GRN,GRN,SKN,DGR,DGR,SKN,GRN,GRN,GRN,WOD,GRN,GRN,GRN,GRN,SND,SND,SND,OCN,OCN,OCN,OCN],
  // Row 8 — ground
  [GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,SND,SND,SND,SND,SND,OCN,OCN,OCN],
  // Row 9 — ground + sand + water
  [GRN,DGN,GRN,GRN,DGN,GRN,GRN,GRN,DGN,GRN,GRN,GRN,GRN,GRN,DGN,GRN,GRN,GRN,GRN,DGN,GRN,GRN,SND,SND,SND,TAN,SND,SND,OCN,OCN,OCN,OCN],
  // Row 10 — sand + water
  [SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,OCN,OCN,OCN,OCN,OCN],
  // Row 11 — sand + water
  [SND,TAN,SND,SND,TAN,SND,SND,SND,TAN,SND,SND,SND,SND,TAN,SND,SND,SND,SND,TAN,SND,SND,SND,TAN,SND,SND,SND,OCN,OCN,OCN,OCN,OCN,OCN],
];

// Zone pixel arts: 16x8 each
export const zonePixelArts = {
  'bahay-kubo': [
    [SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,FGL,FGL,SKY,SKY],
    [SKY,SKY,SKY,SKY,SKY,GRN,DGN,GRN,SKY,SKY,SKY,SKY,GLD,GLD,SKY,SKY],
    [SKY,SKY,SKY,SND,TAN,TAN,TAN,TAN,SND,SKY,SKY,SKY,SKY,SKY,SKY,SKY],
    [SKY,SKY,TAN,SND,TAN,TAN,TAN,TAN,SND,TAN,SKY,SKY,SKY,SKY,SKY,SKY],
    [SKY,SKY,_,WOD,WOD,DWD,WOD,WOD,WOD,_,SKY,SKY,SKY,SKY,SKY,SKY],
    [SKY,SKY,_,WOD,WOD,DWD,WOD,WOD,WOD,_,SKY,SKY,SKY,SKY,SKY,SKY],
    [GRN,GRN,GRN,WOD,WOD,WOD,WOD,WOD,WOD,GRN,GRN,GRN,GRN,GRN,GRN,GRN],
    [GRN,DGN,GRN,GRN,DGN,GRN,GRN,GRN,GRN,GRN,DGN,GRN,GRN,DGN,GRN,GRN],
  ],

  'sentro': [
    [SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY],
    [SKY,SKY,RED,RED,RED,SKY,SKY,TER,TER,TER,SKY,SKY,FBL,FBL,SKY,SKY],
    [SKY,SKY,RED,WHT,RED,SKY,SKY,TER,WHT,TER,SKY,SKY,FBL,WHT,FBL,SKY],
    [SKY,SKY,RED,DWD,RED,SKY,SKY,TER,DWD,TER,SKY,SKY,FBL,DWD,FBL,SKY],
    [SKY,SKY,RED,RED,RED,SKY,SKY,TER,TER,TER,SKY,SKY,FBL,FBL,FBL,SKY],
    [GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY],
    [DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR],
    [GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY],
  ],

  'dalampasigan': [
    [SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY],
    [SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,GRN,DGN,GRN,SKY,SKY,SKY,SKY],
    [SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,DGN,GRN,GRN,GRN,DGN,SKY,SKY,SKY],
    [SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,WOD,SKY,SKY,SKY,SKY,SKY],
    [OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,SKY,SKY,WOD,SKY,SKY,SKY,SKY,SKY],
    [OCN,OCN,WOD,WOD,WOD,OCN,OCN,OCN,SND,SND,SND,SND,SND,SND,SND,SND],
    [SND,SND,SND,WOD,SND,SND,SND,SND,SND,SND,TAN,SND,SND,TAN,SND,SND],
    [SND,TAN,SND,SND,SND,TAN,SND,SND,SND,TAN,SND,SND,TAN,SND,SND,SND],
  ],

  'trike-stop': [
    [SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY],
    [SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY],
    [SKY,SKY,SKY,SKY,SKY,YLW,YLW,YLW,YLW,YLW,SKY,SKY,SKY,SKY,SKY,SKY],
    [SKY,SKY,SKY,SKY,YLW,YLW,GLD,GLD,YLW,YLW,YLW,SKY,SKY,SKY,SKY,SKY],
    [SKY,SKY,SKY,SKY,YLW,DGR,DGR,DGR,DGR,YLW,YLW,SKY,SKY,SKY,SKY,SKY],
    [GRY,GRY,GRY,GRY,GRY,BLK,GRY,GRY,BLK,GRY,BLK,GRY,GRY,GRY,GRY,GRY],
    [DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR],
    [TAN,TAN,TAN,TAN,TAN,TAN,TAN,TAN,TAN,TAN,TAN,TAN,TAN,TAN,TAN,TAN],
  ],

  'bundok-trail': [
    [SKY,SKY,SKY,SKY,SKY,SKY,DGN,DGN,DGN,DGN,SKY,SKY,SKY,SKY,SKY,SKY],
    [SKY,SKY,SKY,SKY,DGN,DGN,GRN,GRN,GRN,GRN,DGN,DGN,SKY,SKY,SKY,SKY],
    [SKY,SKY,DGN,DGN,GRN,GRN,GRN,LGN,GRN,GRN,GRN,GRN,DGN,DGN,SKY,SKY],
    [SKY,DGN,GRN,GRN,GRN,GRN,LGN,GRN,GRN,LGN,GRN,GRN,GRN,GRN,DGN,SKY],
    [DGN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,DGN],
    [GRN,GRN,GRN,GRN,TAN,WOD,TAN,GRN,GRN,GRN,GRN,TAN,WOD,GRN,GRN,GRN],
    [GRN,DGN,GRN,TAN,WOD,TAN,TAN,TAN,GRN,GRN,TAN,WOD,TAN,TAN,GRN,GRN],
    [GRN,GRN,TAN,WOD,TAN,GRN,GRN,TAN,TAN,TAN,WOD,TAN,GRN,GRN,DGN,GRN],
  ],
};

// NPC avatars: 8x8 each
export const npcAvatars = {
  'Aling Nena': [
    [_,_,LGR,LGR,LGR,LGR,_,_],
    [_,LGR,LGR,LGR,LGR,LGR,LGR,_],
    [_,SKN,SKN,SKN,SKN,SKN,SKN,_],
    [_,SKN,BLK,SKN,SKN,BLK,SKN,_],
    [_,_,SKN,SKN,SKN,SKN,_,_],
    [_,WHT,WHT,WHT,WHT,WHT,WHT,_],
    [_,WHT,TER,WHT,WHT,TER,WHT,_],
    [_,_,WHT,WHT,WHT,WHT,_,_],
  ],

  'Ate Merly': [
    [_,_,BLK,BLK,BLK,BLK,_,_],
    [_,BLK,BLK,BLK,BLK,BLK,BLK,_],
    [_,SKN,SKN,SKN,SKN,SKN,SKN,_],
    [_,SKN,BLK,SKN,SKN,BLK,SKN,_],
    [_,_,SKN,PNK,PNK,SKN,_,_],
    [_,TEA,TEA,TEA,TEA,TEA,TEA,_],
    [_,TEA,TEA,TEA,TEA,TEA,TEA,_],
    [_,_,TEA,TEA,TEA,TEA,_,_],
  ],

  'Kuya Jojo': [
    [_,_,BLK,BLK,BLK,BLK,_,_],
    [_,BLK,BLK,BLK,BLK,BLK,BLK,_],
    [_,DSK,DSK,DSK,DSK,DSK,DSK,_],
    [_,DSK,BLK,DSK,DSK,BLK,DSK,_],
    [_,_,DSK,DSK,DSK,DSK,_,_],
    [_,RED,RED,RED,RED,RED,RED,_],
    [_,RED,RED,RED,RED,RED,RED,_],
    [_,_,RED,RED,RED,RED,_,_],
  ],

  'Kapitan Rody': [
    [_,_,BLK,BLK,BLK,BLK,_,_],
    [_,BLK,BLK,BLK,BLK,BLK,BLK,_],
    [_,SKN,SKN,SKN,SKN,SKN,SKN,_],
    [_,SKN,BLK,SKN,SKN,BLK,SKN,_],
    [_,_,SKN,SKN,SKN,SKN,_,_],
    [_,FBL,FBL,WHT,WHT,FBL,FBL,_],
    [_,FBL,WHT,FBL,FBL,WHT,FBL,_],
    [_,_,FBL,FBL,FBL,FBL,_,_],
  ],

  'Padre Miguel': [
    [_,_,LGR,LGR,LGR,LGR,_,_],
    [_,LGR,LGR,LGR,LGR,LGR,LGR,_],
    [_,SKN,SKN,SKN,SKN,SKN,SKN,_],
    [_,SKN,BLK,SKN,SKN,BLK,SKN,_],
    [_,_,SKN,SKN,SKN,SKN,_,_],
    [_,WOD,WOD,WOD,WOD,WOD,WOD,_],
    [_,WOD,WHT,WOD,WOD,WHT,WOD,_],
    [_,_,WOD,WOD,WOD,WOD,_,_],
  ],

  'Tatay Andoy': [
    [_,TAN,TAN,TAN,TAN,TAN,TAN,_],
    [_,TAN,BLK,BLK,BLK,BLK,TAN,_],
    [_,DSK,DSK,DSK,DSK,DSK,DSK,_],
    [_,DSK,BLK,DSK,DSK,BLK,DSK,_],
    [_,_,DSK,DSK,DSK,DSK,_,_],
    [_,OCN,OCN,OCN,OCN,OCN,OCN,_],
    [_,OCN,OCN,OCN,OCN,OCN,OCN,_],
    [_,_,OCN,OCN,OCN,OCN,_,_],
  ],

  'Manong Boy': [
    [_,YLW,YLW,YLW,YLW,YLW,YLW,_],
    [_,YLW,BLK,BLK,BLK,BLK,YLW,_],
    [_,DSK,DSK,DSK,DSK,DSK,DSK,_],
    [_,DSK,BLK,DSK,DSK,BLK,DSK,_],
    [_,_,DSK,DSK,DSK,DSK,_,_],
    [_,YLW,YLW,YLW,YLW,YLW,YLW,_],
    [_,GLD,YLW,YLW,YLW,YLW,GLD,_],
    [_,_,YLW,YLW,YLW,YLW,_,_],
  ],
};

// Gallery scenes: 24x16 each
export const sceneArt = [
  // Scene 1: Bahay Kubo — Umaga (Morning)
  {
    label: 'Bahay Kubo — Umaga',
    labelEn: 'Nipa Hut — Morning',
    grid: [
      [LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,GLD,GLD,FGL,LSK],
      [LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,GLD,GLD,GLD,GLD,FGL],
      [LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,GRN,DGN,GRN,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,GLD,GLD,LSK,LSK],
      [LSK,LSK,LSK,LSK,LSK,LSK,LSK,DGN,GRN,GLD,GRN,DGN,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK],
      [LSK,LSK,LSK,SND,TAN,TAN,TAN,TAN,TAN,SND,WOD,LSK,LSK,DGN,GRN,DGN,LSK,LSK,LSK,LSK,LSK,LSK,LSK,LSK],
      [LSK,LSK,TAN,SND,TAN,TAN,TAN,TAN,TAN,SND,WOD,TAN,DGN,GRN,GLD,GRN,DGN,LSK,LSK,LSK,LSK,LSK,LSK,LSK],
      [LSK,LSK,_,WOD,WOD,DWD,WOD,WOD,WOD,_,WOD,_,_,_,WOD,_,_,LSK,LSK,LSK,LSK,LSK,LSK,LSK],
      [LSK,LSK,_,WOD,WOD,DWD,WOD,WOD,WOD,_,_,_,_,_,WOD,_,_,LSK,LSK,LSK,LSK,LSK,LSK,LSK],
      [GRN,GRN,GRN,WOD,WOD,DWD,WOD,WOD,WOD,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN],
      [GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN],
      [GRN,DGN,GRN,GRN,DGN,GRN,WHT,WHT,GRN,GRN,GRN,DGN,GRN,GRN,DGN,GRN,WHT,GRN,GRN,DGN,GRN,GRN,GRN,GRN],
      [GRN,GRN,GRN,WHT,GRN,GRN,GRN,WHT,WHT,GRN,GRN,GRN,GRN,WHT,GRN,GRN,GRN,GRN,GRN,GRN,GRN,DGN,GRN,GRN],
      [GRN,GRN,GRN,WHT,GRN,RED,RED,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,RED,RED,GRN,GRN,GRN,GRN,GRN],
      [GRN,GRN,GRN,GRN,GRN,RED,GRN,GRN,GRN,GRN,DGN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,RED,GRN,GRN,GRN,DGN,GRN],
      [GRN,DGN,GRN,GRN,GRN,GRN,GRN,DGN,GRN,GRN,GRN,GRN,DGN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,DGN,GRN,GRN,GRN],
      [GRN,GRN,GRN,GRN,DGN,GRN,GRN,GRN,GRN,DGN,GRN,GRN,GRN,GRN,GRN,DGN,GRN,GRN,GRN,GRN,GRN,GRN,GRN,DGN],
    ],
  },

  // Scene 2: Palengke — Tawad (Market — Bargaining)
  {
    label: 'Palengke — Tawad',
    labelEn: 'Market — Bargaining',
    grid: [
      [SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY,SKY],
      [SKY,RED,RED,RED,RED,RED,SKY,SKY,GLD,GLD,GLD,GLD,GLD,SKY,SKY,TEA,TEA,TEA,TEA,TEA,SKY,SKY,SKY,SKY],
      [SKY,RED,RED,RED,RED,RED,SKY,SKY,GLD,GLD,GLD,GLD,GLD,SKY,SKY,TEA,TEA,TEA,TEA,TEA,SKY,SKY,SKY,SKY],
      [SKY,WOD,WOD,WOD,WOD,WOD,SKY,SKY,WOD,WOD,WOD,WOD,WOD,SKY,SKY,WOD,WOD,WOD,WOD,WOD,SKY,SKY,SKY,SKY],
      [SKY,WOD,RED,PNK,ORG,WOD,SKY,SKY,WOD,YLW,GRN,YLW,WOD,SKY,SKY,WOD,OCN,OCN,LGR,WOD,SKY,SKY,SKY,SKY],
      [SKY,WOD,PNK,RED,PNK,WOD,SKY,SKY,WOD,GRN,YLW,GRN,WOD,SKY,SKY,WOD,LGR,OCN,OCN,WOD,SKY,SKY,SKY,SKY],
      [GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY],
      [GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY,GRY],
      [GRY,GRY,_,_,SKN,SKN,_,_,GRY,GRY,DSK,DSK,_,_,GRY,GRY,_,_,_,GRY,GRY,SKN,SKN,GRY],
      [GRY,GRY,_,SKN,FBL,FBL,SKN,_,GRY,GRY,DSK,RED,RED,DSK,GRY,GRY,_,_,_,GRY,SKN,TEA,TEA,SKN],
      [GRY,GRY,_,_,DGR,DGR,_,_,GRY,GRY,_,DGR,DGR,_,GRY,GRY,_,_,_,GRY,_,DGR,DGR,_],
      [DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY],
      [GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR],
      [GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY],
      [DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY],
      [GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR,GRY,GRY,DGR],
    ],
  },

  // Scene 3: Dalampasigan — Hapon (Beach — Afternoon/Sunset)
  {
    label: 'Dalampasigan — Hapon',
    labelEn: 'Beach — Sunset',
    grid: [
      [ORG,GLD,ORG,GLD,ORG,GLD,ORG,GLD,FGL,FGL,GLD,GLD,GLD,FGL,FGL,GLD,ORG,GLD,ORG,GLD,ORG,GLD,ORG,GLD],
      [GLD,ORG,GLD,ORG,GLD,ORG,GLD,FGL,GLD,GLD,GLD,GLD,GLD,GLD,GLD,FGL,GLD,ORG,GLD,ORG,GLD,ORG,GLD,ORG],
      [ORG,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,GLD,ORG],
      [TER,TER,ORG,ORG,ORG,ORG,ORG,GLD,ORG,ORG,ORG,ORG,ORG,ORG,ORG,GLD,ORG,ORG,ORG,ORG,ORG,ORG,TER,TER],
      [OCN,OCN,TER,TER,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,TER,TER,OCN,OCN],
      [OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN],
      [OCN,OCN,OCN,OCN,OCN,OCN,WOD,WOD,WOD,WOD,WOD,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN],
      [OCN,OCN,OCN,OCN,OCN,WOD,WOD,DWD,DWD,DWD,WOD,WOD,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN],
      [OCN,OCN,OCN,OCN,OCN,OCN,WOD,DWD,SKN,DWD,WOD,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN],
      [OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN],
      [OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN,OCN],
      [SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND,SND],
      [SND,TAN,SND,SND,TAN,SND,SND,SND,SND,TAN,SND,SND,SND,TAN,SND,SND,SND,TAN,SND,SND,SND,TAN,SND,SND],
      [SND,SND,SND,SND,SND,SND,TAN,SND,SND,SND,SND,SND,TAN,SND,SND,SND,SND,SND,SND,TAN,SND,SND,SND,SND],
      [SND,SND,TAN,SND,SND,SND,SND,SND,TAN,SND,SND,SND,SND,SND,SND,TAN,SND,SND,SND,SND,SND,SND,TAN,SND],
      [TAN,SND,SND,SND,TAN,SND,SND,SND,SND,SND,TAN,SND,SND,SND,TAN,SND,SND,SND,TAN,SND,SND,SND,SND,TAN],
    ],
  },

  // Scene 4: Bonfire — Gabi (Bonfire — Night)
  {
    label: 'Bonfire — Gabi',
    labelEn: 'Bonfire — Night',
    grid: [
      [DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,WHT,DRK,DRK,DRK,DRK,DRK,DRK,WHT,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK],
      [DRK,DRK,DRK,WHT,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,WHT,DRK,DRK,DRK,DRK,DRK,WHT,DRK,DRK,DRK,DRK],
      [DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK],
      [DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK],
      [DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,YLW,YLW,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK],
      [DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,YLW,ORG,GLD,YLW,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK],
      [DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,YLW,ORG,RED,ORG,GLD,YLW,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK],
      [DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,ORG,RED,YLW,RED,ORG,ORG,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK],
      [DRK,DRK,DRK,DRK,DRK,DRK,DRK,ORG,RED,ORG,RED,ORG,RED,ORG,ORG,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK],
      [DRK,DRK,DRK,DRK,DRK,DRK,WOD,WOD,WOD,WOD,WOD,WOD,WOD,WOD,WOD,WOD,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK],
      [DRK,DRK,DRK,WOD,WOD,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,WOD,WOD,DRK,DRK,DRK,DRK],
      [DRK,DRK,WOD,WOD,WOD,DRK,DRK,SKN,SKN,DRK,DRK,DRK,DRK,SKN,SKN,DRK,DRK,DRK,WOD,WOD,WOD,DRK,DRK,DRK],
      [DRK,DRK,DRK,DRK,DRK,DRK,SKN,FBL,FBL,SKN,DRK,DRK,SKN,RED,RED,SKN,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK],
      [DRK,DRK,DRK,DRK,DRK,DRK,DRK,DGR,DGR,DRK,DRK,DRK,DRK,DGR,DGR,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK,DRK],
      [GRN,DGN,GRN,GRN,DGN,GRN,GRN,GRN,GRN,DGN,GRN,GRN,GRN,GRN,GRN,DGN,GRN,GRN,DGN,GRN,GRN,GRN,DGN,GRN],
      [GRN,GRN,GRN,DGN,GRN,GRN,DGN,GRN,GRN,GRN,GRN,DGN,GRN,GRN,GRN,GRN,DGN,GRN,GRN,GRN,DGN,GRN,GRN,GRN],
    ],
  },
];


// ─────────────────────────────────────────────
// TEXT CONTENT
// ─────────────────────────────────────────────

export const siteContent = {
  nav: {
    brand: 'PASYAL',
    links: [
      { label: 'Tungkol', href: '#about' },
      { label: 'Mga Lugar', href: '#zones' },
      { label: 'Mga Tao', href: '#characters' },
      { label: 'Paano Laruin', href: '#how-to-play' },
      { label: 'Bokabularyo', href: '#vocabulary' },
      { label: 'Gallerya', href: '#gallery' },
      { label: 'Credits', href: '#credits' },
    ],
  },

  hero: {
    title: 'PASYAL',
    tagline: 'Tara, magpasyal tayo.',
    taglineEn: "Let's go for a stroll.",
    subtitle: 'Isang cozy pixel-art na laro kung saan matutututo ka ng Tagalog sa pamamagitan ng paglalakad, pakikipag-usap, at pamumuhay sa isang maliit na coastal village sa Pilipinas.',
    subtitleEn: 'A cozy pixel-art game where you learn Tagalog through walking, talking, and living in a small Philippine coastal village.',
    cta: 'Tuklasin ang Bayan',
    ctaEn: 'Explore the Town',
  },

  about: {
    sectionTitle: 'Tungkol sa Laro',
    sectionTitleEn: 'About the Game',
    intro: 'Ang PASYAL ay hindi lang isang laro — ito ay paglalakbay. Walang timer, walang scoreboard, walang "game over." Maglakad-lakad ka lang, makipag-usap, at unti-unting matututo ng Tagalog habang nakilala mo ang bayan at ang mga tao rito.',
    introEn: "PASYAL isn't just a game — it's a journey. No timers, no scoreboards, no game over. Just walk around, talk to people, and slowly learn Tagalog as you get to know the town and its people.",
    pillars: [
      {
        title: 'Chill Exploration',
        titleTl: 'Mahinahong Paglalakad',
        description: 'Walang dali-dali. Maglakad-lakad sa sarili mong bilis at tuklasin ang bawat sulok ng bayan.',
        descriptionEn: 'No rush. Walk at your own pace and discover every corner of town.',
        icon: 'Compass',
      },
      {
        title: 'Language Through Living',
        titleTl: 'Wika sa Pamumuhay',
        description: 'Matututo ka ng Tagalog hindi sa textbook, kundi sa pakikipag-usap, pamimili, at pang-araw-araw na buhay.',
        descriptionEn: 'Learn Tagalog not from a textbook, but through conversation, shopping, and daily life.',
        icon: 'MessageCircle',
      },
      {
        title: 'Cultural Authenticity',
        titleTl: 'Tunay na Kultura',
        description: 'Bawat detalye — mula sa pagkain hanggang sa pagbati — ay galing sa tunay na kultura ng Pilipinas.',
        descriptionEn: 'Every detail — from the food to the greetings — comes from real Filipino culture.',
        icon: 'Heart',
      },
      {
        title: 'Warmth & Community',
        titleTl: 'Init ng Samahan',
        description: 'Makikilala mo ang mga tao ng bayan — bawat isa may sariling kwento, ugali, at pagkatao.',
        descriptionEn: "You'll meet the people of town — each with their own story, habits, and personality.",
        icon: 'Users',
      },
    ],
    comparable: {
      label: 'Kung nagustuhan mo ang...',
      labelEn: 'If you liked...',
      games: ['Stardew Valley', 'A Short Hike', 'Unpacking', 'Duolingo'],
      outro: '...baka magustuhan mo rin ang PASYAL.',
      outroEn: '...you might enjoy PASYAL too.',
    },
  },

  zones: {
    sectionTitle: 'Mga Lugar sa Bayan',
    sectionTitleEn: 'Places in Town',
    intro: 'Lima ang pangunahing lugar na maaari mong tuklasin. Bawat isa ay may sariling kulay, tunog, at kwento.',
    introEn: 'Five main areas to explore. Each has its own color, sound, and story.',
    list: [
      {
        id: 'bahay-kubo',
        name: 'Bahay Kubo',
        nameEn: 'Nipa Hut — Your Home',
        description: 'Dito ka nakatira — isang simpleng bahay kubo na puno ng init. Dito mo sinimulan ang araw at dito ka rin bumabalik tuwing gabi.',
        descriptionEn: 'Your home — a simple nipa hut full of warmth. This is where you start each day and return each evening.',
        features: ['Tulog at Gising Cycle', 'Personal Journal', 'Cooking Mini-game'],
        color: 'flag-gold',
      },
      {
        id: 'sentro',
        name: 'Sentro ng Bayan',
        nameEn: 'Town Center',
        description: 'Ang puso ng bayan — may palengke, simbahan, at plaza. Dito magkikita-kita ang lahat at maraming mapapakinggan na kwento.',
        descriptionEn: 'The heart of town — market, church, and plaza. Where everyone gathers and stories flow freely.',
        features: ['Palengke (Market)', 'Simbahan (Church)', 'Tindahan (Shops)'],
        color: 'flag-red',
      },
      {
        id: 'dalampasigan',
        name: 'Dalampasigan',
        nameEn: 'The Beach',
        description: 'Ang magandang dalampasigan — kung saan nangingisda ang mga tao at nagpapalipas ng hapon sa buhanginan.',
        descriptionEn: 'The beautiful coastline — where people fish and spend their afternoons on the sand.',
        features: ['Pangingisda (Fishing)', 'Paglangoy (Swimming)', 'Sunset Viewing'],
        color: 'flag-blue',
      },
      {
        id: 'trike-stop',
        name: 'Trike Stop',
        nameEn: 'Tricycle Terminal',
        description: 'Ang trike stop — sentro ng transportasyon ng bayan. Dito mo mahahanap si Manong Boy at makakasakay ng trike papunta sa iba\'t ibang lugar.',
        descriptionEn: 'The trike stop — the town transport hub. Find Manong Boy here and ride to different places.',
        features: ['Fast Travel', 'Mga Chismis (Gossip)', 'Pamasahe (Fare System)'],
        color: 'flag-gold',
      },
      {
        id: 'bundok-trail',
        name: 'Bundok Trail',
        nameEn: 'Mountain Trail',
        description: 'Ang bundok trail — isang tahimik na landas paakyat sa bundok. Dito makikita ang buong bayan mula sa itaas.',
        descriptionEn: 'The mountain trail — a quiet path up the hill. See the whole town from above.',
        features: ['Nature Walk', 'Mga Halaman (Flora)', 'Mirador (Viewpoint)'],
        color: 'flag-blue',
      },
    ],
  },

  characters: {
    sectionTitle: 'Mga Tao ng Bayan',
    sectionTitleEn: 'People of the Town',
    intro: 'Bawat tao sa bayan ay may sariling kwento. Kilalanin sila — at matuto mula sa kanila.',
    introEn: 'Every person in town has their own story. Get to know them — and learn from them.',
    list: [
      {
        name: 'Aling Nena',
        zone: 'Bahay Kubo',
        role: 'Ang Nanay ng Bayan',
        roleEn: 'The Town Mother',
        personality: 'Matiyaga, mapagmahal, at laging may niluluto. Siya ang unang tao na sasalubong sa iyo.',
        personalityEn: 'Patient, loving, and always cooking. She is the first person to welcome you.',
        quote: '"Kumain ka na ba? Halika, may niluto ako."',
        quoteEn: '"Have you eaten? Come, I cooked something."',
      },
      {
        name: 'Ate Merly',
        zone: 'Sentro ng Bayan',
        role: 'Tindera sa Palengke',
        roleEn: 'Market Vendor',
        personality: 'Masayahin, madaldal, at mahusay makipag-tawaran. Alam niya lahat ng chismis ng bayan.',
        personalityEn: 'Cheerful, talkative, and a skilled bargainer. She knows all the town gossip.',
        quote: '"Uy, suki! Tara, tingnan mo \'to — fresh pa \'yan!"',
        quoteEn: '"Hey, regular! Come, look at this — still fresh!"',
      },
      {
        name: 'Kuya Jojo',
        zone: 'Dalampasigan',
        role: 'Mangingisda',
        roleEn: 'Fisherman',
        personality: 'Tahimik, mabait, at malalim ang iniisip. Magtuturo siya sa iyo kung paano mangisda.',
        personalityEn: 'Quiet, kind, and deep thinker. He will teach you how to fish.',
        quote: '"Ang dagat, parang buhay — kailangan mo lang matutong maghintay."',
        quoteEn: '"The sea, like life — you just need to learn to wait."',
      },
      {
        name: 'Kapitan Rody',
        zone: 'Sentro ng Bayan',
        role: 'Kapitan ng Barangay',
        roleEn: 'Village Captain',
        personality: 'Mahigpit pero patas. Iginagalang ng lahat. Siya ang nagpapanatili ng kaayusan sa bayan.',
        personalityEn: 'Strict but fair. Respected by all. He maintains order in town.',
        quote: '"Ang bayan na nagkakaisa, hindi kailanman matatalo."',
        quoteEn: '"A united town can never be defeated."',
      },
      {
        name: 'Padre Miguel',
        zone: 'Sentro ng Bayan',
        role: 'Pari ng Simbahan',
        roleEn: 'Parish Priest',
        personality: 'Maalalahanin, marunong makinig, at puno ng wisdom. Laging may payo para sa lahat.',
        personalityEn: 'Thoughtful, a good listener, and full of wisdom. Always has advice for everyone.',
        quote: '"Ang paglalakbay ay hindi lang tungkol sa destinasyon, anak."',
        quoteEn: '"The journey isn\'t just about the destination, child."',
      },
      {
        name: 'Tatay Andoy',
        zone: 'Dalampasigan',
        role: 'Matandang Mangingisda',
        roleEn: 'Old Fisherman',
        personality: 'Matatanda na pero masigla pa rin. Puno ng mga kwento tungkol sa bayan noon.',
        personalityEn: 'Old but still spirited. Full of stories about how the town used to be.',
        quote: '"Noong araw, walang kuryente dito — pero masaya kami."',
        quoteEn: '"Back in the day, there was no electricity here — but we were happy."',
      },
      {
        name: 'Manong Boy',
        zone: 'Trike Stop',
        role: 'Trike Driver',
        roleEn: 'Tricycle Driver',
        personality: 'Masayahin, magaling magkwento, at kilala ng buong bayan. Alam niya lahat ng daan at lahat ng tao.',
        personalityEn: 'Cheerful, great storyteller, and known by everyone. He knows every road and every person.',
        quote: '"Saan tayo, boss? Kahit saan, basta may bayad!"',
        quoteEn: '"Where to, boss? Anywhere, as long as you pay!"',
      },
    ],
  },

  howToPlay: {
    sectionTitle: 'Paano Laruin',
    sectionTitleEn: 'How to Play',
    intro: 'Simpleng laro, malalim na karanasan. Narito ang mga pangunahing mechanics ng PASYAL.',
    introEn: 'Simple game, deep experience. Here are the core mechanics of PASYAL.',
    features: [
      {
        title: 'Maglakad-lakad',
        titleEn: 'Walk Around',
        description: 'Gamitin ang arrow keys o WASD para maglakad. Tuklasin ang bayan sa sarili mong bilis.',
        descriptionEn: 'Use arrow keys or WASD to walk. Explore the town at your own pace.',
        icon: 'Footprints',
      },
      {
        title: 'Makipag-usap',
        titleEn: 'Talk to People',
        description: 'Lumapit sa mga tao at pindutin ang E para makipag-usap. Ang dialogue ay Tagalog na may English hints.',
        descriptionEn: 'Approach people and press E to talk. Dialogue is in Tagalog with English hints.',
        icon: 'MessageSquare',
      },
      {
        title: 'Mangisda',
        titleEn: 'Go Fishing',
        description: 'Pumunta sa dalampasigan at matutong mangisda kasama si Kuya Jojo. Timing at pasensya ang susi.',
        descriptionEn: 'Head to the beach and learn to fish with Kuya Jojo. Timing and patience are key.',
        icon: 'Fish',
      },
      {
        title: 'Makipag-tawaran',
        titleEn: 'Bargain at the Market',
        description: 'Sa palengke, matutututo kang tumawad. Piliin ang tamang presyo — pero huwag masyadong mababa!',
        descriptionEn: 'At the market, learn to bargain. Pick the right price — but not too low!',
        icon: 'HandCoins',
      },
      {
        title: 'Isulat sa Journal',
        titleEn: 'Write in Your Journal',
        description: 'Bawat bagong salita at karanasan ay maitatala sa iyong journal. Balikan ito anumang oras.',
        descriptionEn: 'Every new word and experience is recorded in your journal. Review it anytime.',
        icon: 'BookOpen',
      },
      {
        title: 'Oras ng Bayan',
        titleEn: 'Town Time System',
        description: 'Ang bayan ay may sariling oras — umaga, tanghali, hapon, at gabi. Iba-iba ang nangyayari depende sa oras.',
        descriptionEn: 'The town has its own time — morning, noon, afternoon, and night. Different things happen at different times.',
        icon: 'Clock',
      },
    ],
  },

  vocabulary: {
    sectionTitle: 'Bokabularyo',
    sectionTitleEn: 'Vocabulary',
    intro: 'Mahigit 200 salita at parirala ang matututunan mo habang naglalaro. Narito ang ilan sa mga kategorya.',
    introEn: 'Over 200 words and phrases to learn while playing. Here are some of the categories.',
    categories: [
      { name: 'Pagbati', nameEn: 'Greetings', samples: ['Magandang umaga', 'Kamusta ka?', 'Mabuhay', 'Ingat ka'] },
      { name: 'Pagkain', nameEn: 'Food', samples: ['Adobo', 'Sinigang', 'Kanin', 'Ulam'] },
      { name: 'Pamilya', nameEn: 'Family', samples: ['Nanay', 'Tatay', 'Kuya', 'Ate'] },
      { name: 'Bilang', nameEn: 'Numbers', samples: ['Isa', 'Dalawa', 'Tatlo', 'Sampu'] },
      { name: 'Kulay', nameEn: 'Colors', samples: ['Pula', 'Asul', 'Dilaw', 'Berde'] },
      { name: 'Kalikasan', nameEn: 'Nature', samples: ['Dagat', 'Bundok', 'Ilog', 'Langit'] },
      { name: 'Panahon', nameEn: 'Weather/Time', samples: ['Umaga', 'Tanghali', 'Hapon', 'Gabi'] },
      { name: 'Hayop', nameEn: 'Animals', samples: ['Isda', 'Manok', 'Aso', 'Pusa'] },
      { name: 'Emosyon', nameEn: 'Emotions', samples: ['Masaya', 'Malungkot', 'Galit', 'Takot'] },
      { name: 'Palengke', nameEn: 'Market', samples: ['Magkano?', 'Tawad', 'Suki', 'Libre'] },
    ],
    untranslatable: {
      title: 'Mga Salitang Walang Katumbas',
      titleEn: 'Untranslatable Words',
      intro: 'May mga salitang Tagalog na walang eksaktong translation sa English. Narito ang ilan na matututunan mo sa laro.',
      introEn: 'Some Tagalog words have no exact English translation. Here are a few you will learn in the game.',
      words: [
        { word: 'Kilig', meaning: 'Ang pakiramdam ng excitement at butterflies kapag may romantic moment.' },
        { word: 'Gigil', meaning: 'Ang urge na kurutin o yakapin ang isang napakacute na bagay.' },
        { word: 'Tampo', meaning: 'Ang silent treatment o pagdaramdam kapag nasaktan ka ng mahal mo.' },
        { word: 'Lihi', meaning: 'Ang cravings at paniniwala tungkol sa pagbubuntis.' },
        { word: 'Umay', meaning: 'Ang pakiramdam ng pagkasawa sa lasa — lalo na sa matamis o maalat.' },
        { word: 'Tiis-ganda', meaning: 'Ang pagtitiyaga sa sakit para maging maganda — tulad ng high heels.' },
        { word: 'Suki', meaning: 'Ang special relationship between a buyer and a seller — loyalty at tiwala.' },
        { word: 'Pasyal', meaning: 'Ang paglalakad-lakad nang walang partikular na destinasyon — just vibes.' },
        { word: 'Bayanihan', meaning: 'Ang sama-samang pagtulong ng komunidad — literally carrying a house together.' },
        { word: 'Merienda', meaning: 'Hindi snack, hindi meal — ang magical time between lunch at dinner.' },
      ],
    },
  },

  gallery: {
    sectionTitle: 'Gallerya ng Pixel Art',
    sectionTitleEn: 'Pixel Art Gallery',
    intro: 'Mga eksena mula sa laro, rendered sa 320x180 pixel art style.',
    introEn: 'Scenes from the game, rendered in 320x180 pixel art style.',
  },

  credits: {
    sectionTitle: 'Credits',
    title: 'Ginawa ni Cale Lamb',
    titleEn: 'Made by Cale Lamb',
    description: 'Isang solo dev project na pinapangarap maging tulay sa pagkatuto ng wikang Filipino.',
    descriptionEn: 'A solo dev project aspiring to be a bridge for learning the Filipino language.',
    tech: [
      { label: 'Engine', value: 'Godot 4.6' },
      { label: 'Language', value: 'C# / .NET' },
      { label: 'Art Style', value: '320x180 Pixel Art' },
      { label: 'Data', value: 'JSON-driven Content' },
    ],
    links: {
      github: 'https://github.com/calelamb/pasyal',
    },
  },

  footer: {
    copyright: '2026 PASYAL — Cale Lamb',
    tagline: 'Matuto. Maglakad. Magpasyal.',
    taglineEn: 'Learn. Walk. Wander.',
    github: 'https://github.com/calelamb/pasyal',
  },
};
