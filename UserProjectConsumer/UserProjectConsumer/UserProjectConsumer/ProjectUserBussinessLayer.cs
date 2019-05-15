using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProject.DAL;
using UserProject.Models;

namespace UserProjectConsumer
{
    public class ProjectUserBussinessLayer
    {

        public void InsertUser(UserProduceDTO userProduceDTO)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userData = unitOfWork.UserUnitRepository.GetFirst(x => x.EmailId.Equals(userProduceDTO.EmailId, StringComparison.OrdinalIgnoreCase));
                if (userData == null)
                {
                    var user = new User()
                    {
                        UserId= userProduceDTO.UserId,
                        EmailId = userProduceDTO.EmailId,
                        UserName = userProduceDTO.UserName,
                        Password = userProduceDTO.Password,
                        TimeStamp = DateTime.Now
                    };
                    unitOfWork.UserUnitRepository.Insert(user);
                    unitOfWork.SaveChanges();
                }
            }
        }
    }
}
