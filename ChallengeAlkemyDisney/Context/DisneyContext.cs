using ChallengeAlkemyDisney.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAlkemyDisney.Context
{
    public class DisneyContext : DbContext
    {
        public DisneyContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Celebrity> Celebrities { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<MovieOrSerie> MovieOrSeries { get; set; }
    }
}
