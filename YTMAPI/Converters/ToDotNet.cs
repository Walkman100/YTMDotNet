using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Converters {
    static class ToDotNet {
        public static Dictionary<string, object> FromDict(dynamic ytmDict) =>
            convertNamedDict(ytmDict);

        public static IEnumerable<Dictionary<string, object>> FromList(dynamic ytmList) {
            foreach (dynamic item in ytmList)
                yield return convertNamedDict(item);
        }

        #region Private Converters
        private static object toDotNet(dynamic value) {
            string className = value.__class__.__name__;

            return className switch {
                "NoneType" => null,
                "bool" => (bool)value,
                "int" => (int)value,
                "float" => Helpers.ParseDouble(value.ToString()),
                "str" => (string)value,
                "list" => convertUnnamedArray(value),
                "dict" => convertNamedDict(value),
                _ => throw new System.ApplicationException($"Unknown Python Type: {className}")
            };
        }

        private static Dictionary<string, object> convertNamedDict(dynamic value) {
            var items = new Dictionary<string, object>();
            foreach (string key in value)
                items.Add(key, toDotNet(value[key]));
            return items;
        }
        private static object[] convertUnnamedArray(dynamic value) {
            int arrayLength = value.__len__();
            object[] items = new object[arrayLength];

            for (int i = 0; i < arrayLength; i++) {
                items[i] = toDotNet(value[i]);
            }
            return items;
        }
        #endregion
    }
}
