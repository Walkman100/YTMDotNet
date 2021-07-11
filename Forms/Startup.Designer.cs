namespace YTMDotNet.Forms {
    partial class Startup {
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
            this.chkPythonInstall = new System.Windows.Forms.CheckBox();
            this.chkYTMAPI = new System.Windows.Forms.CheckBox();
            this.chkLogIn = new System.Windows.Forms.CheckBox();
            this.lblLoading = new System.Windows.Forms.Label();
            this.lblPythonInstall = new System.Windows.Forms.Label();
            this.lblYTMAPI = new System.Windows.Forms.Label();
            this.lblLogIn = new System.Windows.Forms.Label();
            this.lblLogInConfig = new System.Windows.Forms.Label();
            this.lblPythonConfig = new System.Windows.Forms.Label();
            this.chkLogInConfig = new System.Windows.Forms.CheckBox();
            this.chkPythonConfig = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkPythonInstall
            // 
            this.chkPythonInstall.AutoSize = true;
            this.chkPythonInstall.Enabled = false;
            this.chkPythonInstall.Location = new System.Drawing.Point(30, 70);
            this.chkPythonInstall.Name = "chkPythonInstall";
            this.chkPythonInstall.Size = new System.Drawing.Size(15, 14);
            this.chkPythonInstall.TabIndex = 0;
            this.chkPythonInstall.UseVisualStyleBackColor = true;
            // 
            // chkYTMAPI
            // 
            this.chkYTMAPI.AutoSize = true;
            this.chkYTMAPI.Enabled = false;
            this.chkYTMAPI.Location = new System.Drawing.Point(30, 110);
            this.chkYTMAPI.Name = "chkYTMAPI";
            this.chkYTMAPI.Size = new System.Drawing.Size(15, 14);
            this.chkYTMAPI.TabIndex = 1;
            this.chkYTMAPI.UseVisualStyleBackColor = true;
            // 
            // chkLogIn
            // 
            this.chkLogIn.AutoSize = true;
            this.chkLogIn.Enabled = false;
            this.chkLogIn.Location = new System.Drawing.Point(30, 190);
            this.chkLogIn.Name = "chkLogIn";
            this.chkLogIn.Size = new System.Drawing.Size(15, 14);
            this.chkLogIn.TabIndex = 2;
            this.chkLogIn.UseVisualStyleBackColor = true;
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(27, 240);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(54, 13);
            this.lblLoading.TabIndex = 3;
            this.lblLoading.Text = "Loading...";
            // 
            // lblPythonInstall
            // 
            this.lblPythonInstall.AutoSize = true;
            this.lblPythonInstall.Location = new System.Drawing.Point(51, 70);
            this.lblPythonInstall.Name = "lblPythonInstall";
            this.lblPythonInstall.Size = new System.Drawing.Size(104, 13);
            this.lblPythonInstall.TabIndex = 4;
            this.lblPythonInstall.Text = "Python Install Check";
            // 
            // lblYTMAPI
            // 
            this.lblYTMAPI.AutoSize = true;
            this.lblYTMAPI.Location = new System.Drawing.Point(51, 110);
            this.lblYTMAPI.Name = "lblYTMAPI";
            this.lblYTMAPI.Size = new System.Drawing.Size(99, 13);
            this.lblYTMAPI.TabIndex = 5;
            this.lblYTMAPI.Text = "YTmusicAPI Check";
            // 
            // lblLogIn
            // 
            this.lblLogIn.AutoSize = true;
            this.lblLogIn.Location = new System.Drawing.Point(51, 190);
            this.lblLogIn.Name = "lblLogIn";
            this.lblLogIn.Size = new System.Drawing.Size(89, 13);
            this.lblLogIn.TabIndex = 6;
            this.lblLogIn.Text = "Logged In Check";
            // 
            // lblLogInConfig
            // 
            this.lblLogInConfig.AutoSize = true;
            this.lblLogInConfig.Location = new System.Drawing.Point(51, 150);
            this.lblLogInConfig.Name = "lblLogInConfig";
            this.lblLogInConfig.Size = new System.Drawing.Size(104, 13);
            this.lblLogInConfig.TabIndex = 12;
            this.lblLogInConfig.Text = "Log In Config Check";
            // 
            // lblPythonConfig
            // 
            this.lblPythonConfig.AutoSize = true;
            this.lblPythonConfig.Location = new System.Drawing.Point(51, 30);
            this.lblPythonConfig.Name = "lblPythonConfig";
            this.lblPythonConfig.Size = new System.Drawing.Size(107, 13);
            this.lblPythonConfig.TabIndex = 10;
            this.lblPythonConfig.Text = "Python Config Check";
            // 
            // chkLogInConfig
            // 
            this.chkLogInConfig.AutoSize = true;
            this.chkLogInConfig.Enabled = false;
            this.chkLogInConfig.Location = new System.Drawing.Point(30, 150);
            this.chkLogInConfig.Name = "chkLogInConfig";
            this.chkLogInConfig.Size = new System.Drawing.Size(15, 14);
            this.chkLogInConfig.TabIndex = 9;
            this.chkLogInConfig.UseVisualStyleBackColor = true;
            // 
            // chkPythonConfig
            // 
            this.chkPythonConfig.AutoSize = true;
            this.chkPythonConfig.Enabled = false;
            this.chkPythonConfig.Location = new System.Drawing.Point(30, 30);
            this.chkPythonConfig.Name = "chkPythonConfig";
            this.chkPythonConfig.Size = new System.Drawing.Size(15, 14);
            this.chkPythonConfig.TabIndex = 7;
            this.chkPythonConfig.UseVisualStyleBackColor = true;
            // 
            // Startup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 295);
            this.Controls.Add(this.lblLogInConfig);
            this.Controls.Add(this.lblPythonConfig);
            this.Controls.Add(this.chkLogInConfig);
            this.Controls.Add(this.chkPythonConfig);
            this.Controls.Add(this.lblLogIn);
            this.Controls.Add(this.lblYTMAPI);
            this.Controls.Add(this.lblPythonInstall);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.chkLogIn);
            this.Controls.Add(this.chkYTMAPI);
            this.Controls.Add(this.chkPythonInstall);
            this.Name = "Startup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Startup";
            this.Shown += new System.EventHandler(this.Startup_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkPythonInstall;
        private System.Windows.Forms.CheckBox chkYTMAPI;
        private System.Windows.Forms.CheckBox chkLogIn;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.Label lblPythonInstall;
        private System.Windows.Forms.Label lblYTMAPI;
        private System.Windows.Forms.Label lblLogIn;
        private System.Windows.Forms.Label lblLogInConfig;
        private System.Windows.Forms.Label lblPythonConfig;
        private System.Windows.Forms.CheckBox chkLogInConfig;
        private System.Windows.Forms.CheckBox chkPythonConfig;
    }
}
