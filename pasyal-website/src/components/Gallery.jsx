import { useState } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { useInView } from 'react-intersection-observer';
import { Maximize2 } from 'lucide-react';
import { siteContent, sceneArt } from '../data/content';
import PixelArt from './PixelArt';

export default function Gallery() {
  const { scenes } = siteContent.gallery;
  const [titleRef, titleInView] = useInView({ triggerOnce: true, threshold: 0.3 });

  return (
    <section id="mga-tanawin" className="py-20 px-4 bg-white">
      <div className="max-w-5xl mx-auto">
        {/* Section Title */}
        <div ref={titleRef} className="text-center mb-12">
          <motion.h2
            className="font-pixel text-lg text-flag-blue-500"
            initial={{ opacity: 0, y: 20 }}
            animate={titleInView ? { opacity: 1, y: 0 } : {}}
            transition={{ duration: 0.6 }}
          >
            Mga Tanawin
          </motion.h2>
          <motion.p
            className="text-gray-400 text-sm mt-1"
            initial={{ opacity: 0 }}
            animate={titleInView ? { opacity: 1 } : {}}
            transition={{ delay: 0.3 }}
          >
            Game Scenes
          </motion.p>
        </div>

        {/* Scene Grid */}
        <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
          {scenes.map((scene, index) => (
            <SceneCard
              key={scene.label || index}
              scene={scene}
              artGrid={sceneArt[index]}
              index={index}
            />
          ))}
        </div>
      </div>
    </section>
  );
}

function SceneCard({ scene, artGrid, index }) {
  const [ref, inView] = useInView({ triggerOnce: true, threshold: 0.15 });
  const [hovered, setHovered] = useState(false);

  return (
    <motion.div
      ref={ref}
      className="bg-gray-50 rounded-lg overflow-hidden shadow-pixel hover:shadow-pixel-lg transition-all cursor-pointer"
      initial={{ opacity: 0, scale: 0.95 }}
      animate={inView ? { opacity: 1, scale: 1 } : {}}
      transition={{ duration: 0.5, delay: index * 0.1 }}
      whileHover={{ scale: 1.02 }}
      onMouseEnter={() => setHovered(true)}
      onMouseLeave={() => setHovered(false)}
    >
      {/* Pixel Art Display */}
      <div className="bg-gray-100 p-4 flex items-center justify-center">
        {artGrid ? (
          <PixelArt grid={artGrid} cellSize={8} />
        ) : (
          <div className="w-[192px] h-[128px] bg-gray-200 rounded flex items-center justify-center">
            <span className="font-pixel text-xs text-gray-400">No art</span>
          </div>
        )}
      </div>

      {/* Label */}
      <div className="py-3 px-4 flex items-center justify-center gap-2">
        <p className="font-pixel text-xs text-flag-blue-500 text-center">
          {scene.label}
        </p>
        {hovered && (
          <motion.span
            initial={{ opacity: 0, scale: 0.5 }}
            animate={{ opacity: 1, scale: 1 }}
            className="text-flag-blue-400"
          >
            <Maximize2 size={12} />
          </motion.span>
        )}
      </div>
    </motion.div>
  );
}
