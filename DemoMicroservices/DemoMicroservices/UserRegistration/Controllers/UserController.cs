using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserRegistration.BAL;
using UserRegistration.RequestDto;

namespace UserRegistration.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        public string AddUser(UserRequestDto userRequestDto)
        {
            try
            {
                var userBusiness = new UserBusiness();
                return userBusiness.CreateNewUser(userRequestDto);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
