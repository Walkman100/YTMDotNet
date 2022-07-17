using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    class DotNetToLibraryArtists {
        public static List<ArtistBasic> Get(IEnumerable<Dictionary<string, object>> input) =>
            input.Select(GetArtist).ToList();

        private static ArtistBasic GetArtist(Dictionary<string, object> input) =>
            new ArtistBasic() {
                BrowseID = input["browseId"] as string,
                Title = input["artist"] as string,
                ShuffleID = input["shuffleId"] as string,
                RadioID = input["radioId"] as string,
                Subscribers = Helpers.ObjectAsString(input["subscribers"]),
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[])
            };
    }
}
