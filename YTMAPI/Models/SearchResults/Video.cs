using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models.SearchResults {
    class Video : SearchResult {
        public Video() => ItemType = ItemType.Video;

        public string Duration;
        public int? Year;
        public string Views;

        public List<ItemBasic> Artists;
    }
}
