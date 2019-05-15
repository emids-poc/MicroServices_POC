using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace DemoCommon
{
    public class CodeSignKafkaProducer
    {
        private Producer<Null, string> producer;
        public CodeSignKafkaProducer()
        {
            producer = KafkaProducer.GetProducer();
        }

        public void CodeSignProducer(string message, string topicName)
        {

            var _producerDeliveryHandler = new ProducerDeliveryHandler();

            producer.ProduceAsync(topicName, null, message, _producerDeliveryHandler);
           
            Task taskA = new Task(() => Dispose());
            // Start the task.
            taskA.Start();

        }

        public void Dispose()
        {         
            producer.Flush(10000);           
        }
    }
    }
