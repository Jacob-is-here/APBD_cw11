using System.ComponentModel.DataAnnotations;
using APBD_example_test1_2025.Exceptions;
using Apbd11.DTOs;
using Apbd11.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;



namespace Apbd11.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private IDbService _dbService;
    
    public PrescriptionsController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateNew(PrescriptionDTO prescriptionDto, CancellationToken cancellationToken)
    {
        try 
        {
            await _dbService.Create(prescriptionDto, cancellationToken);
            return Created();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var pacjent = await _dbService.GetPacjent(id);
        
        return Ok(pacjent);
        
    }
}