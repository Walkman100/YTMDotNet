using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibrarySubscribeArtist {
        public static SubscribeResult Get(Dictionary<string, object> input) {
            var ResponseContext = input["responseContext"] as Dictionary<string, object>;
            return new SubscribeResult() {
                VisitorData = ResponseContext["visitorData"] as string,
                ServiceTrackingParams = GetTrackingParams(ResponseContext["serviceTrackingParams"] as List<object>),
                Actions = GetActions(input["actions"] as List<object>),
                TrackingParams = input["trackingParams"] as string,
                FrameworkUpdates_EntityBatchUpdate = !input.ContainsKey("frameworkUpdates") ? null :
                    GetEntityBatchUpdate((input["frameworkUpdates"] as Dictionary<string, object>)["entityBatchUpdate"] as Dictionary<string, object>)
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

        private static List<SubscribeResultAction> GetActions(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => {
                    if (dict.ContainsKey("addToToastAction")) {
                        var NotifictionTextRenderer = ((dict["addToToastAction"] as Dictionary<string, object>)
                                                            ["item"] as Dictionary<string, object>)
                                                            ["notificationTextRenderer"] as Dictionary<string, object>;
                        return new SubscribeResultAction() {
                            ClickTrackingParams = dict["clickTrackingParams"] as string,
                            AddToToastAction_Item_NotificationActionRenderer = new RateResultActionNotificationRenderer() {
                                ResponseText_Runs = GetText((NotifictionTextRenderer["responseText"] as Dictionary<string, object>)["runs"] as List<object>),
                                TrackingParams = NotifictionTextRenderer["trackingParams"] as string
                            }
                        };
                    } else if (dict.ContainsKey("runAttestationCommand")) {
                        var RunAttestationCommand = dict["runAttestationCommand"] as Dictionary<string, object>;
                        return new SubscribeResultAction() {
                            ClickTrackingParams = dict["clickTrackingParams"] as string,
                            RunAttestationCommand = new SubscribeResultActionRunAttestationCommand() {
                                IDs = GetExternalChannelIDs(RunAttestationCommand["ids"] as List<object>),
                                EngagementType = RunAttestationCommand["engagementType"] as string
                            }
                        };
                    } else if (dict.ContainsKey("updateSubscribeButtonAction")) {
                        var UpdateSubscribeButtonAction = dict["updateSubscribeButtonAction"] as Dictionary<string, object>;
                        return new SubscribeResultAction() {
                            ClickTrackingParams = dict["clickTrackingParams"] as string,
                            UpdateSubscribeButtonActiion = new SubscribeResultActionUpdateSubscribeButtonAction() {
                                Subscribed = (bool)UpdateSubscribeButtonAction["subscribed"],
                                ChannelID = UpdateSubscribeButtonAction["channelId"] as string
                            }
                        };
                    } else {
                        throw new KeyNotFoundException("Unrecognised Action");
                    }
                }).ToList();
        private static List<Text> GetText(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new Text() {
                    TextM = dict["text"] as string
                }).ToList();
        private static List<SubscribeResultActionRunAttestationCommandID> GetExternalChannelIDs(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new SubscribeResultActionRunAttestationCommandID() {
                    ExternalChannelID = dict["externalChannelId"] as string
                }).ToList();


        private static SubscribeResultFrameworkUpdatesEntityBatchUpdate GetEntityBatchUpdate(Dictionary<string, object> input) {
            var Timestamp = input["timestamp"] as Dictionary<string, object>;
            return new SubscribeResultFrameworkUpdatesEntityBatchUpdate() {
                Mutations = GetMutations(input["mutations"] as List<object>),
                TimestampSeconds = (ulong)Timestamp["seconds"],
                TimestampNanos = (ulong)Timestamp["nanos"]
            };
        }
        private static List<SubscribeResultFrameworkUpdatesEntityBatchUpdateMutation> GetMutations(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => {
                    var SubscriptionNotificationStateEntity = (dict["payload"] as Dictionary<string, object>)
                                        ["subscriptionNotificationStateEntity"] as Dictionary<string, object>;
                    return new SubscribeResultFrameworkUpdatesEntityBatchUpdateMutation() {
                        EntityKey = dict["entityKey"] as string,
                        Type = dict["type"] as string,
                        Payload_SubscriptionNotificationStateEntity_Key = SubscriptionNotificationStateEntity["key"] as string,
                        Payload_SubscriptionNotificationStateEntity_State = SubscriptionNotificationStateEntity["state"] as string
                    };
                }).ToList();
    }
}
