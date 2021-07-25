using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibraryHistory {
        public static List<HistoryTrack> Get(IEnumerable<Dictionary<string, object>> input) =>
            input.Select(GetTrack).ToList();

        private static HistoryTrack GetTrack(Dictionary<string, object> input) =>
            new HistoryTrack {
                BrowseID = input["videoId"] as string,
                Title = input["title"] as string,
                Artists = DotNetToGeneral.GetSimpleItems(input["artists"] as List<object>),
                AlbumName = (input["album"] as Dictionary<string, object>)?["name"] as string,
                AlbumID = (input["album"] as Dictionary<string, object>)?["id"] as string,
                LikeStatus = Helpers.EnumParse<LikeStatus>(input["likeStatus"] as string),
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as List<object>),
                IsAvailable = (bool)input["isAvailable"],
                IsExplicit = (bool)input["isExplicit"],
                Duration = input["duration"] as string,
                RemoveHistoryToken = input["feedbackToken"] as string,
                LastPlayed = input["played"] as string
            };
    }
}
