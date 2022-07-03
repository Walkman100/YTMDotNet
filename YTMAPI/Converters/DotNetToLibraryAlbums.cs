using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    class DotNetToLibraryAlbums {
        public static List<LibraryAlbum> Get(IEnumerable<Dictionary<string, object>> input) =>
            input.Select(GetAlbum).ToList();

        private static LibraryAlbum GetAlbum(Dictionary<string, object> input) =>
            new LibraryAlbum() {
                BrowseID = input["browseId"] as string,
                Title = input["title"] as string,
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as List<object>),
                Type = input.GetValue("type") as string,
                Year = input.GetValue("year") as int?,
                Artists = DotNetToGeneral.GetSimpleItems(input.GetValue("artists") as List<object>)
            };
    }
}
