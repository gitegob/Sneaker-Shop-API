using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Models;
using Sneaker_Shop_API.Services;

namespace Sneaker_Shop_API.Controllers;

[ApiController]
[Route("/api/v1/sneakers")]
[Authorize(Roles = "CLIENT")]
public class SneakerController : ControllerBase
{
    private readonly SneakerService _sneakerService;

    public SneakerController(SneakerService sneakerService)
    {
        _sneakerService = sneakerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sneaker>>> GetSneakers()
    {
        return Ok(await _sneakerService.GetSneakers());
    }

    [HttpPost]
    public async Task<ActionResult<Sneaker>> CreateSneaker(CreateSneakerDto sneakerDto)
    {
        var result = await _sneakerService.CreateSneaker(sneakerDto);
        return Created(nameof(CreateSneaker), result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sneaker>> GetOne(int id)
    {
        return Ok(await _sneakerService.GetSneaker(id));
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<Sneaker?>> UpdateSneaker(int id, UpdateSneakerDto updateSneakerDto)
    {
        var result = await _sneakerService.UpdateSneaker(id, updateSneakerDto);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveSneaker(int id)
    {
        await _sneakerService.RemoveSneaker(id);
        return Ok();
    }
}