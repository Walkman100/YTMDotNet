using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLibraryRemoveResult {
        public static RemoveHistoryResult Get(Dictionary<string, object> input) {
            var ResponseContext = input["responseContext"] as Dictionary<string, object>;
            return new RemoveHistoryResult() {
                VisitorData = ResponseContext["visitorData"] as string,
                ServiceTrackingParams = GetTrackingParams(ResponseContext["serviceTrackingParams"] as List<object>),
                FeedbackResponses = GetResponses(input["feedbackResponses"] as List<object>)
            };
        }

        private static List<HistoryResultTrackingParam> GetTrackingParams(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new HistoryResultTrackingParam() {
                    Service = dict["service"] as string,
                    Params = GetParams(dict["params"] as List<object>)
                }).ToList();
        private static List<HistoryResultParam> GetParams(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new HistoryResultParam() {
                    Key = dict["key"] as string,
                    Value = ObjectAsString(dict["value"])
                }).ToList();
        private static string ObjectAsString(object input) {
            if (input is double dbl)
                return dbl.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
            if (input is int i)
                return i.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
            if (input is string str)
                return str;
            return input.ToString();
        }

        private static List<FeedbackResponse> GetResponses(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new FeedbackResponse() {
                    IsProcessed = (bool)dict["isProcessed"]
                }).ToList();
    }
}