using CSharpInteractive.Api.Contracts;
using CSharpInteractive.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSharpInteractive.Api.Controllers;

[ApiController]
[Route("api/courses")]
public sealed class CoursesController(CourseCatalogService courseCatalogService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CourseSummaryDto>>> GetCourses() =>
        Ok(await courseCatalogService.GetCoursesAsync());

    [HttpGet("{courseId:int}/map")]
    public async Task<ActionResult<CourseMapDto>> GetMap(int courseId)
    {
        var map = await courseCatalogService.GetMapAsync(courseId);
        return map is null ? NotFound() : map;
    }
}
