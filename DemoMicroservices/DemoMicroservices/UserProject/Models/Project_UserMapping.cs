using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserProject.Models
{
    public class Project_UserMapping
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int UserId { get; set; }
        public string AssignedBy { get; set; }
        public DateTime AssignedDate { get; set; }
        public string Status { get; set; }
    }
}