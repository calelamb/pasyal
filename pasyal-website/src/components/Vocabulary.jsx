import { useState } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { useInView } from 'react-intersection-observer';
import {
  Home,
  Store,
  Waves,
  Building,
  UtensilsCrossed,
  Users,
  Hash,
  Cloud,
  Map,
  Heart,
} from 'lucide-react';
import { siteContent } from '../data/content';

const iconMap = {
  Home,
  Store,
  Waves,
  Building,
  UtensilsCrossed,
  Users,
  Hash,
  Cloud,
  Map,
  Heart,
};

export default function Vocabulary() {
  const { categories, untranslatable } = siteContent.vocabulary;
  const [activeCategory, setActiveCategory] = useState(0);

  const [titleRef, titleInView] = useInView({ triggerOnce: true, threshold: 0.3 });
  const [cardsRef, cardsInView] = useInView({ triggerOnce: true, threshold: 0.1 });

  const active = categories[activeCategory];

  return (
    <section id="salitaan" className="py-20 px-4 bg-flag-blue-900 text-white">
      <div className="max-w-4xl mx-auto">
        {/* Section Title */}
        <div ref={titleRef} className="text-center mb-8">
          <motion.h2
            className="font-pixel text-lg text-flag-gold-500"
            initial={{ opacity: 0, y: 20 }}
            animate={titleInView ? { opacity: 1, y: 0 } : {}}
            transition={{ duration: 0.6 }}
          >
            Salitaan
          </motion.h2>
          <motion.p
            className="text-white/50 text-sm mt-1"
            initial={{ opacity: 0 }}
            animate={titleInView ? { opacity: 1 } : {}}
            transition={{ delay: 0.3 }}
          >
            Vocabulary
          </motion.p>
        </div>

        {/* Counter Badge */}
        <div className="flex justify-center mb-8">
          <motion.span
            className="bg-flag-gold-500 text-flag-blue-900 font-pixel text-xs px-4 py-1 rounded-full"
            initial={{ scale: 0.8, opacity: 0 }}
            whileInView={{ scale: 1, opacity: 1 }}
            viewport={{ once: true }}
            transition={{ type: 'spring', stiffness: 300, damping: 20 }}
          >
            200+ salita
          </motion.span>
        </div>

        {/* Category Tabs */}
        <div className="flex overflow-x-auto gap-2 pb-2 scrollbar-hide mb-6">
          {categories.map((cat, i) => {
            const isActive = i === activeCategory;
            return (
              <button
                key={cat.name}
                onClick={() => setActiveCategory(i)}
                className={`flex items-center gap-1.5 px-3 py-1.5 rounded-full text-sm whitespace-nowrap transition-all ${
                  isActive
                    ? 'bg-flag-gold-500 text-flag-blue-900 font-semibold'
                    : 'bg-white/10 text-white/70 hover:bg-white/20'
                }`}
              >
                {cat.name}
              </button>
            );
          })}
        </div>

        {/* Tab Content — Word Cards */}
        <div ref={cardsRef} className="min-h-[200px]">
          <AnimatePresence mode="wait">
            <motion.div
              key={activeCategory}
              initial={{ opacity: 0, y: 12 }}
              animate={{ opacity: 1, y: 0 }}
              exit={{ opacity: 0, y: -12 }}
              transition={{ duration: 0.25 }}
              className="grid grid-cols-2 sm:grid-cols-3 gap-3"
            >
              {active?.samples?.slice(0, 6).map((word, j) => (
                <motion.div
                  key={word}
                  className="bg-white/10 p-3 rounded-lg hover:bg-white/15 transition-colors"
                  initial={{ opacity: 0, y: 8 }}
                  animate={{ opacity: 1, y: 0 }}
                  transition={{ delay: j * 0.05 }}
                >
                  <p className="font-pixel text-xs text-flag-gold-500">{word}</p>
                  <p className="text-xs text-white/60 mt-1">{active.nameEn}</p>
                </motion.div>
              ))}
            </motion.div>
          </AnimatePresence>
        </div>

        {/* Untranslatable Section */}
        <div className="pixel-divider border-white/20 my-12" />

        <div className="text-center mb-6">
          <p className="font-pixel text-xs text-flag-gold-400">
            Mga Salitang Hindi Maisalin
          </p>
          <p className="text-xs text-white/40 mt-1">Untranslatable Concepts</p>
        </div>

        <div className="flex overflow-x-auto gap-4 pb-4 scrollbar-hide">
          {(untranslatable?.words || []).map((item, i) => (
            <motion.div
              key={item.word}
              className="min-w-[200px] bg-gradient-to-br from-flag-red-500/20 to-flag-blue-500/20 border border-white/10 p-4 rounded-lg flex-shrink-0"
              initial={{ opacity: 0, x: 30 }}
              whileInView={{ opacity: 1, x: 0 }}
              viewport={{ once: true }}
              transition={{ delay: i * 0.1 }}
            >
              <p className="font-pixel text-sm text-flag-gold-500">{item.word}</p>
              <p className="text-sm text-white/80 mt-2">{item.meaning}</p>
            </motion.div>
          ))}
        </div>
      </div>
    </section>
  );
}
