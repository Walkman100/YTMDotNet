using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibraryEditTrackStatus {
        public static APIResult Get(Dictionary<string, object> input) {
            var ResponseContext = input["responseContext"] as Dictionary<string, object>;
            return new APIResult() {
                ResponseContext = new APIResultResponseContext() {
                    VisitorData = ResponseContext["visitorData"] as string,
                    ServiceTrackingParams = GetTrackingParams(ResponseContext["serviceTrackingParams"] as List<object>),
                },
                FeedbackResponses = GetFeedbackResponses(input["feedbackResponses"] as List<object>),
                Actions = GetActions(input["actions"] as List<object>)
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

        private static List<APIResultFeedbackResponse> GetFeedbackResponses(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new APIResultFeedbackResponse() {
                    IsProcessed = (bool)dict["isProcessed"]
                }).ToList();

        private static List<APIResultAction> GetActions(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => {
                    var NotificationActionRenderer = ((dict["addToToastAction"] as Dictionary<string, object>)
                                                        ["item"] as Dictionary<string, object>)
                                                        ["notificationActionRenderer"] as Dictionary<string, object>;
                    return new APIResultAction() {
                        ClickTrackingParams = dict["clickTrackingParams"] as string,
                        AddToToastAction_Item_NotificationActionRenderer = new APIResultActionNotificationActionRenderer() {
                            ResponseText_Runs_Text = GetText((NotificationActionRenderer["responseText"] as Dictionary<string, object>)["runs"] as List<object>),
                            TrackingParams = NotificationActionRenderer["trackingParams"] as string
                        }
                    };
                }).ToList();

        private static List<string> GetText(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => dict["text"] as string
            ).ToList();
    }
}
