using System;
using System.Collections.Generic;
using System.Text;

namespace TestBarigui.Core.Interfaces
{
    public  interface IConsumer
    {
        void Connect();
        void ConsumeMessages();
        void SendMessages(string service, string message);
        void Disconnect();
    }
}
