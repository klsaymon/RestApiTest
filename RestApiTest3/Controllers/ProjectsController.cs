using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestApiTest3.Models;

namespace RestApiTest3.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        // GET api/projects
        [HttpGet]
        public ActionResult<IEnumerable<Project>> Get()
        {
            var ps = HttpContext.Session.GetObject<ProjectSeed>(Constants.KEY);
            if (ps == null)
            {
                ps = new ProjectSeed();
                ps.FillProjectList();
                HttpContext.Session.SetObject(Constants.KEY, ps);
            }

            return ps.Projects;
        }

        // GET api/projects/5
        [HttpGet("{id}")]
        public ActionResult<Project> Get(Guid id)
        {
            var ps = HttpContext.Session.GetObject<ProjectSeed>(Constants.KEY);

            if (id == Guid.Empty)
                return StatusCode(404);

            return ps.Projects.Find(x => string.Compare(x.Id.ToString(), id.ToString()) == 0);
        }

        // POST api/projects
        [HttpPost]
        public ActionResult Post([FromBody] Project value)
        {
            // - добавить проект
            if (value.ProjectStatus != ProjectStatus.New)
                return BadRequest();

            if (value.Name == null || value.Name == "")
                return BadRequest();

            if (value.StartDate == null)
                return BadRequest();

            var ps = HttpContext.Session.GetObject<ProjectSeed>(Constants.KEY);
            ps.AddProject(value);
            HttpContext.Session.SetObject(Constants.KEY, ps);
            return StatusCode(201);
        }

        // PUT api/projects/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Project value)
        {
            // - обновить проект ( имя, статус, даты начала и завершения) 
            if (id != value.Id)
                return BadRequest();

            if (value.Name == null || value.Name == "")
                return BadRequest();

            if (value.StartDate == null)
                return BadRequest();

            if (value.ProjectStatus == ProjectStatus.Done && value.EndDate == null)
                return BadRequest();

            var ps = HttpContext.Session.GetObject<ProjectSeed>(Constants.KEY);
            ps.UpdateProject(id, value);
            HttpContext.Session.SetObject(Constants.KEY, ps);
            return StatusCode(202);
        }

        // DELETE api/projects/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var ps = HttpContext.Session.GetObject<ProjectSeed>(Constants.KEY);

            if (!ps.RemoveProject(id))
                return BadRequest();

            HttpContext.Session.SetObject(Constants.KEY, ps);
            return NoContent();
        }
    }
}
