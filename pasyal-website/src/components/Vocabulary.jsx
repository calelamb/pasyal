import { useState } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { siteContent } from '../data/content';

export default function Vocabulary() {
  const { sectionTitle, sectionTitleAccent, sectionTitleEn, totalWords, categories, untranslatable } =
    siteContent.vocabulary;
  const [activeCategory, setActiveCategory] = useState(0);
  const active = categories[activeCategory];

  return (
    <section id="salitaan" className="bg-nes-dark text-white py-20 px-4">
      <div className="max-w-4xl mx-auto">
        {/* Title */}
        <div className="text-center mb-8">
          <h2 className="text-3xl font-black">
            <span className="text-white">{sectionTitle} </span>
            <span className="text-nes-yellow">{sectionTitleAccent}</span>
          </h2>
          <p className="text-white/50 text-sm mt-2">{sectionTitleEn}</p>
        </div>

        {/* Word count badge */}
        <div className="flex justify-center mb-10">
          <span className="bg-nes-yellow text-nes-dark nes-border font-bold text-xs px-4 py-1">
            {totalWords}+ salita
          </span>
        </div>

        {/* Category tabs */}
        <div className="flex overflow-x-auto gap-2 pb-3 mb-8 scrollbar-hide">
          {categories.map((cat, i) => {
            const isActive = i === activeCategory;
            return (
              <button
                key={cat.name}
                onClick={() => setActiveCategory(i)}
                className={`px-4 py-2 font-bold uppercase text-xs whitespace-nowrap transition-all duration-150 ${
                  isActive
                    ? 'bg-nes-yellow text-nes-dark nes-border'
                    : 'bg-nes-dark nes-border-yellow text-nes-yellow hover:bg-nes-yellow/10'
                }`}
              >
                {cat.name}
              </button>
            );
          })}
        </div>

        {/* Word cards */}
        <div className="min-h-[240px]">
          <AnimatePresence mode="wait">
            <motion.div
              key={activeCategory}
              initial={{ opacity: 0, y: 16 }}
              animate={{ opacity: 1, y: 0 }}
              exit={{ opacity: 0, y: -16 }}
              transition={{ duration: 0.25 }}
              className="grid grid-cols-2 sm:grid-cols-3 gap-3"
            >
              {active?.samples?.slice(0, 6).map((word, j) => (
                <motion.div
                  key={word}
                  className="bg-white/10 nes-border p-3"
                  initial={{ opacity: 0, y: 12 }}
                  animate={{ opacity: 1, y: 0 }}
                  transition={{ delay: j * 0.06, duration: 0.25 }}
                >
                  <p className="text-nes-yellow font-bold text-sm uppercase">
                    {word}
                  </p>
                  <p className="text-white/50 text-xs mt-1">{active.nameEn}</p>
                </motion.div>
              ))}
            </motion.div>
          </AnimatePresence>
        </div>

        {/* Divider */}
        <div className="border-t-4 border-nes-yellow/20 my-16" />

        {/* Untranslatable words */}
        <div className="text-center mb-8">
          <h3 className="pixel-text text-sm text-nes-yellow">
            Mga Salitang Walang Katumbas
          </h3>
          <p className="text-xs text-white/40 mt-1">Untranslatable Words</p>
        </div>

        <div className="flex overflow-x-auto gap-4 pb-4 -mx-4 px-4 scrollbar-hide">
          {untranslatable.map((item, i) => (
            <motion.div
              key={item.word}
              className="min-w-[220px] nes-border-red bg-white/5 p-5 flex-shrink-0"
              initial={{ opacity: 0, x: 40 }}
              whileInView={{ opacity: 1, x: 0 }}
              viewport={{ once: true }}
              transition={{ delay: i * 0.08, duration: 0.4 }}
            >
              <p className="text-nes-yellow font-bold text-sm uppercase">
                {item.word}
              </p>
              <p className="text-white/80 text-sm mt-2 leading-relaxed">
                {item.meaning}
              </p>
            </motion.div>
          ))}
        </div>
      </div>
    </section>
  );
}
