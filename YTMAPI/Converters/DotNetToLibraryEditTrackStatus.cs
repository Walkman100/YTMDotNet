using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibraryEditTrackStatus {
        public static APIResult Get(Dictionary<string, object> input) =>
            new APIResult() {
                ResponseContext = DotNetToGeneral.GetResponseContext(input["responseContext"] as Dictionary<string, object>),
                FeedbackResponses = GetFeedbackResponses(input["feedbackResponses"] as List<object>),
                Actions = GetActions(input["actions"] as List<object>)
            };

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
                            ResponseText_Runs_Text = DotNetToGeneral.GetText((NotificationActionRenderer["responseText"] as Dictionary<string, object>)["runs"] as List<object>),
                            TrackingParams = NotificationActionRenderer["trackingParams"] as string
                        }
                    };
                }).ToList();
    }
}
