using System.Collections.Generic;
using YTMDotNet.YTMAPI.Converters;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI {
    static class Library {
        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_library_playlists
        /// <summary>Retrieves the playlists in the user's library.</summary>
        /// <param name="limit">Number of playlists to retrieve</param>
        /// <returns></returns>
        public static List<LibraryPlaylist> GetPlaylists(int limit = 25) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_library_playlists(limit);
            }
            IEnumerable<Dictionary<string, object>> playlists = ToDotNet.FromList(get_results);
            return DotNetToLibraryPlaylists.Get(playlists);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_library_songs
        /// <summary>Gets the songs in the user's library (liked videos are not included). To get liked songs and videos, use get_liked_songs()</summary>
        /// <param name="limit">Number of songs to retrieve</param>
        /// <param name="validate_responses">Flag indicating if responses from YTM should be validated and retried in case when some songs are missing.</param>
        /// <param name="order">Order of songs to return. Allowed values: "a_to_z", "z_to_a", "recently_added". Default: Default order.</param>
        /// <returns></returns>
        public static List<LibraryTrack> GetTracks(int limit = 25, bool validate_responses = false, string order = null) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_library_songs(limit: limit, validate_responses: validate_responses, order: order);
            }
            IEnumerable<Dictionary<string, object>> tracks = ToDotNet.FromList(get_results);
            return DotNetToLibraryTracks.Get(tracks);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_library_artists
        /// <summary>Gets the artists of the songs in the user's library.</summary>
        /// <param name="limit">Number of artists to return</param>
        /// <param name="order">Order of artists to return. Allowed values: "a_to_z", "z_to_a", "recently_added". Default: Default order.</param>
        /// <returns></returns>
        public static List<LibraryArtist> GetArtists(int limit = 25, string order = null) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_library_artists(limit: limit, order: order);
            }
            IEnumerable<Dictionary<string, object>> artists = ToDotNet.FromList(get_results);
            return DotNetToLibraryArtists.Get(artists);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_library_albums
        /// <summary>Gets the albums in the user's library.</summary>
        /// <param name="limit">Number of albums to return</param>
        /// <param name="order">Order of albums to return. Allowed values: "a_to_z", "z_to_a", "recently_added". Default: Default order.</param>
        /// <returns></returns>
        public static List<LibraryAlbum> GetAlbums(int limit = 25, string order = null) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_library_albums(limit: limit, order: order);
            }
            IEnumerable<Dictionary<string, object>> albums = ToDotNet.FromList(get_results);
            return DotNetToLibraryAlbums.Get(albums);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_liked_songs
        /// <summary>Gets playlist items for the 'Liked Songs' playlist</summary>
        /// <param name="limit">How many items to return.</param>
        /// <returns></returns>
        public static Playlist GetLikedTracks(int limit = 100) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_liked_songs(limit: limit);
            }
            Dictionary<string, object> tracks = ToDotNet.FromDict(get_results);
            return DotNetToLibraryLikedTracks.Get(tracks);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_history
        /// <summary>Gets your play history in reverse chronological order</summary>
        /// <returns></returns>
        public static List<HistoryTrack> GetHistory() {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_history();
            }
            IEnumerable<Dictionary<string, object>> tracks = ToDotNet.FromList(get_results);
            return DotNetToLibraryHistory.Get(tracks);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.remove_history_items
        /// <summary>Remove an item from the account’s history. This method does currently not work with brand accounts</summary>
        /// <param name="feedbackTokens">Token to identify the item to remove, obtained from <see cref="GetHistory"/></param>
        /// <returns></returns>
        public static RemoveHistoryResult RemoveHistoryItems(List<string> feedbackTokens) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.remove_history_items(feedbackTokens);
            }
            Dictionary<string, object> result = ToDotNet.FromDict(get_results);
            return DotNetToLibraryRemoveResult.Get(result);
        }
    }
}
