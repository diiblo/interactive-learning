using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSharpInteractive.Api.Controllers;

[ApiController]
[Route("api/reviews")]
public sealed class ReviewsController(SkillProgressService skillProgressService, ProgressService progressService) : ControllerBase
{
    [HttpGet("due")]
    public async Task<ActionResult<IReadOnlyList<ReviewItemDto>>> GetDueReviews()
    {
        var profile = await progressService.GetProfileAsync();
        return Ok(await skillProgressService.GetDueReviewsAsync(profile));
    }
}
