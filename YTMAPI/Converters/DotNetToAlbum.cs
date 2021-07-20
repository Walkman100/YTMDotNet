using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToAlbum {
        public static Album Get(Dictionary<string, object> input) =>
            new Album() {
                Title = input["title"] as string,
                BrowseID = input["playlistId"] as string,
                TrackCount = (int)input["trackCount"],
                DurationMS = (int)input["durationMs"],
                Description = input["description"] as string,
                ReleaseDate = GetReleaseDate(input["releaseDate"] as Dictionary<string, object>),
                Tracks = GetTracks(input["tracks"] as List<object>),
                Artists = DotNetToGeneral.GetSimpleItems(input["artist"] as List<object>),
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as List<object>)
            };

        private static AlbumReleaseDate GetReleaseDate(Dictionary<string, object> input) =>
            new AlbumReleaseDate() {
                Year = (int)input["year"],
                Month = (int)input["month"],
                Day = (int)input["day"]
            };
        private static List<AlbumTrack> GetTracks(List<object> input) =>
            input?.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new AlbumTrack() {
                    Title = dict["title"] as string,
                    BrowseID = dict["videoId"] as string,
                    Index = (int)dict["index"],
                    Artists = dict["artists"] as string,
                    LengthMS = (int)dict["lengthMs"],
                    LikeStatus = Helpers.EnumParse<LikeStatus>(dict["likeStatus"] as string),
                    IsExplicit = (bool)dict["isExplicit"],
                    Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnails"] as List<object>),
                }).ToList();
    }
}
