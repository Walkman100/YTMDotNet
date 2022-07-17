using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToWatchPlaylist {
        public static WatchPlaylist Get(Dictionary<string, object> input) =>
            new WatchPlaylist() {
                Tracks = GetTracks(input["tracks"] as object[]),
                PlaylistID = input["playlistId"] as string,
                LyricsID = input["lyrics"] as string
            };

        private static List<Track_WatchPlaylist> GetTracks(object[] input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new Track_WatchPlaylist() {
                    Title = dict["title"] as string,
                    BrowseID = dict["videoId"] as string,
                    Length = dict["length"] as string,
                    Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnail"] as object[]),
                    LikeStatus = Helpers.EnumParse<LikeStatus>(dict["likeStatus"] as string),
                    Artists = DotNetToGeneral.GetSimpleItems(dict["artists"] as object[]),
                    AlbumName = (dict.GetValue("album") as Dictionary<string, object>)?["name"] as string,
                    AlbumID = (dict.GetValue("album") as Dictionary<string, object>)?["id"] as string,
                    Year = dict.GetValue("year") as int?,
                    Views = dict.GetValue("views") as string
                }).ToList();
    }
}
