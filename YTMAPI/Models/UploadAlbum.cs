using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class UploadAlbum : Item {
        public string Type;
        public int TrackCount;
        public string Duration;

        public List<ItemBasic> Artists;
        public List<UploadTrack> Tracks;
    }
}
