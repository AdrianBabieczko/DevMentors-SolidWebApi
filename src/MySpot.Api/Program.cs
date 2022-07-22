using MySpot.Api.Repositories;
using MySpot.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository >()
    .AddSingleton<IReservationsService,ReservationsService>()
    .AddSingleton<IClock,Clock>()
    .AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();