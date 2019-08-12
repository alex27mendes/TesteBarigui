using System;
using TestBarigui.Core.Interfaces;
using TestBarigui.Ioc;

namespace TestBarigui.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Register Dependencies
            var container = new SimpleInjector.Container();

            container.RegisterServices();
            container.Verify();
            #endregion
            var consumer = container.GetInstance<IConsumer>();
            consumer.ConsumeMessages();


            Console.WriteLine("Sair...");
            Console.ReadLine();
        }
    }
}
