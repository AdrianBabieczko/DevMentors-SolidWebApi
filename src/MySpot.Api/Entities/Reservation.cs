namespace MySpot.Api.Entities;

public class Reservation
{
    public int Id { get; }
    public string EmployeeName { get; private set; }
    public string LicencePlate { get; private set; }
    public DateTime Date { get; private set; }

    public Reservation(int id, string employeeName, string licencePlate, DateTime date)
    {
        Id = id;
        EmployeeName = employeeName;
        LicencePlate = licencePlate;
        Date = date;
    }

    public void ChangeLicencePlate(string licencePlate)
        => LicencePlate = licencePlate;
}