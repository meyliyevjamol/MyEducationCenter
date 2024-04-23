using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEducationCenter.LogicLayer;

namespace MyEducationCenter.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _service;

    public OrganizationController(IOrganizationService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult GetList(OrganizationListFilterParams options)
    {
        try
        {
            return Ok( _service.GetListAsync(options));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrganizationCreateDto dto)
    {
        try
        {
            return Ok( await _service.CreateAsync(dto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
