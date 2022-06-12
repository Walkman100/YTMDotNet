using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class SubscribeResult {
        public string VisitorData;
        public List<RateResultTrackingParam> ServiceTrackingParams;
        public List<SubscribeResultAction> Actions;
        public string TrackingParams;
        public SubscribeResultFrameworkUpdatesEntityBatchUpdate FrameworkUpdates_EntityBatchUpdate;
    }

    class SubscribeResultAction : RateResultAction {
        public SubscribeResultActionRunAttestationCommand RunAttestationCommand;
        public SubscribeResultActionUpdateSubscribeButtonAction UpdateSubscribeButtonActiion;
    }
    class SubscribeResultActionRunAttestationCommand {
        public List<SubscribeResultActionRunAttestationCommandID> IDs;
        public string EngagementType;
    }
    class SubscribeResultActionRunAttestationCommandID {
        public string ExternalChannelID;
    }
    class SubscribeResultActionUpdateSubscribeButtonAction {
        public bool Subscribed;
        public string ChannelID;
    }

    class SubscribeResultFrameworkUpdatesEntityBatchUpdate {
        public List<SubscribeResultFrameworkUpdatesEntityBatchUpdateMutation> Mutations;
        public ulong TimestampSeconds;
        public ulong TimestampNanos;
    }
    class SubscribeResultFrameworkUpdatesEntityBatchUpdateMutation {
        public string EntityKey;
        public string Type;
        public string Payload_SubscriptionNotificationStateEntity_Key;
        public string Payload_SubscriptionNotificationStateEntity_State;
    }
}
