using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibraryPlaylists {
        public static List<LibraryPlaylist> Get(IEnumerable<Dictionary<string, object>> input) =>
            input.Select(GetPlaylist).ToList();

        private static LibraryPlaylist GetPlaylist(Dictionary<string, object> input) =>
            new LibraryPlaylist() {
                Title = input["title"] as string,
                BrowseID = input["playlistId"] as string,
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as List<object>),
                Count = input.GetValue("count") as int?
            };
    }
}
