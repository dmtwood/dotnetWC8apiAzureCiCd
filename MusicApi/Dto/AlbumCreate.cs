using MusicApi.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace MusicApi.Dto
{
    public class AlbumCreate : IHasValidation
    { 
        public const string ALBUM_NAME_EMPTY = "Album name should not be empty.";
        public const string RELEASE_DATE_EMPTY = "Release date should not be empty.";
        public const string ARTIST_ID_INVALID = "Invalid artist id.";
        public const string TRACK_ID_INVALID = "Invalid track id: {0}";
        public const string TRACK_IDS_MISSING = "No track ids provided.";

        public string Name { get; set; }
        public string ReleaseDate { get; set; }
        public int ArtistId { get; set; }
        public int[] TrackIds { get; set; }

        public IEnumerable<string> Validate()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Name))
                errors.Add(ALBUM_NAME_EMPTY);

            if (string.IsNullOrWhiteSpace(ReleaseDate))
                errors.Add(RELEASE_DATE_EMPTY);

            if (ArtistId < 1 || !MusicDb.Artists.ContainsKey(ArtistId))
            {
                errors.Add(ARTIST_ID_INVALID);
            }

            if (TrackIds == null || TrackIds.Count() == 0)
            {
                errors.Add(TRACK_IDS_MISSING);
            }
            else
            {
                foreach (var trackId in TrackIds)
                {
                    if (trackId < 1 || !MusicDb.Tracks.ContainsKey(trackId))
                    {
                        errors.Add(string.Format(TRACK_ID_INVALID, trackId));
                    }
                }
            }

            return errors;
        }
    }
}
