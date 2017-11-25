using System;
using System.Collections.Generic;
using System.Linq;
using Consul;
using Discovery.Contracts;

namespace Discovery.Consul
{
    public class ConsulDiscoveryReader : IDiscoveryReader
    {
        readonly ConsulClient client;

        public ConsulDiscoveryReader(ConsulClient client)
        {
            if (ReferenceEquals(null, client) == true) throw new ArgumentNullException(nameof(client));
            this.client = client;
        }

        public DiscoveryReaderResponseModel Get()
        {
            return Get(string.Empty);
        }

        public DiscoveryReaderResponseModel Get(string boundedContext)
        {
            long updatedAt = 0;
            var endpoints = new List<DiscoverableEndpoint>();

            var result = client.Catalog.Services().Result;
            if (ReferenceEquals(null, result) == true || result.StatusCode != System.Net.HttpStatusCode.OK)
                return new DiscoveryReaderResponseModel(updatedAt, endpoints);

            var publicServices = result.Response.Where(x => x.Value.Any(y => ConsulHelper.IsPublic(y) == true));

            foreach (var publicService in publicServices)
            {
                var parsed = ConsulHelper.Parse(publicService.Value);

                if (parsed.ContainsKey(ConsulHelper.BoundedContext) == false) continue;
                if (parsed.ContainsKey(ConsulHelper.UpdatedAt) == false) continue;
                if (parsed.ContainsKey(ConsulHelper.IntroducedAtVersion) == false) continue;
                if (parsed.ContainsKey(ConsulHelper.EndpointName) == false) continue;
                if (parsed.ContainsKey(ConsulHelper.EndpointUrl) == false) continue;

                if (string.IsNullOrEmpty(boundedContext) == false && parsed[ConsulHelper.BoundedContext] != boundedContext)
                    continue;

                var endpoint = endpoints.Where(x => x.Name == parsed[ConsulHelper.EndpointName] && x.Url == new Uri(parsed[ConsulHelper.EndpointUrl]) && x.BoundedContext == parsed[ConsulHelper.BoundedContext]).SingleOrDefault();
                if (ReferenceEquals(null, endpoint) == true)
                {
                    var depricatedAtVersion = string.Empty;
                    parsed.TryGetValue(ConsulHelper.DepricatedAtVersion, out depricatedAtVersion);

                    long x;
                    if (long.TryParse(parsed[ConsulHelper.UpdatedAt], out x))
                    {
                        if (x > updatedAt)
                            updatedAt = x;
                    }

                    endpoints.Add(new DiscoverableEndpoint(parsed[ConsulHelper.EndpointName], new Uri(parsed[ConsulHelper.EndpointUrl]), parsed[ConsulHelper.BoundedContext], new DiscoveryVersion(parsed[ConsulHelper.IntroducedAtVersion], depricatedAtVersion)));
                }
            }

            return new DiscoveryReaderResponseModel(updatedAt, endpoints);
        }
    }
}
