namespace YTMDotNet.YTMAPI.Models.SearchResults {
    class Playlist : SearchResult {
        public Playlist() => ItemType = SearchResultType.Playlist;

        public string Author;
        public string ItemCount;
    }
}
