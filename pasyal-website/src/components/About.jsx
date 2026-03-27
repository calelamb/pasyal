import { motion } from 'framer-motion';
import { Coffee, MessageCircle, Heart, Home } from 'lucide-react';
import { siteContent } from '../data/content';

const fadeInUp = {
  hidden: { opacity: 0, y: 30 },
  visible: (i = 0) => ({
    opacity: 1,
    y: 0,
    transition: { delay: i * 0.1, duration: 0.5 },
  }),
};

const stagger = {
  hidden: {},
  visible: { transition: { staggerChildren: 0.12 } },
};

const iconMap = {
  Coffee,
  MessageCircle,
  Heart,
  Home,
};

export default function About() {
  const { sectionTitle, sectionSubtitle, description, descriptionEn, pillars, comparables } =
    siteContent.about;

  return (
    <section
      id="tungkol"
      className="py-20 px-4 bg-gradient-to-br from-flag-gold-50 to-orange-50"
    >
      <div className="max-w-5xl mx-auto">
        {/* Section heading */}
        <motion.div
          initial="hidden"
          whileInView="visible"
          viewport={{ once: true, margin: '-60px' }}
          variants={fadeInUp}
          className="text-center mb-14"
        >
          <h2 className="font-pixel text-lg text-flag-blue-500 mb-2">
            {sectionTitle}
          </h2>
          <p className="text-sm text-flag-blue-400 mb-6">{sectionSubtitle}</p>

          <p className="text-gray-700 text-base leading-relaxed max-w-2xl mx-auto mb-3">
            {description}
          </p>
          <p className="text-gray-400 text-sm leading-relaxed max-w-2xl mx-auto italic">
            {descriptionEn}
          </p>
        </motion.div>

        {/* Design pillar cards */}
        <motion.div
          initial="hidden"
          whileInView="visible"
          viewport={{ once: true, margin: '-40px' }}
          variants={stagger}
          className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6 mb-14"
        >
          {pillars.map((pillar, i) => {
            const Icon = iconMap[pillar.icon] || Coffee;
            return (
              <motion.div
                key={pillar.title}
                custom={i}
                variants={fadeInUp}
                className="bg-white p-6 rounded-lg shadow-pixel border-t-4 border-flag-gold-500 hover:shadow-lg transition-shadow duration-300"
              >
                <div className="mb-4 inline-flex items-center justify-center w-10 h-10 rounded-full bg-flag-gold-100 text-flag-gold-600">
                  <Icon size={20} />
                </div>
                <h3 className="font-pixel text-xs text-gray-800 mb-2 leading-relaxed">
                  {pillar.title}
                </h3>
                <p className="text-sm text-gray-500 leading-relaxed">
                  {pillar.description}
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
          <p className="text-sm text-gray-500 mb-3 font-medium">
            Kung gusto mo ng ganitong laro:
          </p>
          <div className="flex flex-wrap items-center justify-center gap-2">
            {comparables.map((game) => (
              <span
                key={game}
                className="bg-flag-blue-100 text-flag-blue-700 px-3 py-1 rounded-full text-sm select-none"
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
