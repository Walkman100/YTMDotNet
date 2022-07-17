namespace YTMDotNet.YTMAPI.Models {
    class SearchResult : Item {
        public SearchResultType ItemType;
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
