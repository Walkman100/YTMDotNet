using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class EditSongStatusResult {
        public string VisitorData;
        public List<EditSongStatusResultTrackingParam> ServiceTrackingParams;
        public List<EditSongStatusResultFeedbackResponse> FeedbackResponses;
        public List<EditSongStatusResultAction> Actions;
    }

    class EditSongStatusResultTrackingParam {
        public string Service;
        public List<EditSongStatusResultParam> Params;
    }
    class EditSongStatusResultParam {
        public string Key;
        public string Value;
    }

    class EditSongStatusResultFeedbackResponse {
        public bool IsProcessed;
    }

    class EditSongStatusResultAction {
        public string ClickTrackingParams;
        public EditSongStatusResultActionNotificationRenderer AddToToastAction_Item_NotificationActionRenderer;
    }
    class EditSongStatusResultActionNotificationRenderer {
        // Text class is in RateResult.cs
        public List<Text> ResponseText_Runs;
        public string TrackingParams;
    }
}
