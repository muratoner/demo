using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Warehouse.Api;

namespace Warehouse.Test
{
    public class TestClientProvider : IDisposable
    {
        protected TestServer Server { get; }
        public HttpClient Client { get; }

        public TestClientProvider()
        {
            Server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = Server.CreateClient();
        }

        public void Dispose()
        {
            Client?.Dispose();
            Server?.Dispose();
        }
    }
}
