using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    class DotNetToLibraryTracks {
        public static List<Track> Get(IEnumerable<Dictionary<string, object>> input) =>
            input.Select(GetTrack).ToList();

        private static Track GetTrack(Dictionary<string, object> input) =>
            new Track() {
                BrowseID = input["videoId"] as string,
                Title = input["title"] as string,
                Artists = DotNetToGeneral.GetSimpleItems(input["artists"] as object[]),
                AlbumName = (input["album"] as Dictionary<string, object>)["name"] as string,
                AlbumID = (input["album"] as Dictionary<string, object>)["id"] as string,
                LikeStatus = Helpers.EnumParse<LikeStatus>(input["likeStatus"] as string),
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[]),
                IsAvailable = (bool)input["isAvailable"],
                IsExplicit = (bool)input["isExplicit"],
                Duration = input["duration"] as string,
                FeedbackTokenAdd = (input["feedbackTokens"] as Dictionary<string, object>)["add"] as string,
                FeedbackTokenRemove = (input["feedbackTokens"] as Dictionary<string, object>)["remove"] as string
            };
    }
}
