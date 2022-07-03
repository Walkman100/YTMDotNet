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
                get_results = YTM.API.get_library_upload_songs(limit, order == Order.Default ? null : order.ToString().ToLowerInvariant());
            }
            IEnumerable<Dictionary<string, object>> tracks = ToDotNet.FromList(get_results);
            return DotNetToUploadTracks.Get(tracks);
        }
    }
}
