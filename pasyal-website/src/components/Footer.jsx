import { Github } from 'lucide-react';
import { siteContent } from '../data/content';

export default function Footer() {
  const { copyright, tagline, github } = siteContent.footer;

  return (
    <footer className="bg-nes-dark text-white py-12 px-4 border-t-4 border-nes-yellow">
      <div className="max-w-4xl mx-auto flex flex-col items-center text-center gap-4">
        {/* Logo */}
        <span className="text-nes-yellow font-bold text-xl tracking-wider">
          PASYAL
        </span>

        {/* Tagline */}
        <p className="text-white/50 text-sm">{tagline}</p>

        {/* GitHub */}
        <a
          href={github}
          target="_blank"
          rel="noopener noreferrer"
          aria-label="GitHub"
          className="inline-flex items-center justify-center w-10 h-10 nes-border bg-white/10 text-white/60 hover:text-nes-yellow hover:bg-white/20 transition-colors duration-150"
        >
          <Github size={18} />
        </a>

        {/* Tech stack badges */}
        <div className="flex flex-wrap justify-center gap-2 mt-2">
          {siteContent.credits.tech.map((t) => (
            <span
              key={t.label}
              className="nes-border text-xs px-2 py-1 text-white/60"
            >
              {t.label}
            </span>
          ))}
        </div>

        {/* Copyright */}
        <p className="text-white/30 text-xs mt-4">{copyright}</p>
      </div>
    </footer>
  );
}
