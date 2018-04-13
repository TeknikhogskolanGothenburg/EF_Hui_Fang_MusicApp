using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Domain
{
    public class ConcertTime
    {
        public int ConcertId { get; set; }
        public Concert Concert { get; set; }

        public int TimeId { get; set; }
        public Time Time { get; set; }
    }
}
