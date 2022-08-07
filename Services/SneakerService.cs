using Microsoft.EntityFrameworkCore;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Exceptions;
using Sneaker_Shop_API.Models;

namespace Sneaker_Shop_API.Services;

public class SneakerService
{
    private readonly DataContext _db;

    public SneakerService(DataContext dataContext)
    {
        _db = dataContext;
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
        _db.Sneakers.Add(newSneaKer);
        await _db.SaveChangesAsync();
        return newSneaKer;
    }

    public async Task<Page<ViewSneakerDto>> GetSneakers(PaginationParams paginationParams)
    {
        var filteredSet = _db.Sneakers.Where(s=>s.Id!=null);
        var sneakers = await filteredSet
            .Skip(paginationParams.Size * paginationParams.Page)
            .Take(paginationParams.Size)
            .Select(s => new ViewSneakerDto(s.Id, s.Model, s.Price))
            .ToListAsync();
        return new Page<ViewSneakerDto>(sneakers, paginationParams.Page, paginationParams.Size,
            filteredSet.Count());
    }

    public async Task<Sneaker?> GetSneaker(int id)
    {
        var sneaker = await _db.Sneakers.FindAsync(id);
        if (sneaker == null) throw new NotFoundException("Sneaker not found");
        return sneaker;
    }

    public async Task<Sneaker?> UpdateSneaker(int id, UpdateSneakerDto sneakerDto)
    {
        var sneaker = await GetSneaker(id);
        if (sneaker == null) return null;
        if (sneakerDto.Model != null) sneaker.Model = sneakerDto.Model;
        if (sneakerDto.Price != null) sneaker.Price = sneakerDto.Price;
        if (sneakerDto.Colors != null) sneaker.Colors = sneakerDto.Colors;
        if (sneakerDto.InStock != null) sneaker.InStock = sneakerDto.InStock;
        await _db.SaveChangesAsync();
        return sneaker;
    }

    public async Task RemoveSneaker(int id)
    {
        var sneaker = await GetSneaker(id);
        if (sneaker != null)
        {
            _db.Sneakers.Remove(sneaker);
            await _db.SaveChangesAsync();
        }
    }
}