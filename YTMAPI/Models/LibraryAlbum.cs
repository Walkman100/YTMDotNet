using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class LibraryAlbum : Item {
        public int? Year;
        public string Type;

        public List<ItemBasic> Artists;
    }
}
