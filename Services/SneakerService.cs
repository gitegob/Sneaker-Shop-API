using Microsoft.EntityFrameworkCore;
using Sneaker_Shop_API.Authorization;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Exceptions;
using Sneaker_Shop_API.Models;
using Sneaker_Shop_API.Utils;

namespace Sneaker_Shop_API.Services;

public class SneakerService
{
    private readonly DatabaseContext _db;

    public SneakerService(DatabaseContext dataContext)
    {
        _db = dataContext;
    }

    public async Task<Sneaker> CreateSneaker(CreateSneakerDto sneaker)
    {
        var userId = int.Parse(CurrentUser.Get("id"));
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
        var query = _db.Sneakers.Where(s => !s.IsDeleted).OrderByDescending(s => s.CreatedAt)
            .Select(s => new ViewSneakerDto(s.Id, s.Model, s.Price));
        var result = await PaginationUtil.Paginate(query, paginationParams.Page, paginationParams.Size);
        return result;
    }

    public async Task<ViewSneakerDto?> GetSneaker(int id)
    {
        var sneaker = await GetOne(id);
        if (sneaker == null) throw new NotFoundException("Sneaker not found");
        return new ViewSneakerDto(sneaker.Id,sneaker.Model,sneaker.Price);
    }

    public async Task<Sneaker?> UpdateSneaker(int id, UpdateSneakerDto sneakerDto)
    {
        var sneaker = await GetOne(id);
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
        var sneaker = await GetOne(id);
        if (sneaker != null)
        {
            _db.Sneakers.Remove(sneaker);
            await _db.SaveChangesAsync();
        }
    }
    
    public async Task<Sneaker?> GetOne(int id)
    {
        var product = await _db.Sneakers.FirstOrDefaultAsync(s => !s.IsDeleted && s.Id == id);
        if (product == null) throw new NotFoundException("Sneaker not found");
        return product;
    }
}