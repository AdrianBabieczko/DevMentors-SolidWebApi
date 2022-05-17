namespace MySpot.Api.Models;

public class Reservation
{
    public int Id { get; set; }
    public string EmmployeeName { get; set; }
    public string ParkingSpotName { get; set; }
    public string LicencePlate { get; set; }
    public DateTime Date { get; set; }
}