import { motion } from 'framer-motion';
import { useInView } from 'react-intersection-observer';
import { siteContent, npcAvatars } from '../data/content';
import PixelArt from './PixelArt';

const fadeInUp = {
  hidden: { opacity: 0, y: 30 },
  visible: (i = 0) => ({
    opacity: 1,
    y: 0,
    transition: { delay: i * 0.12, duration: 0.5 },
  }),
};

const stagger = {
  hidden: {},
  visible: { transition: { staggerChildren: 0.1 } },
};

export default function Characters() {
  const [ref, inView] = useInView({ triggerOnce: true, threshold: 0.05 });

  return (
    <section id="mga-tauhan" className="py-20 px-4 bg-white">
      <div className="text-center mb-14">
        <h2 className="font-pixel text-lg text-flag-blue-500 tracking-wide">
          Mga Tauhan
        </h2>
        <p className="text-gray-400 text-sm mt-1">The Characters</p>
      </div>

      <motion.div
        ref={ref}
        variants={stagger}
        initial="hidden"
        animate={inView ? 'visible' : 'hidden'}
        className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6 max-w-6xl mx-auto"
      >
        {(siteContent.characters.list || []).map((npc, i) => (
          <motion.div
            key={npc.name}
            custom={i}
            variants={{
              hidden: { opacity: 0, scale: 0.9, y: 20 },
              visible: (i = 0) => ({
                opacity: 1,
                scale: 1,
                y: 0,
                transition: { delay: i * 0.1, duration: 0.45, ease: 'easeOut' },
              }),
            }}
            whileHover={{ y: -4, transition: { duration: 0.2 } }}
            className="bg-white border border-gray-100 rounded-lg overflow-hidden shadow-pixel hover:shadow-pixel-lg transition-all duration-300"
          >
            {/* Colored Accent Bar */}
            <div
              className="h-1.5"
              style={{ backgroundColor: npc.color || '#FCD116' }}
            />

            {/* Avatar */}
            <div className="flex justify-center mt-5 mb-2">
              <div className="rounded-full p-2 bg-gray-50">
                {npcAvatars[npc.name] && (
                  <PixelArt grid={npcAvatars[npc.name]} cellSize={5} />
                )}
              </div>
            </div>

            {/* Name */}
            <h3 className="font-pixel text-xs text-center tracking-wide mt-3">
              {npc.name}
            </h3>

            {/* Zone Badge */}
            {npc.zone && (
              <div className="flex justify-center mt-2">
                <span className="bg-gray-100 text-gray-600 text-xs px-2 py-0.5 rounded-full">
                  {npc.zone}
                </span>
              </div>
            )}

            {/* Role */}
            <p className="text-sm font-medium text-center text-gray-700 mt-2 px-4">
              {npc.role}
            </p>

            {/* Personality */}
            {npc.personality && (
              <p className="text-xs text-gray-500 italic text-center mt-1 px-4">
                {npc.personality}
              </p>
            )}

            {/* Quote Box */}
            {npc.quote && (
              <div className="relative mx-4 mb-4 mt-4">
                {/* Speech Triangle */}
                <div
                  className="absolute -top-2 left-6 w-3 h-3 rotate-45 bg-gray-50"
                />
                <div className="bg-gray-50 p-3 rounded-lg relative">
                  <div
                    className="border-l-[3px] pl-3"
                    style={{ borderColor: npc.color || '#FCD116' }}
                  >
                    <p className="text-sm font-medium text-gray-700 leading-relaxed">
                      &ldquo;{npc.quote}&rdquo;
                    </p>
                    {npc.quoteEn && (
                      <p className="text-xs text-gray-400 mt-1 leading-relaxed">
                        &ldquo;{npc.quoteEn}&rdquo;
                      </p>
                    )}
                  </div>
                </div>
              </div>
            )}
          </motion.div>
        ))}
      </motion.div>
    </section>
  );
}
