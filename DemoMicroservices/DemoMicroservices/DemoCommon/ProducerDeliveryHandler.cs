using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace DemoCommon
{
    public class ProducerDeliveryHandler : IDeliveryHandler<Null, string>
    {

        public bool MarshalData
        {
            get
            {
                return false;
            }
        }

        public void HandleDeliveryReport(Message<Null, string> message)
        {
            if (message.Error.HasError)
            {
                //Logger.Error(typeof(ProducerDeliveryHandler), message.Error + "::" + message.Value);
                var codeSignKafkaProducer = new CodeSignKafkaProducer();
                codeSignKafkaProducer.CodeSignProducer(message.Value, message.Topic);
            }
            else
            {
                //Logger.Info(typeof(ProducerDeliveryHandler), "message has been pushed to kafaka on topic with message" + message.Topic + "::" + message.Value);
            }
        }

        //public void HandleDeliveryReport(Message<K, V> message)
        //{
        //    if (message.Error.HasError)
        //    {
        //        Logger.Error("ProducerDeliveryHandler", message.Error);
        //    }
        //}

        public void HandleDeliveryReport(Message message)
        {
            if (message.Error.HasError)
            {
                //Logger.Error("ProducerDeliveryHandler", message.Topic + "::" + message.Error + "::" + message.Value);
            }
        }
    }
}
