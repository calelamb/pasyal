import { motion } from 'framer-motion';
import { Github } from 'lucide-react';
import { siteContent } from '../data/content';

const fadeIn = {
  hidden: { opacity: 0, y: 20 },
  visible: { opacity: 1, y: 0, transition: { duration: 0.6 } },
};

export default function Footer() {
  const { tagline, taglineEn, github } = siteContent.footer;

  return (
    <footer className="bg-gray-950 text-white py-12 px-4">
      <div className="border-t border-white/10 max-w-4xl mx-auto pt-10">
        <motion.div
          initial="hidden"
          whileInView="visible"
          viewport={{ once: true }}
          variants={fadeIn}
          className="flex flex-col items-center text-center gap-4"
        >
          {/* Logo with glow */}
          <span
            className="font-pixel text-sm text-flag-gold-500"
            style={{ textShadow: '0 0 12px rgba(252, 209, 22, 0.35)' }}
          >
            PASYAL
          </span>

          {/* Tagline */}
          <p className="text-white/60 text-sm italic max-w-md">{tagline}</p>
          <p className="text-white/35 text-xs max-w-md">{taglineEn}</p>

          {/* GitHub */}
          <a
            href={github}
            target="_blank"
            rel="noopener noreferrer"
            aria-label="GitHub"
            className="mt-2 inline-flex items-center justify-center w-10 h-10 rounded-full border border-white/15 text-white/50 hover:text-flag-gold-500 hover:border-flag-gold-500/40 transition-colors duration-200"
          >
            <Github size={18} />
          </a>

          {/* Copyright */}
          <p className="text-white/40 text-xs mt-4">
            &copy; 2026 Gawa ni Cale Lamb
          </p>
        </motion.div>
      </div>
    </footer>
  );
}
