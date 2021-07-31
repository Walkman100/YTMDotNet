using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class RemoveHistoryResult {
        public string VisitorData;
        public List<HistoryResultTrackingParam> ServiceTrackingParams;
        public List<FeedbackResponse> FeedbackResponses;
    }

    class HistoryResultTrackingParam {
        public string Service;
        public List<HistoryResultParam> Params;
    }
    class HistoryResultParam {
        public string Key;
        public string Value;
    }

    class FeedbackResponse {
        public bool IsProcessed;
    }
}
