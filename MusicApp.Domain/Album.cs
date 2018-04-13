using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Domain
{
    public class Album
    {
        public Album()
        {
            Songs = new List<AlbumSong>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<AlbumSong> Songs { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
