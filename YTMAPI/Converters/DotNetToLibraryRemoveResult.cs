using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibraryRemoveResult {
        public static APIResult Get(Dictionary<string, object> input) =>
            new APIResult() {
                ResponseContext = DotNetToGeneral.GetResponseContext(input["responseContext"] as Dictionary<string, object>),
                FeedbackResponses = GetResponses(input["feedbackResponses"] as object[])
            };

        private static List<APIResultFeedbackResponse> GetResponses(object[] input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new APIResultFeedbackResponse() {
                    IsProcessed = (bool)dict["isProcessed"]
                }).ToList();
    }
}
