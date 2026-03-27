import { motion } from 'framer-motion';
import { Compass, MessageCircle, Heart, Users } from 'lucide-react';
import { siteContent } from '../data/content';

const iconMap = {
  Compass,
  MessageCircle,
  Heart,
  Users,
};

const stagger = {
  hidden: {},
  visible: { transition: { staggerChildren: 0.12 } },
};

const fadeInUp = {
  hidden: { opacity: 0, y: 30 },
  visible: (i = 0) => ({
    opacity: 1,
    y: 0,
    transition: { delay: i * 0.1, duration: 0.4 },
  }),
};

export default function About() {
  const { sectionTitle, sectionTitleEn, intro, introEn, pillars, comparables } =
    siteContent.about;

  return (
    <section id="tungkol" className="bg-white py-20 px-4">
      <div className="max-w-5xl mx-auto">
        {/* Section heading */}
        <motion.div
          initial="hidden"
          whileInView="visible"
          viewport={{ once: true, margin: '-60px' }}
          variants={fadeInUp}
          className="text-center mb-14"
        >
          <h2 className="pixel-text text-2xl text-nes-dark mb-2">
            {sectionTitle}
          </h2>
          <div className="bg-nes-blue h-1 w-20 mx-auto mb-2" />
          <p className="text-sm text-nes-dark/50 mb-8">{sectionTitleEn}</p>

          <p className="text-nes-dark text-base leading-relaxed max-w-3xl mx-auto mb-3">
            {intro}
          </p>
          <p className="text-nes-dark/40 text-sm leading-relaxed max-w-3xl mx-auto italic">
            {introEn}
          </p>
        </motion.div>

        {/* Pillar cards */}
        <motion.div
          initial="hidden"
          whileInView="visible"
          viewport={{ once: true, margin: '-40px' }}
          variants={stagger}
          className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6 mb-14"
        >
          {pillars.map((pillar, i) => {
            const Icon = iconMap[pillar.icon] || Compass;
            return (
              <motion.div
                key={pillar.title}
                custom={i}
                variants={fadeInUp}
                className="bg-nes-cream nes-border shadow-nes p-6 hover:translate-x-[2px] hover:translate-y-[2px] hover:shadow-none transition-all duration-150"
              >
                <div className="bg-nes-yellow w-12 h-12 flex items-center justify-center nes-border mb-4">
                  <Icon size={20} className="text-nes-dark" />
                </div>
                <h3 className="pixel-text text-xs text-nes-dark mb-1">
                  {pillar.title}
                </h3>
                <p className="text-xs text-nes-dark/50 mb-2 italic">
                  {pillar.titleTl}
                </p>
                <p className="text-sm text-nes-dark/70 leading-relaxed">
                  {pillar.desc}
                </p>
              </motion.div>
            );
          })}
        </motion.div>

        {/* Comparable games */}
        <motion.div
          initial="hidden"
          whileInView="visible"
          viewport={{ once: true }}
          variants={fadeInUp}
          className="text-center"
        >
          <p className="text-xs text-nes-dark/40 uppercase font-bold mb-3 tracking-wider">
            Kung gusto mo ang...
          </p>
          <div className="flex flex-wrap items-center justify-center gap-2">
            {comparables.map((game) => (
              <span
                key={game}
                className="nes-border bg-white px-3 py-1 text-xs font-bold uppercase text-nes-dark"
              >
                {game}
              </span>
            ))}
          </div>
        </motion.div>
      </div>
    </section>
  );
}
