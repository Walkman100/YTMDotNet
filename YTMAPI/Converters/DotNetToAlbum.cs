using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToAlbum {
        public static AlbumFull Get(Dictionary<string, object> input) =>
            new AlbumFull() {
                Title = input["title"] as string,
                Type = Helpers.EnumParse<AlbumType>(input["type"] as string),
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as List<object>),
                Description = input.GetValue("description") as string,
                Artists = DotNetToGeneral.GetSimpleItems(input["artists"] as List<object>),
                Year = WalkmanLibExtensions.NullableParseInt(input["year"] as string),
                TrackCount = (int)input["trackCount"],
                Duration = input["duration"] as string,
                BrowseID = input["audioPlaylistId"] as string,
                Tracks = GetTracks(input["tracks"] as List<object>),
            };

        private static List<Track> GetTracks(List<object> input) =>
            input?.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new Track() {
                    BrowseID = dict["videoId"] as string,
                    Title = dict["title"] as string,
                    Artists = DotNetToGeneral.GetSimpleItems(dict["artists"] as List<object>),
                    AlbumName = dict["album"]?.ToString(),
                    LikeStatus = Helpers.EnumParse<LikeStatus>(dict["likeStatus"] as string),
                    Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnails"] as List<object>),
                    IsAvailable = (bool)dict["isAvailable"],
                    IsExplicit = (bool)dict["isExplicit"],
                    Duration = dict["duration"] as string,
                    FeedbackTokenAdd = (dict["feedbackTokens"] as Dictionary<string, object>)["add"] as string,
                    FeedbackTokenRemove = (dict["feedbackTokens"] as Dictionary<string, object>)["remove"] as string
                }).ToList();
    }
}
