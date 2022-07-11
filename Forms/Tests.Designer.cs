namespace YTMDotNet.Forms {
    partial class Tests {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnAll = new System.Windows.Forms.Button();
            this.btnOne = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbxTestSelect = new System.Windows.Forms.ComboBox();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(12, 12);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(200, 23);
            this.btnAll.TabIndex = 0;
            this.btnAll.Text = "Test All";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnOne
            // 
            this.btnOne.Location = new System.Drawing.Point(424, 12);
            this.btnOne.Name = "btnOne";
            this.btnOne.Size = new System.Drawing.Size(200, 23);
            this.btnOne.TabIndex = 1;
            this.btnOne.Text = "<- Test";
            this.btnOne.UseVisualStyleBackColor = true;
            this.btnOne.Click += new System.EventHandler(this.btnOne_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(630, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(200, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbxTestSelect
            // 
            this.cbxTestSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTestSelect.FormattingEnabled = true;
            this.cbxTestSelect.Location = new System.Drawing.Point(218, 13);
            this.cbxTestSelect.Name = "cbxTestSelect";
            this.cbxTestSelect.Size = new System.Drawing.Size(200, 21);
            this.cbxTestSelect.TabIndex = 3;
            // 
            // rtxtLog
            // 
            this.rtxtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtLog.Location = new System.Drawing.Point(12, 41);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.ReadOnly = true;
            this.rtxtLog.Size = new System.Drawing.Size(818, 398);
            this.rtxtLog.TabIndex = 4;
            this.rtxtLog.Text = "";
            // 
            // Tests
            // 
            this.AcceptButton = this.btnAll;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(842, 451);
            this.Controls.Add(this.rtxtLog);
            this.Controls.Add(this.cbxTestSelect);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOne);
            this.Controls.Add(this.btnAll);
            this.Name = "Tests";
            this.Text = "Tests";
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnOne;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbxTestSelect;
        private System.Windows.Forms.RichTextBox rtxtLog;
    }
}
