using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToUploadAlbum {
        public static UploadAlbum Get(Dictionary<string, object> input) =>
            new UploadAlbum() {
                Title = input["title"] as string,
                Type = input["type"] as string,
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as List<object>),
                Artists = DotNetToGeneral.GetSimpleItems(input["artists"] as List<object>),
                TrackCount = (int)input["trackCount"],
                Duration = input["duration"] as string,
                BrowseID = input["audioPlaylistId"] as string,
                Tracks = GetTracks(input["tracks"] as List<object>),
            };

        private static List<UploadTrack> GetTracks(List<object> input) =>
            input?.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new UploadTrack() {
                    EntityID = dict["entityId"] as string,
                    BrowseID = dict["videoId"] as string,
                    Title = dict["title"] as string,
                    Duration = dict["duration"] as string,
                    Artists = DotNetToGeneral.GetSimpleItems(dict["artists"] as List<object>),
                    AlbumName = (dict["album"] as Dictionary<string, object>)["name"] as string,
                    AlbumID = (dict["album"] as Dictionary<string, object>)["id"] as string,
                    LikeStatus = Helpers.EnumParse<LikeStatus>(dict["likeStatus"] as string),
                    Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnails"] as List<object>),
                }).ToList();
    }
}
