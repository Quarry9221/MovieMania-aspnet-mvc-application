using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MovieMania.Models;
using TMDbLib.Objects.General;

namespace MovieMania.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });
            modelBuilder.Entity<Genre_Movie>().HasKey(gm => new
            {
                gm.GenreId,
                gm.MovieId
            });

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ActorId);


            modelBuilder.Entity<Genre_Movie>().HasOne(g => g.Movie).WithMany(gm => gm.Genremovies).HasForeignKey(g => g.MovieId);
            modelBuilder.Entity<Genre_Movie>().HasOne(gm => gm.Genre).WithMany(m => m.Genremovies).HasForeignKey(gm => gm.GenreId);




            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Genre_Movie> GenreMovies { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Rates> Rates { get; set; }

    }
}
