using System.Collections.Generic;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToLyrics {
        public static Lyrics Get(Dictionary<string, object> input) =>
            new Lyrics() {
                LyricsString = input["lyrics"] as string,
                Source = input["source"] as string
            };
    }
}
