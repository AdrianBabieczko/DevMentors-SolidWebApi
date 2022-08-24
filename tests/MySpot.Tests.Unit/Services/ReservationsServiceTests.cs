using System;
using MySpot.Application.Commands;
using MySpot.Application.Services;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Tests.Unit.Shared;
using Shouldly;
using Xunit;

namespace MySpot.Tests.Unit.Services;

public class ReservationsServiceTests
{
    #region ARRANGE

    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
    private readonly ReservationsService _reservationsService;
    
    public ReservationsServiceTests()
    {
        _clock = new TestClock();
        _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
        _reservationsService = new ReservationsService(_clock, _weeklyParkingSpotRepository);
    }

    #endregion
    
    [Fact]
    public void given_valid_command_create_should_add_reservation()
    {
        //ARRANGE
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Guid.NewGuid(), "Joe Doe", "XYZ123", _clock.Current().AddDays(1));

        //ACT
        var reservationId = _reservationsService.Create(command);
        
        //ASSERT
        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);
    }
    
    [Fact]
    public void given_valid_parking_spot_id_create_should_fail()
    {
        //ARRANGE
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000010"),
            Guid.NewGuid(), "Joe Doe", "XYZ123", _clock.Current().AddDays(1));

        //ACT
        var reservationId = _reservationsService.Create(command);
        
        //ASSERT
        reservationId.ShouldBeNull();
    }
    
    [Fact]
    public void given_reservation_for_already_taken_date_create_should_fail()
    {
        //ARRANGE
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000002"),
            Guid.NewGuid(), "Joe Doe", "XYZ123", _clock.Current().AddDays(1));
        
        _reservationsService.Create(command);

        //ACT
        var reservationId = _reservationsService.Create(command);
        
        //ASSERT
        reservationId.ShouldBeNull();
    }
}