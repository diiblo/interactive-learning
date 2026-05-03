using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSharpInteractive.Api.Controllers;

[ApiController]
[Route("api/progress")]
public sealed class ProgressController(ProgressService progressService, SkillProgressService skillProgressService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProgressDto>> Get() => await progressService.GetProgressAsync();

    [HttpPost("review-completed")]
    public async Task<ActionResult> CompleteReview(ReviewCompletedRequest request)
    {
        var profile = await progressService.GetProfileAsync();
        await skillProgressService.MarkReviewCompletedAsync(profile, request.SkillId);
        return NoContent();
    }
}
