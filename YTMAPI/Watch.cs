using System.Collections.Generic;
using YTMDotNet.YTMAPI.Converters;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI {
    static class Watch {
        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_watch_playlist
        /// <summary>
        /// Get a watch list of tracks. This watch playlist appears when you press play on a track in YouTube Music.
        /// <br />Please note that the INDIFFERENT likeStatus of tracks returned by this endpoint may be either INDIFFERENT or DISLIKE, due to ambiguous data returned by YouTube Music.
        /// </summary>
        /// <param name="videoID">videoId of the played video</param>
        /// <param name="playlistID">playlistId of the played playlist or album</param>
        /// <param name="limit">minimum number of watch playlist items to return</param>
        /// <param name="_params">only used internally by get_watch_playlist_shuffle()</param>
        /// <returns></returns>
        public static WatchPlaylist GetWatchPlaylist(string videoID = null, string playlistID = null, int limit = 25, string _params = null) {
            dynamic get_results;
            using (var YTM = PyYTMAPI.Get()) {
                get_results = YTM.API.get_watch_playlist(videoID, playlistID, limit, _params);
            }
            System.Windows.Forms.MessageBox.Show(get_results.ToString());

            Dictionary<string, object> playlist = ToDotNet.FromDict(get_results);
            System.Windows.Forms.MessageBox.Show(playlist.ToString());

            return DotNetToWatchPlaylist.Get(playlist);
        }
    }
}
