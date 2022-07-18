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
        serviceCollection.AddScoped(typeof(IMessenger<>), typeof(Messenger<>));

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var messenger1 = serviceProvider.GetService<IMessenger<string>>();
        var messenger2 = serviceProvider.GetService<IMessenger<int>>();
        messenger1.ShouldNotBeNull();
        messenger2.ShouldNotBeNull();

        // using(var scope1 = serviceProvider.CreateScope())
        // {
        //     var messenger1 = scope1.ServiceProvider.GetService<IMessenger>();
        //     var messenger2 = scope1.ServiceProvider.GetService<IMessenger>();
        //
        //     messenger1.ShouldBe(messenger2);     
        // }
        //
        // using(var scope2 = serviceProvider.CreateScope())
        // {
        //     var messenger1 = scope2.ServiceProvider.GetService<IMessenger>();
        //     var messenger2 = scope2.ServiceProvider.GetService<IMessenger>();
        //
        //     messenger1.ShouldBe(messenger2);     
        // }
    }

    private interface IMessenger<in T>
    {
        void Send(T message);
    }

    private class Messenger<T> : IMessenger<T>
    {
        private readonly Guid _id = Guid.NewGuid();
        
        public void Send(T message)
        {
            Console.WriteLine($"Sending a message... [ID] {_id}");
        }
    }
}