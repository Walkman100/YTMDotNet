using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class Playlist : Item {
        public string Privacy;
        public string Description;
        public ItemBasic Author;
        public string Duration;
        public int TrackCount;
        public string SuggestionsToken;
        public List<PlaylistTrack> Tracks;
    }

    class PlaylistTrack : ItemWithTokens {
        public bool IsAvailable;
        public bool IsExplicit;
        public string Duration;
        public string SetVideoID;
        public LikeStatus LikeStatus;

        public string AlbumName;
        public string AlbumID;
        public List<ItemBasic> Artists;
    }
}
