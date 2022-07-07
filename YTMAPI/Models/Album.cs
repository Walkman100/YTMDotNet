using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class Album : Item {
        public string Type;
        public string Description;
        public string Year;
        public int TrackCount;
        public string Duration;

        public List<ItemBasic> Artists;
        public List<AlbumTrack> Tracks;
    }

    class AlbumTrack : ItemWithTokens {
        public string Artists;
        public string Album;
        public LikeStatus LikeStatus;
        public bool IsAvailable;
        public bool IsExplicit;
        public string Duration;
    }
}
