using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace DemoConsumerComman
{
    public class CodeSignKafkaConsumer
    {
        public delegate void CodeSignHandler(string message, string topic);
        public event CodeSignHandler CodeSignEvent;
        private int pollInterval = 0;
        public int PollInterval
        {

            get
            {
                return pollInterval == 0 ? 200 : pollInterval;
            }

            set
            {
                this.pollInterval = value;
            }
        }
        public void CodeSignConsumer(string[] topics, string groupid)
        {

            var config = new Dictionary<string, object>
            {
                { "group.id", groupid },
                { "bootstrap.servers", ConfigurationManager.AppSettings["BrokerList"] },
                { "enable.auto.commit", ConfigurationManager.AppSettings["enable.auto.commit"]},
                { "auto.commit.interval.ms", ConfigurationManager.AppSettings["auto.commit.interval.ms"]},
                { "statistics.interval.ms", ConfigurationManager.AppSettings["statistics.interval.ms"] },
                { "default.topic.config",new Dictionary<string,object> { { "auto.offset.reset", ConfigurationManager.AppSettings["auto.offset.reset"] } } }
            };

            using (var consumer = new Consumer<Null, string>(config, null, new StringDeserializer(new UTF8Encoding())))
            {
                bool cancelled = false;

                consumer.Subscribe(topics);

                consumer.OnMessage += (_, msg) =>
                {
                    //Logger.Info("CodeSignKafkaConsumer", "OnMessage Event" + msg.Topic);
                    CodeSignEvent(msg.Value, msg.Topic);
                    //this.OnMessage(msg);
                };

                //consumer.OnPartitionEOF += (_, end) => { Logger.Info("CodeSignKafkaConsumer", "OnPartitionEOF" + end.Topic); };

                consumer.OnError += (_, error) =>
                {
                    //Logger.Info("CodeSignKafkaConsumer", "Error Ocuured in Consumer" + error.Reason);
                    //cancelled = true;
                };

                while (!cancelled)
                {
                    consumer.Poll(TimeSpan.FromMilliseconds(PollInterval));
                }
            }
        }
    }
}
