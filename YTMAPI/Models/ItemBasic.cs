using System.Diagnostics;

namespace YTMDotNet.YTMAPI.Models {
    [DebuggerDisplay("{GetType().Name}: {Title}")]
    class ItemBasic {
        public string BrowseID;
        public string Title;
    }
}
