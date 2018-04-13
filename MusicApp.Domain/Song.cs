using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Domain
{
    public class Song
    {
        public Song()
        {
            Albums = new List<AlbumSong>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public TimeSpan Duration { get; set; }
        public List<AlbumSong> Albums { get; set; }
    }
}
