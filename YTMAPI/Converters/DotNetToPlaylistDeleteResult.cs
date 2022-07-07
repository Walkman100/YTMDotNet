using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToPlaylistDeleteResult {
        public static DeleteResult Get(Dictionary<string, object> input) {
            var ResponseContext = input["responseContext"] as Dictionary<string, object>;
            return new DeleteResult() {
                VisitorData = ResponseContext["visitorData"] as string,
                ServiceTrackingParams = GetTrackingParams(ResponseContext["serviceTrackingParams"] as List<object>),
                Command = GetCommand(input["command"] as Dictionary<string, object>)
            };
        }

        private static List<RateResultTrackingParam> GetTrackingParams(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new RateResultTrackingParam() {
                    Service = dict["service"] as string,
                    Params = GetParams(dict["params"] as List<object>)
                }).ToList();
        private static List<RateResultParam> GetParams(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new RateResultParam() {
                    Key = dict["key"] as string,
                    Value = Helpers.ObjectAsString(dict["value"])
                }).ToList();

        private static DeleteResultCommand GetCommand(Dictionary<string, object> input) =>
            new DeleteResultCommand() {
                ClickTrackingParams = input["clickTrackingParams"] as string,
                HandlePlaylistDeletionCommand_PlaylistID = (input["handlePlaylistDeletionCommand"] as Dictionary<string, object>)["playlistId"] as string,
            };
    }
}
