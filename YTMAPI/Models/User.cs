using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class User : ItemBasic {
        public string VideoBrowseID;
        public string PlaylistBrowseID;
        public string PlaylistParams;

        public List<Video> Videos;
        public List<Item> Playlists;
    }
}
