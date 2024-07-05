using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
public class ValuesController : Controller
{
    // GET api/value
    [HttpGet]
    public string Get()
    {
        return "value";
    }
}

