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
    public class ArtistHelper
    {
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

        public static async Task<List<Artist>> AsyncPerformDatabase()
        {
            //1.SaveChanges begins to push the new artist to the database Once the command is sent to the database 
            //no more compute time is needed on the current managed thread. 
            //The AsyncPerformDatabase method returns (even though it hasn't finished executing) and
            //program flow in the Main method continues. 

            //2."I am going to do another task while waiting.." is written to console. 
            //The managed thread is blocked on the Wait call until the database operation completes. 
            //Once it completes, the remainder of our AsyncPerformDatabase will be executed.

            //3.SaveChanges completes.

            //4.Query for all artists is sent to the database Again, 
            //the managed thread is free to do other work while the query is processed in the database. 
            //all other execution has completed, the thread will just halt on the Wait call though.

            //5.Query results are written to Console.

            //Benefit:
            //Async programming is primarily focused on freeing up the current managed thread(thread running.NET code) to do other work 
            //while it waits for an operation that does not require any compute time from a managed thread.
            //In client applications the current thread can be used to keep the UI responsive 
            //while the async operation is performed.
            //In server applications the thread can be used to process other incoming requests 
            // this can reduce memory usage and/or increase throughput of the server.
            using (var context = new MusicContext())
            {
                //Create a new artist and save it
                context.Artists.Add(new Artist
                {
                    FirstName = "Elvis",
                    LastName = "Presley",
                    BirthOfDate = new DateTime(1935, 1, 8),
                    Country = "USA",
                    Gender = "Male"
                });
                Console.WriteLine("Calling SaveChanges");
                await context.SaveChangesAsync();
                Console.WriteLine("SaveChanges completed.");

                // Query for all artists ordered by name
                Console.WriteLine("\nExecuting query.");
                var artists = await context.Artists
                    .OrderByDescending(a => a.FirstName)
                    .ToListAsync();

                //Print out all artists
                Console.WriteLine("\nQuery completed! The result is: ");
                Console.WriteLine("All artists:");
                foreach (var artist in artists)
                {
                    Console.WriteLine(" - " + artist.FirstName + " " + artist.LastName);
                }

                return artists;
            }


        }
    }
}
