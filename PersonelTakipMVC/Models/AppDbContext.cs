using Microsoft.EntityFrameworkCore;
using PersonelTakipMVC.Models.Entities;

namespace PersonelTakipMVC.Models
{
    public class AppDbContext : DbContext
    {

            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<Departman> Departmanlar { get; set; }
            public DbSet<Personel> Personeller { get; set; }
    }
}
