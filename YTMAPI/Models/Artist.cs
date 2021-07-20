using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class Artist : Item {
        public string Description;
        public string Views;
        public string Subscribers;
        public bool Subscribed;

        public string ShuffleID;
        public string RadioID;

        public string TrackBrowseID;
        public string VideoBrowseID;
        public string RelatedBrowseID;
        public string AlbumBrowseID;
        public string AlbumParams;
        public string SinglesBrowseID;
        public string SinglesParams;

        public List<ArtistTrack> Tracks;
        public List<ArtistVideo> Videos;
        public List<ArtistMiniAlbum> Albums;
        public List<ArtistSingle> Singles;
        public List<ArtistRelated> Related;
    }

    class ArtistTrack : Item {
        public bool IsExplicit;
        public bool IsAvailable;
        public LikeStatus LikeStatus;

        public string AlbumName;
        public string AlbumID;
        public List<ItemBasic> Artists;

    }
    class ArtistVideo : Item {
        public string Views;

        public string PlaylistID;
    }
    class ArtistMiniAlbum : Item {
        public int? Year;
    }
    class ArtistSingle : Item {
        public int? Year;
    }
    class ArtistRelated : Item {
        public string Subscribers;
    }
}
