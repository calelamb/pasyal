import Navbar from './components/Navbar'
import Hero from './components/Hero'
import About from './components/About'
import Zones from './components/Zones'
import HowToPlay from './components/HowToPlay'
import Characters from './components/Characters'
import Vocabulary from './components/Vocabulary'
import CallToAction from './components/CallToAction'
import Credits from './components/Credits'
import Footer from './components/Footer'

export default function App() {
  return (
    <div className="min-h-screen">
      <Navbar />
      <Hero />
      <About />
      <Zones />
      <HowToPlay />
      <Characters />
      <Vocabulary />
      <CallToAction />
      <Credits />
      <Footer />
    </div>
  )
}
