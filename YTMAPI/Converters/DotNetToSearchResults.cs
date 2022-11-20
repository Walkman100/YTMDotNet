using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToSearchResults {
        public static List<SearchResult> Get(IEnumerable<Dictionary<string, object>> input) =>
            input.Select(GetResult).ToList();

        private static SearchResult GetResult(Dictionary<string, object> input) {
            string resultType = input["resultType"] as string;

            return resultType switch {
                "song" => GetTrack(input),
                "video" => GetVideo(input),
                "album" => GetAlbum(input),
                "artist" => GetArtist(input),
                "playlist" => GetPlaylist(input),
                _ => throw new System.NotSupportedException($"Search result type \"{resultType}\" not supported!"),
            };
        }

        private static Models.SearchResults.Track GetTrack(Dictionary<string, object> input) =>
            new Models.SearchResults.Track() {
                Category = input["category"] as string,
                Title = input["title"] as string,
                AlbumName = (input["album"] as Dictionary<string, object>)?["name"] as string,
                AlbumID = (input["album"] as Dictionary<string, object>)?["id"] as string,
                BrowseID = input["videoId"] as string,
                Duration = input["duration"] as string,
                Year = input["year"] as int?,
                DurationSeconds = (int)input["duration_seconds"],
                IsExplicit = (bool)input["isExplicit"],
                Artists = DotNetToGeneral.GetSimpleItems(input["artists"] as object[]),
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[])
            };
        private static Models.SearchResults.Video GetVideo(Dictionary<string, object> input) =>
            new Models.SearchResults.Video() {
                Category = input["category"] as string,
                Title = input["title"] as string,
                Views = input["views"] as string,
                BrowseID = input["videoId"] as string,
                Duration = input["duration"] as string,
                Year = input["year"] as int?,
                DurationSeconds = (int)input["duration_seconds"],
                Artists = DotNetToGeneral.GetSimpleItems(input["artists"] as object[]),
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[])
            };
        private static Models.SearchResults.Album GetAlbum(Dictionary<string, object> input) =>
            new Models.SearchResults.Album() {
                Category = input["category"] as string,
                Title = input["title"] as string,
                Type = Helpers.EnumParse<AlbumType>(input["type"] as string),
                Duration = input["duration"] as string,
                Year = input["year"] as int?,
                BrowseID = input["browseId"] as string,
                IsExplicit = (bool)input["isExplicit"],
                Artists = DotNetToGeneral.GetSimpleItems(input["artists"] as object[]),
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[])
            };
        private static Models.SearchResults.Artist GetArtist(Dictionary<string, object> input) =>
            new Models.SearchResults.Artist() {
                Category = input["category"] as string,
                Title = input["artist"] as string,
                ShuffleID = input["shuffleId"] as string,
                RadioID = input["radioId"] as string,
                BrowseID = input["browseId"] as string,
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[])
            };
        private static Models.SearchResults.Playlist GetPlaylist(Dictionary<string, object> input) =>
            new Models.SearchResults.Playlist() {
                Category = input["category"] as string,
                Title = input["title"] as string,
                ItemCount = input["itemCount"] as string,
                Author = input["author"] as string,
                BrowseID = input["browseId"] as string,
                Thumbnails = DotNetToGeneral.GetThumbnails(input["thumbnails"] as object[])
            };
    }
}
