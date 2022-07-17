using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToAddPlaylistItemsResult {
        public static AddPlaylistItemsResult Get(Dictionary<string, object> input) {
            return new AddPlaylistItemsResult() {
                Status = input["status"] as string,
                PlaylistEditResults = !input.ContainsKey("playlistEditResults") ? null : GetPlaylistEditResults(input["playlistEditResults"] as List<object>),
                ErrorData = !input.ContainsKey("responseContext") && !input.ContainsKey("actions") ? null : GetErrorData(input)
            };
        }

        private static List<AddPlaylistItemsResultEditResult> GetPlaylistEditResults(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new AddPlaylistItemsResultEditResult() {
                    VideoID = dict["videoId"] as string,
                    SetVideoID = dict["setVideoId"] as string
                }).ToList();

        private static APIResult GetErrorData(Dictionary<string, object> input) =>
            new APIResult() {
                ResponseContext = DotNetToGeneral.GetResponseContext(input.GetValue("responseContext") as Dictionary<string, object>),
                Actions = !input.ContainsKey("actions") ? null : GetActions(input["actions"] as List<object>),
                TrackingParams = input["trackingParams"] as string
            };

        private static List<APIResultAction> GetActions(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => {
                    var confirmDialogRenderer = ((dict["confirmDialogEndpoint"] as Dictionary<string, object>)["content"] as Dictionary<string, object>)["confirmDialogRenderer"] as Dictionary<string, object>;
                    return new APIResultAction() {
                        ClickTrackingParams = dict["clickTrackingParams"] as string,
                        ConfirmDialogEndpoint_Content_ConfirmDialogRenderer = new APIResultActionConfirmDialogRenderer() {
                            Title_Runs_Text = DotNetToGeneral.GetText((confirmDialogRenderer["title"] as Dictionary<string, object>)["runs"] as List<object>),
                            TrackingParams = confirmDialogRenderer["trackingParams"] as string,
                            DialogMessages_Runs_Text = (confirmDialogRenderer["dialogMessages"] as List<object>).Select(obj => obj as Dictionary<string, object>).Select(dict => DotNetToGeneral.GetText(dict["runs"] as List<object>)).ToList(),
                            ConfirmButton_ButtonRenderer = GetRenderer((confirmDialogRenderer["confirmButton"] as Dictionary<string, object>)["buttonRenderer"] as Dictionary<string, object>),
                            CancelButton_ButtonRenderer = GetRenderer((confirmDialogRenderer["cancelButton"] as Dictionary<string, object>)["buttonRenderer"] as Dictionary<string, object>)
                        }
                    };
                }).ToList();

        private static APIResultActionConfirmDialogRenderer.ButtonRenderer GetRenderer(Dictionary<string, object> input) =>
            new APIResultActionConfirmDialogRenderer.ButtonRenderer() {
                Style = input["style"] as string,
                Size = input["size"] as string,
                IsDisabled = (bool)input["isDisabled"],
                Text_Runs_Text = DotNetToGeneral.GetText((input["text"] as Dictionary<string, object>)["runs"] as List<object>),
                TrackingParams = input["trackingParams"] as string,
                Command_ClickTrackingParams = (input["command"] as Dictionary<string, object>)["clickTrackingParams"] as string,
                Command_PlaylistEditEndpoint_PlaylistID = ((input["command"] as Dictionary<string, object>)["playlistEditEndpoint"] as Dictionary<string, object>)["playlistId"] as string,
                Command_PlaylistEditEndpoint_Actions = GetCommandActions(((input["command"] as Dictionary<string, object>)["playlistEditEndpoint"] as Dictionary<string, object>)["actions"] as List<object>)
            };

        private static List<APIResultActionConfirmDialogRenderer.ButtonCommandAction> GetCommandActions(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new APIResultActionConfirmDialogRenderer.ButtonCommandAction() {
                    Action = dict["action"] as string,
                    AddedFullListID = dict.GetValue("addedFullListId") as string,
                    DedupeOption = dict["dedupeOption"] as string
                }).ToList();
    }
}
