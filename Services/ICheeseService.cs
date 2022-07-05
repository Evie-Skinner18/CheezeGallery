namespace CheezeGallery.Services;

using CheezeGallery.Models;

public interface ICheeseService
{
    Task<IEnumerable<Cheese>> GetCheeses(string? query);
    Task CreateCheese();
}