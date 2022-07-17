using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToWatchTrack {
        public static WatchTrack Get(Dictionary<string, object> input) =>
            new WatchTrack() {
                PlayabilityStatus = GetPlayabilityStatus(input["playabilityStatus"] as Dictionary<string, object>),
                StreamingData = GetStreamingData(input["streamingData"] as Dictionary<string, object>),
                VideoDetails = GetVideoDetails(input["videoDetails"] as Dictionary<string, object>),
                Microformat = GetMicroformat(input["microformat"] as Dictionary<string, object>)
            };


        private static WatchTrackPlayabilityStatus GetPlayabilityStatus(Dictionary<string, object> input) {
            var AOPR =
                (input["audioOnlyPlayability"] as Dictionary<string, object>)
                ["audioOnlyPlayabilityRenderer"] as Dictionary<string, object>;

            return new WatchTrackPlayabilityStatus() {
                Status = input["status"] as string,
                PlayableInEmbed = (bool)input["playableInEmbed"],
                AudioOnlyPlayabilityRenderer_TrackingParams = AOPR["trackingParams"] as string,
                AudioOnlyPlayabilityRenderer_AudioOnlyAvailability = AOPR["audioOnlyAvailability"] as string,
                MiniplayerRenderer_PlaybackMode =
                    ((input["miniplayer"] as Dictionary<string, object>)
                    ["miniplayerRenderer"] as Dictionary<string, object>)
                    ["playbackMode"] as string,
                ContextParams = input["contextParams"] as string
            };
        }

        private static WatchTrackStreamingData GetStreamingData(Dictionary<string, object> input) =>
            new WatchTrackStreamingData() {
                ExpiresInSeconds = input["expiresInSeconds"] as string,
                Formats = GetStreamingDataFormats(input["formats"] as object[]),
                AdaptiveFormats = GetStreamingDataAdaptiveFormats(input["adaptiveFormats"] as object[])
            };
        private static List<WatchTrackFormat> GetStreamingDataFormats(object[] input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new WatchTrackFormat() {
                    ITag = (int)dict["itag"],
                    MimeType = dict["mimeType"] as string,
                    Bitrate = (int)dict["bitrate"],
                    Width = (int)dict["width"],
                    Height = (int)dict["height"],
                    LastModified = dict["lastModified"] as string,
                    ContentLength = dict["contentLength"] as string,
                    Quality = dict["quality"] as string,
                    FPS = (int)dict["fps"],
                    QualityLabel = dict["qualityLabel"] as string,
                    ProjectionType = dict["projectionType"] as string,
                    AverageBitrate = (int)dict["averageBitrate"],
                    AudioQuality = dict["audioQuality"] as string,
                    ApproxDurationMS = dict["approxDurationMs"] as string,
                    AudioSampleRate = dict["audioSampleRate"] as string,
                    AudioChannels = (int)dict["audioChannels"],
                    SignatureCipher = dict["signatureCipher"] as string
                }).ToList();
        private static List<WatchTrackFormat> GetStreamingDataAdaptiveFormats(object[] input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new WatchTrackFormat {
                    ITag = (int)dict["itag"],
                    MimeType = dict["mimeType"] as string,
                    Bitrate = (int)dict["bitrate"],
                    Width = (int?)dict.GetValue("width"),
                    Height = (int?)dict.GetValue("height"),
                    InitRangeStart = (dict["initRange"] as Dictionary<string, object>)["start"] as string,
                    InitRangeEnd = (dict["initRange"] as Dictionary<string, object>)["end"] as string,
                    IndexRangeStart = (dict["indexRange"] as Dictionary<string, object>)["start"] as string,
                    IndexRangeEnd = (dict["indexRange"] as Dictionary<string, object>)["end"] as string,
                    LastModified = dict["lastModified"] as string,
                    ContentLength = dict["contentLength"] as string,
                    Quality = dict["quality"] as string,
                    FPS = (int?)dict.GetValue("fps"),
                    QualityLabel = dict.GetValue("qualityLabel") as string,
                    ProjectionType = dict["projectionType"] as string,
                    AverageBitrate = (int)dict["averageBitrate"],
                    AudioQuality = dict.GetValue("audioQuality") as string,
                    ApproxDurationMS = dict["approxDurationMs"] as string,
                    AudioSampleRate = dict.GetValue("audioSampleRate") as string,
                    AudioChannels = (int?)dict.GetValue("audioChannels"),
                    LoudnessDB = (double?)dict.GetValue("loudnessDb"),
                    SignatureCipher = dict["signatureCipher"] as string
                }).ToList();

        private static WatchTrackVideoDetails GetVideoDetails(Dictionary<string, object> input) =>
            new WatchTrackVideoDetails() {
                BrowseID = input["videoId"] as string,
                Title = input["title"] as string,
                LengthSeconds = input["lengthSeconds"] as string,
                ChannelID = input["channelId"] as string,
                IsOwnerViewing = (bool)input["isOwnerViewing"],
                IsCrawlable = (bool)input["isCrawlable"],
                Thumbnails = DotNetToGeneral.GetThumbnails(
                    (input["thumbnail"] as Dictionary<string, object>)
                    ["thumbnails"] as object[]),
                AverageRating = (double?)input.GetValue("averageRating"),
                AllowRatings = (bool)input["allowRatings"],
                ViewCount = input["viewCount"] as string,
                Author = input["author"] as string,
                IsPrivate = (bool)input["isPrivate"],
                IsUnpluggedCorpus = (bool)input["isUnpluggedCorpus"],
                MusicVideoType = input["musicVideoType"] as string,
                IsLiveContent = (bool)input["isLiveContent"]
            };

        private static WatchTrackMicroformatDataRenderer GetMicroformat(Dictionary<string, object> input) {
            var MFDR = input["microformatDataRenderer"] as Dictionary<string, object>;
            var MFDR_POD = MFDR["pageOwnerDetails"] as Dictionary<string, object>;
            var MFDR_VD = MFDR["videoDetails"] as Dictionary<string, object>;
            return new WatchTrackMicroformatDataRenderer {
                URLCanonical = MFDR["urlCanonical"] as string,
                Title = MFDR["title"] as string,
                Description = MFDR["description"] as string,
                Thumbnails = DotNetToGeneral.GetThumbnails(
                    (MFDR["thumbnail"] as Dictionary<string, object>)
                    ["thumbnails"] as object[]),
                SiteName = MFDR["siteName"] as string,
                AppName = MFDR["appName"] as string,
                AndroidPackage = MFDR["androidPackage"] as string,
                iOSAppStoreID = MFDR["iosAppStoreId"] as string,
                iOSAppArguments = MFDR["iosAppArguments"] as string,
                OGType = MFDR["ogType"] as string,
                URLApplinksiOS = MFDR["urlApplinksIos"] as string,
                URLApplinksAndroid = MFDR["urlApplinksAndroid"] as string,
                URLTwitteriOS = MFDR["urlTwitterIos"] as string,
                URLTwitterAndroid = MFDR["urlTwitterAndroid"] as string,
                TwitterCardType = MFDR["twitterCardType"] as string,
                TwitterSiteHandle = MFDR["twitterSiteHandle"] as string,
                SchemaDotOrgType = MFDR["schemaDotOrgType"] as string,
                NoIndex = (bool)MFDR["noindex"],
                Unlisted = (bool)MFDR["unlisted"],
                Paid = (bool)MFDR["paid"],
                FamilySafe = (bool)MFDR["familySafe"],
                Tags = (MFDR["tags"] as object[]).Select(s => s as string).ToList(),
                AvailableCountries = (MFDR["availableCountries"] as object[]).Select(s => s as string).ToList(),
                PageOwnerName = MFDR_POD["name"] as string,
                PageOwnerExternalChannelID = MFDR_POD["externalChannelId"] as string,
                PageOwnerYouTubeProfileURL = MFDR_POD["youtubeProfileUrl"] as string,
                BrowseID = MFDR_VD["externalVideoId"] as string,
                VideoDetailsDurationInSeconds = MFDR_VD["durationSeconds"] as string,
                VideoDetailsDurationISO8601 = MFDR_VD["durationIso8601"] as string,
                LinkAlternates = GetLinkAlternates(MFDR["linkAlternates"] as object[]),
                ViewCount = MFDR["viewCount"] as string,
                Category = MFDR["category"] as string,
                PublishDate = System.DateTime.Parse(MFDR["publishDate"] as string),
                UploadDate = System.DateTime.Parse(MFDR["uploadDate"] as string)
            };
        }
        private static List<WatchTrackLinkAlternates> GetLinkAlternates(object[] input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new WatchTrackLinkAlternates() {
                    HREF_URL = dict["hrefUrl"] as string,
                    Title = dict.GetValue("title") as string,
                    AlternateType = dict.GetValue("alternateType") as string
                }).ToList();
    }
}
