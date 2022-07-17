using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibraryLikedTracks {
        public static Playlist Get(Dictionary<string, object> input) =>
            new Playlist() {
                BrowseID = input["id"] as string,
                Privacy = Helpers.EnumParse<PrivacyStatus>(input["privacy"] as string),
                Title = input["title"] as string,
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[]),
                Description = input["description"] as string,
                TrackCount = (int)input["trackCount"],
                SuggestionsToken = input["suggestions_token"] as string,
                Tracks = (input["tracks"] as object[]).Select(DotNetToTrack.Get).ToList()
            };
    }
}
