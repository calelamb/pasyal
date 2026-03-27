import { motion } from 'framer-motion';
import { useInView } from 'react-intersection-observer';
import { siteContent } from '../data/content';

const containerVariants = {
  hidden: {},
  visible: {
    transition: { staggerChildren: 0.08 },
  },
};

const cardVariants = {
  hidden: { opacity: 0, scale: 0.92, y: 24 },
  visible: (i = 0) => ({
    opacity: 1,
    scale: 1,
    y: 0,
    transition: {
      delay: i * 0.08,
      duration: 0.4,
      ease: 'easeOut',
    },
  }),
};

export default function Characters() {
  const [ref, inView] = useInView({ triggerOnce: true, threshold: 0.05 });

  return (
    <section id="mga-tauhan" className="bg-nes-cream py-20 px-4">
      <div className="max-w-6xl mx-auto">
        {/* Title */}
        <div className="text-center mb-14">
          <h2 className="pixel-text text-2xl text-nes-dark mb-2">
            {siteContent.characters.sectionTitle}
          </h2>
          <div className="bg-nes-blue h-1 w-20 mx-auto mb-2" />
          <p className="text-sm text-nes-dark/50">
            {siteContent.characters.sectionTitleEn}
          </p>
        </div>

        {/* Character grid */}
        <motion.div
          ref={ref}
          variants={containerVariants}
          initial="hidden"
          animate={inView ? 'visible' : 'hidden'}
          className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6"
        >
          {siteContent.characters.list.map((npc, i) => (
            <motion.div
              key={npc.name}
              custom={i}
              variants={cardVariants}
              whileHover={{
                y: -2,
                boxShadow: 'none',
                transition: { duration: 0.15 },
              }}
              className="bg-white nes-border shadow-nes overflow-hidden"
            >
              {/* Top color bar */}
              <div className="h-2" style={{ backgroundColor: npc.color }} />

              {/* Emoji avatar */}
              <div className="flex justify-center mt-4">
                <div className="bg-nes-cream w-14 h-14 nes-border flex items-center justify-center text-3xl">
                  {npc.emoji}
                </div>
              </div>

              {/* Name */}
              <h3 className="pixel-text text-sm text-center text-nes-dark mt-3">
                {npc.name}
              </h3>

              {/* Zone + Role */}
              <p className="text-xs text-nes-dark/60 text-center mt-1">
                {npc.zone} &middot; {npc.role}
              </p>

              {/* Quote box */}
              <div className="bg-nes-cream nes-border p-3 mx-3 mb-3 mt-3">
                <p className="font-bold text-sm text-nes-dark text-center">
                  {npc.quote}
                </p>
                <p className="text-xs italic text-nes-dark/50 text-center mt-1">
                  {npc.quoteEn}
                </p>
              </div>
            </motion.div>
          ))}
        </motion.div>
      </div>
    </section>
  );
}
