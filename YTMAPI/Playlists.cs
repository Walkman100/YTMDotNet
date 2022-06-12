using System.Collections.Generic;
using YTMDotNet.YTMAPI.Converters;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI {
    static class Playlists {
        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_playlist
        /// <summary>Returns a list of playlist items</summary>
        /// <param name="playlistID">Playlist id</param>
        /// <param name="limit">How many songs to return. Default: 100</param>
        /// <returns></returns>
        public static Playlist GetPlaylist(string playlistID, int limit = 100) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_playlist(playlistID, limit);
            }
            Dictionary<string, object> playlist = ToDotNet.FromDict(get_results);
            return DotNetToPlaylist.Get(playlist);
        }
    }
}
