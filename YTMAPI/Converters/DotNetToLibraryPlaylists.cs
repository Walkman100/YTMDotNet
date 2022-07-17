using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibraryPlaylists {
        public static List<PlaylistBasic> Get(IEnumerable<Dictionary<string, object>> input) =>
            input.Select(GetPlaylist).ToList();

        private static PlaylistBasic GetPlaylist(Dictionary<string, object> input) =>
            new PlaylistBasic() {
                Title = input["title"] as string,
                BrowseID = input["playlistId"] as string,
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[]),
                TrackCount = input.GetValue("count") as int?
            };
    }
}
