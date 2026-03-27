import { motion } from 'framer-motion';
import { useInView } from 'react-intersection-observer';
import { Gamepad2, MessageSquare, Fish, HandCoins, BookOpen, Clock, Footprints } from 'lucide-react';
import { siteContent } from '../data/content';

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

const iconMap = {
  Gamepad2,
  MessageSquare,
  Fish,
  HandCoins,
  BookOpen,
  Clock,
  Footprints,
};

export default function HowToPlay() {
  const [ref, inView] = useInView({ triggerOnce: true, threshold: 0.05 });

  return (
    <section id="paano" className="py-20 px-4 bg-gradient-to-br from-flag-gold-50 to-amber-50">
      <div className="text-center mb-14">
        <h2 className="font-pixel text-lg text-flag-blue-500 tracking-wide">
          Paano Maglaro
        </h2>
        <p className="text-gray-400 text-sm mt-1">How to Play</p>
      </div>

      <motion.div
        ref={ref}
        variants={stagger}
        initial="hidden"
        animate={inView ? 'visible' : 'hidden'}
        className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 max-w-5xl mx-auto"
      >
        {siteContent.howToPlay.features.map((feature, i) => {
          const IconComponent = iconMap[feature.icon] || Gamepad2;

          return (
            <motion.div
              key={i}
              custom={i}
              variants={fadeInUp}
              whileHover={{ y: -3, transition: { duration: 0.2 } }}
              className="bg-white p-6 rounded-lg shadow-pixel border-b-4 border-flag-blue-500 hover:shadow-pixel-lg transition-all duration-300 group"
            >
              {/* Icon */}
              <motion.div
                whileHover={{ y: -2 }}
                transition={{ type: 'spring', stiffness: 400, damping: 15 }}
                className="inline-block"
              >
                <IconComponent
                  size={32}
                  className="text-flag-blue-500 group-hover:text-flag-red-500 transition-colors duration-300"
                  strokeWidth={1.5}
                />
              </motion.div>

              {/* Title */}
              <h3 className="font-pixel text-xs tracking-wide mt-3">
                {feature.title}
              </h3>
              {feature.titleEn && (
                <p className="text-xs text-gray-400 mt-0.5">
                  {feature.titleEn}
                </p>
              )}

              {/* Description */}
              <p className="text-sm text-gray-600 mt-2 leading-relaxed">
                {feature.description}
              </p>
            </motion.div>
          );
        })}
      </motion.div>
    </section>
  );
}
