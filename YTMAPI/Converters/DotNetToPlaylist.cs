using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToPlaylist {
        public static Playlist Get(Dictionary<string, object> input) =>
            new Playlist() {
                BrowseID = input["id"] as string,
                Privacy = Helpers.EnumParse<PrivacyStatus>(input["privacy"] as string),
                Title = input["title"] as string,
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[]),
                Description = input["description"] as string,
                Author = !input.ContainsKey("author") ? null : new ItemBasic() {
                    Title = (input["author"] as Dictionary<string, object>)["name"] as string,
                    BrowseID = (input["author"] as Dictionary<string, object>)["id"] as string
                },
                Duration = input.GetValue("duration") as string,
                TrackCount = (int)input["trackCount"],
                SuggestionsToken = input["suggestions_token"] as string,
                Tracks = GetTracks(input["tracks"] as object[])
            };

        private static List<Track> GetTracks(object[] input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new Track() {
                    BrowseID = dict["videoId"] as string,
                    Title = dict["title"] as string,
                    Artists = DotNetToGeneral.GetSimpleItems(dict["artists"] as object[]),
                    AlbumName = (dict["album"] as Dictionary<string, object>)?["name"] as string,
                    AlbumID = (dict["album"] as Dictionary<string, object>)?["id"] as string,
                    LikeStatus = Helpers.EnumParseNullable<LikeStatus>(dict["likeStatus"] as string) ?? LikeStatus.None,
                    Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnails"] as object[]),
                    IsAvailable = (bool)dict["isAvailable"],
                    IsExplicit = (bool)dict["isExplicit"],
                    Duration = dict.GetValue("duration") as string,
                    UniqueID = dict.GetValue("setVideoId") as string,
                    FeedbackTokenAdd = (dict.GetValue("feedbackTokens") as Dictionary<string, object>)?["add"] as string,
                    FeedbackTokenRemove = (dict.GetValue("feedbackTokens") as Dictionary<string, object>)?["remove"] as string
                }).ToList();
    }
}
