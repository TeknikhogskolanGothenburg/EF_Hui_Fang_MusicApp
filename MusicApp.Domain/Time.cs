using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Domain
{
    public class Time
    {
        public Time()
        {
            Concerts = new List<ConcertTime>();
        }
        public int Id { get; set; }
        public DateTime StartTime { get; set; }

        public int VenueId { get; set; }
        public Venue Venue { get; set; }

        public List<ConcertTime> Concerts { get; set; }
    }
}
