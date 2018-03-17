using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCqrsDemo.Domain.EventStore.Raven.Extensions
{
    public interface IDocumentStoreHolder
    {
        IDocumentStore Store { get; }
    }
}
