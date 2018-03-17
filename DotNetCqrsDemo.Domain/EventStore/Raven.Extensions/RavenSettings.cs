using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCqrsDemo.Domain.EventStore.Raven.Extensions
{
    public class RavenSettings
    {
        public string Url { get; set; }
        public string DefaultDatabase { get; set; }
    }
}
