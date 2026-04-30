using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSharpInteractive.Api.Controllers;

[ApiController]
[Route("api/progress")]
public sealed class ProgressController(ProgressService progressService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProgressDto>> Get() => await progressService.GetProgressAsync();
}
