using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibrarySubscribeArtist {
        public static APIResult Get(Dictionary<string, object> input) =>
            new APIResult() {
                ResponseContext = DotNetToGeneral.GetResponseContext(input["responseContext"] as Dictionary<string, object>),
                Actions = GetActions(input["actions"] as List<object>),
                TrackingParams = input["trackingParams"] as string,
                FrameworkUpdates_EntityBatchUpdate = !input.ContainsKey("frameworkUpdates") ? null :
                    GetEntityBatchUpdate((input["frameworkUpdates"] as Dictionary<string, object>)["entityBatchUpdate"] as Dictionary<string, object>)
            };

        private static List<APIResultAction> GetActions(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => {
                    if (dict.ContainsKey("addToToastAction")) {
                        var NotifictionTextRenderer = ((dict["addToToastAction"] as Dictionary<string, object>)
                                                            ["item"] as Dictionary<string, object>)
                                                            ["notificationTextRenderer"] as Dictionary<string, object>;
                        return new APIResultAction() {
                            ClickTrackingParams = dict["clickTrackingParams"] as string,
                            AddToToastAction_Item_NotificationActionRenderer = new APIResultActionNotificationActionRenderer() {
                                ResponseText_Runs_Text = DotNetToGeneral.GetText((NotifictionTextRenderer["successResponseText"] as Dictionary<string, object>)["runs"] as List<object>),
                                TrackingParams = NotifictionTextRenderer["trackingParams"] as string
                            }
                        };
                    } else if (dict.ContainsKey("runAttestationCommand")) {
                        var RunAttestationCommand = dict["runAttestationCommand"] as Dictionary<string, object>;
                        return new APIResultAction() {
                            ClickTrackingParams = dict["clickTrackingParams"] as string,
                            RunAttestationCommand = new APIResultActionRunAttestationCommand() {
                                IDs = GetExternalChannelIDs(RunAttestationCommand["ids"] as List<object>),
                                EngagementType = RunAttestationCommand["engagementType"] as string
                            }
                        };
                    } else if (dict.ContainsKey("updateSubscribeButtonAction")) {
                        var UpdateSubscribeButtonAction = dict["updateSubscribeButtonAction"] as Dictionary<string, object>;
                        return new APIResultAction() {
                            ClickTrackingParams = dict["clickTrackingParams"] as string,
                            UpdateSubscribeButtonAction = new APIResultActionUpdateSubscribeButtonAction() {
                                Subscribed = (bool)UpdateSubscribeButtonAction["subscribed"],
                                ChannelID = UpdateSubscribeButtonAction["channelId"] as string
                            }
                        };
                    } else {
                        throw new KeyNotFoundException("Unrecognised Action");
                    }
                }).ToList();
        private static List<string> GetExternalChannelIDs(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => dict["externalChannelId"] as string
            ).ToList();


        private static APIResultFrameworkUpdatesEntityBatchUpdate GetEntityBatchUpdate(Dictionary<string, object> input) {
            var Timestamp = input["timestamp"] as Dictionary<string, object>;
            return new APIResultFrameworkUpdatesEntityBatchUpdate() {
                Mutations = GetMutations(input["mutations"] as List<object>),
                TimestampSeconds = Helpers.ObjectAsULong(Timestamp["seconds"]),
                TimestampNanos = Helpers.ObjectAsULong(Timestamp["nanos"])
            };
        }
        private static List<APIResultFrameworkUpdatesEntityBatchUpdateMutation> GetMutations(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => {
                    var SubscriptionNotificationStateEntity = (dict["payload"] as Dictionary<string, object>)
                                        ["subscriptionNotificationStateEntity"] as Dictionary<string, object>;
                    return new APIResultFrameworkUpdatesEntityBatchUpdateMutation() {
                        EntityKey = dict["entityKey"] as string,
                        Type = dict["type"] as string,
                        Payload_SubscriptionNotificationStateEntity_Key = SubscriptionNotificationStateEntity["key"] as string,
                        Payload_SubscriptionNotificationStateEntity_State = SubscriptionNotificationStateEntity["state"] as string
                    };
                }).ToList();
    }
}
