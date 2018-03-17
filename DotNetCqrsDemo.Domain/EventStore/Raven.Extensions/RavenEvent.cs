using CQRSlite.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCqrsDemo.Domain.EventStore.Raven.Extensions
{
    /// <summary>
    /// RavenDB 4.0 is not support type Guid ID any more.
    /// Use new entity with string type Id to store the IEvent entity.
    /// </summary>
    public class RavenEvent
    {
        public string Id { get; set; }
        public IEvent Event { get; set; }

        public RavenEvent(Guid id, IEvent @event)
        {
            Id = id.ToString();
            Event = @event;
        }
    }
}
