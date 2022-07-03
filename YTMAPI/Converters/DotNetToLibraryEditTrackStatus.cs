using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibraryEditTrackStatus {
        public static EditTrackStatusResult Get(Dictionary<string, object> input) {
            var ResponseContext = input["responseContext"] as Dictionary<string, object>;
            return new EditTrackStatusResult() {
                VisitorData = ResponseContext["visitorData"] as string,
                ServiceTrackingParams = GetTrackingParams(ResponseContext["serviceTrackingParams"] as List<object>),
                FeedbackResponses = GetFeedbackResponses(input["feedbackResponses"] as List<object>),
                Actions = GetActions(input["actions"] as List<object>)
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

        private static List<EditTrackStatusResultFeedbackResponse> GetFeedbackResponses(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new EditTrackStatusResultFeedbackResponse() {
                    IsProcessed = (bool)dict["isProcessed"]
                }).ToList();

        private static List<RateResultAction> GetActions(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => {
                    var NotificationActionRenderer = ((dict["addToToastAction"] as Dictionary<string, object>)
                                                        ["item"] as Dictionary<string, object>)
                                                        ["notificationActionRenderer"] as Dictionary<string, object>;
                    return new RateResultAction() {
                        ClickTrackingParams = dict["clickTrackingParams"] as string,
                        AddToToastAction_Item_NotificationActionRenderer = new RateResultActionNotificationRenderer() {
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
