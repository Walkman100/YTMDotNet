using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class LibraryTrack : Item {
        public bool IsAvailable;
        public bool IsExplicit;
        public string Duration;
        public LikeStatus LikeStatus;

        public string AlbumName;
        public string AlbumID;
        public List<ItemBasic> Artists;
    }
}
