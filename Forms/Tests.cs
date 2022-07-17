using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace YTMDotNet.Forms {
    public partial class Tests : Form {
        public Tests() {
            InitializeComponent();
            WalkmanLib.ApplyTheme(WalkmanLib.Theme.Dark, this);
            if (this.components != null)
                WalkmanLib.ApplyTheme(WalkmanLib.Theme.Dark, this.components.Components);
            ToolStripManager.Renderer = new WalkmanLib.CustomPaint.ToolStripSystemRendererWithDisabled(WalkmanLib.Theme.Dark.ToolStripItemDisabledText);

            foreach (var item in WalkmanLibExtensions.GetValues<OneSelectionTests>()) {
                cbxTestSelect.Items.Add(item);
            }
        }

        private void btnClose_Click(object _, EventArgs __) => this.Close();

        private static readonly Color defaultTextColor = WalkmanLib.Theme.Dark.TextBoxFG;
        private void outputTestStart(string testName) {
            rtxtLog.AppendText("[", defaultTextColor);
            rtxtLog.AppendText(DateTime.Now.ToString(), Color.White);
            rtxtLog.AppendText("] ", defaultTextColor);
            rtxtLog.AppendText("Testing " + testName + "... ", Color.Yellow);
        }
        private void outputDone() {
            rtxtLog.AppendText("[", defaultTextColor);
            rtxtLog.AppendText(DateTime.Now.ToString(), Color.White);
            rtxtLog.AppendText("] ", defaultTextColor);
            rtxtLog.AppendText("Done.", Color.Yellow, true);
        }
        private void outputTestResults(bool success, string input = null, string expected = null, Exception ex = null) {
            rtxtLog.AppendText("[", defaultTextColor);
            if (success) {
                rtxtLog.AppendText("Y", Color.Green);
                rtxtLog.AppendText("]", defaultTextColor);
            } else {
                rtxtLog.AppendText("N", Color.Red);
                if (ex == null) {
                    rtxtLog.AppendText("]: in:", defaultTextColor);
                    rtxtLog.AppendText(input?.ToString(), Color.Magenta);
                    rtxtLog.AppendText(" expected:", defaultTextColor);
                    rtxtLog.AppendText(expected?.ToString(), Color.Cyan);
                } else {
                    rtxtLog.AppendText("]: Exception: ", defaultTextColor);
                    rtxtLog.AppendText(ex.ToString(), Color.Magenta);
                }
            }
            rtxtLog.AppendText(Environment.NewLine, defaultTextColor);
        }

        private void runTest(Action test) {
            try {
                test();
                outputTestResults(true);
            } catch (Exception ex) {
                outputTestResults(false, ex: ex);
            }
        }
        private void outputTestResults<T>(T input, T expected) {
            if ((input == null && expected == null) || input != null && expected != null && Comparer<T>.Default.Compare(input, expected) == 0) {
                outputTestResults(true);
            } else {
                outputTestResults(false, input?.ToString(), expected?.ToString());
            }
        }

        private void btnAll_Click(object sender, EventArgs e) {
            try {
                btnAll.Enabled = false;
                btnOne.Enabled = false;
                TestSearch();
                TestBrowsing();
                TestWatch();
                TestLibrary();
                TestPlaylists();
                TestUploads();
                outputDone();
            } finally {
                btnAll.Enabled = true;
                btnOne.Enabled = true;
            }
        }

        enum OneSelectionTests {
            Search,
            Browsing,
            Watch,
            Library,
            Playlists,
            Uploads
        }
        private void btnOne_Click(object sender, EventArgs e) {
            try {
                btnAll.Enabled = false;
                btnOne.Enabled = false;
                switch ((OneSelectionTests)cbxTestSelect.SelectedIndex) {
                    case OneSelectionTests.Search: TestSearch(); break;
                    case OneSelectionTests.Browsing: TestBrowsing(); break;
                    case OneSelectionTests.Watch: TestWatch(); break;
                    case OneSelectionTests.Library: TestLibrary(); break;
                    case OneSelectionTests.Playlists: TestPlaylists(); break;
                    case OneSelectionTests.Uploads: TestUploads(); break;
                }
                outputDone();
            } finally {
                btnAll.Enabled = true;
                btnOne.Enabled = true;
            }
        }

        private void TestSearch() {
            outputTestStart("Search");
            runTest(() => YTMAPI.Search.Get("Oasis Wonderwall"));
            Application.DoEvents();
        }

        private void TestBrowsing() {
            outputTestStart("Browsing.GetArtist");
            YTMAPI.Models.Artist artist = null;
            runTest(() => artist = YTMAPI.Browsing.GetArtist("UCmMUZbaYdNH0bEd1PAlAqsA"));
            Application.DoEvents();

            outputTestStart("Browsing.GetArtistAlbums");
            runTest(() => YTMAPI.Browsing.GetArtistAlbums("UCmMUZbaYdNH0bEd1PAlAqsA", artist.AlbumParams));
            Application.DoEvents();

            outputTestStart("Browsing.GetUser1"); // random user with a couple videos
            runTest(() => YTMAPI.Browsing.GetUser("UC23zamSb6iYbDzQYAQBnxmA"));
            Application.DoEvents();

            outputTestStart("Browsing.GetUser2"); // random user with a couple playlists
            runTest(() => YTMAPI.Browsing.GetUser("UCN0qp8OTf3a-Q3fizeVxY-g"));
            Application.DoEvents();

            outputTestStart("Browsing.GetUser3"); // random user with some videos AND playlists
            YTMAPI.Models.User user3 = null;
            runTest(() => user3 = YTMAPI.Browsing.GetUser("UC44hbeRoCZVVMVg5z0FfIww"));
            Application.DoEvents();

            outputTestStart("Browsing.GetUserPlaylists");
            runTest(() => YTMAPI.Browsing.GetUserPlaylists("UC44hbeRoCZVVMVg5z0FfIww", user3.PlaylistParams));
            Application.DoEvents();

            outputTestStart("Browsing.GetAlbum1");
            runTest(() => YTMAPI.Browsing.GetAlbum("MPREb_9nqEki4ZDpp"));
            Application.DoEvents();

            outputTestStart("Browsing.GetAlbum2");
            runTest(() => YTMAPI.Browsing.GetAlbum("MPREb_TQ7pNaCBby6"));
            Application.DoEvents();

            outputTestStart("Browsing.GetAlbum3");
            runTest(() => YTMAPI.Browsing.GetAlbum("MPREb_kOpbd82J23W"));
            Application.DoEvents();

            outputTestStart("Browsing.GetAlbum4");
            runTest(() => YTMAPI.Browsing.GetAlbum("MPREb_YIyUCFozEL4"));
            Application.DoEvents();

            outputTestStart("Browsing.GetTrack");
            runTest(() => YTMAPI.Browsing.GetTrack("rj5wZqReXQE"));
            Application.DoEvents();

            outputTestStart("Browsing.GetLyrics");
            // need value from get_watch_playlist
            string lyricsID;
            using (var YTM = new YTMAPI.PyYTMAPI()) {
                dynamic wp = YTM.API.get_watch_playlist(videoId: "s1N5SQdMpF4", limit: 1);
                lyricsID = wp["lyrics"];
            }
            runTest(() => YTMAPI.Browsing.GetLyrics(lyricsID));
            Application.DoEvents();
        }

        private void TestWatch() {
            outputTestStart("Watch.GetWatchPlaylist");
            runTest(() => YTMAPI.Watch.GetWatchPlaylist("s1N5SQdMpF4"));
            Application.DoEvents();

            outputTestStart("Watch.GetWatchPlaylistShuffle");
            runTest(() => YTMAPI.Watch.GetWatchPlaylistShuffle(null, "OLAK5uy_n9iHP1rHfEFVJiJa1ACN3757FsX7yamGM"));
            Application.DoEvents();
        }

        private void TestLibrary() {
            outputTestStart("Library.GetPlaylists");
            runTest(() => YTMAPI.Library.GetPlaylists());
            Application.DoEvents();

            outputTestStart("Library.GetTracks");
            runTest(() => YTMAPI.Library.GetTracks());
            Application.DoEvents();

            outputTestStart("Library.GetArtists");
            runTest(() => YTMAPI.Library.GetArtists());
            Application.DoEvents();

            outputTestStart("Library.GetSubscriptions");
            runTest(() => YTMAPI.Library.GetSubscriptions());
            Application.DoEvents();

            outputTestStart("Library.GetAlbums");
            runTest(() => YTMAPI.Library.GetAlbums());
            Application.DoEvents();

            outputTestStart("Library.GetLikedTracks");
            runTest(() => YTMAPI.Library.GetLikedTracks());
            Application.DoEvents();

            outputTestStart("Library.GetHistory");
            runTest(() => YTMAPI.Library.GetHistory());
            Application.DoEvents();

            //outputTestStart("Library.RemoveHistoryItems");
            //runTest(() => YTMAPI.Library.RemoveHistoryItems(new[] { "" }.ToList()));
            //Application.DoEvents();

            outputTestStart("Library.RateTrack");
            runTest(() => {
                YTMAPI.Library.RateTrack("kpPt8NpYtJg", YTMAPI.Models.LikeStatus.Like);
                YTMAPI.Library.RateTrack("kpPt8NpYtJg", YTMAPI.Models.LikeStatus.Indifferent);
            });
            Application.DoEvents();

            //outputTestStart("Library.EditTrackStatus");
            //runTest(() => YTMAPI.Library.EditTrackStatus(new[] { "" }.ToList()));
            //Application.DoEvents();

            outputTestStart("Library.RatePlaylist");
            runTest(() => {
                YTMAPI.Library.RatePlaylist("PLvuTwyc1NvRilgVXtPvYKAoV0RfHJcD3W", YTMAPI.Models.LikeStatus.Like);
                YTMAPI.Library.RatePlaylist("PLvuTwyc1NvRilgVXtPvYKAoV0RfHJcD3W", YTMAPI.Models.LikeStatus.Indifferent);
            });
            Application.DoEvents();

            string artistToSubscribe = "UCI4YNnmHjXFaaKvfdmpWvJQ";
            outputTestStart("Library.SubscribeArtists_Initial");
            runTest(() => YTMAPI.Library.SubscribeArtists(new List<string>() { artistToSubscribe }));
            Application.DoEvents();

            outputTestStart("Library.SubscribeArtists_Double");
            runTest(() => YTMAPI.Library.SubscribeArtists(new List<string>() { artistToSubscribe }));
            Application.DoEvents();

            outputTestStart("Library.UnsubscribeArtists_Initial");
            runTest(() => YTMAPI.Library.UnsubscribeArtists(new List<string>() { artistToSubscribe }));
            Application.DoEvents();

            outputTestStart("Library.UnsubscribeArtists_Double");
            runTest(() => YTMAPI.Library.UnsubscribeArtists(new List<string>() { artistToSubscribe }));
            Application.DoEvents();
        }

        private void TestPlaylists() {
            string playlistID = null;
            outputTestStart("Playlists.CreatePlaylist");
            runTest(() => playlistID = YTMAPI.Playlists.CreatePlaylist(title: "YTMDotNet Test Playlist", description: "this playlist should be automatically deleted, if the delete test failed then delete it manually"));
            Application.DoEvents();

            string status = null;
            outputTestStart("Playlists.EditPlaylist_Details");
            runTest(() => status = YTMAPI.Playlists.EditPlaylist(playlistID, "YTMDotNet Test Playlist _edit", privacyStatus: YTMAPI.Models.PrivacyStatus.Private));
            outputTestStart("Playlists.EditPlaylist_Details Return");
            outputTestResults(status, YTMAPI.Playlists.StatusSucceeded);
            Application.DoEvents();

            status = null;
            outputTestStart("Playlists.EditPlaylist_Add");
            runTest(() => status = YTMAPI.Playlists.EditPlaylist(playlistID, addPlaylistID: "PLkLwuW0_fEYV1h1G6j0JPcgCsTpgDwcvZ")); // Emperor: Battle for Dune Soundtrack Playlist - owned by me (Walkman100)
            outputTestStart("Playlists.EditPlaylist_Add Return");
            outputTestResults(status, YTMAPI.Playlists.StatusSucceeded);
            Application.DoEvents();

            YTMAPI.Models.Playlist playlist = null;
            outputTestStart("Playlists.GetPlaylist");
            runTest(() => playlist = YTMAPI.Playlists.GetPlaylist(playlistID));
            Application.DoEvents();

            status = null;
            outputTestStart("Playlists.RemovePlaylistItems");
            runTest(() => {
                var videoIDs = new List<(string VideoID, string SetVideoID)>() {
                    (playlist.Tracks[8].BrowseID, playlist.Tracks[8].UniqueID),
                    (playlist.Tracks[9].BrowseID, playlist.Tracks[9].UniqueID)
                };
                status = YTMAPI.Playlists.RemovePlaylistItems(playlistID, videoIDs);
            });
            outputTestStart("Playlists.RemovePlaylistItems Return");
            outputTestResults(status, YTMAPI.Playlists.StatusSucceeded);
            Application.DoEvents();

            status = null;
            outputTestStart("Playlists.EditPlaylist_Move");
            runTest(() => status = YTMAPI.Playlists.EditPlaylist(playlistID, moveItem: (playlist.Tracks[4].UniqueID, playlist.Tracks[3].UniqueID)));
            outputTestStart("Playlists.EditPlaylist_Move Return");
            outputTestResults(status, YTMAPI.Playlists.StatusSucceeded);
            Application.DoEvents();

            status = null;
            outputTestStart("Playlists.AddPlaylistItems");
            runTest(() => status = YTMAPI.Playlists.AddPlaylistItems(playlistID, sourcePlaylist: "PLFw_6jJxziT2dW5eWgo9H74dcYGzuc0Nl").Status); // ThomasTheTankEngineTheme Playlist - owned by me (Walkman100)
            outputTestStart("Playlists.AddPlaylistItems Return");
            outputTestResults(status, YTMAPI.Playlists.StatusSucceeded);
            Application.DoEvents();

            status = null;
            outputTestStart("Playlists.DeletePlaylist");
            runTest(() => status = YTMAPI.Playlists.DeletePlaylist(playlistID).Command.HandlePlaylistDeletionCommand_PlaylistID);
            outputTestStart("Playlists.DeletePlaylist Return");
            outputTestResults(status, playlistID);
            Application.DoEvents();
        }

        private void TestUploads() {
            outputTestStart("Uploads.GetLibraryUploadTracks");
            runTest(() => YTMAPI.Uploads.GetLibraryUploadTracks());
            Application.DoEvents();

            List<YTMAPI.Models.ArtistBasic> artists = null;
            outputTestStart("Uploads.GetLibraryUploadArtists");
            runTest(() => artists = YTMAPI.Uploads.GetLibraryUploadArtists());
            Application.DoEvents();

            outputTestStart("Uploads.GetLibraryUploadArtist");
            if (artists != null && artists.Count > 0)
                runTest(() => YTMAPI.Uploads.GetLibraryUploadArtist(artists[0].BrowseID));
            else
                rtxtLog.AppendText("[Skipped]", defaultTextColor, true);
            Application.DoEvents();

            List<YTMAPI.Models.AlbumBasic> albums = null;
            outputTestStart("Uploads.GetLibraryUploadAlbums");
            runTest(() => albums = YTMAPI.Uploads.GetLibraryUploadAlbums());
            Application.DoEvents();

            outputTestStart("Uploads.GetLibraryUploadAlbum");
            if (albums != null && albums.Count > 0)
                runTest(() => YTMAPI.Uploads.GetLibraryUploadAlbum(albums[0].BrowseID));
            else
                rtxtLog.AppendText("[Skipped]", defaultTextColor, true);
            Application.DoEvents();

            //string status = null;
            //outputTestStart("Uploads.UploadSong");
            //runTest(() => status = YTMAPI.Uploads.UploadSong(@""));
            //outputTestStart("Uploads.UploadSong Return");
            //outputTestResults(status, YTMAPI.Playlists.StatusSucceeded);
            //Application.DoEvents();

            //status = null;
            //outputTestStart("Uploads.DeleteUploadEntity");
            //runTest(() => status = YTMAPI.Uploads.DeleteUploadEntity(""));
            //outputTestStart("Uploads.DeleteUploadEntity Return");
            //outputTestResults(status, YTMAPI.Playlists.StatusSucceeded);
            //Application.DoEvents();
        }
    }

    public static class RichTextBoxExtensions {
        public static void AppendText(this RichTextBox box, string text, Color color, bool addNewLine = false) {
            if (text == null)
                text = "[NULL]";

            box.SuspendLayout();
            box.SelectionColor = color;
            box.AppendText(addNewLine ? $"{text}{Environment.NewLine}" : text);
            box.ScrollToCaret();
            box.ResumeLayout();
        }
    }
}
