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
                outputDone();
            } finally {
                btnAll.Enabled = true;
                btnOne.Enabled = true;
            }
        }

        private void btnOne_Click(object sender, EventArgs e) {
            try {
                btnAll.Enabled = false;
                btnOne.Enabled = false;
                outputDone();
            } finally {
                btnAll.Enabled = true;
                btnOne.Enabled = true;
            }
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
