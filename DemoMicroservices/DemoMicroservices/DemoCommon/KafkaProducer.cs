using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace DemoCommon
{
    class KafkaProducer
    {
        private static Producer<Null, string> _kafkaProducer;
        private static readonly object lockObj = new object();

        private KafkaProducer()
        {
            //Private constructor so no one can create this object outside of this class.
        }
        public static Producer<Null, string> GetProducer()
        {
            //Let us not waste time with the lock if the object already exists.
            if (_kafkaProducer != null)
                return _kafkaProducer;
            lock (lockObj)
            {
                /*As we allow the lock to be very optimistic there are situations that the code is waiting on the lock 
                while the other thread just created the producer. 
                */
                if (_kafkaProducer != null)
                    return _kafkaProducer;
                //kafkaProducerContainer = new KafkaProducer();
                _kafkaProducer = new Producer<Null, string>(GetProducerConfig(), null, new StringSerializer(new UTF8Encoding()));
                return _kafkaProducer;
            }
        }

        private static Dictionary<string, object> GetProducerConfig()
        {
            var config = new Dictionary<string, object>
            {
                { "bootstrap.servers", ConfigurationManager.AppSettings["BrokerList"]},
                {"queue.buffering.max.messages",ConfigurationManager.AppSettings["queue.buffering.max.messages"] },
                {"queue.buffering.max.ms",ConfigurationManager.AppSettings["queue.buffering.max.ms"] },
                {"message.send.max.retries",ConfigurationManager.AppSettings["message.send.max.retries"] },
                {"retry.backoff.ms",ConfigurationManager.AppSettings["retry.backoff.ms"] },
                {"message.timeout.ms",ConfigurationManager.AppSettings["message.timeout.ms"] },
                { "default.topic.config", new Dictionary<string, object>
                    {
                        { "acks",ConfigurationManager.AppSettings["acks"] }
                    }
                }
            };
            return config;
        }
    }
}
