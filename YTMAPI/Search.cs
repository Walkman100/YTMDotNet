using System.Collections.Generic;
using YTMDotNet.YTMAPI.Converters;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI {
    static class Search {
        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.search
        /// <summary>Search YouTube music Returns results within the provided category.</summary>
        /// <param name="query">Query string, i.e. ‘Oasis Wonderwall’</param>
        /// <param name="filter">Filter for item types. Allowed values: songs, videos, albums, artists, playlists, community_playlists, featured_playlists, uploads. Default: Default search, including all types of items.</param>
        /// <param name="scope">Search scope. Allowed values: library, uploads. For uploads, no filter can be set! An exception will be thrown if you attempt to do so. Default: Search the public YouTube Music catalogue.</param>
        /// <param name="limit">Number of search results to return Default: 20</param>
        /// <param name="ignore_spelling">Whether to ignore YTM spelling suggestions. If True, the exact search term will be searched for, and will not be corrected. This does not have any effect when the filter is set to uploads. Default: False, will use YTM’s default behavior of autocorrecting the search.</param>
        /// <returns></returns>
        public static List<SearchResult> Get(string query, string filter = null, string scope = null, int limit = 20, bool ignore_spelling = false) {
            dynamic search_results;
            using (var YTM = new PyYTMAPI()) {
                search_results = YTM.API.search(query: query, filter: filter, scope: scope, limit: limit, ignore_spelling: ignore_spelling);
            }
            IEnumerable<Dictionary<string, object>> list = ToDotNet.FromList(search_results);
            return DotNetToSearchResults.Get(list);
        }
    }
}
