using MySpot.Api.Entities;

namespace MySpot.Api.Services;

public sealed class ReservationsService
{
    private static readonly string[] ParkingSpotNames = {"P1", "P2", "P3", "P4", "P5"};
    private static readonly List<Reservation> Reservations = new();

    private static int Id = 1;

    public IEnumerable<Reservation> GetAll() => Reservations;

    public Reservation Get(int id)
        => Reservations.SingleOrDefault(x => x.Id == id);

    public int? Create(Reservation reservation)
    {
        var now = DateTime.UtcNow.Date;
        var pastDays = now.DayOfWeek is DayOfWeek.Sunday ? 7 : (int) now.DayOfWeek;
        var remainingDays = 7 - pastDays;

        if (!(reservation.Date.Date >= now && reservation.Date.Date <= now.AddDays(remainingDays)))
        {
            return default;
        }

        reservation.Id = Id;
        
        if (ParkingSpotNames.All(x => x != reservation.ParkingSpotName))
        {
            return default;
        }

        var reservationAlreadyExists = Reservations.Any(x => x.Date.Date == reservation.Date.Date
                                                             && x.ParkingSpotName == reservation.ParkingSpotName);

        if (reservationAlreadyExists)
        {
            return default;
        }

        Id++;
        Reservations.Add(reservation);
        return reservation.Id;
    }

    public bool Update(Reservation reservation)
    {
        var existingReservation = Reservations.SingleOrDefault(x => x.Id == reservation.Id);

        if (existingReservation is null)
        {
            return false;
        }

        if (existingReservation.Date < DateTime.UtcNow.Date)
        {
            return false;
        }

        existingReservation.LicencePlate = reservation.LicencePlate;
        return true;
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
}