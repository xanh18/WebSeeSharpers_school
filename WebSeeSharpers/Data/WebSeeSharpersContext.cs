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
        public WebSeeSharpersContext (DbContextOptions<WebSeeSharpersContext> options)
            : base(options)
        {
        }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Theatre> Theatres{ get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Viewing> Viewings { get; set; }
        public DbSet<ViewingSeat> ViewingSeats { get; set; }
    }
}
