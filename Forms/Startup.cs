using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Python.Runtime;

namespace YTMDotNet.Forms {
    public partial class Startup : Form {
        public Startup() {
            InitializeComponent();
            WalkmanLib.ApplyTheme(WalkmanLib.Theme.Dark, this);
            if (this.components != null)
                WalkmanLib.ApplyTheme(WalkmanLib.Theme.Dark, this.components.Components);
            ToolStripManager.Renderer = new WalkmanLib.CustomPaint.ToolStripSystemRendererWithDisabled(WalkmanLib.Theme.Dark.ToolStripItemDisabledText);
        }

        private CancellationTokenSource loadingTextUpdateCancel;
        private void Startup_Shown(object _, EventArgs __) {
            Application.DoEvents(); // ensure UI has been drawn

            loadingTextUpdateCancel = new CancellationTokenSource();
            _ = Task.Run(() => {
                while (!loadingTextUpdateCancel.IsCancellationRequested) {
                    if (lblLoading.Text.EndsWith("..."))
                        lblLoading.Text = "Loading";
                    else
                        lblLoading.Text += ".";
                    Thread.Sleep(250);
                }
            });

            if (Helpers.Settings.Init() == false)
                Helpers.Settings.HeadersPath = Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS", "YTMDotNetHeaders.json");

            while (!File.Exists(Helpers.Settings.PythonDLL)) {
                string tmp = GetPythonInstall();
                if (tmp == null) {
                    Application.Exit();
                    return;
                }
                Helpers.Settings.PythonDLL = tmp;
            }
            chkPythonConfig.Checked = true;

            try {
                PythonInitAndCheck();
                chkPythonInstall.Checked = true;
            } catch (Exception ex) {
                switch (WalkmanLib.CustomMsgBox($"Error Loading Python!{Environment.NewLine}{Environment.NewLine}{ex.Message}{Environment.NewLine}{Environment.NewLine}Selecting a new path requires a restart.",
                                                "Python Initialization Error", "Restart Application", "Show Full Error", "Cancel", MessageBoxIcon.Error, ownerForm: this)) {
                    case "Restart Application":
                        Helpers.Settings.PythonDLL = null; // reset path & save so it can be selected on next startup
                        Application.Restart();
                        return;

                    case "Show Full Error":
                        WalkmanLib.ErrorDialog(ex, showMsgBox: false);
                        //Application.Exit(); // WalkmanLib.ErrorDialog is Async...
                        return;

                    case "Cancel":
                        Application.Exit();
                        return;
                }
            }

            while (true) {
                try {
                    YTMAPICheck();
                    chkYTMAPI.Checked = true;
                } catch (PythonException ex) when (ex.Message == $"No module named '{YTMAPI.PyYTMAPI.YTMAPIModuleName}'") {
                    string pythonInstallFolder = Path.GetDirectoryName(Helpers.Settings.PythonDLL);

                    switch (WalkmanLib.CustomMsgBox($"YTM API module not found in Python install{Environment.NewLine}\"{pythonInstallFolder}\"!",
                                                                        "YTM API Initialization Error", null, "Install", "Cancel", MessageBoxIcon.Warning, ownerForm: this)) {
                        case "Install":
                            InstallYTMAPI(pythonInstallFolder);
                            continue;
                        case "Cancel":
                            Application.Exit();
                            return;
                    }
                } catch (Exception ex) {
                    WalkmanLib.ErrorDialog(ex);
                    //Application.Exit(); // WalkmanLib.ErrorDialog is Async...
                    return;
                }
                break;
            }

            while (true) {
                if (!File.Exists(Helpers.Settings.HeadersPath) || string.IsNullOrWhiteSpace(File.ReadAllText(Helpers.Settings.HeadersPath))) {
                    var inputDialog = new HeadersInput(this) {
                        Text = "Authentication",
                        MainInstruction = "Paste Headers",
                        Content = "See ytmusicapi.readthedocs.io/en/latest/setup.html"
                    };
                    inputDialog.SetContentLink(4, 999, "https://ytmusicapi.readthedocs.io/en/latest/setup.html");
                    WalkmanLib.ApplyTheme(WalkmanLib.Theme.Dark, inputDialog);

                    if (inputDialog.ShowDialog() == DialogResult.Cancel) {
                        Application.Exit();
                        return;
                    }

                    try {
                        YTMASetup(inputDialog.Input);
                    } catch (PythonException ex) {
                        WalkmanLib.ErrorDialog(ex);
                        continue;
                    } catch (Exception ex) {
                        WalkmanLib.ErrorDialog(ex);
                        //Application.Exit(); // WalkmanLib.ErrorDialog is Async...
                        return;
                    }
                }
                chkLogInConfig.Checked = true;

                try {
                    YTMALoginCheck();
                    chkLogIn.Checked = true;
                } catch (PythonException ex) {
                    switch (WalkmanLib.CustomMsgBox($"Login Failed!{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                                                    "YTM API Login Error", "Try New Headers", "Show Full Error", "Cancel", MessageBoxIcon.Warning, ownerForm: this)) {
                        case "Try New Headers":
                            File.WriteAllText(Helpers.Settings.HeadersPath, "");
                            chkLogInConfig.Checked = false;
                            continue;

                        case "Show Full Error":
                            WalkmanLib.ErrorDialog(ex, showMsgBox: false);
                            //Application.Exit(); // WalkmanLib.ErrorDialog is Async...
                            return;

                        case "Cancel":
                            Application.Exit();
                            return;
                    }
                } catch (Exception ex) {
                    WalkmanLib.ErrorDialog(ex);
                    //Application.Exit(); // WalkmanLib.ErrorDialog is Async...
                    return;
                }
                break;
            }

            loadingTextUpdateCancel.Cancel();
            Application.DoEvents();
            lblLoading.Text = "Switching Windows...";

            new Tests().Show();
        }

        private string GetPythonInstall() {
            OpenFileDialog selectPythonDLL = new() {
                Title = "Select Python DLL",
                Filter = "Python DLL|*.dll",
                FileName = "python39.dll",
                AddExtension = false,
            };

            return selectPythonDLL.ShowDialog() == DialogResult.Cancel ? null : selectPythonDLL.FileName;
        }
        private void PythonInitAndCheck() {
            Runtime.PythonDLL = Helpers.Settings.PythonDLL;
            PythonEngine.Initialize();
        }

        private void YTMAPICheck() {
            using (Py.GIL()) {
                dynamic YTMusicAPI = Py.Import(YTMAPI.PyYTMAPI.YTMAPIModuleName);
                _ = YTMusicAPI.YTMusic;
            }
        }
        private void InstallYTMAPI(string pythonInstallFolder) {
            string pipPath = Path.Combine(pythonInstallFolder, "Scripts", "pip.exe");
            System.Diagnostics.Process.Start("cmd.exe", $"/c \"{pipPath}\" install {YTMAPI.PyYTMAPI.YTMAPIModuleName} & pause");
            MessageBox.Show("Select OK when install is complete", "Installing YTM API", MessageBoxButtons.OK);
        }

        private void YTMASetup(string headersInput) {
            headersInput = headersInput.Replace("\r\n", "\n");

            using (Py.GIL()) {
                dynamic YTMusicAPI = Py.Import(YTMAPI.PyYTMAPI.YTMAPIModuleName);
                dynamic YTMusic = YTMusicAPI.YTMusic;
                YTMusic.setup(filepath: Helpers.Settings.HeadersPath, headers_raw: headersInput);
            }
        }
        private void YTMALoginCheck() {
            using (var YTM = new YTMAPI.PyYTMAPI()) {
                _ = YTM.API.get_library_songs(limit: 1);
            }
        }
    }
}
