using MusicApi.DataAccess;
using MusicApi.Dto;
using System.Linq;
using Xunit;

namespace MusicApi.Tests
{
    public class AlbumControllerTests
    {
        //[Fact]
        //public void This_Test_Should_Always_Fail()
        //{
        //    var actual = 2 + 2;
        //    Assert.Equal(5, actual);
        //}

        [Fact]
        public void AlbumCreate_Should_Be_Valid()
        {
            // Arrange
            var album = new AlbumCreate
            {
                Name = "Test",
                ArtistId = MusicDb.Artists.Keys.First(),
                ReleaseDate = "1983",
                TrackIds = new int[] { MusicDb.Tracks.Keys.First() }
            };

            // Act
            var actual = album.Validate();

            // Assert
            Assert.Empty(actual);
        }

        [Fact]
        public void AlbumCreate_Invalid_Name()
        {
            // Arrange
            var albumEmptyName = new AlbumCreate
            {
                Name = "",
            };

            var albumWhitespaceName = new AlbumCreate
            {
                Name = "     ",
            };

            var albumNoName = new AlbumCreate();

            // Act
            var albumEmptyNameValidation = albumEmptyName.Validate();
            var albumWhitespaceNameValidation = albumWhitespaceName.Validate();
            var albumNullNameValidation = albumNoName.Validate();

            // Assert
            Assert.Contains(AlbumCreate.ALBUM_NAME_EMPTY, albumEmptyNameValidation);
            Assert.Contains(AlbumCreate.ALBUM_NAME_EMPTY, albumWhitespaceNameValidation);
            Assert.Contains(AlbumCreate.ALBUM_NAME_EMPTY, albumNullNameValidation);
        }

        [Fact]
        public void AlbumCreate_Invalid_ReleaseDate()
        {
            // Arrange
            var albumEmptyReleaseDate = new AlbumCreate
            {
                ReleaseDate = "",
            };

            var albumWhitespaceReleaseDate = new AlbumCreate
            {
                ReleaseDate = "     ",
            };

            var albumNoReleaseDate = new AlbumCreate();

            // Act
            var albumEmptyReleaseDataValidation = albumEmptyReleaseDate.Validate();
            var albumWhitespaceReleaseDateValidation = albumWhitespaceReleaseDate.Validate();
            var albumNoReleaseDateValidation = albumNoReleaseDate.Validate();

            // Assert
            Assert.Contains(AlbumCreate.RELEASE_DATE_EMPTY, albumEmptyReleaseDataValidation);
            Assert.Contains(AlbumCreate.RELEASE_DATE_EMPTY, albumWhitespaceReleaseDateValidation);
            Assert.Contains(AlbumCreate.RELEASE_DATE_EMPTY, albumNoReleaseDateValidation);
        }

        [Fact]
        public void AlbumCreate_No_Track_Ids()
        {
            // Arrange
            var album = new AlbumCreate
            {
                Name = "Test",
                ArtistId = MusicDb.Artists.Keys.First(),
                ReleaseDate = "1983"
            };

            // Act
            var validation = album.Validate();

            // Assert
            Assert.Contains(AlbumCreate.TRACK_IDS_MISSING, validation);
        }

        [Fact]
        public void AlbumCreate_Empty_Track_Ids()
        {
            // Arrange
            var album = new AlbumCreate
            {
                Name = "Test",
                ArtistId = MusicDb.Artists.Keys.First(),
                ReleaseDate = "1983",
                TrackIds = new int[] { }
            };

            // Act
            var validation = album.Validate();

            // Assert
            Assert.Contains(AlbumCreate.TRACK_IDS_MISSING, validation);
        }

        [Fact]
        public void AlbumCreate_Nonexistant_Track_Id()
        {
            // Arrange
            var album = new AlbumCreate
            {
                Name = "Test",
                ArtistId = MusicDb.Artists.Keys.First(),
                ReleaseDate = "1983",
                TrackIds = new int[] { int.MaxValue }
            };

            // Act
            var validation = album.Validate();

            // Assert
            // check of MusicDb niet toevallig deze artiest al kent, anders geeft de test vals negatief
            Assert.False(MusicDb.Tracks.ContainsKey(album.TrackIds[0]));
            Assert.Contains(string.Format(AlbumCreate.TRACK_ID_INVALID, album.TrackIds[0]), validation);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public void AlbumCreate_Invalid_Track_Ids(int trackId)
        {
            // Arrange
            var album = new AlbumCreate
            {
                Name = "Test",
                ArtistId = MusicDb.Artists.Keys.First(),
                ReleaseDate = "1983",
                TrackIds = new int[] { trackId }
            };

            // Act
            var validation = album.Validate();

            // Assert
            Assert.Contains(string.Format(AlbumCreate.TRACK_ID_INVALID, trackId), validation);
        }
    }
}
