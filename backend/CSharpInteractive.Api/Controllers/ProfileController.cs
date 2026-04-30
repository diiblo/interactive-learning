using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Data;
using CSharpInteractive.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSharpInteractive.Api.Controllers;

[ApiController]
[Route("api/profile")]
public sealed class ProfileController(AppDbContext db, ProgressService progressService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProfileDto>> Get()
    {
        var profile = await progressService.GetProfileAsync();
        return await progressService.ToProfileDtoAsync(profile);
    }

    [HttpPut]
    public async Task<ActionResult<ProfileDto>> Update(UpdateProfileRequest request)
    {
        var profile = await progressService.GetProfileAsync();
        profile.DisplayName = string.IsNullOrWhiteSpace(request.DisplayName) ? "Apprenant" : request.DisplayName.Trim();
        profile.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return await progressService.ToProfileDtoAsync(profile);
    }
}
