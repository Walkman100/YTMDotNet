using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models.SearchResults {
    class Track : SearchResult {
        public Track() => ItemType = SearchResultType.Track;

        public string Duration;
        public int DurationSeconds;
        public int? Year;
        public bool IsExplicit;

        public string AlbumName;
        public string AlbumID;
        public List<ItemBasic> Artists;
    }
}
