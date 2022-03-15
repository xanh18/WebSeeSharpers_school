#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebSeeSharpers.Models;

namespace WebSeeSharpers.Data
{
    public class WebSeeSharpersContext : DbContext
    {
        public WebSeeSharpersContext(DbContextOptions<WebSeeSharpersContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure one-to-many relationship
            modelBuilder.Entity<Viewing>()
                .HasOne<Movie>(v => v.Movie);

            modelBuilder.Entity<Viewing>()
                .HasOne<Theatre>(v => v.Theatre);

            modelBuilder.Entity<Viewing>()
                .HasMany<ViewingSeat>(v => v.ViewingSeats);
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Theatre> Theatres { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Viewing> Viewings { get; set; }
        public DbSet<ViewingSeat> ViewingSeats { get; set; }
    }
}