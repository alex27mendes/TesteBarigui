using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TestBarigui.Core.Conifg;
using TestBarigui.Core.Infra;
using TestBarigui.Domain.Model;

namespace TestBarigui.Core.Consumer
{
    public  class Consumer: Interfaces.IConsumer
    {
        private IConnection connection;
        private IModel channel;

       // private const string ExchangeName = "PubSubTestExchange";

        private string queueName;
        public void Connect()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = ConnectionConstants.HostName
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.QueueDeclare(queue: ConnectionConstants.QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        //public void ConsumeMessages()
        //{
        //    var consumer = new EventingBasicConsumer(channel);
        //    WriteStartMessage();

        //    consumer.Received += (model, ea) =>
        //    {
        //        try
        //        {
        //            object message = SerializationHelper.FromByteArray(ea.Body);
        //            Console.WriteLine(" [x] Received {0}", message);
        //            // Sending ACK to the queue
        //            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

        //        }
        //        catch
        //        {
        //            // Sending NACK to requeue the message
        //            channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
        //        }
        //    };

        //    channel.BasicConsume(queue: ConnectionConstants.QueueName,
        //             autoAck: true,
        //             consumer: consumer);

        //    //connection.Close();
        //    //connection.Dispose();
        //    //connection = null;
        //}

        private static void WriteStartMessage()
        {
            string startMessage = string.Format("Waiting for messages on {0}/{1}. Press 'q' to quit",
                ConnectionConstants.HostName, ConnectionConstants.QueueName);
            Console.WriteLine(startMessage);
        }


        public void Disconnect()
        {
            channel = null;

            if (connection.IsOpen)
            {
                connection.Close();
            }

            connection.Dispose();
            connection = null;
        }
        public void SendMessages(string serviceName, string text)
        {
            WriteStartMessage();
            Random random = new Random();
            int senderId = random.Next(99999);
            int index = 0;
            while (true)
            {
                index++;
                Message message = new Message
                {
                    Id = Guid.NewGuid(),
                    ServiceName = serviceName,
                    Text = text,
                    TimeStamp = DateTime.Now
                };
                Console.WriteLine("Sent message #{0} ", message.ToString());
                SendMessage(message.ToString());
                Thread.Sleep(5000);
            }

        }

        private void SendMessage<T>(T message)
        {
            byte[] messageBody = message.ToByteArray();
            channel.BasicPublish(string.Empty, ConnectionConstants.QueueName, null, messageBody);
        }

        public void ConsumeMessages()
        {
            var factory = new ConnectionFactory() { HostName = ConnectionConstants.HostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: ConnectionConstants.QueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        object message = SerializationHelper.FromByteArray(ea.Body);
                        Console.WriteLine(" [x] Received {0}", message);
                        // Sending ACK to the queue
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                    }
                    catch
                    {
                        // Sending NACK to requeue the message
                        channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                    }
                };
                channel.BasicConsume(queue: ConnectionConstants.QueueName,
                         autoAck: true,
                         consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
