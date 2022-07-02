using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class AddPlaylistItemsResult {
        public string Status;
        public List<AddPlaylistItemsResultEditResult> PlaylistEditResults;
        public AddPlaylistItemsErrorData ErrorData;
    }

    class AddPlaylistItemsResultEditResult {
        public string VideoID;
        public string SetVideoID;
    }

    class AddPlaylistItemsErrorData {
        public string VisitorData;
        public List<RateResultTrackingParam> ServiceTrackingParams;
        public List<AddPlaylistItemsErrorDataAction> Actions;
        public string TrackingParams;
    }
    class AddPlaylistItemsErrorDataAction {
        public string ClickTrackingParams;
        public AddPlaylistItemsConfirmDialogRenderer ConfirmDialogEndpoint_Content_ConfirmDialogRenderer;
    }
    class AddPlaylistItemsConfirmDialogRenderer {
        public List<Text> Title_Runs;
        public string TrackingParams;
        public List<List<Text>> DialogMessages_Runs;
        public ButtonRenderer ConfirmButton_ButtonRenderer;
        public ButtonRenderer CancelButton_ButtonRenderer;

        public class ButtonRenderer {
            public string Style;
            public string Size;
            public bool IsDisabled;
            public List<Text> Text_Runs;
            public string TrackingParams;
            public string Command_ClickTrackingParams;
            public string Command_PlaylistEditEndpoint_PlaylistID;
            public List<ButtonCommandAction> Command_PlaylistEditEndpoint_Actions;
        }

        public class ButtonCommandAction {
            public string Action;
            public string AddedFullListID;
            public string DedupeOption;
        }
    }
}
