using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class APIResult {
        public APIResultResponseContext ResponseContext;
        public List<APIResultAction> Actions;
        public List<APIResultFeedbackResponse> FeedbackResponses;
        public string TrackingParams;
        public APIResultFrameworkUpdatesEntityBatchUpdate FrameworkUpdates_EntityBatchUpdate;
        public APIResultCommand Command;
    }

    class APIResultResponseContext {
        public string VisitorData;
        public List<APIResultTrackingParam> ServiceTrackingParams;
    }
    class APIResultTrackingParam {
        public string Service;
        public List<APIResultParam> Params;
    }
    class APIResultParam {
        public string Key;
        public string Value;
    }

    class APIResultAction {
        public string ClickTrackingParams;
        public APIResultActionNotificationActionRenderer AddToToastAction_Item_NotificationActionRenderer;
        public APIResultActionNotificationTextRenderer AddToToastAction_Item_NotificationTextRenderer;
        public APIResultActionConfirmDialogRenderer ConfirmDialogEndpoint_Content_ConfirmDialogRenderer;
        public APIResultActionRunAttestationCommand RunAttestationCommand;
        public APIResultActionUpdateSubscribeButtonAction UpdateSubscribeButtonAction;
    }
    class APIResultActionNotificationActionRenderer {
        public List<string> ResponseText_Runs_Text;
        public string ActionButton_ButtonRenderer_Style;
        public bool ActionButton_ButtonRenderer_IsDisabled;
        public List<string> ActionButton_ButtonRenderer_Text_Runs_Text;
        public string ActionButton_ButtonRenderer_NavigationEndpoint_ClickTrackingParams;
        public string ActionButton_ButtonRenderer_NavigationEndpoint_BrowseEndpoint_BrowseID;
        public string ActionButton_ButtonRenderer_TrackingParams;
        public string TrackingParams;
    }
    class APIResultActionNotificationTextRenderer {
        public List<string> SuccessResponseText_Runs_Text;
        public string TrackingParams;
    }
    class APIResultActionConfirmDialogRenderer {
        public List<string> Title_Runs_Text;
        public string TrackingParams;
        public List<List<string>> DialogMessages_Runs_Text;
        public ButtonRenderer ConfirmButton_ButtonRenderer;
        public ButtonRenderer CancelButton_ButtonRenderer;

        public class ButtonRenderer {
            public string Style;
            public string Size;
            public bool IsDisabled;
            public List<string> Text_Runs_Text;
            public string TrackingParams;
            public string Command_ClickTrackingParams;
            public string Command_PlaylistEditEndpoint_PlaylistID;
            public List<ButtonCommandAction> Command_PlaylistEditEndpoint_Actions;
        }

        public class ButtonCommandAction {
            public string Action;
            public string AddedFullListID;
            public string DedupeOption;
        }
    }
    class APIResultActionRunAttestationCommand {
        public List<string> IDs;
        public string EngagementType;
    }
    class APIResultActionUpdateSubscribeButtonAction {
        public bool Subscribed;
        public string ChannelID;
    }

    class APIResultFeedbackResponse {
        public bool IsProcessed;
    }

    class APIResultFrameworkUpdatesEntityBatchUpdate {
        public List<APIResultFrameworkUpdatesEntityBatchUpdateMutation> Mutations;
        public ulong TimestampSeconds;
        public ulong TimestampNanos;
    }
    class APIResultFrameworkUpdatesEntityBatchUpdateMutation {
        public string EntityKey;
        public string Type;
        public string Payload_SubscriptionNotificationStateEntity_Key;
        public string Payload_SubscriptionNotificationStateEntity_State;
    }

    class APIResultCommand {
        public string ClickTrackingParams;
        public string HandlePlaylistDeletionCommand_PlaylistID;
    }
}
