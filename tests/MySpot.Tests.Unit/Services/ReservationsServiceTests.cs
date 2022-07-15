using System;
using System.Collections.Generic;
using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using MySpot.Tests.Unit.Shared;
using Shouldly;
using Xunit;

namespace MySpot.Tests.Unit.Services;

public class ReservationsServiceTests
{
    #region ARRANGE

    private readonly IClock _clock;
    private readonly ReservationsService _reservationsService;
    
    public ReservationsServiceTests()
    {
        _clock = new TestClock();
        var weeklyParkingSpots = new List<WeeklyParkingSpot>
        {
            new(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(_clock.Current()), "P1"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(_clock.Current()), "P2"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(_clock.Current()), "P3"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(_clock.Current()), "P4"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P5"),
        };

        _reservationsService = new ReservationsService(_clock, weeklyParkingSpots);
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