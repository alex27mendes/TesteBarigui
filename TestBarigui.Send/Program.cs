using System;
using TestBarigui.Core.Interfaces;
using TestBarigui.Ioc;

namespace TestBarigui.Send
{
    class Program
    {
        
        public static void Main(string[] args)
        {
            string serviceName = typeof(Program).Assembly.GetName().Name;
            #region Register Dependencies
            var container = new SimpleInjector.Container();

            container.RegisterServices();
            container.Verify();
            #endregion
            var consumer = container.GetInstance<IConsumer>();
            consumer.Connect();
            consumer.SendMessages(serviceName, "Hello World");
            consumer.Disconnect();


            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();


        }
    }
}
