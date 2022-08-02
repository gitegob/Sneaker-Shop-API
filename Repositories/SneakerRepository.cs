using Sneaker_Shop_API.Models;

namespace Sneaker_Shop_API.Repositories;

public class SneakerRepository
{
    private readonly List<Sneaker> sneakers = new List<Sneaker>();
    public void Add(Sneaker sneaker)
    {
        sneakers.Add(sneaker);
    }

    public IEnumerable<Sneaker> GetAll()
    {
        return sneakers;
    }

    public void Remove(Sneaker sneaker)
    {
        sneakers.Remove(sneaker);
    }

    public Sneaker? Find(Guid id)
    {
        return sneakers.Find(sneaker => sneaker.Id == id);
    }
}