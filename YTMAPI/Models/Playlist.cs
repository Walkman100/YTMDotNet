using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class PlaylistBasic : Item {
        public int? TrackCount;
    }

    class Playlist : PlaylistBasic {
        public PrivacyStatus? Privacy;
        public string Description;
        public string Duration;
        public string SuggestionsToken;

        public ItemBasic Author;
        public List<Track> Tracks;
    }
}
