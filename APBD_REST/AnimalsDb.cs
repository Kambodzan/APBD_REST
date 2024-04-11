using Microsoft.EntityFrameworkCore;

class AnimalsDb : DbContext
{
    public AnimalsDb(DbContextOptions<AnimalsDb> options)
        : base(options) {}

    public DbSet<Animals> Animals => Set<Animals>();
}