using System;
using Discovery.Contracts;
using Machine.Specifications;

namespace Discovery.Tests
{
    public class When_comparing_two_equal__DiscoverableEndpoint__
    {
        Establish context = () =>
        {
            first = new DiscoverableEndpoint("endpoint", new Uri("https://eldersoss.com"), "elders", new DiscoveryVersion("v1"));
            second = new DiscoverableEndpoint("endpoint", new Uri("https://eldersoss.com"), "elders", new DiscoveryVersion("v1"));
        };

        Because of = () => result = first.Equals(second);

        It should_return_true = () => result.ShouldBeTrue();

        static DiscoverableEndpoint first;
        static DiscoverableEndpoint second;
        static bool result;
    }
}
