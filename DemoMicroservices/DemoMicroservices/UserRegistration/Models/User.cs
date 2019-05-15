using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserRegistration.Models
{
    public class User
    {
        public int Id { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}