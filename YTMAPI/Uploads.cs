using System.Collections.Generic;
using YTMDotNet.YTMAPI.Converters;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI {
    static class Uploads {
        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_library_upload_songs
        /// <summary>Returns a list of uploaded songs</summary>
        /// <param name="limit">How many songs to return. Default: 25</param>
        /// <param name="order">Order of songs to return. Allowed values: 'a_to_z', 'z_to_a', 'recently_added'. Default: Default order.</param>
        /// <returns>List of uploaded songs.</returns>
        public static List<UploadTrack> GetLibraryUploadTracks(int limit = 25, Order order = Order.Default) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_library_upload_songs(limit, Converters.Helpers.OrderToString(order));
            }
            IEnumerable<Dictionary<string, object>> tracks = ToDotNet.FromList(get_results);
            return DotNetToUploadTracks.Get(tracks);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_library_upload_artists
        /// <summary>Gets the artists of uploaded songs in the user's library.</summary>
        /// <param name="limit">Number of artists to return. Default: 25</param>
        /// <param name="order">Order of artists to return. Allowed values: 'a_to_z', 'z_to_a', 'recently_added'. Default: Default order.</param>
        /// <returns>List of artists as returned by <see cref="Library.GetArtists"/></returns>
        public static List<LibraryArtist> GetLibraryUploadArtists(int limit = 25, Order order = Order.Default) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_library_upload_artists(limit, Converters.Helpers.OrderToString(order));
            }
            IEnumerable<Dictionary<string, object>> artists = ToDotNet.FromList(get_results);
            return DotNetToLibraryArtists.Get(artists);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_library_upload_albums
        /// <summary>Gets the albums of uploaded songs in the user's library.</summary>
        /// <param name="limit">Number of albums to return. Default: 25</param>
        /// <param name="order">Order of albums to return. Allowed values: 'a_to_z', 'z_to_a', 'recently_added'. Default: Default order.</param>
        /// <returns>List of albums as returned by <see cref="Library.GetAlbums"/></returns>
        public static List<LibraryAlbum> GetLibraryUploadAlbums(int limit = 25, Order order = Order.Default) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_library_upload_albums(limit, Converters.Helpers.OrderToString(order));
            }
            IEnumerable<Dictionary<string, object>> albums = ToDotNet.FromList(get_results);
            return DotNetToLibraryAlbums.Get(albums);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_library_upload_artist
        /// <summary>Returns a list of uploaded tracks for the artist.</summary>
        /// <param name="browseID">Browse id of the upload artist, i.e. from <see cref="GetLibraryUploadTracks"/></param>
        /// <param name="limit">Number of songs to return (increments of 25).</param>
        /// <returns>List of uploaded songs.</returns>
        public static List<UploadTrack> GetLibraryUploadArtist(string browseID, int limit = 25) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_library_upload_artist(browseID, limit);
            }
            IEnumerable<Dictionary<string, object>> tracks = ToDotNet.FromList(get_results);
            return DotNetToUploadTracks.Get(tracks);
        }
    }
}
