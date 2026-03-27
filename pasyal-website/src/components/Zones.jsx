import { motion } from 'framer-motion';
import { useInView } from 'react-intersection-observer';
import { Lock } from 'lucide-react';
import { siteContent } from '../data/content';

const containerVariants = {
  hidden: {},
  visible: {
    transition: { staggerChildren: 0.12 },
  },
};

const cardVariants = {
  hidden: { opacity: 0, y: 40 },
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

export default function Zones() {
  const [ref, inView] = useInView({ triggerOnce: true, threshold: 0.05 });

  return (
    <section id="mga-lugar" className="bg-nes-cream py-20 px-4">
      {/* Red stripe */}
      <div className="h-2 bg-nes-red -mt-20 mb-20" />

      <div className="max-w-5xl mx-auto">
        {/* Title */}
        <div className="text-center mb-14">
          <h2 className="pixel-text text-2xl text-nes-dark mb-2">
            {siteContent.zones.sectionTitle}
          </h2>
          <div className="bg-nes-red h-1 w-20 mx-auto mb-2" />
          <p className="text-sm text-nes-dark/50">
            {siteContent.zones.sectionTitleEn}
          </p>
        </div>

        {/* Zone cards grid */}
        <motion.div
          ref={ref}
          variants={containerVariants}
          initial="hidden"
          animate={inView ? 'visible' : 'hidden'}
          className="grid grid-cols-1 md:grid-cols-2 gap-8"
        >
          {siteContent.zones.list.map((zone, i) => (
            <motion.div
              key={zone.id}
              custom={i}
              variants={cardVariants}
              whileHover={{
                y: -2,
                boxShadow: 'none',
                transition: { duration: 0.15 },
              }}
              className={`relative bg-white nes-border shadow-nes overflow-hidden ${
                zone.locked ? 'grayscale' : ''
              }`}
            >
              {/* Colored header bar */}
              <div className="h-3" style={{ backgroundColor: zone.color }} />

              {/* Image area placeholder */}
              <div
                className="h-[200px] flex items-center justify-center relative"
                style={{
                  background: `linear-gradient(135deg, ${zone.color}33, ${zone.color}11)`,
                }}
              >
                <span
                  className="pixel-text text-3xl text-center px-4"
                  style={{ color: zone.color, opacity: 0.3 }}
                >
                  {zone.name}
                </span>
              </div>

              {/* Content */}
              <div className="p-5">
                <h3 className="pixel-text text-sm text-nes-dark mb-1">
                  {zone.name}
                </h3>
                <p className="text-xs text-nes-dark/50 mb-3">{zone.nameEn}</p>
                <p className="text-sm text-nes-dark/70 leading-relaxed mb-4">
                  {zone.desc}
                </p>

                {/* Features */}
                <ul className="space-y-1.5 mb-4">
                  {zone.features.map((feature, fi) => (
                    <li
                      key={fi}
                      className="flex items-center gap-2 text-xs text-nes-dark/60"
                    >
                      <span
                        className="w-2 h-2 flex-shrink-0"
                        style={{
                          backgroundColor: zone.color,
                          border: '2px solid #0a0a0a',
                        }}
                      />
                      {feature}
                    </li>
                  ))}
                </ul>

                {/* Progress bar */}
                <div className="nes-border h-4 bg-nes-cream overflow-hidden">
                  <div
                    className="h-full transition-all duration-500"
                    style={{
                      width: zone.progress,
                      backgroundColor: zone.color,
                    }}
                  />
                </div>
                <p className="text-xs text-nes-dark/40 mt-1 text-right">
                  {zone.progress}
                </p>
              </div>

              {/* Locked overlay */}
              {zone.locked && (
                <div className="absolute inset-0 bg-black/60 flex flex-col items-center justify-center gap-3 z-10">
                  <Lock size={32} className="text-white/60" />
                  <span className="pixel-text text-xs text-white/80 tracking-wider">
                    SARADO — V2
                  </span>
                </div>
              )}
            </motion.div>
          ))}
        </motion.div>
      </div>
    </section>
  );
}
