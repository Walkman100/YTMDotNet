using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToUploadAlbum {
        public static AlbumFull Get(Dictionary<string, object> input) =>
            new AlbumFull() {
                Title = input["title"] as string,
                Type = Helpers.EnumParse<AlbumType>(input["type"] as string),
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[]),
                Artists = DotNetToGeneral.GetSimpleItems(input["artists"] as object[]),
                TrackCount = (int)input["trackCount"],
                Duration = input["duration"] as string,
                BrowseID = input["audioPlaylistId"] as string,
                Tracks = (input["tracks"] as object[]).Select(DotNetToTrack.Get).ToList(),
            };
    }
}
