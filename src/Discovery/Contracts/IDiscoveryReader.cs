namespace Discovery.Contracts
{
    public interface IDiscoveryReader
    {
        DiscoveryReaderResponseModel Get();

        DiscoveryReaderResponseModel Get(string boundedContext);
    }
}
