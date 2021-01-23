using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MusicApi.DataAccess;
using MusicLib;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        // GET api/album
        [HttpGet]
        public IEnumerable<Album> Get()
        {
            return MusicDb.Albums.Values;
        }

        // GET api/album/5
        [HttpGet("{id:int?}")]
        public ActionResult<Album> Get(int id)
        {
            if (MusicDb.Albums.TryGetValue(id, out Album album))
            {
                return Ok(album);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/album/{zoekterm}
        [HttpGet("{zoekTerm}")]
        public ActionResult<IEnumerable<Album>> Find(string zoekTerm)
        {
            var foundAlbums = MusicDb.Albums.Values.Where(x => x.Name.ToLower().Contains(zoekTerm.ToLower()));
            if (foundAlbums.Count() == 0)
                return NotFound();

            return foundAlbums.ToList();
        }

        // POST api/album
        [HttpPost]
        public ActionResult<IEnumerable<string>> Create([FromBody]Dto.AlbumCreate albumCreate)
        {
            var validationResults = albumCreate.Validate();
            if (validationResults.Any())
                return BadRequest(validationResults);

            Album newAlbum = new Album
            {
                Artist = MusicDb.Artists.GetValueOrDefault(albumCreate.ArtistId),
                Name = albumCreate.Name,
                ReleaseDate = albumCreate.ReleaseDate
            };

            newAlbum.Id = MusicDb.Albums.Keys.Max() + 1;
            foreach (var trackId in albumCreate.TrackIds)
            {
                newAlbum.Tracks.Add(MusicDb.Tracks.GetValueOrDefault(trackId));
            }

            MusicDb.Albums.Add(newAlbum.Id, newAlbum);

            return Ok();
        }
    }
}
