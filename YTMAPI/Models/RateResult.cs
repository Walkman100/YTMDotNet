using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class RateResult {
        public string VisitorData;
        public List<RateResultTrackingParam> ServiceTrackingParams;
        public List<RateResultAction> Actions;
    }

    class RateResultTrackingParam {
        public string Service;
        public List<RateResultParam> Params;
    }
    class RateResultParam {
        public string Key;
        public string Value;
    }

    class RateResultAction {
        public string ClickTrackingParams;
        public RateResultActionNotificationRenderer AddToToastAction_Item_NotificationActionRenderer;
        public RateResultActionNotificationTextRenderer AddToToastAction_Item_NotificationTextRenderer;
    }
    class RateResultActionNotificationRenderer {
        public List<Text> ResponseText_Runs;
        public string ActionButton_ButtonRenderer_Style;
        public bool ActionButton_ButtonRenderer_IsDisabled;
        public List<Text> ActionButton_ButtonRenderer_Text_Runs;
        public string ActionButton_ButtonRenderer_NavigationEndpoint_ClickTrackingParams;
        public string ActionButton_ButtonRenderer_NavigationEndpoint_BrowseEndpoint_BrowseID;
        public string ActionButton_ButtonRenderer_TrackingParams;
        public string TrackingParams;
    }
    class RateResultActionNotificationTextRenderer {
        public List<Text> SuccessResponseText_Runs;
        public string TrackingParams;
    }

    class Text {
        public string TextM;
    }
}
