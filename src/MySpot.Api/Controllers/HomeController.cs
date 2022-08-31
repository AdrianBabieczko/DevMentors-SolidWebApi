using Microsoft.AspNetCore.Mvc;

namespace MySpot.Api.Controllers;

[Route("")]
public class HomeController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string _apiName;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
        _apiName = _configuration["app:name"];
    }

    public ActionResult Get() => Ok(_apiName);
}