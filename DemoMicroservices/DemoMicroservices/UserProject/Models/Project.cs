using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserProject.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
    }
}