using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class User : ItemBasic {
        public string VideoBrowseID;
        public string PlaylistBrowseID;
        public string PlaylistParams;

        public List<UserVideo> Videos;
        public List<Item> Playlists;
    }

    class UserVideo : Item {
        public string PlaylistID;
        public string Views;
    }
}
