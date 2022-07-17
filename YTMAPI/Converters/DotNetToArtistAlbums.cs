using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToArtistAlbums {
        public static List<AlbumBasic> Get(IEnumerable<Dictionary<string, object>> input) =>
            input.Select(GetAlbum).ToList();

        private static AlbumBasic GetAlbum(Dictionary<string, object> input) =>
            new AlbumBasic() {
                Title = input["title"] as string,
                BrowseID = input["browseId"] as string,
                Type = Helpers.EnumParse<AlbumType>(input["type"] as string),
                Year = input["year"] as int?,
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[])
            };
    }
}
