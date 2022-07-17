using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class WatchPlaylist {
        public string PlaylistID;
        public string LyricsID;

        public List<Track_WatchPlaylist> Tracks;
    }
}
