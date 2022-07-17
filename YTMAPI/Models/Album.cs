using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class AlbumMini : Item {
        public int? Year;
    }

    class AlbumBasic : AlbumMini {
        public AlbumType? Type;
        public int? TrackCount;
        public string Duration;

        public List<ItemBasic> Artists;
    }

    class AlbumFull : AlbumBasic {
        public string Description;

        public List<Track> Tracks;
    }
}
