using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToWatchPlaylist {
        public static WatchPlaylist Get(Dictionary<string, object> input) =>
            new WatchPlaylist() {
                Tracks = GetTracks(input["tracks"] as List<object>),
                PlaylistID = input["playlistId"] as string,
                LyricsID = input["lyrics"] as string
            };

        private static List<WatchTrack> GetTracks(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new WatchTrack() {
                    Title = dict["title"] as string,
                    BrowseID = dict["videoId"] as string,
                    Length = dict["length"] as string,
                    Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnail"] as List<object>),
                    LikeStatus = Helpers.EnumParse<LikeStatus>(dict["likeStatus"] as string),
                    Artists = DotNetToGeneral.GetSimpleItems(dict["artists"] as List<object>),
                    AlbumName = (dict.GetValue("album") as Dictionary<string, object>)?["name"] as string,
                    AlbumID = (dict.GetValue("album") as Dictionary<string, object>)?["id"] as string,
                    Year = dict.GetValue("year") as int?,
                    Views = dict.GetValue("views") as string
                }).ToList();
    }
}
