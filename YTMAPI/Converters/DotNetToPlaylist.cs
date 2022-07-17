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
                Tracks = (input["tracks"] as object[]).Select(DotNetToTrack.Get).ToList()
            };
    }
}
