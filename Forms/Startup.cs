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
        private void Startup_Shown(object sender, EventArgs e) {
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

            Helpers.Settings.Init();

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
                } catch (PythonException ex) when (ex.Message == "No module named 'ytmusicapi'") {
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

            //

            loadingTextUpdateCancel.Cancel();
            lblLoading.Text = "Switching Windows...";
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
                dynamic YTMusicAPI = Py.Import("ytmusicapi");
                _ = YTMusicAPI.YTMusic;
            }
        }
        private void InstallYTMAPI(string pythonInstallFolder) {
            string pipPath = Path.Combine(pythonInstallFolder, "Scripts", "pip.exe");
            System.Diagnostics.Process.Start("cmd.exe", $"/c \"{pipPath}\" install ytmusicapi & pause");
            MessageBox.Show("Select OK when install is complete", "Installing YTM API", MessageBoxButtons.OK);
        }
    }
}
