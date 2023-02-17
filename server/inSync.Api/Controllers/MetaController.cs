using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace inSync.Api.Controllers;

[ApiController]
[Route("api/meta")]
[Produces("application/json")]
public class MetaController : ControllerBase
{
    private readonly string _environment;

    public MetaController(IHostEnvironment env)
    {
        _environment = env.EnvironmentName;
    }

    [HttpGet("version")]
    public async Task<IActionResult> GetVersion()
    {
        var version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            .InformationalVersion;
        return Ok(version);
    }

    [HttpGet("environment")]
    public IActionResult GetEnvironment()
    {
        return Ok(_environment);
    }
}