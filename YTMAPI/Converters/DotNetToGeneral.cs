using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToGeneral {
        public static List<Thumbnail> GetThumbnails(object[] input) =>
            input?.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new Thumbnail() {
                    URL = dict["url"] as string,
                    Width = (int)dict["width"],
                    Height = (int)dict["height"]
                }).ToList();

        /// <summary>Note this takes "name" and "id" field</summary>
        public static List<ItemBasic> GetSimpleItems(object[] input) =>
            input?.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new ItemBasic() {
                    Title = dict["name"] as string,
                    BrowseID = dict["id"] as string,
                }).ToList();

        public static APIResultResponseContext GetResponseContext(Dictionary<string, object> input) =>
            input == null ? null : new APIResultResponseContext() {
                VisitorData = input["visitorData"] as string,
                ServiceTrackingParams = GetTrackingParams(input["serviceTrackingParams"] as object[]),
            };
        private static List<APIResultTrackingParam> GetTrackingParams(object[] input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new APIResultTrackingParam() {
                    Service = dict["service"] as string,
                    Params = GetParams(dict["params"] as object[])
                }).ToList();
        private static List<APIResultParam> GetParams(object[] input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new APIResultParam() {
                    Key = dict["key"] as string,
                    Value = Helpers.ObjectAsString(dict["value"])
                }).ToList();

        public static List<string> GetText(object[] input) =>
            input.Select(obj => (obj as Dictionary<string, object>)["text"] as string).ToList();
    }
}
