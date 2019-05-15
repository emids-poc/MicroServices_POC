using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoConsumerComman;
using Newtonsoft.Json;
using UserProject.Models;

namespace UserProjectConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var codeSignKafkaConsumer = new CodeSignKafkaConsumer();
                codeSignKafkaConsumer.CodeSignEvent += GetAllMessages;
                string[] topic = new string[] { "userResister" };

                codeSignKafkaConsumer.CodeSignConsumer(topic, "projectConsumer");

            }
            catch(Exception ex)
            {

            }
        }

        private static void GetAllMessages(string message, string topic)
        {
            switch(topic)
            {
                case "userResister":

                    var produceDTO = JsonConvert.DeserializeObject<UserProduceDTO>(message);
                    var projectUserBussinessLayer = new ProjectUserBussinessLayer();
                    projectUserBussinessLayer.InsertUser(produceDTO);
                    break;

            }
        }
    }
}
