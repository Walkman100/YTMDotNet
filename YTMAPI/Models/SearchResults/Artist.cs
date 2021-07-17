using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models.SearchResults {
    class Artist : SearchResult {
        public Artist() => ItemType = ItemType.Artist;

        public string ShuffleID;
        public string RadioID;
    }
}
