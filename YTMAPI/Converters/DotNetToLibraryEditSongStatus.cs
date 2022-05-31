using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibraryEditSongStatus {
        public static EditSongStatusResult Get(Dictionary<string, object> input) {
            var ResponseContext = input["responseContext"] as Dictionary<string, object>;
            return new EditSongStatusResult() {
                VisitorData = ResponseContext["visitorData"] as string,
                ServiceTrackingParams = GetTrackingParams(ResponseContext["serviceTrackingParams"] as List<object>),
                FeedbackResponses = GetFeedbackResponses(input["feedbackResponses"] as List<object>),
                Actions = GetActions(input["actions"] as List<object>)
            };
        }

        private static List<EditSongStatusResultTrackingParam> GetTrackingParams(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new EditSongStatusResultTrackingParam() {
                    Service = dict["service"] as string,
                    Params = GetParams(dict["params"] as List<object>)
                }).ToList();
        private static List<EditSongStatusResultParam> GetParams(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new EditSongStatusResultParam() {
                    Key = dict["key"] as string,
                    Value = Helpers.ObjectAsString(dict["value"])
                }).ToList();

        private static List<EditSongStatusResultFeedbackResponse> GetFeedbackResponses(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new EditSongStatusResultFeedbackResponse() {
                    IsProcessed = (bool)dict["isProcessed"]
                }).ToList();

        private static List<EditSongStatusResultAction> GetActions(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => {
                    var NotificationActionRenderer = ((dict["addToToastAction"] as Dictionary<string, object>)
                                                        ["item"] as Dictionary<string, object>)
                                                        ["notificationActionRenderer"] as Dictionary<string, object>;
                    return new EditSongStatusResultAction() {
                        ClickTrackingParams = dict["clickTrackingParams"] as string,
                        AddToToastAction_Item_NotificationActionRenderer = new EditSongStatusResultActionNotificationRenderer() {
                            ResponseText_Runs = GetText((NotificationActionRenderer["responseText"] as Dictionary<string, object>)["runs"] as List<object>),
                            TrackingParams = NotificationActionRenderer["trackingParams"] as string
                        }
                    };
                }).ToList();

        private static List<Text> GetText(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new Text() {
                    TextM = dict["text"] as string
                }).ToList();
    }
}
