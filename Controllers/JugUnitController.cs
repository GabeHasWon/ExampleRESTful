using Microsoft.AspNetCore.Mvc;

namespace ExampleRESTful.Controllers;

[ApiController]
[Route("[controller]")]
public class JugUnitController(ILogger<JugStateController> logger) : ControllerBase
{
    private readonly static DeliverState[] TestStates = 
    [
        new DeliverState() { Left = 9, Right = 6, Target = 3 },
        new DeliverState() { Left = 8, Right = 2, Target = 7 },
        new DeliverState() { Left = 5, Right = 29, Target = 13 },
        new DeliverState() { Left = 3, Right = -1, Target = 0 }
    ];

    private readonly ILogger<JugStateController> _logger = logger;

    /// <summary>
    /// Runs one of 4 preset tests.<br/>
    /// 1: Left: 9, Right: 6, Target: 3 - outputs a 2-step solution
    /// 2: Left: 8, Right: 2, Target: 7 - outputs a 1-step invalid solution
    /// 3: Left: 5, Right: 29, Target: 13 - outputs a 20-step solution
    /// 4: Left: 3, Right: -1, Target: 0 - outputs a 1-step invalid solution
    /// </summary>
    /// <param name="test"></param>
    /// <returns></returns>
    [HttpPost(Name = "UnitTest")]
    public IActionResult DeliverInfo([FromQuery] int test) => test >= TestStates.Length ? BadRequest() : RedirectToRoute("SolveJugState", TestStates[test]);
}
