using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibraryHistory {
        public static List<Track_History> Get(IEnumerable<Dictionary<string, object>> input) =>
            input.Select(GetTrack).ToList();

        private static Track_History GetTrack(Dictionary<string, object> input) =>
            new Track_History() {
                BrowseID = input["videoId"] as string,
                Title = input["title"] as string,
                Artists = DotNetToGeneral.GetSimpleItems(input["artists"] as object[]),
                AlbumName = (input["album"] as Dictionary<string, object>)?["name"] as string,
                AlbumID = (input["album"] as Dictionary<string, object>)?["id"] as string,
                LikeStatus = Helpers.EnumParse<LikeStatus>(input["likeStatus"] as string),
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[]),
                IsAvailable = (bool)input["isAvailable"],
                IsExplicit = (bool)input["isExplicit"],
                Duration = input["duration"] as string,
                RemoveHistoryToken = input["feedbackToken"] as string,
                LastPlayed = input["played"] as string
            };
    }
}
