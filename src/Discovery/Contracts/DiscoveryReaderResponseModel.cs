using System.Collections.Generic;

namespace Discovery.Contracts
{
    public class DiscoveryReaderResponseModel
    {
        public DiscoveryReaderResponseModel(long updatedAt, IList<DiscoverableEndpoint> endpoints)
        {
            UpdatedAt = updatedAt;
            Endpoints = new List<DiscoverableEndpoint>(endpoints);
        }

        /// <summary>
        /// FileTimeUtc
        /// </summary>
        public long UpdatedAt { get; private set; }

        public IList<DiscoverableEndpoint> Endpoints { get; private set; }
    }
}
