/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{js,jsx}'],
  theme: {
    extend: {
      colors: {
        'nes-blue': '#0038A8',
        'nes-red': '#CE1126',
        'nes-yellow': '#FCD116',
        'nes-dark': '#1a1a2e',
        'nes-green': '#4CAF50',
        'nes-cream': '#FFFDF0',
        'nes-black': '#0a0a0a',
      },
      fontFamily: {
        pixel: ['Space Grotesk', 'monospace'],
      },
      borderRadius: {
        none: '0px',
        sm: '0px',
        DEFAULT: '0px',
        md: '0px',
        lg: '0px',
        xl: '0px',
        '2xl': '0px',
        '3xl': '0px',
        full: '0px',
      },
      boxShadow: {
        nes: '4px 4px 0px #000',
        'nes-sm': '2px 2px 0px #000',
        'nes-red': '4px 4px 0px #CE1126',
        'nes-blue': '4px 4px 0px #0038A8',
        'nes-yellow': '4px 4px 0px #FCD116',
      },
      animation: {
        float: 'float 3s ease-in-out infinite',
        twinkle: 'twinkle 2s ease-in-out infinite',
      },
      keyframes: {
        float: {
          '0%, 100%': { transform: 'translateY(0)' },
          '50%': { transform: 'translateY(-10px)' },
        },
        twinkle: {
          '0%, 100%': { opacity: '1' },
          '50%': { opacity: '0.3' },
        },
      },
    },
  },
  plugins: [],
}
