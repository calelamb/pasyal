/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{js,jsx}'],
  theme: {
    extend: {
      colors: {
        'flag-blue': {
          50: '#e6eef8',
          100: '#b3ccea',
          200: '#80aadc',
          300: '#4d88ce',
          400: '#2670c0',
          500: '#0038A8',
          600: '#003296',
          700: '#002a7e',
          800: '#002166',
          900: '#00184e',
          DEFAULT: '#0038A8',
        },
        'flag-red': {
          50: '#fce8eb',
          100: '#f5b8c0',
          200: '#ee8895',
          300: '#e7586a',
          400: '#da3449',
          500: '#CE1126',
          600: '#b90f22',
          700: '#9b0d1c',
          800: '#7d0a17',
          900: '#5f0811',
          DEFAULT: '#CE1126',
        },
        'flag-gold': {
          50: '#fef9e4',
          100: '#fdedab',
          200: '#fce172',
          300: '#fcd539',
          400: '#FCD116',
          500: '#e8b800',
          600: '#c89e00',
          700: '#a88400',
          800: '#886a00',
          900: '#685000',
          DEFAULT: '#FCD116',
        },
      },
      fontFamily: {
        pixel: ['"Press Start 2P"', 'cursive'],
        body: ['Inter', 'system-ui', 'sans-serif'],
      },
      boxShadow: {
        pixel: '4px 4px 0 0 rgba(0, 0, 0, 0.2)',
        'pixel-blue': '4px 4px 0 0 #0038A8',
        'pixel-red': '4px 4px 0 0 #CE1126',
        'pixel-gold': '4px 4px 0 0 #FCD116',
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
