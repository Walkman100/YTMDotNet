using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibraryRateResult {
        public static APIResult Get(Dictionary<string, object> input) {
            var ResponseContext = input["responseContext"] as Dictionary<string, object>;
            return new APIResult() {
                ResponseContext=new APIResultResponseContext() {
                    VisitorData = ResponseContext["visitorData"] as string,
                    ServiceTrackingParams = GetTrackingParams(ResponseContext["serviceTrackingParams"] as List<object>),
                },
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

        private static List<APIResultAction> GetActions(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => {
                    var AddToToastAction_Item = (dict["addToToastAction"] as Dictionary<string, object>)["item"] as Dictionary<string, object>;
                    if (AddToToastAction_Item.ContainsKey("notificationActionRenderer")) {
                        var NotificationActionRenderer = AddToToastAction_Item["notificationActionRenderer"] as Dictionary<string, object>;
                        var NotificationActionRendererActionButtonButtonRenderer = (NotificationActionRenderer["actionButton"] as Dictionary<string, object>)
                                                                                    ["buttonRenderer"] as Dictionary<string, object>;
                        return new APIResultAction() {
                            ClickTrackingParams = dict["clickTrackingParams"] as string,
                            AddToToastAction_Item_NotificationActionRenderer = new APIResultActionNotificationActionRenderer() {
                                ResponseText_Runs_Text = GetText((NotificationActionRenderer["responseText"] as Dictionary<string, object>)["runs"] as List<object>),
                                ActionButton_ButtonRenderer_Style = NotificationActionRendererActionButtonButtonRenderer["style"] as string,
                                ActionButton_ButtonRenderer_IsDisabled = (bool)NotificationActionRendererActionButtonButtonRenderer["isDisabled"],
                                ActionButton_ButtonRenderer_Text_Runs_Text = GetText((NotificationActionRendererActionButtonButtonRenderer["text"] as Dictionary<string, object>)["runs"] as List<object>),
                                ActionButton_ButtonRenderer_NavigationEndpoint_ClickTrackingParams = (NotificationActionRendererActionButtonButtonRenderer["navigationEndpoint"] as Dictionary<string, object>)["clickTrackingParams"] as string,
                                ActionButton_ButtonRenderer_NavigationEndpoint_BrowseEndpoint_BrowseID = ((NotificationActionRendererActionButtonButtonRenderer["navigationEndpoint"] as Dictionary<string, object>)["browseEndpoint"] as Dictionary<string, object>)["browseId"] as string,
                                ActionButton_ButtonRenderer_TrackingParams = NotificationActionRendererActionButtonButtonRenderer["trackingParams"] as string,
                                TrackingParams = NotificationActionRenderer["trackingParams"] as string
                            }
                        };
                    } else if (AddToToastAction_Item.ContainsKey("notificationTextRenderer")) {
                        var NotificationTextRenderer = AddToToastAction_Item["notificationTextRenderer"] as Dictionary<string, object>;
                        return new APIResultAction() {
                            ClickTrackingParams = dict["clickTrackingParams"] as string,
                            AddToToastAction_Item_NotificationTextRenderer = new APIResultActionNotificationTextRenderer() {
                                SuccessResponseText_Runs_Text = GetText((NotificationTextRenderer["successResponseText"] as Dictionary<string, object>)["runs"] as List<object>),
                                TrackingParams = NotificationTextRenderer["trackingParams"] as string
                            }
                        };
                    } else {
                        return new APIResultAction() {
                            ClickTrackingParams = dict["clickTrackingParams"] as string
                        };
                    }
                }).ToList();

        private static List<string> GetText(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => dict["text"] as string
            ).ToList();
    }
}
