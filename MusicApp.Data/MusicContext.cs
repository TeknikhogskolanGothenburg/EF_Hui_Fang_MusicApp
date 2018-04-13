using Microsoft.EntityFrameworkCore;
using MusicApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Data
{
    public class MusicContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumSong> AlbumSong { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<ConcertTime> ConcertTime { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlbumSong>().HasKey(m => new { m.AlbumId, m.SongId });
            modelBuilder.Entity<ConcertTime>().HasKey(c => new { c.ConcertId, c.TimeId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = MusicDb; Trusted_Connection = True;");
        }
    }
}
