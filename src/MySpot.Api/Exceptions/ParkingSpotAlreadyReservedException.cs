namespace MySpot.Api.Exceptions;

public sealed class ParkingSpotAlreadyReservedException : CustomException
{
    public string ParkingSpot { get; set; }
    public DateTime Date { get; set; }

    public ParkingSpotAlreadyReservedException(string parkingSpot, DateTime date)
        : base($"Parking spot with name {parkingSpot} is already reserved for date: {date}")
    {
        ParkingSpot = parkingSpot;
        Date = date;
    }
}