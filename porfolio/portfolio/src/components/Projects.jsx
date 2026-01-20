import projects from "../data/projects.json";

function Projects() {
  return (
    <section id="projects" className="section">
      <h2>Projects</h2>

      <div className="grid">
        {projects.map((p, i) => (
          <div key={i} className="project-card">
            <h3>{p.title}</h3>
            <p>{p.description}</p>

            <div className="project-buttons">
              <a href={p.github} target="_blank">GitHub</a>
            </div>
          </div>
        ))}
      </div>
    </section>
  );
}

export default Projects;
