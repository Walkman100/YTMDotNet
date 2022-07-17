using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class AddPlaylistItemsResult {
        public string Status;
        public List<AddPlaylistItemsResultEditResult> PlaylistEditResults;
        public APIResult ErrorData;
    }

    class AddPlaylistItemsResultEditResult {
        public string VideoID;
        public string SetVideoID;
    }
}
