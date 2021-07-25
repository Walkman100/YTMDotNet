using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class Playlist : Item {
        public string Privacy;
        public string Description;
        public int TrackCount;
        public string SuggestionsToken;
        public List<PlaylistTrack> Tracks;
    }

    class PlaylistTrack : Item {
        public bool IsAvailable;
        public bool IsExplicit;
        public string Duration;
        public LikeStatus LikeStatus;

        public string AlbumName;
        public string AlbumID;
        public List<ItemBasic> Artists;
    }
}
