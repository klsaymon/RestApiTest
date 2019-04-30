using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace RestApiTest3.Models
{
    [DataContract]
    public class Project
    {
        [DataMember(Name = "id")]
        [ReadOnly(true)]
        public Guid Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "projectStatus")]
        public ProjectStatus ProjectStatus { get; set; }

        [DataMember(Name = "startDate")]
        public DateTime? StartDate { get; set; }

        [DataMember(Name = "endDate")]
        public DateTime? EndDate { get; set; }
    }
}
