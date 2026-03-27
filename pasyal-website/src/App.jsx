import Navbar from './components/Navbar'
import Hero from './components/Hero'
import About from './components/About'
import Zones from './components/Zones'
import Characters from './components/Characters'
import HowToPlay from './components/HowToPlay'
import Vocabulary from './components/Vocabulary'
import Gallery from './components/Gallery'
import Credits from './components/Credits'
import Footer from './components/Footer'

export default function App() {
  return (
    <div className="min-h-screen">
      <Navbar />
      <section id="hero">
        <Hero />
      </section>
      <section id="about">
        <About />
      </section>
      <section id="zones">
        <Zones />
      </section>
      <section id="characters">
        <Characters />
      </section>
      <section id="how-to-play">
        <HowToPlay />
      </section>
      <section id="vocabulary">
        <Vocabulary />
      </section>
      <section id="gallery">
        <Gallery />
      </section>
      <section id="credits">
        <Credits />
      </section>
      <Footer />
    </div>
  )
}
