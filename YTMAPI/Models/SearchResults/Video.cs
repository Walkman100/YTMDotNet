using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models.SearchResults {
    class Video : SearchResult {
        public Video() => ItemType = SearchResultType.Video;

        public string Duration;
        public int DurationSeconds;
        public int? Year;
        public string Views;

        public List<ItemBasic> Artists;
    }
}
