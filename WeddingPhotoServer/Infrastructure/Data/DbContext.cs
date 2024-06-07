using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeddingPhotoServer.Model;

namespace WeddingPhotoServer.Infrastrucure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Photo> Photos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
