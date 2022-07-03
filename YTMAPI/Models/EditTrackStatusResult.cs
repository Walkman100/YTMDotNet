using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class EditTrackStatusResult {
        public string VisitorData;
        public List<RateResultTrackingParam> ServiceTrackingParams;
        public List<EditTrackStatusResultFeedbackResponse> FeedbackResponses;
        public List<RateResultAction> Actions;
    }

    class EditTrackStatusResultFeedbackResponse {
        public bool IsProcessed;
    }
}
