import { motion } from 'framer-motion';
import { Github } from 'lucide-react';
import { siteContent } from '../data/content';

export default function Credits() {
  const { name, role, bio, github, tech } = siteContent.credits;

  return (
    <section className="bg-white py-20 px-4">
      <div className="max-w-lg mx-auto text-center">
        {/* Title */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ duration: 0.5 }}
        >
          <h2 className="pixel-text text-xl text-nes-dark mb-2">
            SINO ANG GUMAWA
          </h2>
          <div className="bg-nes-yellow h-1 w-20 mx-auto mb-8" />
        </motion.div>

        {/* Creator card */}
        <motion.div
          className="bg-nes-cream nes-border shadow-nes p-8"
          initial={{ opacity: 0, y: 30 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ duration: 0.5, delay: 0.1 }}
        >
          {/* Avatar */}
          <div className="w-16 h-16 bg-nes-yellow nes-border flex items-center justify-center mx-auto">
            <span className="pixel-text text-lg text-nes-dark">CL</span>
          </div>

          <p className="pixel-text text-sm text-nes-dark mt-4">{name}</p>
          <p className="text-sm text-nes-dark/60 mt-1">{role}</p>
          <p className="text-sm text-nes-dark/70 mt-3 italic">{bio}</p>

          {/* GitHub */}
          <a
            href={github}
            target="_blank"
            rel="noopener noreferrer"
            className="inline-flex items-center gap-2 mt-4 bg-nes-dark text-white px-4 py-2 nes-border shadow-nes-sm text-sm font-bold hover:translate-x-[1px] hover:translate-y-[1px] hover:shadow-none transition-all duration-150"
          >
            <Github size={16} />
            GitHub
          </a>
        </motion.div>

        {/* Tech stack */}
        <div className="flex flex-wrap justify-center gap-3 mt-8">
          {tech.map((t, i) => (
            <motion.span
              key={t.label}
              className="nes-border bg-white px-3 py-1.5 text-xs font-bold text-nes-dark/70"
              initial={{ opacity: 0, y: 15 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ delay: i * 0.08, duration: 0.3 }}
            >
              {t.label}
            </motion.span>
          ))}
        </div>
      </div>
    </section>
  );
}
