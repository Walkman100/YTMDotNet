using System.Collections.Generic;
using YTMDotNet.YTMAPI.Converters;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI {
    static class Browsing {
        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_artist
        /// <summary>
        /// Get information about an artist and their top releases (songs, albums, singles, videos, and related artists). 
        /// The top lists contain pointers for getting the full list of releases. For songs/videos, pass the browseId to get_playlist(). 
        /// For albums/singles, pass browseId and params to :py:func: get_artist_albums.
        /// </summary>
        /// <param name="channelID">channel id of the artist</param>
        /// <returns></returns>
        public static Artist GetArtist(string channelID) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_artist(channelID);
            }
            Dictionary<string, object> artist = ToDotNet.FromDict(get_results);
            return DotNetToArtist.Get(artist);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_artist_albums
        /// <summary>Get the full list of an artist’s albums or singles</summary>
        /// <param name="channelID">channel Id of the artist</param>
        /// <param name="_params">params obtained by get_artist()</param>
        /// <returns></returns>
        public static List<AlbumBasic> GetArtistAlbums(string channelID, string _params) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_artist_albums(channelID, _params);
            }
            IEnumerable<Dictionary<string, object>> list = ToDotNet.FromList(get_results);
            return DotNetToArtistAlbums.Get(list);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_user
        /// <summary>Retrieve a user’s page. A user may own videos or playlists.</summary>
        /// <param name="channelID">channelId of the user</param>
        /// <returns></returns>
        public static User GetUser(string channelID) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_user(channelID);
            }
            Dictionary<string, object> user = ToDotNet.FromDict(get_results);
            return DotNetToUser.Get(user, channelID);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_user_playlists
        /// <summary>Retrieve a list of playlists for a given user. Call this function again with the returned params to get the full list.</summary>
        /// <param name="channelID">channelId of the user</param>
        /// <param name="_params">params obtained by get_artist()</param>
        /// <returns></returns>
        public static List<Item> GetUserPlaylists(string channelID, string _params) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_user_playlists(channelID, _params);
            }
            IEnumerable<Dictionary<string, object>> list = ToDotNet.FromList(get_results);
            return DotNetToUserPlaylists.Get(list);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_album
        /// <summary>Get information and tracks of an album</summary>
        /// <param name="browseID">browseId of the album, for example returned by search()</param>
        /// <returns></returns>
        public static AlbumFull GetAlbum(string browseID) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_album(browseID);
            }
            Dictionary<string, object> album = ToDotNet.FromDict(get_results);
            return DotNetToAlbum.Get(album);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_song
        /// <summary>Returns metadata and streaming information about a song or video.</summary>
        /// <param name="videoID">Video id</param>
        /// <param name="signatureTimestamp">Provide the current YouTube signatureTimestamp. If not provided a default value will be used, which might result in invalid streaming URLs</param>
        /// <returns></returns>
        public static WatchTrack GetTrack(string videoID, int signatureTimestamp = 0) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                if (signatureTimestamp == 0)
                    get_results = YTM.API.get_song(videoID);
                else
                    get_results = YTM.API.get_song(videoID, signatureTimestamp: signatureTimestamp);
            }
            Dictionary<string, object> track = ToDotNet.FromDict(get_results);
            return DotNetToTrack.Get(track);
        }

        //https://ytmusicapi.readthedocs.io/en/latest/reference.html#ytmusicapi.YTMusic.get_lyrics
        /// <summary>Returns lyrics of a song or video.</summary>
        /// <param name="lyricsID">Lyrics browse id obtained from get_watch_playlist</param>
        /// <returns></returns>
        public static Lyrics GetLyrics(string lyricsID) {
            dynamic get_results;
            using (var YTM = new PyYTMAPI()) {
                get_results = YTM.API.get_lyrics(lyricsID);
            }
            Dictionary<string, object> lyrics = ToDotNet.FromDict(get_results);
            return DotNetToLyrics.Get(lyrics);
        }
    }
}
