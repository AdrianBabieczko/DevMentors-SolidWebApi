using MySpot.Api.ValueObjects;

namespace MySpot.Api.Entities;

public class Reservation
{
    public Guid Id { get; }
    public string EmployeeName { get; private set; }
    public LicencePlate LicencePlate { get; private set; }
    public DateTime Date { get; private set; }

    public Reservation(Guid id, string employeeName, LicencePlate licencePlate, DateTime date)
    {
        Id = id;
        EmployeeName = employeeName;
        LicencePlate = licencePlate;
        Date = date;
    }

    public void ChangeLicencePlate(LicencePlate licencePlate)
        => LicencePlate = licencePlate;
}