using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToAddPlaylistItemsResult {
        public static AddPlaylistItemsResult Get(Dictionary<string, object> input) {
            return new AddPlaylistItemsResult() {
                Status = input["status"] as string,
                PlaylistEditResults = !input.ContainsKey("playlistEditResults") ? null : GetPlaylistEditResults(input["playlistEditResults"] as List<object>),
                ErrorData = GetErrorData(input)
            };
        }

        private static List<AddPlaylistItemsResultEditResult> GetPlaylistEditResults(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new AddPlaylistItemsResultEditResult() {
                    VideoID = dict["videoId"] as string,
                    SetVideoID = dict["setVideoId"] as string
                }).ToList();

        private static AddPlaylistItemsErrorData GetErrorData(Dictionary<string, object> input) {
            if (!input.ContainsKey("responseContext") && !input.ContainsKey("actions"))
                return null;

            var ResponseContext = input.GetValue("responseContext") as Dictionary<string, object>;
            return new AddPlaylistItemsErrorData() {
                VisitorData = ResponseContext?["visitorData"] as string,
                ServiceTrackingParams = ResponseContext == null ? null : GetTrackingParams(ResponseContext["serviceTrackingParams"] as List<object>),
                Actions = !input.ContainsKey("actions") ? null : GetActions(input["actions"] as List<object>),
                TrackingParams = input["trackingParams"] as string
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

        private static List<AddPlaylistItemsErrorDataAction> GetActions(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => {
                    var confirmDialogRenderer = ((dict["confirmDialogEndpoint"] as Dictionary<string, object>)["content"] as Dictionary<string, object>)["confirmDialogRenderer"] as Dictionary<string, object>;
                    return new AddPlaylistItemsErrorDataAction() {
                        ClickTrackingParams = dict["clickTrackingParams"] as string,
                        ConfirmDialogEndpoint_Content_ConfirmDialogRenderer = new AddPlaylistItemsConfirmDialogRenderer() {
                            Title_Runs = GetText((confirmDialogRenderer["title"] as Dictionary<string, object>)["runs"] as List<object>),
                            TrackingParams = confirmDialogRenderer["trackingParams"] as string,
                            DialogMessages_Runs = (confirmDialogRenderer["dialogMessages"] as List<object>).Select(obj => obj as Dictionary<string, object>).Select(dict => GetText(dict["runs"] as List<object>)).ToList(),
                            ConfirmButton_ButtonRenderer = GetRenderer((confirmDialogRenderer["confirmButton"] as Dictionary<string, object>)["buttonRenderer"] as Dictionary<string, object>),
                            CancelButton_ButtonRenderer = GetRenderer((confirmDialogRenderer["cancelButton"] as Dictionary<string, object>)["buttonRenderer"] as Dictionary<string, object>)
                        }
                    };
                }).ToList();

        private static AddPlaylistItemsConfirmDialogRenderer.ButtonRenderer GetRenderer(Dictionary<string, object> input) =>
            new AddPlaylistItemsConfirmDialogRenderer.ButtonRenderer() {
                Style = input["style"] as string,
                Size = input["size"] as string,
                IsDisabled = (bool)input["isDisabled"],
                Text_Runs = GetText((input["text"] as Dictionary<string, object>)["runs"] as List<object>),
                TrackingParams = input["trackingParams"] as string,
                Command_ClickTrackingParams = (input["command"] as Dictionary<string, object>)["clickTrackingParams"] as string,
                Command_PlaylistEditEndpoint_PlaylistID = ((input["command"] as Dictionary<string, object>)["playlistEditEndpoint"] as Dictionary<string, object>)["playlistId"] as string,
                Command_PlaylistEditEndpoint_Actions = GetCommandActions(((input["command"] as Dictionary<string, object>)["playlistEditEndpoint"] as Dictionary<string, object>)["actions"] as List<object>)
            };

        private static List<AddPlaylistItemsConfirmDialogRenderer.ButtonCommandAction> GetCommandActions(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new AddPlaylistItemsConfirmDialogRenderer.ButtonCommandAction() {
                    Action = dict["action"] as string,
                    AddedFullListID = dict.GetValue("addedFullListId") as string,
                    DedupeOption = dict["dedupeOption"] as string
                }).ToList();

        private static List<Text> GetText(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new Text() {
                    TextM = dict["text"] as string
                }).ToList();
    }
}
