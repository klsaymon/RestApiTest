using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RestApiTest3.Models
{
    [DataContract]
    public class ProjectSeed
    {
        [DataMember(Name = "Projects")]
        public List<Project> Projects { get; private set; }

    
        public void FillProjectList()
        {
            if (Projects != null)
                return;

            Projects = new List<Project>()
            {
                new Project { Id = Guid.NewGuid(), Name = "Project1", ProjectStatus = ProjectStatus.New, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(8)},
                new Project { Id = Guid.NewGuid(), Name = "Project2", ProjectStatus = ProjectStatus.New, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(80)},
                new Project { Id = Guid.NewGuid(), Name = "Project3", ProjectStatus = ProjectStatus.New, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2)}
            };
        }

        public bool RemoveProject(Guid id)
        {
            if (id == Guid.Empty)
                return false;

            var i = Projects.FindIndex(x => string.Compare(x.Id.ToString(), id.ToString()) == 0);
            if (i == -1)
                return false;

            if (Projects[i].ProjectStatus != ProjectStatus.New && Projects[i].ProjectStatus != ProjectStatus.Processing)
                return false;

            Projects.RemoveAt(i);

            return true;
        }

        public void AddProject(Project p)
        {
            if (p == null)
                return;

            Projects.Add(p);
        }

        public void UpdateProject(Guid id, Project p)
        {
            if (id == Guid.Empty || p == null)
                return;

            var i = Projects.FindIndex(x => string.Compare(x.Id.ToString(), id.ToString()) == 0);
            if (i == -1)
                return;

            Projects[i].Name = p.Name;
            Projects[i].ProjectStatus = p.ProjectStatus;
            Projects[i].StartDate = p.StartDate;
            Projects[i].EndDate = p.EndDate;
        }
    }
}
