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
    }
}
