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

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.create_playlist
        /// <summary>Creates a new empty playlist and returns its ID</summary>
        /// <param name="title">Playlist title</param>
        /// <param name="description">Playlist description</param>
        /// <param name="privacyStatus">Playlists can be 'PUBLIC', 'PRIVATE', or 'UNLISTED'. Default: 'PRIVATE'</param>
        /// <param name="videoIDs">IDs of songs to create the playlist with</param>
        /// <param name="sourcePlaylist">Another playlist whose songs should be added to the new playlist</param>
        /// <returns>ID of the YouTube playlist or full response if there was an error</returns>
        public static string CreatePlaylist(string title, string description, PrivacyStatus privacyStatus = PrivacyStatus.Private, List<string> videoIDs = null, string sourcePlaylist = null) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.create_playlist(title, description, privacyStatus.ToString().ToUpperInvariant(), videoIDs, sourcePlaylist);
            }
            return get_results;
        }
    }
}
