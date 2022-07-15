using System;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace MySpot.Tests.Unit.Framework;

public class ServiceCollectionTests
{
    [Fact]
    public void test()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IMessenger, Messenger>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        var messenger1 = serviceProvider.GetService<IMessenger>();
        var messenger2 = serviceProvider.GetService<IMessenger>();
        
        messenger1.ShouldBe(messenger2);
    }

    private interface IMessenger
    {
        void Send();
    }

    private class Messenger : IMessenger
    {
        private readonly Guid _id = Guid.NewGuid();
        
        public void Send()
        {
            Console.WriteLine($"Sending a message... [ID] {_id}");
        }
    }
}