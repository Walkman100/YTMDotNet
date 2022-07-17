namespace YTMDotNet.YTMAPI.Models.SearchResults {
    class Artist : SearchResult {
        public Artist() => ItemType = SearchResultType.Artist;

        public string ShuffleID;
        public string RadioID;
    }
}
