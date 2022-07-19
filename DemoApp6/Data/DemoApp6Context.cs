using Microsoft.EntityFrameworkCore;
using DemoApp6.Models;

namespace DemoApp6.Data
{
    public class DemoApp6Context : DbContext
    {
        public DemoApp6Context (DbContextOptions<DemoApp6Context> options)
            : base(options)
        {
        }

        public DbSet<Movies> Movies { get; set; }
    }
}
