using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Domain
{
    public class Concert
    {
        public Concert()
        {
            Times = new List<ConcertTime>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public List<ConcertTime> Times { get; set; }
    }
}
