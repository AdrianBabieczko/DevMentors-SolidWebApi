using MySpot.Api.Entities;
using MySpot.Api.ValueObjects;

namespace MySpot.Tests.Unit.Entities;

public class WeeklyParkingSpotTests
{
    public void given_invalid_date_add_reservation_should_fail()
    {
        //ARRANGE
        var weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(DateTimeOffset.Now), "P1");
        //var reservation = new Reservation(Guid.NewGuid(), "Joe Doe",  "XYZ123", )


        //ACT

        //ASSERT
    }
}