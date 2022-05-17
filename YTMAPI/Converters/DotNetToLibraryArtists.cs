using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    class DotNetToLibraryArtists {
        public static List<LibraryArtist> Get(IEnumerable<Dictionary<string, object>> input) =>
            input.Select(GetArtist).ToList();

        private static LibraryArtist GetArtist(Dictionary<string, object> input) =>
            new LibraryArtist() {
                BrowseID = input["browseId"] as string,
                Title = input["artist"] as string,
                ShuffleID = input["shuffleId"] as string,
                RadioID = input["radioId"] as string,
                Subscribers = Helpers.ObjectAsString(input["subscribers"]),
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as List<object>)
            };
    }
}
