using Microsoft.AspNetCore.Mvc;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Models;
using Sneaker_Shop_API.Repositories;

namespace Sneaker_Shop_API.Controllers;

[ApiController]
[Route("/sneakers")]
public class SneakerController : ControllerBase
{
    private readonly SneakerRepository _sneakerRepository;

    public SneakerController(SneakerRepository sneakerRepository)
    {
        _sneakerRepository = sneakerRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Sneaker>> GetSneakers()
    {
        return Ok(_sneakerRepository.GetAll());
    }

    [HttpPost]
    public ActionResult<Sneaker> CreateSneaker(CreateSneakerDto sneaker)
    {
        var newSneaKer = new Sneaker
        {
            Model = sneaker.Model,
            Colors = sneaker.Colors,
            Price = sneaker.Price,
            InStock = sneaker.InStock
        };
        _sneakerRepository.Add(newSneaKer);
        return CreatedAtAction(nameof(CreateSneaker), _sneakerRepository.Find(newSneaKer.Id));
    }

    [HttpGet("{id}")]
    public ActionResult<Sneaker> GetOne(Guid id)
    {
        var sneaker = _sneakerRepository.Find(id);
        if (sneaker == null)
        {
            return NotFound();
        }
        return Ok(sneaker);
    }
}