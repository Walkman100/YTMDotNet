using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Converters {
    static class ToDotNet {
        public static IEnumerable<Dictionary<string, object>> FromSearchResults(dynamic ytmSearchResults) {
            foreach (dynamic searchResult in ytmSearchResults)
                yield return convertNamedDict(searchResult);
        }

        #region Private Converters
        private static object tryAll(dynamic value) {
            if (tryBasic(value.ToString(), out object result1))
                return result1;
            else if (tryNamedDict(value, out Dictionary<string, object> result2))
                return result2;
            else if (tryUnnamedArray(value, out List<object> result3))
                return result3;
            else
                return value.ToString();
        }

        private static bool tryBasic(string value, out object result) {
            if (value == "None") {
                result = null;
                return true;
            } else if (value == "True") {
                result = true;
                return true;
            } else if (value == "False") {
                result = false;
                return true;
            } else if (Helpers.TryParse(value, out int result2)) {
                result = result2;
                return true;
            } else if (Helpers.TryParse(value, out ulong result3)) {
                result = result3;
                return true;
            } else if (Helpers.TryParse(value, out double result4)) {
                result = result4;
                return true;
            }
            result = null;
            return false;
        }

        private static Dictionary<string, object> convertNamedDict(dynamic value) {
            Dictionary<string, object> items = new();
            foreach (string key in value)
                items.Add(key, tryAll(value[key]));
            return items;
        }
        private static bool tryNamedDict(dynamic value, out Dictionary<string, object> result) {
            try {
                if (!value.ToString().StartsWith("{")) {
                    result = null;
                    return false;
                }
                result = convertNamedDict(value);
                return true;
            } catch {
                result = null;
                return false;
            }
        }

        private static List<object> convertUnnamedArray(dynamic value) {
            List<object> items = new();
            foreach (object item in value)
                items.Add(tryAll(item));
            return items;
        }
        private static bool tryUnnamedArray(dynamic value, out List<object> result) {
            try {
                if (!value.ToString().StartsWith("[")) {
                    result = null;
                    return false;
                }
                result = convertUnnamedArray(value);
                return true;
            } catch {
                result = null;
                return false;
            }
        }
        #endregion
    }
}
