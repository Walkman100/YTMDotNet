using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class WatchPlaylist {
        public string PlaylistID;
        public string LyricsID;

        public List<WatchTrack> Tracks;
    }

    class WatchTrack : Item {
        public string Length;
        public int? Year;
        public LikeStatus LikeStatus;
        public List<ItemBasic> Artists;
        public string AlbumName;
        public string AlbumID;
        public string Views;
    }
}
