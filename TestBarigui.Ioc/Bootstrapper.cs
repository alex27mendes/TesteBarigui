using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;
using TestBarigui.Core.Consumer;
using TestBarigui.Core.Interfaces;

namespace TestBarigui.Ioc
{
     public static class Bootstrapper
    {
        public static void RegisterServices(this SimpleInjector.Container container)
        {
            container.Register<IConsumer, Consumer>(Lifestyle.Singleton);
        }
    }
}
