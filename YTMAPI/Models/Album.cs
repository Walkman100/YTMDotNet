using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class Album : Item {
        public int TrackCount;
        public int DurationMS;
        public AlbumReleaseDate ReleaseDate;
        public string Description;

        public List<ItemBasic> Artists;
        public List<AlbumSong> Songs;
    }

    class AlbumReleaseDate {
        public int Year;
        public int Month;
        public int Day;
    }

    class AlbumSong : Item {
        public int Index;
        public string Artists;
        public int LengthMS;
        public LikeStatus LikeStatus;
        public bool IsExplicit;
    }
}
