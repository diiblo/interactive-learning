using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSharpInteractive.Api.Controllers;

[ApiController]
[Route("api/skills")]
public sealed class SkillsController(SkillProgressService skillProgressService, ProgressService progressService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SkillDto>>> GetSkills() =>
        Ok(await skillProgressService.GetSkillsAsync());

    [HttpGet("progress")]
    public async Task<ActionResult<IReadOnlyList<SkillProgressDto>>> GetProgress()
    {
        var profile = await progressService.GetProfileAsync();
        return Ok(await skillProgressService.GetProgressAsync(profile));
    }

    [HttpGet("progress/{courseLanguage}")]
    public async Task<ActionResult<IReadOnlyList<SkillProgressDto>>> GetProgressByCourse(string courseLanguage)
    {
        var profile = await progressService.GetProfileAsync();
        return Ok(await skillProgressService.GetProgressAsync(profile, courseLanguage));
    }
}
