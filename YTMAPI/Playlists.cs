using System.Collections.Generic;
using System.Linq;
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

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.edit_playlist
        /// <summary>Edit title, description or privacyStatus of a playlist. You may also move an item within a playlist or append another playlist to this playlist.</summary>
        /// <param name="playlistID">Playlist id</param>
        /// <param name="title">Optional. New title for the playlist</param>
        /// <param name="description">Optional. New description for the playlist</param>
        /// <param name="privacyStatus">Optional. New privacy status for the playlist</param>
        /// <param name="moveItem">Optional. Move one item before another. Items are specified by <see cref="Track.UniqueID"/>, see <see cref="GetPlaylist"/></param>
        /// <param name="addPlaylistID">Optional. ID of another playlist to add to this playlist</param>
        /// <returns>Status String or full response</returns>
        public static string EditPlaylist(string playlistID, string title = null, string description = null, PrivacyStatus? privacyStatus = null, (string, string)? moveItem = null, string addPlaylistID = null) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.edit_playlist(playlistID, title, description, privacyStatus?.ToString()?.ToUpperInvariant(), moveItem == null ? null : new[] { moveItem.Value.Item1, moveItem.Value.Item2 }, addPlaylistID);
            }
            return get_results;
        }
        public static string StatusSucceeded = "STATUS_SUCCEEDED";
        public static string StatusFailed = "STATUS_FAILED";

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.delete_playlist
        /// <summary>Delete a playlist.</summary>
        /// <param name="playlistID">Playlist id</param>
        /// <returns>Status String or full response</returns>
        public static APIResult DeletePlaylist(string playlistID) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.delete_playlist(playlistID);
            }
            Dictionary<string, object> result = ToDotNet.FromDict(get_results);
            return DotNetToPlaylistDeleteResult.Get(result);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.add_playlist_items
        /// <summary>Add songs to an existing playlist</summary>
        /// <param name="playlistID">Playlist id</param>
        /// <param name="videoIDs">List of Video ids</param>
        /// <param name="sourcePlaylist">Playlist id of a playlist to add to the current playlist (no duplicate check)</param>
        /// <param name="duplicates">If <see langword="true"/>, duplicates will be added. If <see langword="false"/>, an error will be returned if there are duplicates (no items are added to the playlist)</param>
        /// <returns>Status String and a dict containing the new setVideoId for each videoId or full response</returns>
        public static AddPlaylistItemsResult AddPlaylistItems(string playlistID, List<string> videoIDs = null, string sourcePlaylist = null, bool duplicates = false) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.add_playlist_items(playlistID, videoIDs, sourcePlaylist, duplicates);
            }
            Dictionary<string, object> result = ToDotNet.FromDict(get_results);
            return DotNetToAddPlaylistItemsResult.Get(result);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.remove_playlist_items
        /// <summary>Remove songs from an existing playlist</summary>
        /// <param name="playlistID">Playlist id</param>
        /// <param name="videoIDs">
        /// List of playlist items. Get SetVideoID from <see cref="Track.UniqueID"/> returned by <see cref="GetPlaylist"/>.
        /// <br />Must contain both <paramref name="videoIDs"/>.VideoID and <paramref name="videoIDs"/>.SetVideoID values.
        /// </param>
        /// <returns>Status String or full response</returns>
        public static string RemovePlaylistItems(string playlistID, IEnumerable<(string VideoID, string SetVideoID)> videoIDs) {
            // videos parameter python input type: List[Dict[KT, VT]]
            // description: List of PlaylistItems, see `get_playlist()`. Must contain videoId and setVideoId
            var videos = videoIDs.Select(v => new Dictionary<string, string>() {
                { "videoId", v.VideoID },
                { "setVideoId", v.SetVideoID }
            }).ToList();

            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.remove_playlist_items(playlistId: playlistID, videos: videos);
            }
            return get_results;
        }
    }
}
