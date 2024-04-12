using Microsoft.EntityFrameworkCore;

class AnimalsDb : DbContext
{
    public AnimalsDb(DbContextOptions<AnimalsDb> options) : base(options) {}

    public DbSet<Animals> Animals { get; set; }
    public DbSet<Visits> Visits { get; set; }

}