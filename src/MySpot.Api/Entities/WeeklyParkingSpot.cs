namespace MySpot.Api.Entities;

public class WeeklyParkingSpot
{
    public Guid Id { get;}
    public DateTime From { get; private set; }
    public DateTime To { get; private set; }
    public string Name { get; private set; }
    public IEnumerable<Reservation> Reservations => _reservations;

    private readonly HashSet<Reservation> _reservations = new();

    public WeeklyParkingSpot(Guid id, DateTime from, DateTime to, string name)
    {
        Id = id;
        From = from;
        To = to;
        Name = name;
    }

    public bool AddReservation(Reservation reservation)
    {
        if (reservation.Date.Date < From || reservation.Date.Date < DateTime.UtcNow.Date)
        {
            throw new 
        }
    }
} 