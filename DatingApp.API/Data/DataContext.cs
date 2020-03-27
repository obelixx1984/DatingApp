using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users {get; set;}
        public DbSet<Photo> Zdjecia { get; set; }
        public DbSet<Lubie> Lajki { get; set; }
        public DbSet<Wiadomosci> Wiadomosc { get; set; }

         protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Lubie>()
                .HasKey(k => new {k.LubiId, k.LubiiId});

            builder.Entity<Lubie>()
                .HasOne(u => u.Lubii)
                .WithMany(u => u.Lubisz)
                .HasForeignKey(u => u.LubiiId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Lubie>()
                .HasOne(u => u.Lubi)
                .WithMany(u => u.Lubic)
                .HasForeignKey(u => u.LubiId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Wiadomosci>()
                .HasOne(u => u.Wyslal)
                .WithMany(m => m.WiadomoscWyslana)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Wiadomosci>()
                .HasOne(u => u.Odbiorca)
                .WithMany(m => m.WiadomosciOdebrane)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}