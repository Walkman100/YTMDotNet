using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class ArtistBasic : Item {
        public string Subscribers;
        public string ShuffleID;
        public string RadioID;
    }

    class Artist : ArtistBasic {
        public string Description;
        public string Views;
        public bool Subscribed;

        public string TrackBrowseID;
        public string VideoBrowseID;
        public string RelatedBrowseID;
        public string AlbumBrowseID;
        public string AlbumParams;
        public string SinglesBrowseID;
        public string SinglesParams;

        public List<Track> Tracks;
        public List<Video> Videos;
        public List<AlbumMini> Albums;
        public List<AlbumMini> Singles;
        public List<ArtistRelated> Related;
    }

    class ArtistRelated : Item {
        public string Subscribers;
    }
}
