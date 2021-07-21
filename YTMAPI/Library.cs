using System.Collections.Generic;
using YTMDotNet.YTMAPI.Converters;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI {
    static class Library {
        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_library_playlists
        /// <summary>Retrieves the playlists in the user’s library.</summary>
        /// <param name="limit">Number of playlists to retrieve</param>
        /// <returns></returns>
        public static List<LibraryPlaylist> GetPlaylists(int limit = 25) {
            dynamic get_results;
            using (var YTM = PyYTMAPI.Get()) {
                get_results = YTM.API.get_library_playlists(limit);
            }
            IEnumerable<Dictionary<string, object>> playlists = ToDotNet.FromList(get_results);
            return DotNetToLibraryPlaylists.Get(playlists);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_library_songs
        /// <summary>
        /// Gets the songs in the user’s library (liked videos are not included). To get liked songs and videos, use get_liked_songs()
        /// </summary>
        /// <param name="limit">Number of songs to retrieve</param>
        /// <param name="validate_responses">Flag indicating if responses from YTM should be validated and retried in case when some songs are missing.</param>
        /// <param name="order">Order of songs to return. Allowed values: "a_to_z", "z_to_a", "recently_added". Default: Default order.</param>
        /// <returns></returns>
        public static List<LibraryTrack> GetTracks(int limit = 25, bool validate_responses = false, string order = null) {
            dynamic get_results;
            using (var YTM = PyYTMAPI.Get()) {
                get_results = YTM.API.get_library_songs(limit: limit, validate_responses: validate_responses, order: order);
            }
            IEnumerable<Dictionary<string, object>> tracks = ToDotNet.FromList(get_results);
            return DotNetToLibraryTracks.Get(tracks);
        }
    }
}