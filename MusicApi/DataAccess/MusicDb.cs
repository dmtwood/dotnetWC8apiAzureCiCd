using MusicLib;
using System.Collections.Generic;

namespace MusicApi.DataAccess
{
    public static class MusicDb
    {
        public static Dictionary<int, Album> Albums { get; private set; }
        public static Dictionary<int, Track> Tracks { get; private set; }
        public static Dictionary<int, Artist> Artists { get; private set; }

        static MusicDb()
        {
            Artists = new Dictionary<int, Artist>
            {
                { 1,  new Artist { Id = 1, Name = "Mr. Flagio", Bio = "Flagio stands for Flavio Giorgio, the two members of the band: Flavio Vidulich and Giorgio Bacco." } }
            };

            Tracks = new Dictionary<int, Track>
            {
                { 1, new Track { Id = 1, Title = "Take A Chance (Vocal Version)", DurationMs = 476000 } },
                { 2, new Track { Id = 2, Title = "Take A Chance (Instrumental Version)", DurationMs = 465000 } }
            };

            Albums = new Dictionary<int, Album>
            {
                {
                    1,
                    new Album
                    {
                        Id = 1,
                        Artist = Artists.GetValueOrDefault(1),
                        Name = "Take A Chance",
                        ReleaseDate = "1983",
                        Tracks = new List<Track>
                        {
                            Tracks.GetValueOrDefault(1),
                            Tracks.GetValueOrDefault(2)
                        }
                    }
                }
            };
        }
    }
}
