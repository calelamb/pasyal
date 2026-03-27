import { useState } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { useInView } from 'react-intersection-observer';
import { Github, Gamepad2, Code, Grid3x3, Database } from 'lucide-react';
import { siteContent } from '../data/content';

const techIconMap = {
  Gamepad2,
  Code,
  Grid3x3,
  Database,
};

const techStack = [
  { icon: 'Gamepad2', label: 'Phaser 3' },
  { icon: 'Code', label: 'JavaScript' },
  { icon: 'Grid3x3', label: 'Pixel Art' },
  { icon: 'Database', label: 'Tiled Maps' },
];

export default function Credits() {
  const [cardRef, cardInView] = useInView({ triggerOnce: true, threshold: 0.2 });
  const [techRef, techInView] = useInView({ triggerOnce: true, threshold: 0.3 });

  const credits = siteContent.credits || {};

  return (
    <section id="kredits" className="py-20 px-4 bg-flag-blue-800 text-white">
      <div className="max-w-4xl mx-auto">
        {/* Section Title */}
        <div className="text-center mb-10">
          <motion.h2
            className="font-pixel text-lg text-flag-gold-500"
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.6 }}
          >
            Sino ang Gumawa
          </motion.h2>
          <motion.p
            className="text-white/50 text-sm mt-1"
            initial={{ opacity: 0 }}
            whileInView={{ opacity: 1 }}
            viewport={{ once: true }}
            transition={{ delay: 0.3 }}
          >
            Credits
          </motion.p>
        </div>

        {/* Creator Card */}
        <motion.div
          ref={cardRef}
          className="max-w-lg mx-auto bg-white/10 rounded-lg p-8"
          initial={{ opacity: 0, x: -40 }}
          animate={cardInView ? { opacity: 1, x: 0 } : {}}
          transition={{ duration: 0.6, ease: 'easeOut' }}
        >
          {/* Avatar */}
          <div className="w-16 h-16 rounded-full bg-flag-gold-500 text-flag-blue-900 flex items-center justify-center mx-auto">
            <span className="font-pixel text-xl">CL</span>
          </div>

          {/* Name & Role */}
          <p className="font-pixel text-sm text-center mt-4">
            {credits.name || 'Cale Lamb'}
          </p>
          <p className="text-white/70 text-sm text-center">
            {credits.role || 'Developer & Designer'}
          </p>

          {/* Bio — Tagalog */}
          <p className="text-white/80 text-sm text-center mt-3">
            {credits.bioTagalog ||
              'Isang laro na ginawa upang ipakita ang kagandahan ng wikang Filipino at kulturang Pilipino sa buong mundo.'}
          </p>

          {/* Bio — English */}
          <p className="text-white/50 text-xs text-center mt-1">
            {credits.bioEnglish ||
              'A game crafted to share the beauty of the Filipino language and Philippine culture with the world.'}
          </p>

          {/* GitHub Button */}
          <div className="mt-4 flex justify-center">
            <a
              href={credits.github || 'https://github.com/calelamb'}
              target="_blank"
              rel="noopener noreferrer"
              className="bg-white/10 hover:bg-white/20 px-4 py-2 rounded-lg text-sm flex items-center gap-2 transition-colors"
            >
              <Github size={16} />
              GitHub
            </a>
          </div>
        </motion.div>

        {/* Tech Stack */}
        <div ref={techRef} className="flex flex-wrap justify-center gap-3 mt-8">
          {techStack.map((tech, i) => {
            const TechIcon = techIconMap[tech.icon] || Code;
            return (
              <motion.div
                key={tech.label}
                className="bg-white/10 px-3 py-1.5 rounded-full flex items-center gap-2 text-sm"
                initial={{ opacity: 0, x: 30 }}
                animate={techInView ? { opacity: 1, x: 0 } : {}}
                transition={{ delay: 0.1 * i, duration: 0.4 }}
              >
                <TechIcon size={14} className="text-flag-gold-500" />
                <span className="text-white/80">{tech.label}</span>
              </motion.div>
            );
          })}
        </div>
      </div>
    </section>
  );
}
