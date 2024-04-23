﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEducationCenter.LogicLayer;

namespace MyEducationCenter.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;

    public RoleController(IRoleService service)
    {
        _service = service;
    }




    [HttpPost]
    public IActionResult GetList(RoleListFilterParams options)
    {
        try
        {
            return Ok(_service.GetListAsync(options));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoleCreateDto dto)
    {
        try
        {
            return Ok(await _service.CreateAsync(dto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpPut]
    public async Task<IActionResult> Update([FromBody] RoleUpdateDto dto)
    {
        try
        {
            return Ok(await _service.UpdateAsync(dto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpDelete]
    public IActionResult Delete(int id)
    {
        try
        {
            _service.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
