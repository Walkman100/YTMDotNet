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
    }
}
