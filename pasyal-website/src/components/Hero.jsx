import { motion } from 'framer-motion';
import { siteContent } from '../data/content';

export default function Hero() {
  const { title, subtitle, subtitleEn, cta, ctaSecondary } = siteContent.hero;

  return (
    <section className="relative min-h-screen bg-nes-cream overflow-hidden flex flex-col">
      {/* Top yellow stripe */}
      <div className="bg-nes-yellow h-2 w-full flex-shrink-0" />

      {/* Content - centered above the scene */}
      <div className="flex-1 flex flex-col items-center justify-center px-4 pt-20 pb-8 relative z-10">
        {/* Blue accent bar */}
        <motion.div
          initial={{ scaleX: 0 }}
          animate={{ scaleX: 1 }}
          transition={{ duration: 0.5, delay: 0.2 }}
          className="bg-nes-blue h-1 w-24 mb-4"
        />

        {/* Title with blue text shadow */}
        <motion.h1
          initial={{ y: -50, opacity: 0 }}
          animate={{ y: 0, opacity: 1 }}
          transition={{ duration: 0.6 }}
          className="text-7xl md:text-9xl font-black text-nes-dark tracking-tight text-center"
          style={{ textShadow: '4px 4px 0px #0038A8' }}
        >
          {title}
        </motion.h1>

        {/* Subtitles */}
        <motion.p
          initial={{ opacity: 0, y: 15 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.5 }}
          className="text-lg text-nes-dark/70 max-w-xl mx-auto text-center mt-6"
        >
          {subtitle}
        </motion.p>
        <motion.p
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 0.7 }}
          className="text-sm text-nes-dark/40 max-w-xl mx-auto text-center mt-2 italic"
        >
          {subtitleEn}
        </motion.p>

        {/* CTA Buttons */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.9 }}
          className="flex items-center justify-center gap-4 flex-wrap mt-10"
        >
          <a
            href={cta.href}
            className="bg-nes-red text-white px-6 py-3 nes-border shadow-nes font-bold uppercase text-sm hover:translate-x-[2px] hover:translate-y-[2px] hover:shadow-none transition-all duration-150"
          >
            {cta.label}
          </a>
          <a
            href={ctaSecondary.href}
            className="bg-transparent text-nes-dark px-6 py-3 nes-border font-bold uppercase text-sm hover:bg-nes-dark hover:text-nes-cream transition-all duration-150"
          >
            {ctaSecondary.label}
          </a>
        </motion.div>
      </div>

      {/* SVG PIXEL ART SCENE — Filipino Coastal Sunset */}
      <motion.div
        initial={{ opacity: 0, y: 30 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ delay: 0.5, duration: 0.8 }}
        className="w-full flex-shrink-0"
        style={{ borderTop: '4px solid #0a0a0a' }}
      >
        <svg
          viewBox="0 0 1200 400"
          className="w-full h-auto block"
          xmlns="http://www.w3.org/2000/svg"
          style={{ imageRendering: 'pixelated' }}
          shapeRendering="crispEdges"
        >
          <defs>
            {/* Sky gradient — sunset orange to deep blue */}
            <linearGradient id="skyGrad" x1="0" y1="0" x2="0" y2="1">
              <stop offset="0%" stopColor="#0038A8" />
              <stop offset="50%" stopColor="#FF8C42" />
              <stop offset="80%" stopColor="#FFB74D" />
              <stop offset="100%" stopColor="#FCD116" />
            </linearGradient>
            {/* Water gradient — shallow to deep */}
            <linearGradient id="waterGrad" x1="0" y1="0" x2="0" y2="1">
              <stop offset="0%" stopColor="#87CEEB" />
              <stop offset="100%" stopColor="#0038A8" />
            </linearGradient>
          </defs>

          {/* ===== LAYER 1: SKY ===== */}
          <rect x="0" y="0" width="1200" height="280" fill="url(#skyGrad)" />

          {/* Sun — half-setting at horizon with 8 rectangular rays */}
          <circle cx="600" cy="200" r="40" fill="#FCD116" />
          {/* Sun rays — 8 directions */}
          <rect x="595" y="140" width="10" height="24" fill="#FCD116" />
          <rect x="595" y="216" width="10" height="24" fill="#FCD116" />
          <rect x="536" y="195" width="24" height="10" fill="#FCD116" />
          <rect x="640" y="195" width="24" height="10" fill="#FCD116" />
          {/* Diagonal rays */}
          <rect x="554" y="158" width="16" height="8" fill="#FCD116" transform="rotate(-45 562 162)" />
          <rect x="630" y="158" width="16" height="8" fill="#FCD116" transform="rotate(45 638 162)" />
          <rect x="554" y="228" width="16" height="8" fill="#FCD116" transform="rotate(45 562 232)" />
          <rect x="630" y="228" width="16" height="8" fill="#FCD116" transform="rotate(-45 638 232)" />

          {/* ===== LAYER 2: DISTANT MOUNTAINS — Mayon-like volcanic peak ===== */}
          {/* Main Mayon volcano — stepped pixel pyramid */}
          <rect x="130" y="190" width="200" height="10" fill="#2D1B4E" />
          <rect x="150" y="180" width="160" height="10" fill="#2D1B4E" />
          <rect x="170" y="170" width="120" height="10" fill="#2D1B4E" />
          <rect x="190" y="160" width="80" height="10" fill="#2D1B4E" />
          <rect x="205" y="150" width="50" height="10" fill="#2D1B4E" />
          <rect x="218" y="140" width="24" height="10" fill="#2D1B4E" />
          <rect x="225" y="130" width="10" height="10" fill="#2D1B4E" />
          {/* Secondary mountain range */}
          <rect x="0" y="195" width="160" height="5" fill="#2D1B4E" />
          <rect x="20" y="190" width="110" height="5" fill="#2D1B4E" />
          <rect x="40" y="185" width="70" height="5" fill="#2D1B4E" />
          <rect x="55" y="180" width="40" height="5" fill="#2D1B4E" />
          {/* Far right mountain */}
          <rect x="900" y="190" width="300" height="10" fill="#2D1B4E" />
          <rect x="940" y="180" width="220" height="10" fill="#2D1B4E" />
          <rect x="980" y="170" width="140" height="10" fill="#2D1B4E" />
          <rect x="1010" y="160" width="80" height="10" fill="#2D1B4E" />
          <rect x="1030" y="152" width="40" height="8" fill="#2D1B4E" />

          {/* ===== LAYER 3: GREEN HILLS WITH RICE TERRACES ===== */}
          {/* Main hill left */}
          <rect x="0" y="210" width="500" height="20" fill="#4CAF50" />
          <rect x="30" y="200" width="400" height="10" fill="#4CAF50" />
          <rect x="80" y="195" width="300" height="5" fill="#388E3C" />
          {/* Rice terrace steps carved into left hill */}
          <rect x="100" y="200" width="60" height="4" fill="#66BB6A" />
          <rect x="110" y="204" width="60" height="4" fill="#81C784" />
          <rect x="120" y="208" width="60" height="4" fill="#66BB6A" />
          <rect x="130" y="212" width="60" height="4" fill="#81C784" />
          <rect x="140" y="216" width="60" height="4" fill="#66BB6A" />
          {/* Main hill right */}
          <rect x="700" y="210" width="500" height="20" fill="#4CAF50" />
          <rect x="750" y="200" width="400" height="10" fill="#388E3C" />
          <rect x="800" y="195" width="300" height="5" fill="#4CAF50" />
          {/* Rice terrace steps on right hill */}
          <rect x="850" y="200" width="50" height="4" fill="#66BB6A" />
          <rect x="858" y="204" width="50" height="4" fill="#81C784" />
          <rect x="866" y="208" width="50" height="4" fill="#66BB6A" />
          <rect x="874" y="212" width="50" height="4" fill="#81C784" />

          {/* Small church silhouette on right hill */}
          <rect x="1020" y="192" width="20" height="18" fill="#5C3D2E" />
          <rect x="1025" y="184" width="10" height="8" fill="#5C3D2E" />
          <rect x="1028" y="178" width="4" height="6" fill="#5C3D2E" />
          {/* Cross on church */}
          <rect x="1029" y="174" width="2" height="6" fill="#5C3D2E" />
          <rect x="1027" y="176" width="6" height="2" fill="#5C3D2E" />

          {/* ===== LAYER 4: MID-GROUND — Road, Jeepney, Bahay Kubo, Palms ===== */}
          {/* Brown road */}
          <rect x="0" y="230" width="1200" height="24" fill="#8B5E3C" />
          <rect x="0" y="228" width="1200" height="2" fill="#5C3D2E" />
          {/* Road line markings */}
          <rect x="100" y="240" width="40" height="4" fill="#E8D5B7" />
          <rect x="200" y="240" width="40" height="4" fill="#E8D5B7" />
          <rect x="300" y="240" width="40" height="4" fill="#E8D5B7" />
          <rect x="400" y="240" width="40" height="4" fill="#E8D5B7" />
          <rect x="700" y="240" width="40" height="4" fill="#E8D5B7" />
          <rect x="800" y="240" width="40" height="4" fill="#E8D5B7" />
          <rect x="900" y="240" width="40" height="4" fill="#E8D5B7" />
          <rect x="1050" y="240" width="40" height="4" fill="#E8D5B7" />

          {/* Jeepney — on the road */}
          {/* Body */}
          <rect x="480" y="218" width="80" height="30" fill="#CE1126" />
          {/* Roof */}
          <rect x="476" y="214" width="88" height="6" fill="#FCD116" />
          {/* Blue trim stripe */}
          <rect x="480" y="230" width="80" height="4" fill="#0038A8" />
          {/* Yellow trim lines */}
          <rect x="480" y="224" width="80" height="2" fill="#FCD116" />
          {/* Windows */}
          <rect x="486" y="220" width="8" height="8" fill="#87CEEB" />
          <rect x="498" y="220" width="8" height="8" fill="#87CEEB" />
          <rect x="510" y="220" width="8" height="8" fill="#87CEEB" />
          <rect x="522" y="220" width="8" height="8" fill="#87CEEB" />
          <rect x="534" y="220" width="8" height="8" fill="#87CEEB" />
          <rect x="546" y="220" width="8" height="8" fill="#87CEEB" />
          {/* Wheels */}
          <rect x="490" y="246" width="12" height="12" fill="#0a0a0a" />
          <rect x="540" y="246" width="12" height="12" fill="#0a0a0a" />
          {/* Wheel hubs */}
          <rect x="494" y="250" width="4" height="4" fill="#888" />
          <rect x="544" y="250" width="4" height="4" fill="#888" />
          {/* Front bumper */}
          <rect x="556" y="236" width="8" height="12" fill="#FCD116" />
          {/* Headlight */}
          <rect x="558" y="238" width="4" height="4" fill="#FFE082" />

          {/* Bahay Kubo (Nipa Hut) — left of road */}
          {/* Stilts */}
          <rect x="52" y="230" width="4" height="28" fill="#5C3D2E" />
          <rect x="84" y="230" width="4" height="28" fill="#5C3D2E" />
          <rect x="116" y="230" width="4" height="28" fill="#5C3D2E" />
          {/* Platform */}
          <rect x="46" y="228" width="80" height="6" fill="#8B5E3C" />
          {/* Hut walls */}
          <rect x="50" y="210" width="72" height="20" fill="#FFB74D" />
          {/* Door */}
          <rect x="78" y="216" width="10" height="14" fill="#5C3D2E" />
          {/* Window */}
          <rect x="58" y="216" width="8" height="8" fill="#5C3D2E" />
          {/* Triangular roof (stepped pixel triangle) */}
          <rect x="42" y="208" width="88" height="4" fill="#8B5E3C" />
          <rect x="50" y="204" width="72" height="4" fill="#8B5E3C" />
          <rect x="58" y="200" width="56" height="4" fill="#8B5E3C" />
          <rect x="66" y="196" width="40" height="4" fill="#8B5E3C" />
          <rect x="74" y="192" width="24" height="4" fill="#8B5E3C" />
          <rect x="82" y="188" width="8" height="4" fill="#8B5E3C" />
          {/* Smoke puffs from bahay kubo */}
          <rect x="88" y="180" width="6" height="6" fill="#ccc" opacity="0.6" />
          <rect x="92" y="172" width="8" height="6" fill="#ccc" opacity="0.4" />
          <rect x="96" y="164" width="6" height="6" fill="#ccc" opacity="0.25" />

          {/* Coconut Palm 1 — near bahay kubo */}
          <rect x="150" y="180" width="6" height="50" fill="#5C3D2E" />
          {/* Fronds — blocky rectangles */}
          <rect x="132" y="172" width="20" height="6" fill="#388E3C" />
          <rect x="154" y="170" width="22" height="6" fill="#4CAF50" />
          <rect x="140" y="166" width="26" height="6" fill="#388E3C" />
          <rect x="148" y="160" width="18" height="6" fill="#4CAF50" />
          {/* Coconuts */}
          <rect x="150" y="174" width="4" height="4" fill="#5C3D2E" />
          <rect x="155" y="176" width="4" height="4" fill="#5C3D2E" />

          {/* Coconut Palm 2 — right side */}
          <rect x="780" y="186" width="6" height="44" fill="#5C3D2E" />
          <rect x="762" y="178" width="20" height="6" fill="#388E3C" />
          <rect x="784" y="176" width="22" height="6" fill="#4CAF50" />
          <rect x="770" y="172" width="24" height="6" fill="#388E3C" />
          <rect x="778" y="166" width="16" height="6" fill="#4CAF50" />
          <rect x="780" y="180" width="4" height="4" fill="#5C3D2E" />

          {/* Coconut Palm 3 — far right */}
          <rect x="1120" y="190" width="5" height="38" fill="#5C3D2E" />
          <rect x="1104" y="184" width="18" height="5" fill="#388E3C" />
          <rect x="1124" y="182" width="18" height="5" fill="#4CAF50" />
          <rect x="1110" y="178" width="22" height="5" fill="#388E3C" />

          {/* Philippine flag on a pole — near jeepney */}
          <rect x="460" y="196" width="3" height="52" fill="#5C3D2E" />
          {/* Flag — blue top, red bottom, white triangle, sun */}
          <rect x="463" y="196" width="20" height="7" fill="#0038A8" />
          <rect x="463" y="203" width="20" height="7" fill="#CE1126" />
          {/* White triangle on flag */}
          <polygon points="463,196 463,210 472,203" fill="white" />
          {/* Tiny sun on flag */}
          <rect x="465" y="202" width="2" height="2" fill="#FCD116" />

          {/* ===== LAYER 5: BEACH & BANGKA ===== */}
          {/* Sandy beach */}
          <rect x="0" y="254" width="1200" height="30" fill="#E8D5B7" />

          {/* Bangka (outrigger canoe) on beach edge */}
          {/* Main hull */}
          <rect x="650" y="268" width="60" height="8" fill="#8B5E3C" />
          <rect x="646" y="272" width="4" height="4" fill="#8B5E3C" />
          <rect x="710" y="272" width="4" height="4" fill="#8B5E3C" />
          {/* Outrigger arms */}
          <rect x="660" y="264" width="4" height="16" fill="#5C3D2E" />
          <rect x="696" y="264" width="4" height="16" fill="#5C3D2E" />
          {/* Outrigger floats */}
          <rect x="654" y="280" width="16" height="4" fill="#8B5E3C" />
          <rect x="690" y="280" width="16" height="4" fill="#8B5E3C" />
          {/* Sail mast */}
          <rect x="678" y="248" width="3" height="24" fill="#5C3D2E" />
          {/* Small sail */}
          <polygon points="681,250 681,266 696,266" fill="white" />

          {/* Tsinelas (slippers) on the beach */}
          <rect x="620" y="274" width="6" height="10" fill="#CE1126" />
          <rect x="628" y="274" width="6" height="10" fill="#0038A8" />

          {/* ===== LAYER 6: OCEAN & WAVES ===== */}
          {/* Shallow water */}
          <rect x="0" y="284" width="1200" height="20" fill="#87CEEB" />
          {/* Deep ocean */}
          <rect x="0" y="304" width="1200" height="96" fill="url(#waterGrad)" />

          {/* Pixelated wave patterns — white caps */}
          <rect x="60" y="290" width="20" height="4" fill="white" opacity="0.6" />
          <rect x="180" y="296" width="16" height="4" fill="white" opacity="0.5" />
          <rect x="340" y="288" width="24" height="4" fill="white" opacity="0.6" />
          <rect x="500" y="294" width="18" height="4" fill="white" opacity="0.5" />
          <rect x="720" y="286" width="20" height="4" fill="white" opacity="0.6" />
          <rect x="860" y="292" width="22" height="4" fill="white" opacity="0.5" />
          <rect x="1020" y="288" width="16" height="4" fill="white" opacity="0.6" />
          <rect x="1140" y="296" width="20" height="4" fill="white" opacity="0.5" />

          {/* Deeper wave rows */}
          <rect x="40" y="320" width="30" height="4" fill="#1565C0" opacity="0.4" />
          <rect x="200" y="326" width="24" height="4" fill="#1565C0" opacity="0.3" />
          <rect x="400" y="318" width="28" height="4" fill="#1565C0" opacity="0.4" />
          <rect x="580" y="324" width="20" height="4" fill="#1565C0" opacity="0.3" />
          <rect x="760" y="316" width="26" height="4" fill="#1565C0" opacity="0.4" />
          <rect x="950" y="322" width="22" height="4" fill="#1565C0" opacity="0.3" />
          <rect x="1100" y="318" width="28" height="4" fill="#1565C0" opacity="0.4" />

          {/* Far ocean wave row */}
          <rect x="100" y="350" width="24" height="4" fill="#002B80" opacity="0.3" />
          <rect x="300" y="356" width="20" height="4" fill="#002B80" opacity="0.25" />
          <rect x="520" y="348" width="26" height="4" fill="#002B80" opacity="0.3" />
          <rect x="700" y="354" width="22" height="4" fill="#002B80" opacity="0.25" />
          <rect x="900" y="350" width="24" height="4" fill="#002B80" opacity="0.3" />
          <rect x="1080" y="356" width="20" height="4" fill="#002B80" opacity="0.25" />

          {/* ===== OPTIONAL DETAILS ===== */}
          {/* Rooster on a fence post — near bahay kubo */}
          {/* Fence post */}
          <rect x="170" y="226" width="4" height="12" fill="#5C3D2E" />
          {/* Rooster body */}
          <rect x="168" y="218" width="8" height="8" fill="#CE1126" />
          {/* Rooster head */}
          <rect x="174" y="214" width="6" height="6" fill="#CE1126" />
          {/* Comb */}
          <rect x="176" y="212" width="4" height="3" fill="#FF8C42" />
          {/* Beak */}
          <rect x="180" y="216" width="3" height="2" fill="#FCD116" />
          {/* Tail feathers */}
          <rect x="166" y="214" width="4" height="6" fill="#2D1B4E" />
          {/* Eye */}
          <rect x="176" y="216" width="2" height="2" fill="#0a0a0a" />
        </svg>
      </motion.div>
    </section>
  );
}
