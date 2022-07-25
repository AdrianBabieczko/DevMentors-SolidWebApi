using MySpot.Application.Services;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.Repositories;
using MySpot.Infrastructure.Time;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository >()
    .AddSingleton<IReservationsService,ReservationsService>()
    .AddSingleton<IClock,Clock>()
    .AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();