import { useState, useEffect } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { Menu, X } from 'lucide-react';
import { siteContent } from '../data/content';

export default function Navbar() {
  const [scrolled, setScrolled] = useState(false);
  const [mobileOpen, setMobileOpen] = useState(false);

  useEffect(() => {
    const onScroll = () => setScrolled(window.scrollY > 50);
    window.addEventListener('scroll', onScroll, { passive: true });
    return () => window.removeEventListener('scroll', onScroll);
  }, []);

  const links = siteContent.nav.links;

  const handleClick = (e, href) => {
    e.preventDefault();
    setMobileOpen(false);
    const target = document.querySelector(href);
    if (target) {
      target.scrollIntoView({ behavior: 'smooth' });
    }
  };

  return (
    <nav
      className={`fixed top-0 z-50 w-full bg-nes-cream border-b-4 border-black transition-shadow duration-300 ${
        scrolled ? 'shadow-nes' : ''
      }`}
    >
      <div className="max-w-6xl mx-auto flex items-center justify-between px-6 py-3">
        {/* Logo */}
        <a
          href="#"
          className="font-bold text-xl tracking-wider text-nes-dark"
        >
          PASYAL
        </a>

        {/* Desktop links */}
        <div className="hidden md:flex items-center gap-1">
          <ul className="flex items-center gap-1">
            {links.map((link) => (
              <li key={link.href}>
                <a
                  href={link.href}
                  onClick={(e) => handleClick(e, link.href)}
                  className="uppercase text-sm font-bold text-nes-dark hover:text-nes-red px-3 py-2 transition-colors duration-200"
                >
                  {link.label}
                </a>
              </li>
            ))}
          </ul>

          {/* CTA */}
          <a
            href="#mga-lugar"
            onClick={(e) => handleClick(e, '#mga-lugar')}
            className="ml-4 bg-nes-red text-white px-4 py-2 nes-border font-bold uppercase text-xs shadow-nes hover:translate-x-[2px] hover:translate-y-[2px] hover:shadow-none transition-all duration-150"
          >
            {siteContent.nav.cta}
          </a>
        </div>

        {/* Mobile hamburger */}
        <button
          className="md:hidden p-2 nes-border bg-white hover:bg-nes-yellow transition-colors duration-150"
          onClick={() => setMobileOpen((v) => !v)}
          aria-label="Toggle menu"
        >
          {mobileOpen ? (
            <X size={20} className="text-nes-dark" />
          ) : (
            <Menu size={20} className="text-nes-dark" />
          )}
        </button>
      </div>

      {/* Mobile dropdown */}
      <AnimatePresence>
        {mobileOpen && (
          <motion.div
            initial={{ height: 0, opacity: 0 }}
            animate={{ height: 'auto', opacity: 1 }}
            exit={{ height: 0, opacity: 0 }}
            transition={{ duration: 0.2 }}
            className="md:hidden overflow-hidden bg-nes-cream nes-border border-t-0"
          >
            <ul className="flex flex-col py-4 px-4">
              {links.map((link, i) => (
                <motion.li
                  key={link.href}
                  initial={{ x: -20, opacity: 0 }}
                  animate={{ x: 0, opacity: 1 }}
                  transition={{ delay: i * 0.05, duration: 0.15 }}
                >
                  <a
                    href={link.href}
                    onClick={(e) => handleClick(e, link.href)}
                    className="block px-4 py-3 uppercase text-sm font-bold text-nes-dark hover:text-nes-red hover:bg-nes-yellow/20 transition-all duration-150"
                  >
                    {link.label}
                  </a>
                </motion.li>
              ))}
              <motion.li
                initial={{ x: -20, opacity: 0 }}
                animate={{ x: 0, opacity: 1 }}
                transition={{ delay: links.length * 0.05, duration: 0.15 }}
                className="mt-2 px-4"
              >
                <a
                  href="#mga-lugar"
                  onClick={(e) => handleClick(e, '#mga-lugar')}
                  className="block text-center bg-nes-red text-white px-4 py-3 nes-border font-bold uppercase text-xs shadow-nes hover:translate-x-[2px] hover:translate-y-[2px] hover:shadow-none transition-all duration-150"
                >
                  {siteContent.nav.cta}
                </a>
              </motion.li>
            </ul>
          </motion.div>
        )}
      </AnimatePresence>
    </nav>
  );
}
