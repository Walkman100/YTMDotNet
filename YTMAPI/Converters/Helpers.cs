using System;
using System.Collections.Generic;
using System.Globalization;

namespace YTMDotNet.YTMAPI.Converters {
    static class Helpers {
        // T-enabled Enum.Parse
        public static TEnum EnumParse<TEnum>(string value, bool ignoreCase = true) where TEnum : struct =>
            (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);

        // get default value instead of exception on missing key: https://stackoverflow.com/a/33223183/2999220
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default) =>
            dict.TryGetValue(key, out TValue value) ? value : defaultValue;

        /// <summary>Duplicate of the <see cref="int.TryParse(string, out int)"/> method except with <see cref="NumberFormatInfo.InvariantInfo"/> instead of <see cref="NumberFormatInfo.CurrentInfo"/></summary>
        public static bool TryParse(string s, out int result) =>
            int.TryParse(s, NumberStyles.Integer | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo, out result);
        /// <summary>Duplicate of the <see cref="ulong.TryParse(string, out ulong)"/> method except with <see cref="NumberFormatInfo.InvariantInfo"/> instead of <see cref="NumberFormatInfo.CurrentInfo"/></summary>
        public static bool TryParse(string s, out ulong result) =>
            ulong.TryParse(s, NumberStyles.Integer | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo, out result);
        /// <summary>Duplicate of the <see cref="double.TryParse(string, out double)"/> method except with <see cref="NumberFormatInfo.InvariantInfo"/> instead of <see cref="NumberFormatInfo.CurrentInfo"/></summary>
        public static bool TryParse(string s, out double result) =>
            double.TryParse(s, NumberStyles.AllowLeadingWhite |
                NumberStyles.AllowTrailingWhite |
                NumberStyles.AllowLeadingSign |
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowThousands |
                NumberStyles.AllowExponent,
                NumberFormatInfo.InvariantInfo, out result);

        /// <summary>Converts <paramref name="input"/> to string using <see cref="NumberFormatInfo.InvariantInfo"/> for <see cref="double"/> and <see cref="int"/></summary>
        public static string ObjectAsString(object input) =>
            input switch {
                double dbl => dbl.ToString(NumberFormatInfo.InvariantInfo),
                int i => i.ToString(NumberFormatInfo.InvariantInfo),
                string str => str,
                _ => input.ToString()
            };
    }
}
