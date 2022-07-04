using CheezeGallery.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfigurationSection cosmosDbConfigSection = builder.Configuration.GetSection("CosmosDb");
CheeseService cheeseService = await InstantiateCheeseService(cosmosDbConfigSection);

builder.Services.AddSingleton<ICheeseService>(cheeseService);
builder.Services.AddControllersWithViews();

async Task<CheeseService> InstantiateCheeseService(IConfigurationSection configurationSection)
{
    string databaseName = configurationSection.GetSection("DatabaseName").Value;
    string containerName = configurationSection.GetSection("ContainerName").Value;
    string account = configurationSection.GetSection("Account").Value;
    string key = configurationSection.GetSection("Key").Value;

    CosmosClient cosmosClient = new CosmosClient(account, key);
    CheeseService cheeseService = new CheeseService(cosmosClient, databaseName, containerName);

    DatabaseResponse database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

    return cheeseService;
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
