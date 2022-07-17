using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models.SearchResults {
    class Album : SearchResult {
        public Album() => ItemType = SearchResultType.Album;

        public AlbumType Type;
        public string Duration;
        public int? Year;
        public bool IsExplicit;

        public List<ItemBasic> Artists;
    }
}
