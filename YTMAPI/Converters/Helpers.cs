using System;
using System.Collections.Generic;
using System.Globalization;

namespace YTMDotNet.YTMAPI.Converters {
    static class Helpers {
        // T-enabled Enum.Parse
        public static TEnum EnumParse<TEnum>(string value, bool ignoreCase = true) where TEnum : struct =>
            (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);

        /// <summary>Duplicate of the <see cref="int.TryParse(string, out int)"/> method except with <see cref="NumberFormatInfo.InvariantInfo"/> instead of <see cref="NumberFormatInfo.CurrentInfo"/></summary>
        public static bool TryParse(string s, out int result) =>
            int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result);
        /// <summary>Duplicate of the <see cref="ulong.TryParse(string, out ulong)"/> method except with <see cref="NumberFormatInfo.InvariantInfo"/> instead of <see cref="NumberFormatInfo.CurrentInfo"/></summary>
        public static bool TryParse(string s, out ulong result) =>
            ulong.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result);
        /// <summary>Duplicate of the <see cref="double.TryParse(string, out double)"/> method except with <see cref="NumberFormatInfo.InvariantInfo"/> instead of <see cref="NumberFormatInfo.CurrentInfo"/></summary>
        public static bool TryParse(string s, out double result) =>
            double.TryParse(s, NumberStyles.AllowLeadingWhite |
                NumberStyles.AllowTrailingWhite |
                NumberStyles.AllowLeadingSign |
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowThousands |
                NumberStyles.AllowExponent,
                NumberFormatInfo.InvariantInfo, out result);
    }
}
