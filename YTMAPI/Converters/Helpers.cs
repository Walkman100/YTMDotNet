using System;
using System.Globalization;

namespace YTMDotNet.YTMAPI.Converters {
    static class Helpers {
        // T-enabled Enum.Parse
        public static TEnum EnumParse<TEnum>(string value, bool ignoreCase = true) where TEnum : struct, Enum =>
            WalkmanLibExtensions.Parse<TEnum>(value, ignoreCase);
        public static TEnum? EnumParseNullable<TEnum>(string value, bool ignoreCase = true) where TEnum : struct, Enum =>
            WalkmanLibExtensions.NullableParseEnum<TEnum>(value, ignoreCase);

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

        /// <summary>Duplicate of the <see cref="double.Parse(string)"/> method except with <see cref="NumberFormatInfo.InvariantInfo"/> instead of <see cref="NumberFormatInfo.CurrentInfo"/></summary>
        public static double ParseDouble(string s) =>
            double.Parse(s, NumberStyles.AllowLeadingWhite |
                NumberStyles.AllowTrailingWhite |
                NumberStyles.AllowLeadingSign |
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowThousands |
                NumberStyles.AllowExponent,
                NumberFormatInfo.InvariantInfo);

        /// <summary>Converts <paramref name="input"/> to string using <see cref="NumberFormatInfo.InvariantInfo"/> for <see cref="double"/> and <see cref="int"/></summary>
        public static string ObjectAsString(object input) =>
            input switch {
                string str => str,
                int i => i.ToString(NumberFormatInfo.InvariantInfo),
                ulong u => u.ToString(NumberFormatInfo.InvariantInfo),
                double dbl => dbl.ToString(NumberFormatInfo.InvariantInfo),
                _ => input.ToString()
            };

        public static ulong ObjectAsULong(object input) =>
            input switch {
                ulong u => u,
                int i => (ulong)i,
                double dbl => (ulong)dbl,
                string str => ulong.Parse(str),
                _ => throw new InvalidCastException($"Cannot convert {input.GetType().FullName} to ulong")
            };

        public static string OrderToString(Models.Order order) =>
            order == Models.Order.Default ? null : order.ToString().ToLowerInvariant();
    }
}
