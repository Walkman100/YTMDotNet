using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToUserPlaylists {
        public static List<Item> Get(IEnumerable<Dictionary<string, object>> input) =>
            input.Select(GetPlaylist).ToList();

        private static Item GetPlaylist(Dictionary<string, object> input) =>
            new Item() {
                Title = input["title"] as string,
                BrowseID = input["playlistId"] as string,
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[])
            };
    }
}
