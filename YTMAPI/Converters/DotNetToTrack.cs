using System.Collections.Generic;
using System.Linq;
using YTMDotNet.YTMAPI.Models;

namespace YTMDotNet.YTMAPI.Converters {
    static class DotNetToTrack {
        public static Track Get(Dictionary<string, object> input) =>
            new Track() {
                PlayabilityStatus = GetPlayabilityStatus(input["playabilityStatus"] as Dictionary<string, object>),
                StreamingData = GetStreamingData(input["streamingData"] as Dictionary<string, object>),
                VideoDetails = GetVideoDetails(input["videoDetails"] as Dictionary<string, object>),
                Microformat = GetMicroformat(input["microformat"] as Dictionary<string, object>)
            };


        private static TrackPlayabilityStatus GetPlayabilityStatus(Dictionary<string, object> input) {
            var AOPR =
                (input["audioOnlyPlayability"] as Dictionary<string, object>)
                ["audioOnlyPlayabilityRenderer"] as Dictionary<string, object>;

            return new TrackPlayabilityStatus() {
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

        private static TrackStreamingData GetStreamingData(Dictionary<string, object> input) =>
            new TrackStreamingData() {
                ExpiresInSeconds = (int)input["expiresInSeconds"],
                Formats = GetStreamingDataFormats(input["formats"] as List<object>),
                AdaptiveFormats = GetStreamingDataAdaptiveFormats(input["adaptiveFormats"] as List<object>)
            };
        private static List<TrackFormat> GetStreamingDataFormats(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new TrackFormat() {
                    ITag = (int)dict["itag"],
                    MimeType = dict["mimeType"] as string,
                    Bitrate = (int)dict["bitrate"],
                    Width = (int)dict["width"],
                    Height = (int)dict["height"],
                    LastModified = (ulong)dict["lastModified"],
                    ContentLength = (int)dict["contentLength"],
                    Quality = dict["quality"] as string,
                    FPS = (int)dict["fps"],
                    QualityLabel = dict["qualityLabel"] as string,
                    ProjectionType = dict["projectionType"] as string,
                    AverageBitrate = (int)dict["averageBitrate"],
                    AudioQuality = dict["audioQuality"] as string,
                    ApproxDurationMS = (int)dict["approxDurationMs"],
                    AudioSampleRate = (int)dict["audioSampleRate"],
                    AudioChannels = (int)dict["audioChannels"],
                    SignatureCipher = dict["signatureCipher"] as string
                }).ToList();
        private static List<TrackFormat> GetStreamingDataAdaptiveFormats(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new TrackFormat {
                    ITag = (int)dict["itag"],
                    MimeType = dict["mimeType"] as string,
                    Bitrate = (int)dict["bitrate"],
                    Width = (int?)dict.GetValue("width"),
                    Height = (int?)dict.GetValue("height"),
                    InitRangeStart = (int)(dict["initRange"] as Dictionary<string, object>)["start"],
                    InitRangeEnd = (int)(dict["initRange"] as Dictionary<string, object>)["end"],
                    IndexRangeStart = (int)(dict["indexRange"] as Dictionary<string, object>)["start"],
                    IndexRangeEnd = (int)(dict["indexRange"] as Dictionary<string, object>)["end"],
                    LastModified = (ulong)dict["lastModified"],
                    ContentLength = (int)dict["contentLength"],
                    Quality = dict["quality"] as string,
                    FPS = (int?)dict.GetValue("fps"),
                    QualityLabel = dict.GetValue("qualityLabel") as string,
                    ProjectionType = dict["projectionType"] as string,
                    AverageBitrate = (int)dict["averageBitrate"],
                    AudioQuality = dict.GetValue("audioQuality") as string,
                    ApproxDurationMS = (int)dict["approxDurationMs"],
                    AudioSampleRate = (int?)dict.GetValue("audioSampleRate"),
                    AudioChannels = (int?)dict.GetValue("audioChannels"),
                    LoudnessDB = (double?)dict.GetValue("loudnessDb"),
                    SignatureCipher = dict["signatureCipher"] as string
                }).ToList();

        private static TrackVideoDetails GetVideoDetails(Dictionary<string, object> input) =>
            new TrackVideoDetails() {
                BrowseID = input["videoId"] as string,
                Title = input["title"] as string,
                LengthSeconds = (int)input["lengthSeconds"],
                ChannelID = input["channelId"] as string,
                IsOwnerViewing = (bool)input["isOwnerViewing"],
                IsCrawlable = (bool)input["isCrawlable"],
                Thumbnails = DotNetToGeneral.GetThumbnails(
                    (input["thumbnail"] as Dictionary<string, object>)
                    ["thumbnails"] as List<object>),
                AverageRating = (double)input["averageRating"],
                AllowRatings = (bool)input["allowRatings"],
                ViewCount = (int)input["viewCount"],
                Author = input["author"] as string,
                IsPrivate = (bool)input["isPrivate"],
                IsUnpluggedCorpus = (bool)input["isUnpluggedCorpus"],
                MusicVideoType = input["musicVideoType"] as string,
                IsLiveContent = (bool)input["isLiveContent"]
            };

        private static TrackMicroformatDataRenderer GetMicroformat(Dictionary<string, object> input) {
            var MFDR = input["microformatDataRenderer"] as Dictionary<string, object>;
            var MFDR_POD = MFDR["pageOwnerDetails"] as Dictionary<string, object>;
            var MFDR_VD = MFDR["videoDetails"] as Dictionary<string, object>;
            return new TrackMicroformatDataRenderer {
                URLCanonical = MFDR["urlCanonical"] as string,
                Title = MFDR["title"] as string,
                Description = MFDR["description"] as string,
                Thumbnails = DotNetToGeneral.GetThumbnails(
                    (MFDR["thumbnail"] as Dictionary<string, object>)
                    ["thumbnails"] as List<object>),
                SiteName = MFDR["siteName"] as string,
                AppName = MFDR["appName"] as string,
                AndroidPackage = MFDR["androidPackage"] as string,
                iOSAppStoreID = (int)MFDR["iosAppStoreId"],
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
                Tags = (MFDR["tags"] as List<object>).Select(s => s as string).ToList(),
                AvailableCountries = (MFDR["availableCountries"] as List<object>).Select(s => s as string).ToList(),
                PageOwnerName = MFDR_POD["name"] as string,
                PageOwnerExternalChannelID = MFDR_POD["externalChannelId"] as string,
                PageOwnerYouTubeProfileURL = MFDR_POD["youtubeProfileUrl"] as string,
                BrowseID = MFDR_VD["externalVideoId"] as string,
                VideoDetailsDurationInSeconds = (int)MFDR_VD["durationSeconds"],
                VideoDetailsDurationISO8601 = MFDR_VD["durationIso8601"] as string,
                LinkAlternates = GetLinkAlternates(MFDR["linkAlternates"] as List<object>),
                ViewCount = (int)MFDR["viewCount"],
                Category = MFDR["category"] as string,
                PublishDate = System.DateTime.Parse(MFDR["publishDate"] as string),
                UploadDate = System.DateTime.Parse(MFDR["uploadDate"] as string)
            };
        }
        private static List<TrackLinkAlternates> GetLinkAlternates(List<object> input) =>
            input.Select(obj => obj as Dictionary<string, object>).Select(
                dict => new TrackLinkAlternates() {
                    HREF_URL = dict["hrefUrl"] as string,
                    Title = dict.GetValue("title") as string,
                    AlternateType = dict.GetValue("alternateType") as string
                }).ToList();
    }
}
