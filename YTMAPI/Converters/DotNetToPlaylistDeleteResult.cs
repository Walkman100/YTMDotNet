using System.Collections.Generic;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToPlaylistDeleteResult {
        public static APIResult Get(Dictionary<string, object> input) =>
            new APIResult() {
                ResponseContext = DotNetToGeneral.GetResponseContext(input["responseContext"] as Dictionary<string, object>),
                Command = GetCommand(input["command"] as Dictionary<string, object>)
            };

        private static APIResultCommand GetCommand(Dictionary<string, object> input) =>
            new APIResultCommand() {
                ClickTrackingParams = input["clickTrackingParams"] as string,
                HandlePlaylistDeletionCommand_PlaylistID = (input["handlePlaylistDeletionCommand"] as Dictionary<string, object>)["playlistId"] as string,
            };
    }
}
