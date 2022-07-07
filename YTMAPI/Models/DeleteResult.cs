using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class DeleteResult {
        public string VisitorData;
        public List<RateResultTrackingParam> ServiceTrackingParams;
        public DeleteResultCommand Command;
    }

    class DeleteResultCommand {
        public string ClickTrackingParams;
        public string HandlePlaylistDeletionCommand_PlaylistID;
    }
}
