using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AnimalsDb>(opt => opt.UseInMemoryDatabase("Animals"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/", () => "Cześć Świat!");

// Get every animal
app.MapGet("/animals", async (AnimalsDb db) => 
    await db.Animals.ToListAsync());

// Get animal based on id
app.MapGet("/animals/{id}", async (int id, AnimalsDb db) =>
    await db.Animals.FindAsync(id)
        is Animals animal ? Results.Ok(animal) : Results.NotFound());

// Add animal
app.MapPost("/animals", async (Animals animal, AnimalsDb db) =>
{
    db.Animals.Add(animal);
    await db.SaveChangesAsync();

    return Results.Created($"/animals/{animal.Id}", animal);
});

// Edit animal
app.MapPut("/animals/{id}", async (int id, Animals inputAnimal, AnimalsDb db) => 
{
    var animal = await db.Animals.FindAsync(id);

    if (animal is null) return Results.NotFound();

    animal.Name = inputAnimal.Name;
    animal.Category = inputAnimal.Category;
    animal.FurColor = inputAnimal.FurColor;
    animal.Mass = inputAnimal.Mass;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// Delete animal
app.MapDelete("/animals/{id}", async (int id, AnimalsDb db) =>
{
    if (await db.Animals.FindAsync(id) is Animals animal)
    {
        db.Animals.Remove(animal);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();
