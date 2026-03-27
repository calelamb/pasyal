import { motion } from 'framer-motion';
import { siteContent } from '../data/content';

export default function Hero() {
  const { title, subtitle, subtitleEn, cta, ctaSecondary } = siteContent.hero;

  return (
    <section className="relative min-h-screen bg-nes-cream overflow-hidden flex flex-col">
      {/* Top yellow stripe */}
      <div className="bg-nes-yellow h-2 w-full flex-shrink-0" />

      {/* Content */}
      <div className="flex-1 flex flex-col items-center justify-center px-4 pt-20 pb-8 relative z-10">
        {/* Blue accent bar */}
        <motion.div
          initial={{ scaleX: 0 }}
          animate={{ scaleX: 1 }}
          transition={{ duration: 0.5, delay: 0.2 }}
          className="bg-nes-blue h-1 w-24 mb-4"
        />

        {/* Title */}
        <motion.h1
          initial={{ y: -50, opacity: 0 }}
          animate={{ y: 0, opacity: 1 }}
          transition={{ duration: 0.6, ease: 'easeOut' }}
          className="text-7xl md:text-9xl font-black text-nes-dark tracking-tight text-center"
          style={{ textShadow: '4px 4px 0px #0038A8' }}
        >
          {title}
        </motion.h1>

        {/* Subtitle - Tagalog */}
        <motion.p
          initial={{ opacity: 0, y: 15 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.5, duration: 0.5 }}
          className="text-lg text-nes-dark/70 max-w-xl mx-auto text-center mt-6"
        >
          {subtitle}
        </motion.p>

        {/* Subtitle - English */}
        <motion.p
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 0.7, duration: 0.5 }}
          className="text-sm text-nes-dark/40 max-w-xl mx-auto text-center mt-2 italic"
        >
          {subtitleEn}
        </motion.p>

        {/* CTA Buttons */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.9, duration: 0.4 }}
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

      {/* CSS Pixel Art Tropical Island Scene */}
      <div className="relative w-full h-[300px] flex-shrink-0 overflow-hidden">
        {/* Sky */}
        <div
          className="absolute inset-0"
          style={{
            background: 'linear-gradient(180deg, #87CEEB 0%, #b8e4f9 50%, #FFFDF0 80%)',
          }}
        />

        {/* Sun */}
        <div
          className="absolute w-16 h-16 bg-nes-yellow nes-border"
          style={{
            top: '20px',
            right: '12%',
            boxShadow: '4px 4px 0px #E8A838',
          }}
        />

        {/* Clouds */}
        <div className="absolute" style={{ top: '30px', left: '10%' }}>
          <div className="bg-white nes-border w-20 h-6" />
          <div className="bg-white nes-border w-12 h-6 -mt-4 ml-4" />
        </div>
        <div className="absolute" style={{ top: '50px', left: '55%' }}>
          <div className="bg-white nes-border w-16 h-5" />
          <div className="bg-white nes-border w-10 h-5 -mt-3 ml-3" />
        </div>

        {/* Island / Mountain */}
        <div
          className="absolute bottom-[80px] left-1/2 -translate-x-1/2"
          style={{
            width: 0,
            height: 0,
            borderLeft: '160px solid transparent',
            borderRight: '160px solid transparent',
            borderBottom: '120px solid #4CAF50',
          }}
        />
        {/* Mountain peak accent */}
        <div
          className="absolute bottom-[160px] left-1/2 -translate-x-1/2"
          style={{
            width: 0,
            height: 0,
            borderLeft: '40px solid transparent',
            borderRight: '40px solid transparent',
            borderBottom: '30px solid #66BB6A',
          }}
        />

        {/* Palm tree trunk */}
        <div
          className="absolute bg-[#8B5E3C]"
          style={{
            bottom: '100px',
            left: 'calc(50% - 100px)',
            width: '12px',
            height: '80px',
            transform: 'rotate(-8deg)',
            border: '2px solid #0a0a0a',
          }}
        />
        {/* Palm tree top */}
        <div
          className="absolute"
          style={{
            bottom: '170px',
            left: 'calc(50% - 140px)',
          }}
        >
          <div className="bg-[#2E7D32] w-16 h-5" style={{ border: '2px solid #0a0a0a', transform: 'rotate(-15deg)' }} />
          <div className="bg-[#388E3C] w-14 h-5 -mt-3 ml-4" style={{ border: '2px solid #0a0a0a', transform: 'rotate(10deg)' }} />
          <div className="bg-[#43A047] w-12 h-4 -mt-3 ml-1" style={{ border: '2px solid #0a0a0a', transform: 'rotate(-5deg)' }} />
        </div>

        {/* Second palm tree */}
        <div
          className="absolute bg-[#8B5E3C]"
          style={{
            bottom: '90px',
            right: 'calc(50% - 80px)',
            width: '10px',
            height: '60px',
            transform: 'rotate(5deg)',
            border: '2px solid #0a0a0a',
          }}
        />
        <div
          className="absolute"
          style={{
            bottom: '142px',
            right: 'calc(50% - 110px)',
          }}
        >
          <div className="bg-[#2E7D32] w-12 h-4" style={{ border: '2px solid #0a0a0a', transform: 'rotate(12deg)' }} />
          <div className="bg-[#388E3C] w-10 h-4 -mt-2 ml-2" style={{ border: '2px solid #0a0a0a', transform: 'rotate(-8deg)' }} />
        </div>

        {/* Beach / Sand */}
        <div
          className="absolute bottom-[40px] left-0 right-0 h-[50px] bg-[#E8D5B7]"
          style={{ border: '4px solid #0a0a0a', borderBottom: 'none' }}
        />

        {/* Ocean */}
        <div
          className="absolute bottom-0 left-0 right-0 h-[48px] bg-nes-blue"
          style={{ borderTop: '4px solid #0a0a0a' }}
        />
        {/* Wave accents */}
        <div className="absolute bottom-[20px] left-[10%] w-12 h-2 bg-[#1565C0]" style={{ border: '2px solid #0a0a0a' }} />
        <div className="absolute bottom-[28px] left-[30%] w-16 h-2 bg-[#1565C0]" style={{ border: '2px solid #0a0a0a' }} />
        <div className="absolute bottom-[16px] left-[55%] w-14 h-2 bg-[#1565C0]" style={{ border: '2px solid #0a0a0a' }} />
        <div className="absolute bottom-[24px] left-[75%] w-10 h-2 bg-[#1565C0]" style={{ border: '2px solid #0a0a0a' }} />
      </div>
    </section>
  );
}
