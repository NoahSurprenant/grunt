using Microsoft.AspNetCore.Mvc;
using Surprenant.Grunt.Core;
using Surprenant.Grunt.Models.HaloInfinite;
using Surprenant.Grunt.Util;

namespace Grunt.ApiTest.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class AdminController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<AdminController> _logger;
    private readonly HaloInfiniteClientFactory _haloInfiniteClientFactory;
    private readonly IStateSeed _stateSeed;
    private readonly IAccountAuthorization _accountAuthorization;

    public AdminController(ILogger<AdminController> logger, HaloInfiniteClientFactory haloInfiniteClientFactory, IAccountAuthorization accountAuthorization, IStateSeed stateSeed)
    {
        _logger = logger;
        _haloInfiniteClientFactory = haloInfiniteClientFactory;
        _accountAuthorization = accountAuthorization;
        _stateSeed = stateSeed;
    }

    [HttpGet(Name = "MatchStats")]
    public async Task<MatchStats> Get()
    {
        var c = await _haloInfiniteClientFactory.CreateAsync();

        var response = await c.StatsGetMatchStats("21416434-4717-4966-9902-af7097469f74");

        var xuids = response.Result.Players.Select(x => x.PlayerId.Replace("xuid(", "").Replace(")", "")).ToList();

        var players = await c.Users(xuids);

        return response.Result;
    }

    [HttpGet(Name = "SetCode")]
    public async Task<ActionResult> SetCode([FromQuery] string code, [FromQuery] string state)
    {
        if (string.IsNullOrWhiteSpace(code))
            return BadRequest("Missing code");

        if (string.IsNullOrWhiteSpace(state))
            return BadRequest("Missing state");

        if (state != _stateSeed.State.ToString())
            return BadRequest("Invalid state");

        await _accountAuthorization.SetCodeAsync(code);

        return Ok("Code saved");
    }
}
