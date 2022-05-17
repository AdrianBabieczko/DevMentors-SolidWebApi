using Microsoft.AspNetCore.Mvc;

namespace MySpot.Api.Controllers;

[Route("reservations")]
public class ReservationsController : ControllerBase
{
    [HttpGet("get")]
    public void GetReservation()
    {
        
    }
}