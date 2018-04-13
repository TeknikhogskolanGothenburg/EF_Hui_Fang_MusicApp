using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            Console.WriteLine("===============================================");



            //ArtistHelper.AddArtists();
            //AddAlbums();
            //AddSongs();
            //AddAlbumSong();
            //AddVenue();
            //AddTime();
            //AddConcert();
            //AddConcertTime();
            //ProjectionLoadingAlbums();
            //ProjectionLoadingCountingAllAlbums();
            //GetAlbumArtistSongs();
            //DisplayConcertInfo();
            //ArtistHelper.FindArtist();
            //Update();
            //Delete();
            //SelectAlbumSongs();
            //SelectArtistAlbum();
            //SelectConcertTimeVenue();
            //SelectArtist();
            //SelectUsingStoredProcedure();
            //DisplayArtistAlbum();

            //var task = ArtistHelper.GetAllArtistsAsync();
            //Console.WriteLine("I am going to do another task while waiting");
            //Console.WriteLine("Waiting for a little bit more");
            //task.Wait();
            //Console.WriteLine("===============================================");
            //Console.WriteLine("Done.");

            Console.ReadKey();
        }

        

        public static void SelectArtist()
        {
            //one to many
            var context = new MusicContext();
            string sqlCommand = "SELECT * FROM Artists";
            string sqlCommand1 = "SELECT * FROM Albums";
            var artists = context.Artists.FromSql(sqlCommand).ToList();
            var albums = context.Albums.FromSql(sqlCommand1).ToList();

            foreach (var artist in artists)
            {
                Console.WriteLine("\nArtist ===>" + artist.FirstName + " " + artist.LastName);
                foreach (var album in artist.Albums)
                {
                    Console.WriteLine("\tAlbum: " + album.Name);
                }
            }
        }


        public static void SelectUsingStoredProcedure()
        {
            var context = new MusicContext();
            string searchString = "Tour";
            var concerts = context.Concerts.FromSql("EXEC FilterConcertByTitlePart {0}", searchString).ToList();
            foreach(var concert in concerts)
            {
                Console.WriteLine(concert.Name);
            }
        }

        private static void SelectConcertTimeVenue()
        {
            var context = new MusicContext();
            string sqlCommand1 = "SELECT * FROM Times";
            string sqlCommand2 = "SELECT * FROM Concerts";
            var venues = context.Venues.FromSql("SELECT * FROM Venues").ToList();
            var times = context.Times.FromSql(sqlCommand1).ToList();
            var concerts = context.Concerts.FromSql(sqlCommand2).ToList();
            var concertTime = context.ConcertTime.FromSql("SELECT * FROM ConcertTime").ToList();
            foreach(var concert in concerts)
            {
                Console.WriteLine("\nConcert==> " + concert.Name);
                foreach(var time in concert.Times)
                {
                    Console.WriteLine("Time: " + time.Time.StartTime + " Venue: " + time.Time.Venue.Name);
                    
                }
            }
        }

        public static void DisplayArtistAlbum()
        {
            var context = new MusicContext();
            var artists = context.Artists.ToList();
            var albums = context.Albums.ToList();
            var query = artists.Join(
                                albums,
                                artist => artist.Id,
                                album => album.ArtistId,
                                (artist, album) => new
                                {
                                    ArtistID = artist.Id,
                                    AlbumID = album.Id,
                                    ArtistFirstName = artist.FirstName,
                                    ArtistLastName = artist.LastName,
                                    AlbumName = album.Name,
                                    AlbumRelease = album.ReleaseDate
                                });
            foreach(var artist_album in query)
            {
                
                Console.WriteLine("\nArtist: " + artist_album.ArtistFirstName + " " + artist_album.ArtistLastName);
                Console.WriteLine("Album: " + artist_album.AlbumName);
                Console.WriteLine("Album Release Date: "+ artist_album.AlbumRelease);
            }
        }

        public static void SelectArtistAlbum()
        {
            //one to many
            var context = new MusicContext();
            string sqlCommand = "SELECT * FROM Artists";
            string sqlCommand1 = "SELECT * FROM Albums";
            var artists = context.Artists.FromSql(sqlCommand).ToList();
            var albums = context.Albums.FromSql(sqlCommand1).ToList();
            foreach(var artist in artists)
            {
                Console.WriteLine("\nArtist: " + artist.FirstName + " " + artist.LastName);
                foreach(var album in artist.Albums)
                {
                    Console.WriteLine("\n\tAlbums: " + album.Name);
                }
            }

        }

        public static void SelectAlbumSongs()
        {
            //many to many
            var context = new MusicContext();
            string sqlCommand = "SELECT * FROM Albums";
            string sqlCommand1 = "SELECT * FROM AlbumSong";
            string sqlCommand2 = "SELECT * FROM Songs";
            var albums = context.Albums.FromSql(sqlCommand).ToList();
            var songs = context.Songs.FromSql(sqlCommand2).ToList();
            var albumSong = context.AlbumSong.FromSql(sqlCommand1).ToList();   
            foreach(var album in albums)
            {
                Console.WriteLine("\nAlbum ===> " + album.Name);
                foreach (var song in album.Songs )
                {
                    Console.WriteLine("\n\tSongs: " + song.Song.Name);
                };
            }

        }

        public static void FindArtist()
        {
            var context = new MusicContext();
            var artists = context.Artists.ToList();
            var album = context.Albums
                .Where(a => a.ArtistId == a.Artist.Id)
                .FirstOrDefault(a => a.Id == 2);
            Console.WriteLine("Searching result: ");
            Console.WriteLine("The album is {0}, \n which is released by the artist {1} at {2}", album.Name, album.Artist.FirstName + " " + album.Artist.LastName, album.ReleaseDate);
        }

        public static void Delete()
        {
            var context = new MusicContext();
            var album = context.Albums.Find(1);
            context.Albums.Remove(album);
            context.SaveChanges();
        }

        public static void Update()
        {
            var context = new MusicContext();
            var times = context.Times.Find(9);
            times.StartTime = new DateTime(1992, 7, 17, 19, 0, 0);
            context.SaveChanges();

        }

       

        private static void DisplayConcertInfo()
        {
            var context = new MusicContext();
            var concerts = context.Concerts
                .Include(a => a.Times)
                .ThenInclude(c => c.Time)
                .ThenInclude(t => t.Venue)
                .Include(c => c.Artist)
                .ToList();

            foreach(var concert in concerts)
            {
                Console.WriteLine("\nArtist: " + concert.Artist.FirstName + " " + concert.Artist.LastName);
                Console.WriteLine("Concerts: ");
                foreach(var t in concert.Times)
                {
                    Console.WriteLine("\t" + t.Concert.Name + " has been placed in " + t.Time.Venue.Name + " at " + t.Time.StartTime);
                }
            }
        }

        private static void GetAlbumArtistSongs()
        {
            var context = new MusicContext();
            var albums = context.Albums
                .Include(a => a.Songs)
                .ThenInclude(s => s.Song)
                .Include(a => a.Artist)
                .ToList();

            foreach(var album in albums)
            {
                Console.WriteLine("\nAlbum: " + album.Name);
                Console.WriteLine("Artist: " + album.Artist.FirstName + " " + album.Artist.LastName);
                Console.WriteLine("Songs: ");
                foreach(var s in album.Songs)
                {
                    Console.WriteLine("\t"+ s.Song.Name);
                }
            }
        }

        public static void ProjectionLoadingCountingAllAlbums()
        {
            var context = new MusicContext();
            var projectedArtist = context.Artists.Select(a =>
                new { a.FirstName, a.LastName, AlbumCount = a.Albums.Count })
                .Where(a => a.AlbumCount > 1)
                .ToList();
            projectedArtist.ForEach(pa => Console.WriteLine(pa.FirstName + " " + pa.LastName + " has " + pa.AlbumCount+ " albums."));
        }

        public static void ProjectionLoadingAlbums()
        {
            var context = new MusicContext();
            var projectedAlbum = context.Albums
                .OrderByDescending(a => a.ReleaseDate)
                .Select(a => new { a.Name, a.ReleaseDate })
                .Where(a => a.ReleaseDate >  new DateTime(2000, 1, 1))
                .ToList();
            projectedAlbum.ForEach(pa => Console.WriteLine(pa.Name + " has been released in " + pa.ReleaseDate));
               
        }

        public static void AddConcertTime()
        {
            var context = new MusicContext();
            var concert = context.Concerts.First();
            var time1 = context.Times.Find(9);
            var time2 = context.Times.Find(10);
            List<Time> times = new List<Time> { time1, time2 };
            foreach (var time in times)
            {
                context.ConcertTime.Add(new ConcertTime { ConcertId = concert.Id, TimeId = time.Id });
            }
            context.SaveChanges();
        }

        public static void AddConcert()
        {
            var context = new MusicContext();
            var artist = context.Artists.Find(1);
            var concert = new Concert
            {
                Name = "Dangerous World Tour",
                Description = "The Dangerous World Tour was the second worldwide solo tour by American recording artist Michael Jackson. The tour, sponsored by Pepsi-Cola, included 70 performances. All profits were donated to various charities including Jackson's own foundation",
                ArtistId = artist.Id,
            };
            context.Concerts.Add(concert);
            context.SaveChanges();
        }

        public static void AddTime()
        {
            var context = new MusicContext();
            var venue = context.Venues.FirstOrDefault(v => v.Name.StartsWith("Stockholm"));
            if (venue != null)
            {
                venue.Times.Add(new Time { StartTime = new DateTime(1992, 7, 17) });
                venue.Times.Add(new Time { StartTime = new DateTime(1992, 7, 18) });
                context.SaveChanges();
            }
        }

        public static void AddVenue()
        {
            var context = new MusicContext();
            //Venue venue = new Venue { Name = "Stockholm Olympic Stadium", Address = "Lidingövägen", City = "Stockholm", Country = "Sweden" };
            //context.Venues.Add(venue);
            Venue venue1 = new Venue { Name = "Wembley Stadium", Address = "London HA9 0WS", City = "London", Country = "UK" };
            Venue venue2 = new Venue { Name = " Friends Arena", Address = "Råsta Strandväg 1", City = "Stockholm", Country = "Sweden" };
            Venue venue3 = new Venue { Name = "Telenor Arena", Address = "Widerøeveien 1", City = "Oslo", Country = "Norway" };
            Venue venue4 = new Venue { Name = "AccorHotels Arena", Address = "8 Boulevard de Bercy", City = "Paris", Country = "France" };
            Venue venue5 = new Venue { Name = "Citi Field", Address = "120–01 Roosevelt Avenue", City = "New York", Country = "USA" };
            Venue venue6 = new Venue { Name = " Toyota Center", Address = "1510 Polk Street", City = "Houston", Country = "USA" };
            List<Venue> venues = new List<Venue> { venue1, venue2, venue3, venue4, venue5, venue6 };
            context.Venues.AddRange(venues);
            context.SaveChanges();
        }

        public static void AddAlbumSong()
        {
            var context = new MusicContext();
            var album = context.Albums.First();
            var song1 = context.Songs.FirstOrDefault(s => s.Name.StartsWith("Remember"));
            var song2 = context.Songs.FirstOrDefault(s => s.Name.StartsWith("Heal"));
            List<Song> songs = new List<Song> { song1, song2 };
            foreach (var song in songs)
            {
                context.AlbumSong.Add(new AlbumSong { AlbumId = album.Id, SongId = song.Id });
            }
            var album1 = context.Albums.FirstOrDefault(a => a.Name.StartsWith("HIStory"));
            var song3 = context.Songs.FirstOrDefault(s => s.Name.StartsWith("Beat"));
            var song4 = context.Songs.FirstOrDefault(s => s.Name.StartsWith("I Just"));
            List<Song> songs1 = new List<Song> { song2, song3, song4 };
            foreach (var song in songs1)
            {
                context.AlbumSong.Add(new AlbumSong { AlbumId = album1.Id, SongId = song.Id });
            }
            context.SaveChanges();

        }

        public static void AddSongs()
        {
            List<Song> mySongs = new List<Song>
            {
                new Song
                {
                    Name = "Remember the time", Genre = "POP", Duration = new TimeSpan(0,4,0), ReleaseYear = 1991
                },
                new Song
                {
                    Name = "Heal the world", Genre = "POP", Duration = new TimeSpan(0,6,25), ReleaseYear = 1991
                },
                new Song
                {
                    Name = "Beat it", Genre = "POP", Duration = new TimeSpan(0,4,18), ReleaseYear = 1982
                },
                new Song
                {
                    Name = "I Just Can't Stop Loving You", Genre = "POP", Duration = new TimeSpan(0,4,12), ReleaseYear = 1995
                }
            };
            var context = new MusicContext();
            context.Songs.AddRange(mySongs);
            context.SaveChanges();
        }

        public static void AddAlbums()
        {
            var context = new MusicContext();
            var artist = context.Artists.FirstOrDefault(a => a.FirstName.StartsWith("Michael"));
            if (artist != null)
            {
                artist.Albums.Add(new Album { Name = "Dangerous", ReleaseDate = new DateTime(1991, 11, 26) });
                artist.Albums.Add(new Album { Name = "HIStory: Past, Present and Future, Book I", ReleaseDate = new DateTime(1995, 6, 16) });
                context.SaveChanges();
            }
        }

        public static void AddArtists()
        {
            var context = new MusicContext();
            Artist artist1 = new Artist { FirstName = "Michael", LastName = "Jackson", BirthOfDate = new DateTime(1958, 8, 29), Gender = "Male", Country = "USA" };
            Artist artist2 = new Artist { FirstName = "Ed", LastName = "Sheeran", BirthOfDate = new DateTime(1991, 2, 17), Gender = "Male", Country = "UK" };
            Artist artist3 = new Artist { FirstName = "Adele", LastName = "Adkins ", BirthOfDate = new DateTime(1988, 5, 5), Gender = "Female", Country = "UK" };
            Artist artist4 = new Artist { FirstName = "Rihanna", LastName = "Fenty", BirthOfDate = new DateTime(1988, 2, 20), Gender = "Female", Country = "Barbados" };
            Artist artist5 = new Artist { FirstName = "Stefani", LastName = "Germanotta", BirthOfDate = new DateTime(1986, 3, 28), Gender = "Female", Country = "USA" };
            List<Artist> myArtists = new List<Artist> { artist1, artist2, artist3, artist4, artist5 };
            context.Artists.AddRange(myArtists);
            context.SaveChanges();
        }

        

    }
}
