using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToPlaylistDeleteResult {
        public static APIResult Get(Dictionary<string, object> input) {
            var ResponseContext = input["responseContext"] as Dictionary<string, object>;
            return new APIResult() {
                ResponseContext = new APIResultResponseContext() {
                    VisitorData = ResponseContext["visitorData"] as string,
                    ServiceTrackingParams = GetTrackingParams(ResponseContext["serviceTrackingParams"] as List<object>),
                },
                Command = GetCommand(input["command"] as Dictionary<string, object>)
            };
        }

        private static List<APIResultTrackingParam> GetTrackingParams(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new APIResultTrackingParam() {
                    Service = dict["service"] as string,
                    Params = GetParams(dict["params"] as List<object>)
                }).ToList();
        private static List<APIResultParam> GetParams(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new APIResultParam() {
                    Key = dict["key"] as string,
                    Value = Helpers.ObjectAsString(dict["value"])
                }).ToList();

        private static APIResultCommand GetCommand(Dictionary<string, object> input) =>
            new APIResultCommand() {
                ClickTrackingParams = input["clickTrackingParams"] as string,
                HandlePlaylistDeletionCommand_PlaylistID = (input["handlePlaylistDeletionCommand"] as Dictionary<string, object>)["playlistId"] as string,
            };
    }
}
