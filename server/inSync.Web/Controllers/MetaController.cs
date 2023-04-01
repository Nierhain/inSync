using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace inSync.Web.Controllers;

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
    public ActionResult GetVersion()
    {
        var assemblyAttribute = typeof(MetaController).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        if (assemblyAttribute is null) return NotFound("Assembly Attribute not found");
        var version = assemblyAttribute.InformationalVersion;
        return Ok(version);
    }

    [HttpGet("environment")]
    public ActionResult GetEnvironment()
    {
        return Ok(_environment);
    }
}