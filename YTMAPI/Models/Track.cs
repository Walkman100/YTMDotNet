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
        public int ExpiresInSeconds;
        public List<TrackFormat> Formats;
        public List<TrackFormat> AdaptiveFormats;
    }
    class TrackFormat {
        public string QualityLabel;
        public string Quality;
        public string AudioQuality;
        public string MimeType;
        public int ContentLength;
        public int ApproxDurationMS;
        public int? Width;
        public int? Height;
        public string ProjectionType;
        public int? FPS;
        public int Bitrate;
        public int AverageBitrate;
        public int? AudioSampleRate;
        public int? AudioChannels;
        public double? LoudnessDB;
        public int ITag;
        public ulong LastModified;
        public string SignatureCipher;
        public int? InitRangeStart;
        public int? InitRangeEnd;
        public int? IndexRangeStart;
        public int? IndexRangeEnd;
    }

    class TrackVideoDetails : Item {
        public int LengthSeconds;
        public string ChannelID;
        public bool IsOwnerViewing;
        public bool IsCrawlable;
        public double AverageRating;
        public bool AllowRatings;
        public int ViewCount;
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
        public int iOSAppStoreID;
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
        public int VideoDetailsDurationInSeconds;
        public string VideoDetailsDurationISO8601;
        public List<TrackLinkAlternates> LinkAlternates;
        public int ViewCount;
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
