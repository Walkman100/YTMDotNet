using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToTrack {
        public static List<Track> Get(IEnumerable<Dictionary<string, object>> input) =>
            input.Select(Get).ToList();

        public static Track Get(object input) =>
            Get(input as Dictionary<string, object>);

        public static Track Get(Dictionary<string, object> dict) =>
            new Track() {
                BrowseID = dict["videoId"] as string,
                Title = dict["title"] as string,
                Artists = DotNetToGeneral.GetSimpleItems(dict["artists"] as object[]),
                AlbumName = (dict["album"] as Dictionary<string, object>)?["name"] as string,
                AlbumID = (dict["album"] as Dictionary<string, object>)?["id"] as string,
                LikeStatus = Helpers.EnumParseNullable<LikeStatus>(dict["likeStatus"] as string) ?? LikeStatus.None,
                Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnails"] as object[]),
                IsAvailable = dict.GetValue("isAvailable") as bool?,
                IsExplicit = dict.GetValue("isExplicit") as bool?,
                Duration = dict.GetValue("duration") as string,
                UniqueID = dict.GetValue("setVideoId") as string ?? dict.GetValue("entityId") as string,
                FeedbackTokenAdd = (dict.GetValue("feedbackTokens") as Dictionary<string, object>)?["add"] as string,
                FeedbackTokenRemove = (dict.GetValue("feedbackTokens") as Dictionary<string, object>)?["remove"] as string
            };
    }
}
