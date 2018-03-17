using CQRSlite.Events;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.Client.ServerWide.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Raven.Client.Documents.Conventions;
using DotNetCqrsDemo.Domain.EventStore.Raven.Extensions;

namespace DotNetCqrsDemo.Domain.EventStore
{
    public class RavenDBEventStore : IEventStore
    {
        private readonly IEventPublisher _eventPublisher;
        //private IDocumentStore _Store;
        private IDocumentSession _session;

        public RavenDBEventStore(IEventPublisher eventPublisher,
            //IDocumentStoreHolder store,
            IDocumentSession session)
        {
            _eventPublisher = eventPublisher;
            //_Store = store.Store;
            _session = session;
        }

        public Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = default(CancellationToken))
        {
            var events = _session.Query<RavenEvent>().Where(t => t.Id == aggregateId.ToString()).Where(x => x.Event.Version > fromVersion);
            return Task.FromResult(events.AsEnumerable().Select(t => t.Event) ?? new List<IEvent>());
        }

        public async Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var @event in events)
            {
                _session.Store(new RavenEvent(@event.Id, @event));
                _session.SaveChanges();

                await _eventPublisher.Publish(@event, cancellationToken);
            }
        }
    }
}