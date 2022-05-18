using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;

namespace MySpot.Api.Controllers;

[Route("reservations")]
public class ReservationsController : ControllerBase
{
    private static string[] _parkingSpotNames = {"P1", "P2", "P3", "P4", "P5"};
    private readonly List<Reservation> _reservations = new();

    private static int Id = 1;

    [HttpGet]
    public void Get()
    {
        
    }

    [HttpPost]
    public void Post([FromBody] Reservation reservation)
    {
        reservation.Id = Id;
        reservation.Date = DateTime.Now.AddDays(1).Date;


        if (_parkingSpotNames.All(x => x != reservation.ParkingSpotName))
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        var reservationAlreadyExists = _reservations.Any(x => x.Date.Date == reservation.Date.Date
                                                              && x.ParkingSpotName == reservation.ParkingSpotName);

        if (reservationAlreadyExists)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        Id++;
        _reservations.Add(reservation);
    }

    [HttpPut]
    public void Put()
    {
        
    }
    
    [HttpDelete]
    public void Delete()
    {
        
    }
}