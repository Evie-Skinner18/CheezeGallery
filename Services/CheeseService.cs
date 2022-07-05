namespace CheezeGallery.Services;

using System.Threading.Tasks;
using CheezeGallery.Models;
using Microsoft.Azure.Cosmos;

public class CheeseService : ICheeseService
{
    public CheeseService(
        CosmosClient cosmosClient, 
        string dbName, 
        string containerName)
    {
       _container = cosmosClient.GetContainer(dbName, containerName);
    }

    private Container _container;

    public async Task<IEnumerable<Cheese>> GetCheeses(string? query)
    {
        FeedIterator<Cheese> dbQuery = _container
            .GetItemQueryIterator<Cheese>(new QueryDefinition(query));

        List<Cheese> cheeses = new List<Cheese>();
            while (dbQuery.HasMoreResults)
            {
                FeedResponse<Cheese> feedResponse = await dbQuery.ReadNextAsync();
                cheeses.AddRange(feedResponse.ToList());
            }

            return cheeses;
    }

    public async Task CreateCheese(Cheese newCheese)
    {
        await _container.CreateItemAsync<Cheese>(newCheese, new PartitionKey(newCheese.Id));
    }
}