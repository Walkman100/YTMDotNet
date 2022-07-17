using System.Collections.Generic;
using System.Diagnostics;

namespace YTMDotNet.YTMAPI.Models {
    [DebuggerDisplay("{GetType().Name}: {Title}")]
    class ItemBasic {
        public string BrowseID;
        public string Title;
    }

    class Item : ItemBasic {
        public List<Thumbnail> Thumbnails;
    }

    class ItemWithTokens : Item {
        public string FeedbackTokenAdd;
        public string FeedbackTokenRemove;
    }

    class Thumbnail {
        public string URL;
        public int Width;
        public int Height;
    }
}
