using Microsoft.EntityFrameworkCore;
using PolovniAutomobiliFromScratch.Models;

namespace PolovniAutomobiliFromScratch.Data
{
    public class AutomobiliDbContext : DbContext
    {
        public AutomobiliDbContext(DbContextOptions<AutomobiliDbContext> options) : base(options)

        {

        }
        public DbSet<Automobil> Automobil { get; set; }
    }
}
