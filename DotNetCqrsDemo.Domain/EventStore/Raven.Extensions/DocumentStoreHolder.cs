using System;
using System.Collections.Generic;
using System.Text;
using CQRSlite.Events;
using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;

namespace DotNetCqrsDemo.Domain.EventStore.Raven.Extensions
{
    public class DocumentStoreHolder : IDocumentStoreHolder
    {
        public DocumentStoreHolder(IOptions<RavenSettings> options)
        {
            var setting = options.Value;

            Store = new DocumentStore
            {
                Urls = new string[] { setting.Url },
                Database = setting.DefaultDatabase,
                Conventions =
                {
                    FindCollectionName = type =>
                    {
                        //if (typeof(IEvent).IsAssignableFrom(type))
                        //{
                        //    return "CQRSLiteEvent";
                        //}

                        return DocumentConventions.DefaultGetCollectionName(type);
                    }
                }
            }.Initialize();
        }

        public IDocumentStore Store { get; }
    }
}
