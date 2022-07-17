using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToAlbumBasic {
        public static List<AlbumBasic> Get(IEnumerable<Dictionary<string, object>> input) =>
            input?.Select(GetAlbum).ToList();

        private static AlbumBasic GetAlbum(Dictionary<string, object> input) =>
            new AlbumBasic() {
                Title = input["title"] as string,
                BrowseID = input["browseId"] as string,
                Type = Helpers.EnumParseNullable<AlbumType>(input.GetValue("type") as string),
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[]),
                Year = input.GetValue("year") as int?,
                Artists = DotNetToGeneral.GetSimpleItems(input.GetValue("artists") as object[])
            };
    }
}
