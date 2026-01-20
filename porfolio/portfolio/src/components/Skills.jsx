function Skills() {
  const skills = ["JavaScript", "React", "Node.js", "C#", "MySQL", "HTML & CSS","Python", "ASP.NET", "RESTful APIs"];

  return (
    <section id="skills" className="section gray">
      <h2>Skills</h2>
      <div className="grid">
        {skills.map((skill, index) => (
          <div key={index} className="card">
            {skill}
          </div>
        ))}
      </div>
    </section>
  );
}

export default Skills;
