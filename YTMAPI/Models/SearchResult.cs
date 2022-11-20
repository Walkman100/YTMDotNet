namespace YTMDotNet.YTMAPI.Models {
    class SearchResult : Item {
        public SearchResultType ItemType;
        public string Category;
    }

    enum SearchResultType {
        None,
        Track,
        Video,
        Album,
        Artist,
        Playlist
    }
}
