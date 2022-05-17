using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibraryRateTrack {
        public static RateResult Get(Dictionary<string, object> input) {
            var ResponseContext = input["responseContext"] as Dictionary<string, object>;
            return new RateResult() {
                VisitorData = ResponseContext["visitorData"] as string,
                ServiceTrackingParams = GetTrackingParams(ResponseContext["serviceTrackingParams"] as List<object>),
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

        private static List<RateResultAction> GetActions(List<object> input) => 
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => {
                    var NotificationActionRenderer = ((dict["addToToastAction"] as Dictionary<string, object>)
                                                        ["item"] as Dictionary<string, object>)
                                                        ["notificationActionRenderer"] as Dictionary<string, object>;
                    var NotificationActionRendererActionButtonButtonRenderer = (NotificationActionRenderer["actionButton"] as Dictionary<string, object>)
                                                                                ["buttonRenderer"] as Dictionary<string, object>;
                    return new RateResultAction() {
                        ClickTrackingParams = dict["clickTrackingParams"] as string,
                        AddToToastAction_Item_NotificationActionRenderer = new RateResultActionNotificationRenderer() {
                            ResponseText_Runs = GetText((NotificationActionRenderer["responseText"] as Dictionary<string, object>)["runs"] as List<object>),
                            ActionButton_ButtonRenderer_Style = NotificationActionRendererActionButtonButtonRenderer["style"] as string,
                            ActionButton_ButtonRenderer_IsDisabled = (bool)NotificationActionRendererActionButtonButtonRenderer["isDisabled"],
                            ActionButton_ButtonRenderer_Text_Runs = GetText((NotificationActionRendererActionButtonButtonRenderer["text"] as Dictionary<string, object>)["runs"] as List<object>),
                            ActionButton_ButtonRenderer_NavigationEndpoint_ClickTrackingParams = (NotificationActionRendererActionButtonButtonRenderer["navigationEndpoint"] as Dictionary<string, object>)["clickTrackingParams"] as string,
                            ActionButton_ButtonRenderer_NavigationEndpoint_BrowseEndpoint_BrowseID = ((NotificationActionRendererActionButtonButtonRenderer["navigationEndpoint"] as Dictionary<string, object>)["browseEndpoint"] as Dictionary<string, object>)["browseId"] as string,
                            ActionButton_ButtonRenderer_TrackingParams = NotificationActionRendererActionButtonButtonRenderer["trackingParams"] as string,
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
