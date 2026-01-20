import { motion } from "framer-motion";

function Hero() {
  return (
    <motion.section
      className="hero"
      initial={{ opacity: 0, y: 40 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 1 }}
    >
      <h1>Hi, I'm Agam</h1>
    </motion.section>
  );
}

export default Hero;