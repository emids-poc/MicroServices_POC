using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoCommon;
using Newtonsoft.Json;
using UserRegistration.DAL;
using UserRegistration.Models;
using UserRegistration.RequestDto;

namespace UserRegistration.BAL
{
    public class UserBusiness
    {
        public string CreateNewUser(UserRequestDto userRequestDto)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userData= unitOfWork.UserUnitRepository.GetFirst(x=>x.EmailId.Equals(userRequestDto.EmailId,StringComparison.OrdinalIgnoreCase));
                if (userData != null)
                    return null;
                var user = new User()
                {
                    EmailId = userRequestDto.EmailId,
                    UserName = userRequestDto.UserName,
                    Password = userRequestDto.Password,
                    TimeStamp = DateTime.Now,
                };
                unitOfWork.UserUnitRepository.Insert(user);
                unitOfWork.SaveChanges();
                userData = unitOfWork.UserUnitRepository.GetFirst(x => x.EmailId.Equals(userRequestDto.EmailId, StringComparison.OrdinalIgnoreCase));
                var userProduceDTO = new UserProduceDTO()
                {
                    UserId = userData.Id,
                    EmailId = userData.EmailId,
                    UserName = userData.UserName,
                    Password = userData.Password
                };
                var message = JsonConvert.SerializeObject(userProduceDTO);
                var codeSignKafkaProducer = new CodeSignKafkaProducer();
                codeSignKafkaProducer.CodeSignProducer(message, "userResister");
                return "Success";
            }
        }
    }
}