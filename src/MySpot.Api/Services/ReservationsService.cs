using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;

namespace MySpot.Api.Services;

public sealed class ReservationsService
{
    private static WeeklyParkingSpot[] _weeklyParkingSpots =
    {
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(2), "P1"),
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(2), "P2"),
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(2), "P3"),
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(2), "P4"),
        new WeeklyParkingSpot(Guid.NewGuid(), DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(2), "P5")
    };

    public IEnumerable<ReservationDto> GetAllWeekly()
        => _weeklyParkingSpots.SelectMany(x => x.Reservations).Select(x => new ReservationDto
        {
            Id = x.Id,
            EmployeeName = x.EmployeeName,
            Date = x.Date.Date
        });

    public ReservationDto Get(Guid id)
        => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public Guid? Create(CreateReservation command)
    {
        var (parkingSpotId, reservationId, employeeName, licencePlate, date) = command;

        var weeklyParkingSpot = _weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);

        if (weeklyParkingSpot is null)
        {
            return default;
        }

        var reservation = new Reservation(reservationId, employeeName, licencePlate, date);

        weeklyParkingSpot.AddReservation(reservation);
        return reservation.Id;
    }

    public bool Update(ChangeReservationLicencePlate command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

        if (weeklyParkingSpot is null)
        {
            return false;
        }

        var reservation = weeklyParkingSpot  
    }



    public bool Delete(int id)
    {
        var existingReservation = Reservations.SingleOrDefault(x => x.Id == id);

        if (existingReservation is null)
        {
            return false;
        }

        return Reservations.Remove(existingReservation);
    }

    private object GetWeeklyParkingSpotByReservation(Guid reservationId)
    {
        throw new NotImplementedException();
    }
}