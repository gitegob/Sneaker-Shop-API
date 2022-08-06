using Microsoft.EntityFrameworkCore;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Models;

namespace Sneaker_Shop_API.Services;

public class SneakerService
{
    private readonly DataContext _dataContext;
    public SneakerService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Sneaker> CreateSneaker(CreateSneakerDto sneaker)
    {
        var newSneaKer = new Sneaker
        {
            Model = sneaker.Model,
            Colors = sneaker.Colors,
            Price = sneaker.Price,
            InStock = sneaker.InStock
        };
        _dataContext.Sneakers.Add(newSneaKer);
        await _dataContext.SaveChangesAsync();
        return newSneaKer;
    }

    public async Task<List<Sneaker>> GetSneakers()
    {
        return await _dataContext.Sneakers.ToListAsync();
    }

    public async Task<Sneaker?> GetSneaker(int id)
    {
        return await _dataContext.Sneakers.FindAsync(id);
    }

    public async Task<Sneaker?> UpdateSneaker(int id, UpdateSneakerDto sneakerDto)
    {
        var sneaker = await GetSneaker(id);
        if (sneaker == null) return null;
        if (sneakerDto.Model != null) sneaker.Model = sneakerDto.Model;
        if (sneakerDto.Price != null) sneaker.Price = sneakerDto.Price;
        if (sneakerDto.Colors != null) sneaker.Colors = sneakerDto.Colors;
        if (sneakerDto.InStock != null) sneaker.InStock = sneakerDto.InStock;
        await _dataContext.SaveChangesAsync();
        return sneaker;
    }

    public async Task RemoveSneaker(int id)
    {
        var sneaker = await GetSneaker(id);
        if (sneaker != null)
        {
            _dataContext.Sneakers.Remove(sneaker);
            await _dataContext.SaveChangesAsync();

        }
    }
}