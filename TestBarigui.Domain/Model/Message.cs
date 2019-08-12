using System;
using System.Collections.Generic;
using System.Text;

namespace TestBarigui.Domain.Model
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public DateTime TimeStamp {get; set;}
        public string ServiceName { get; set; }

        public override string ToString()
        {
            return string.Format($"Service: {ServiceName}  Id: {Id} Text: '{Text}' Timestamp: {TimeStamp}  ");
        }
    }
}
