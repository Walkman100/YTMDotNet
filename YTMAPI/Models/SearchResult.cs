namespace YTMDotNet.YTMAPI.Models {
    class SearchResult : Item {
        public ItemType ItemType;
    }

    enum ItemType {
        None,
        Track,
        Video,
        Album,
        Artist,
        Playlist
    }
}
