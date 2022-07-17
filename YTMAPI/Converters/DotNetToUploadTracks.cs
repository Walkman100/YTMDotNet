using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    class DotNetToUploadTracks {
        public static List<Track> Get(IEnumerable<Dictionary<string, object>> input) =>
            input.Select(GetTrack).ToList();

        private static Track GetTrack(Dictionary<string, object> input) =>
            new Track() {
                UniqueID = input["entityId"] as string,
                BrowseID = input["videoId"] as string,
                Title = input["title"] as string,
                Duration = input["duration"] as string,
                Artists = DotNetToGeneral.GetSimpleItems(input["artists"] as object[]),
                AlbumName = (input["album"] as Dictionary<string, object>)?["name"] as string,
                AlbumID = (input["album"] as Dictionary<string, object>)?["id"] as string,
                LikeStatus = Helpers.EnumParse<LikeStatus>(input["likeStatus"] as string),
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[]),
            };
    }
}
