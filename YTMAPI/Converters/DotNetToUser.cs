using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToUser {
        public static User Get(Dictionary<string, object> input, string id) =>
            new User() {
                Title = input["name"] as string,
                BrowseID = id,
                VideoBrowseID = (input.GetValue("videos") as Dictionary<string, object>)?.GetValue("browseId") as string,
                PlaylistBrowseID = (input.GetValue("playlists") as Dictionary<string, object>)?.GetValue("browseId") as string,
                PlaylistParams = (input.GetValue("playlists") as Dictionary<string, object>)?.GetValue("params") as string,
                Videos = GetVideos((input.GetValue("videos") as Dictionary<string, object>)?.GetValue("results") as object[]),
                Playlists = GetPlaylists((input.GetValue("playlists") as Dictionary<string, object>)?.GetValue("results") as object[])
            };

        private static List<Video> GetVideos(object[] input) =>
            input?.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new Video() {
                    Title = dict["title"] as string,
                    BrowseID = dict["videoId"] as string,
                    PlaylistID = dict["playlistId"] as string,
                    Views = dict["views"] as string,
                    Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnails"] as object[])
                }).ToList();
        private static List<Item> GetPlaylists(object[] input) =>
            input?.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new Item() {
                    Title = dict["title"] as string,
                    BrowseID = dict["playlistId"] as string,
                    Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnails"] as object[])
                }).ToList();
    }
}
