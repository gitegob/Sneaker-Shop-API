using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Models;
using Sneaker_Shop_API.Services;

namespace Sneaker_Shop_API.Controllers;

[ApiController]
[Route("/api/v1/sneakers")]
[Authorize]
public class SneakerController : ControllerBase
{
    private readonly SneakerService _sneakerService;

    public SneakerController(SneakerService sneakerService)
    {
        _sneakerService = sneakerService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<Page<ViewSneakerDto>>>> GetSneakers(
        [FromQuery] PaginationParams paginationParams)
    {
        var result = await _sneakerService.GetSneakers(paginationParams);
        return Ok(new ApiResponse("Sneakers retrieved",result));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<Sneaker>>> CreateSneaker(CreateSneakerDto sneakerDto)
    {
        var result = await _sneakerService.CreateSneaker(sneakerDto);
        return Created(nameof(CreateSneaker), new ApiResponse("Sneaker retrieved",result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<Sneaker?>>> GetOne(int id)
    {
        var result = await _sneakerService.GetSneaker(id);
        return Ok(new ApiResponse("Sneaker retrieved",result));
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<ApiResponse<Sneaker?>>> UpdateSneaker(int id, UpdateSneakerDto updateSneakerDto)
    {
        var result = await _sneakerService.UpdateSneaker(id, updateSneakerDto);
        return Ok(new ApiResponse("Sneaker retrieved",result));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveSneaker(int id)
    {
        await _sneakerService.RemoveSneaker(id);
        return Ok(new ApiResponse("Sneaker deleted"));
    }
}