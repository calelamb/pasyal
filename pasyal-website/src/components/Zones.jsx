import { motion } from 'framer-motion';
import { useInView } from 'react-intersection-observer';
import { Home, Store, Waves, Bike, Mountain, Lock } from 'lucide-react';
import { siteContent, zonePixelArts } from '../data/content';
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

const iconMap = {
  Home,
  Store,
  Waves,
  Bike,
  Mountain,
};

export default function Zones() {
  const [ref, inView] = useInView({ triggerOnce: true, threshold: 0.05 });

  return (
    <section id="mga-lugar" className="py-20 px-4 bg-flag-blue-900 text-white">
      <div className="text-center mb-14">
        <h2 className="font-pixel text-lg text-flag-gold-500 tracking-wide">
          Mga Lugar
        </h2>
        <p className="text-white/50 text-sm mt-1">The Zones</p>
      </div>

      <motion.div
        ref={ref}
        variants={stagger}
        initial="hidden"
        animate={inView ? 'visible' : 'hidden'}
        className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 max-w-6xl mx-auto"
      >
        {(siteContent.zones.list || []).map((zone, i) => {
          const IconComponent = iconMap[zone.icon] || Home;

          return (
            <motion.div
              key={zone.id}
              custom={i}
              variants={fadeInUp}
              whileHover={{ y: -4 }}
              transition={{ type: 'spring', stiffness: 300, damping: 20 }}
              className="relative bg-white/10 backdrop-blur rounded-lg overflow-hidden hover:bg-white/15 transition-colors duration-300 group"
            >
              {/* Pixel Art Banner */}
              <div className="w-full overflow-hidden h-32 flex items-center justify-center bg-black/20">
                <div className="transform scale-[1.2] opacity-80 group-hover:opacity-100 transition-opacity duration-300">
                  {zonePixelArts[zone.id] && (
                    <PixelArt grid={zonePixelArts[zone.id]} cellSize={5} />
                  )}
                </div>
              </div>

              {/* Colored Accent Bar */}
              <div
                className="h-1"
                style={{ backgroundColor: zone.color }}
              />

              {/* Content */}
              <div className="p-5">
                <div className="flex items-center gap-2 mb-1">
                  <IconComponent
                    size={16}
                    style={{ color: zone.color }}
                    className="flex-shrink-0"
                  />
                  <span className="font-pixel text-xs tracking-wide">
                    {zone.name}
                  </span>
                </div>
                <p className="text-white/50 text-xs mb-3">{zone.nameEn}</p>

                <p className="text-sm text-white/80 leading-relaxed mb-4">
                  {zone.description}
                </p>

                {zone.features && zone.features.length > 0 && (
                  <ul className="space-y-1.5 mb-4">
                    {zone.features.map((feature, fi) => (
                      <li key={fi} className="flex items-center gap-2 text-xs text-white/70">
                        <span
                          className="w-1.5 h-1.5 rounded-full flex-shrink-0"
                          style={{ backgroundColor: zone.color }}
                        />
                        {feature}
                      </li>
                    ))}
                  </ul>
                )}

                {zone.size && (
                  <span className="text-xs text-white/40">{zone.size}</span>
                )}
              </div>

              {/* Locked Overlay */}
              {zone.locked && (
                <div className="absolute inset-0 bg-black/50 backdrop-blur-sm flex flex-col items-center justify-center gap-2 z-10">
                  <Lock size={28} className="text-white/60" />
                  <span className="font-pixel text-xs text-white/70 tracking-wider">
                    Abangan!
                  </span>
                </div>
              )}
            </motion.div>
          );
        })}
      </motion.div>
    </section>
  );
}
