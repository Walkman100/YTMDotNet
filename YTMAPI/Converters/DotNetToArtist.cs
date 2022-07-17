using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToArtist {
        public static Artist Get(Dictionary<string, object> input) =>
            new Artist() {
                Title = input["name"] as string,
                Description = input["description"] as string,
                Views = input["views"] as string,
                BrowseID = input["channelId"] as string,
                ShuffleID = input["shuffleId"] as string,
                RadioID = input["radioId"] as string,
                Subscribers = input["subscribers"] as string,
                Subscribed = (bool)input["subscribed"],
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[]),
                TrackBrowseID = (input["songs"] as Dictionary<string, object>)["browseId"] as string,
                VideoBrowseID = (input["videos"] as Dictionary<string, object>)["browseId"] as string,
                RelatedBrowseID = (input["related"] as Dictionary<string, object>)["browseId"] as string,
                AlbumBrowseID = (input["albums"] as Dictionary<string, object>)["browseId"] as string,
                AlbumParams = (input["albums"] as Dictionary<string, object>)["params"] as string,
                SinglesBrowseID = (input["singles"] as Dictionary<string, object>)["browseId"] as string,
                SinglesParams = (input["singles"] as Dictionary<string, object>)["params"] as string,
                Tracks = GetTracks((input["songs"] as Dictionary<string, object>)["results"] as object[]),
                Albums = GetAlbums((input["albums"] as Dictionary<string, object>)["results"] as object[]),
                Singles = GetSingles((input["singles"] as Dictionary<string, object>)["results"] as object[]),
                Videos = GetVideos((input["videos"] as Dictionary<string, object>)["results"] as object[]),
                Related = GetRelated((input["related"] as Dictionary<string, object>)["results"] as object[])
            };

        private static List<Track> GetTracks(object[] input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new Track() {
                    Title = dict["title"] as string,
                    BrowseID = dict["videoId"] as string,
                    IsAvailable = (bool)dict["isAvailable"],
                    IsExplicit = (bool)dict["isExplicit"],
                    LikeStatus = Helpers.EnumParse<LikeStatus>(dict["likeStatus"] as string),
                    AlbumName = (dict["album"] as Dictionary<string, object>)["name"] as string,
                    AlbumID = (dict["album"] as Dictionary<string, object>)["id"] as string,
                    Artists = DotNetToGeneral.GetSimpleItems(dict["artists"] as object[]),
                    Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnails"] as object[]),
                }).ToList();
        private static List<Video> GetVideos(object[] input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new Video() {
                    Title = dict["title"] as string,
                    BrowseID = dict["videoId"] as string,
                    PlaylistID = dict["playlistId"] as string,
                    Views = dict["views"] as string,
                    Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnails"] as object[])
                }).ToList();
        private static List<AlbumMini> GetAlbums(object[] input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new AlbumMini() {
                    Title = dict["title"] as string,
                    BrowseID = dict["browseId"] as string,
                    Year = dict["year"] as int?,
                    Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnails"] as object[])
                }).ToList();
        private static List<AlbumMini> GetSingles(object[] input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new AlbumMini() {
                    Title = dict["title"] as string,
                    BrowseID = dict["browseId"] as string,
                    Year = dict["year"] as int?,
                    Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnails"] as object[])
                }).ToList();
        private static List<ArtistRelated> GetRelated(object[] input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new ArtistRelated() {
                    Title = dict["title"] as string,
                    BrowseID = dict["browseId"] as string,
                    Subscribers = dict["subscribers"] as string,
                    Thumbnails = DotNetToGeneral.GetThumbnails(dict["thumbnails"] as object[])
                }).ToList();
    }
}
