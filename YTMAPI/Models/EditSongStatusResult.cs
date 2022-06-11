using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class EditSongStatusResult {
        public string VisitorData;
        public List<RateResultTrackingParam> ServiceTrackingParams;
        public List<EditSongStatusResultFeedbackResponse> FeedbackResponses;
        public List<RateResultAction> Actions;
    }

    class EditSongStatusResultFeedbackResponse {
        public bool IsProcessed;
    }
}
