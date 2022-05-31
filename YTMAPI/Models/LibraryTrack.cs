using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class LibraryTrack : ItemWithTokens {
        public bool IsAvailable;
        public bool IsExplicit;
        public string Duration;
        public LikeStatus LikeStatus;

        public string AlbumName;
        public string AlbumID;
        public List<ItemBasic> Artists;
    }
}
