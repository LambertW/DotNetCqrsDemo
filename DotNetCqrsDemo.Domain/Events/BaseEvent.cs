using CQRSlite.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCqrsDemo.Domain.Events
{
    public class BaseEvent : IEvent
    {
        public Guid Id { get; set; }

        public int Version { get; set; }

        public DateTimeOffset TimeStamp { get; set; }
    }
}
