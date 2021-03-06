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
        public static List<Track> GetLibraryUploadTracks(int limit = 25, Order order = Order.Default) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_library_upload_songs(limit, Converters.Helpers.OrderToString(order));
            }
            IEnumerable<Dictionary<string, object>> tracks = ToDotNet.FromList(get_results);
            return DotNetToTrack.Get(tracks);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_library_upload_artists
        /// <summary>Gets the artists of uploaded songs in the user's library.</summary>
        /// <param name="limit">Number of artists to return. Default: 25</param>
        /// <param name="order">Order of artists to return. Allowed values: 'a_to_z', 'z_to_a', 'recently_added'. Default: Default order.</param>
        /// <returns>List of artists as returned by <see cref="Library.GetArtists"/></returns>
        public static List<ArtistBasic> GetLibraryUploadArtists(int limit = 25, Order order = Order.Default) {
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
        public static List<AlbumBasic> GetLibraryUploadAlbums(int limit = 25, Order order = Order.Default) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_library_upload_albums(limit, Converters.Helpers.OrderToString(order));
            }
            IEnumerable<Dictionary<string, object>> albums = ToDotNet.FromList(get_results);
            return DotNetToAlbumBasic.Get(albums);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_library_upload_artist
        /// <summary>Returns a list of uploaded tracks for the artist.</summary>
        /// <param name="browseID">Browse id of the upload artist, i.e. from <see cref="GetLibraryUploadTracks"/></param>
        /// <param name="limit">Number of songs to return (increments of 25).</param>
        /// <returns>List of uploaded songs.</returns>
        public static List<Track> GetLibraryUploadArtist(string browseID, int limit = 25) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_library_upload_artist(browseID, limit);
            }
            IEnumerable<Dictionary<string, object>> tracks = ToDotNet.FromList(get_results);
            return DotNetToTrack.Get(tracks);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_library_upload_album
        /// <summary>Get information and tracks of an album associated with uploaded tracks</summary>
        /// <param name="browseID">Browse id of the upload album, i.e. from <see cref="GetLibraryUploadTracks"/></param>
        /// <returns>Dictionary with title, description, artist and tracks.</returns>
        public static AlbumFull GetLibraryUploadAlbum(string browseID) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_library_upload_album(browseID);
            }
            Dictionary<string, object> album = ToDotNet.FromDict(get_results);
            return DotNetToUploadAlbum.Get(album);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.upload_song
        /// <summary>Uploads a song to YouTube Music</summary>
        /// <param name="filePath">Path to the music file (mp3, m4a, wma, flac or ogg)</param>
        /// <returns>Status String or full response</returns>
        public static string UploadSong(string filePath) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.upload_song(filePath);
            }
            return get_results.ToString();
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.delete_upload_entity
        /// <summary>Deletes a previously uploaded song or album</summary>
        /// <param name="entityID">The entity id of the uploaded song or album, e.g. <see cref="Track.UniqueID"/> retrieved from <see cref="GetLibraryUploadTracks"/></param>
        /// <returns>Status String or error</returns>
        public static string DeleteUploadEntity(string entityID) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.delete_upload_entity(entityID);
            }
            return get_results.ToString();
        }
    }
}
