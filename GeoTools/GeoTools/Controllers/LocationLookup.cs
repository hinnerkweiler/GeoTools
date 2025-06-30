using GeoTools.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoTools.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LocationLookupController(ILogger<LocationLookupController> _logger, LocationLookupService _lookup) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] double lat, [FromQuery] double lon)
    {
        var result = await _lookup.LookupAsync(lat, lon);
        if (result == null)
            return NotFound(new { message = "No marine location found." });

        return Ok(result);
    }
}