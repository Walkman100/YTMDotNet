using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class UploadTrack : Item {
        public string EntityID;
        public string Duration;
        public LikeStatus LikeStatus;

        public string AlbumName;
        public string AlbumID;
        public List<ItemBasic> Artists;
    }
}
