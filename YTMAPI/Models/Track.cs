using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class Track : ItemWithTokens {
        public bool? IsAvailable;
        public bool? IsExplicit;
        public string Duration;
        public string UniqueID; // EntityID for Upload Tracks and SetVideoID for Playlist Tracks
        public LikeStatus LikeStatus;

        public string AlbumName;
        public string AlbumID;
        public List<ItemBasic> Artists;
    }

    class Track_WatchPlaylist : Track {
        public string Length;
        public int? Year;
        public string Views;
    }

    class Track_History : Track {
        public string LastPlayed;
        public string RemoveHistoryToken;
    }
}
