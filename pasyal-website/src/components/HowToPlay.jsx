import { motion } from 'framer-motion';
import { useInView } from 'react-intersection-observer';
import { Footprints, MessageSquare, Fish } from 'lucide-react';
import { siteContent } from '../data/content';

const iconMap = {
  Footprints,
  MessageSquare,
  Fish,
};

const containerVariants = {
  hidden: {},
  visible: {
    transition: { staggerChildren: 0.12 },
  },
};

const cardVariants = {
  hidden: { opacity: 0, y: 35 },
  visible: (i = 0) => ({
    opacity: 1,
    y: 0,
    transition: {
      delay: i * 0.1,
      duration: 0.45,
      ease: [0.25, 0.46, 0.45, 0.94],
    },
  }),
};

export default function HowToPlay() {
  const [ref, inView] = useInView({ triggerOnce: true, threshold: 0.05 });
  const { sectionTitle, sectionTitleAccent, sectionTitleEn, features } =
    siteContent.howToPlay;

  return (
    <section id="paano" className="bg-white py-20 px-4">
      <div className="max-w-5xl mx-auto">
        {/* Title */}
        <div className="text-center mb-14">
          <h2 className="text-3xl font-black text-nes-dark">
            <span>{sectionTitle}</span>{' '}
            <span className="text-nes-red italic">{sectionTitleAccent}</span>
          </h2>
          <p className="text-sm text-nes-dark/50 mt-2">{sectionTitleEn}</p>
        </div>

        {/* Feature cards */}
        <motion.div
          ref={ref}
          variants={containerVariants}
          initial="hidden"
          animate={inView ? 'visible' : 'hidden'}
          className="grid grid-cols-1 sm:grid-cols-3 gap-6"
        >
          {features.map((feature, i) => {
            const Icon = iconMap[feature.icon] || Footprints;
            return (
              <motion.div
                key={i}
                custom={i}
                variants={cardVariants}
                whileHover={{
                  y: -2,
                  boxShadow: 'none',
                  transition: { duration: 0.15 },
                }}
                className="bg-nes-cream nes-border shadow-nes p-6 border-b-4 border-b-nes-red"
              >
                <Icon size={40} className="text-nes-dark mb-4" strokeWidth={1.5} />
                <h3 className="pixel-text text-xl text-nes-dark mb-1">
                  {feature.title}
                </h3>
                <p className="text-xs text-nes-dark/50 mb-3">{feature.titleEn}</p>
                <p className="text-sm text-nes-dark/70 leading-relaxed">
                  {feature.desc}
                </p>
              </motion.div>
            );
          })}
        </motion.div>
      </div>
    </section>
  );
}
