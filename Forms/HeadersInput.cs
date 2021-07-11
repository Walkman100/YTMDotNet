using System.Windows.Forms;

namespace YTMDotNet.Forms {
    public partial class HeadersInput : Form {
        public HeadersInput(Form owner) {
            this.Owner = owner;
            InitializeComponent();
            this.Icon = Owner.Icon;
            this.CenterToParent();
        }

        public string MainInstruction {
            get => lblMainInstruction.Text;
            set => lblMainInstruction.Text = value;
        }

        public string Content {
            get => lblContent.Text;
            set {
                lblContent.Text = value;
                lblContent.LinkArea = new LinkArea(0, 0);
            }
        }
        private string url;
        public void SetContentLink(int start, int length, string targetUrl) {
            lblContent.LinkArea = new LinkArea(start, length);
            url = targetUrl;
        }
        private void lblContent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(url);
        }

        public string Input {
            get => txtInput.Text;
            set => txtInput.Text = value;
        }
        public string[] InputArr {
            get => txtInput.Lines;
            set => txtInput.Lines = value;
        }
    }
}
