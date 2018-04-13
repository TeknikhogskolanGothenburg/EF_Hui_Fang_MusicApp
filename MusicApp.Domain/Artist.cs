using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Domain
{
    public class Artist
    {
        public Artist()
        {
            Concerts = new List<Concert>();
            Albums = new List<Album>();

        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthOfDate { get; set; }
        public string Country { get; set; }
        public List<Concert> Concerts { get; set; }
        public List<Album> Albums { get; set; }
    }
}
