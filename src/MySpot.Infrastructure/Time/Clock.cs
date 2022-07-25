using System.Runtime.CompilerServices;
using MySpot.Application.Services;

[assembly:InternalsVisibleTo("MySpot.Api")]
namespace MySpot.Infrastructure.Time
{
    internal sealed  class Clock : IClock
    {
        public DateTime Current() => DateTime.UtcNow;
    }
}