import { motion } from 'framer-motion';
import { Star } from 'lucide-react';
import { siteContent, heroScene } from '../data/content';
import PixelArt from './PixelArt';

const stars = [
  { top: '12%', left: '15%', size: 16, delay: 0 },
  { top: '8%',  left: '78%', size: 20, delay: 0.4 },
  { top: '25%', left: '88%', size: 12, delay: 0.8 },
  { top: '18%', left: '45%', size: 14, delay: 1.2 },
  { top: '35%', left: '10%', size: 10, delay: 0.6 },
];

export default function Hero() {
  const { title, tagline, subtitle, cta } = siteContent.hero;

  return (
    <section className="relative min-h-screen flex items-center justify-center overflow-hidden bg-gradient-to-br from-flag-blue-900 via-flag-blue-700 to-flag-blue-500">
      {/* Decorative gold stars */}
      {stars.map((s, i) => (
        <Star
          key={i}
          size={s.size}
          className="absolute text-flag-gold-500 fill-flag-gold-500 animate-twinkle"
          style={{
            top: s.top,
            left: s.left,
            animationDelay: `${s.delay}s`,
          }}
        />
      ))}

      {/* Main content */}
      <div className="relative z-10 text-center px-4 pb-32">
        {/* Title */}
        <motion.h1
          initial={{ y: -50, opacity: 0 }}
          animate={{ y: 0, opacity: 1 }}
          transition={{ duration: 0.7, ease: 'easeOut' }}
          className="font-pixel text-5xl md:text-7xl lg:text-8xl text-white mb-4 drop-shadow-lg"
        >
          {title}
        </motion.h1>

        {/* Tagline */}
        <motion.p
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 0.5, duration: 0.6 }}
          className="font-pixel text-xs md:text-sm text-flag-gold-500 mb-6"
        >
          {tagline}
        </motion.p>

        {/* Subtitle */}
        <motion.p
          initial={{ opacity: 0, y: 15 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.8, duration: 0.6 }}
          className="text-white/80 text-sm md:text-base max-w-lg mx-auto mb-10 leading-relaxed"
        >
          {subtitle}
        </motion.p>

        {/* CTA buttons */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 1.1, duration: 0.5 }}
          className="flex items-center justify-center gap-4 flex-wrap"
        >
          <motion.a
            href={cta.primary.href}
            whileHover={{ scale: 1.05 }}
            whileTap={{ scale: 0.97 }}
            className="bg-flag-red-500 hover:bg-flag-red-600 text-white px-6 py-3 font-pixel text-xs shadow-pixel transition-colors duration-200 rounded"
          >
            {cta.primary.label}
          </motion.a>

          <motion.a
            href={cta.secondary.href}
            whileHover={{ scale: 1.05 }}
            whileTap={{ scale: 0.97 }}
            className="border-2 border-white text-white hover:bg-white hover:text-flag-blue-500 px-6 py-3 font-pixel text-xs transition-colors duration-200 rounded"
          >
            {cta.secondary.label}
          </motion.a>
        </motion.div>
      </div>

      {/* Pixel art scene */}
      <div className="absolute bottom-0 left-1/2 -translate-x-1/2 animate-parallax-drift pointer-events-none">
        {/* Mobile: cellSize 4, Desktop: cellSize 6 */}
        <div className="block md:hidden">
          <PixelArt grid={heroScene} cellSize={4} />
        </div>
        <div className="hidden md:block">
          <PixelArt grid={heroScene} cellSize={6} />
        </div>
      </div>
    </section>
  );
}
