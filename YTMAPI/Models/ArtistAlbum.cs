namespace YTMDotNet.YTMAPI.Models {
    class ArtistAlbum : Item {
        public AlbumType Type;
        public int? Year;
    }

    enum AlbumType {
        None,
        Album,
        EP,
        Single
    }
}
