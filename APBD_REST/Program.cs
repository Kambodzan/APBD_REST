using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AnimalsDb>(options => options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/", () => "Cześć Świat!");

// Get every animal
app.MapGet("/animals", async (AnimalsDb db) => 
    await db.Animals.ToListAsync());

// Get animal by id
app.MapGet("/animals/{id}", async (int id, AnimalsDb db) =>
    await db.Animals.FindAsync(id)
        is Animals animal ? Results.Ok(animal) : Results.NotFound());

// Get animal visit by id
app.MapGet("/visit/{id}", async (int id, AnimalsDb db) => 
{
    var data = await db.Visits.Where(e => e.AnimalID == id).ToListAsync();

    if (data is null) return Results.NotFound();

    return Results.Ok(data);

});

// Add visit
app.MapPost("/visit", async (Visits visit, AnimalsDb db) =>
{
    
    var animal = await db.Animals.FindAsync(visit.AnimalID);
    if (animal is null) return Results.NotFound("No animal found");

    db.Visits.Add(visit);
    await db.SaveChangesAsync();

    return Results.Created($"/visit/{visit.Id}",visit);
    
});    

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
