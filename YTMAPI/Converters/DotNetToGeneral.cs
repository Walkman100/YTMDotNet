using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToGeneral {
        public static List<Thumbnail> GetThumbnails(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new Thumbnail() {
                    URL = dict["url"] as string,
                    Width = (int)dict["width"],
                    Height = (int)dict["height"]
                }).ToList();

        /// <summary>Note this takes "name" and "id" field</summary>
        public static List<ItemBasic> GetSimpleItems(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new ItemBasic() {
                    Title = dict["name"] as string,
                    BrowseID = dict["id"] as string,
                }).ToList();
    }
}
