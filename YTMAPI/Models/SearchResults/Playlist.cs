namespace YTMDotNet.YTMAPI.Models.SearchResults {
    class Playlist : SearchResult {
        public Playlist() => ItemType = ItemType.Playlist;

        public string Author;
        public int ItemCount;
    }
}
