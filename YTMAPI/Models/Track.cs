using System;
using System.Collections.Generic;

namespace YTMDotNet.YTMAPI.Models {
    class Track {
        public TrackPlayabilityStatus PlayabilityStatus;
        public TrackStreamingData StreamingData;
        public TrackVideoDetails VideoDetails;
        public TrackMicroformatDataRenderer Microformat;
    }


    class TrackPlayabilityStatus {
        public string Status;
        public bool PlayableInEmbed;
        public string AudioOnlyPlayabilityRenderer_TrackingParams;
        public string AudioOnlyPlayabilityRenderer_AudioOnlyAvailability;
        public string MiniplayerRenderer_PlaybackMode;
        public string ContextParams;
    }

    class TrackStreamingData {
        public string ExpiresInSeconds;
        public List<TrackFormat> Formats;
        public List<TrackFormat> AdaptiveFormats;
    }
    class TrackFormat {
        public string QualityLabel;
        public string Quality;
        public string AudioQuality;
        public string MimeType;
        public string ContentLength;
        public string ApproxDurationMS;
        public int? Width;
        public int? Height;
        public string ProjectionType;
        public int? FPS;
        public int Bitrate;
        public int AverageBitrate;
        public string AudioSampleRate;
        public int? AudioChannels;
        public double? LoudnessDB;
        public int ITag;
        public string LastModified;
        public string SignatureCipher;
        public string InitRangeStart;
        public string InitRangeEnd;
        public string IndexRangeStart;
        public string IndexRangeEnd;
    }

    class TrackVideoDetails : Item {
        public string LengthSeconds;
        public string ChannelID;
        public bool IsOwnerViewing;
        public bool IsCrawlable;
        public double? AverageRating;
        public bool AllowRatings;
        public string ViewCount;
        public string Author;
        public bool IsPrivate;
        public bool IsUnpluggedCorpus;
        public string MusicVideoType;
        public bool IsLiveContent;
    }

    class TrackMicroformatDataRenderer : Item {
        public string URLCanonical;
        public string Description;
        public string SiteName;
        public string AppName;
        public string AndroidPackage;
        public string iOSAppStoreID;
        public string iOSAppArguments;
        public string OGType;
        public string URLApplinksiOS;
        public string URLApplinksAndroid;
        public string URLTwitteriOS;
        public string URLTwitterAndroid;
        public string TwitterCardType;
        public string TwitterSiteHandle;
        public string SchemaDotOrgType;
        public bool NoIndex;
        public bool Unlisted;
        public bool Paid;
        public bool FamilySafe;
        public List<string> Tags;
        public List<string> AvailableCountries;
        public string PageOwnerName;
        public string PageOwnerExternalChannelID;
        public string PageOwnerYouTubeProfileURL;
        public string VideoDetailsDurationInSeconds;
        public string VideoDetailsDurationISO8601;
        public List<TrackLinkAlternates> LinkAlternates;
        public string ViewCount;
        public string Category;
        public DateTime PublishDate;
        public DateTime UploadDate;
    }
    class TrackLinkAlternates {
        public string HREF_URL;
        public string Title;
        public string AlternateType;
    }
}
