import { motion } from 'framer-motion';
import { siteContent } from '../data/content';

export default function CallToAction() {
  const { title, titleAccent, titleEn, button, github } = siteContent.cta;

  return (
    <section className="bg-nes-cream py-20 px-4 text-center">
      <motion.div
        initial={{ opacity: 0, y: 30 }}
        whileInView={{ opacity: 1, y: 0 }}
        viewport={{ once: true }}
        transition={{ duration: 0.5 }}
        className="max-w-2xl mx-auto"
      >
        <h2 className="text-2xl font-bold text-nes-dark mb-2">{title}</h2>
        <p className="text-3xl font-black text-nes-red mb-2">{titleAccent}</p>
        <p className="text-sm text-nes-dark/50 italic mb-10">{titleEn}</p>

        <a
          href={github}
          target="_blank"
          rel="noopener noreferrer"
          className="inline-block bg-nes-red text-white px-8 py-4 nes-border shadow-nes text-lg font-bold uppercase hover:translate-x-[2px] hover:translate-y-[2px] hover:shadow-none transition-all duration-150"
        >
          {button}
        </a>

        <p className="text-sm text-nes-dark/40 mt-6">
          <a
            href={github}
            target="_blank"
            rel="noopener noreferrer"
            className="hover:text-nes-blue transition-colors duration-150 underline"
          >
            View on GitHub
          </a>
        </p>
      </motion.div>
    </section>
  );
}
