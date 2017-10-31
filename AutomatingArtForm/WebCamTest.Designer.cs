namespace AutomatingArtForm
{
    partial class WebCamTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.imageArt = new Emgu.CV.UI.ImageBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabViews = new System.Windows.Forms.TabControl();
            this.tabCamera = new System.Windows.Forms.TabPage();
            this.tabLines = new System.Windows.Forms.TabPage();
            this.tabTexture = new System.Windows.Forms.TabPage();
            this.trackTextureDark = new System.Windows.Forms.TrackBar();
            this.chkTextureDark = new System.Windows.Forms.CheckBox();
            this.trackTextureLight = new System.Windows.Forms.TrackBar();
            this.chkTextureLight = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.trackTextureThreshold = new System.Windows.Forms.TrackBar();
            this.tabText = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.trackTextRes = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.trackTextDarkness = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.imageArt)).BeginInit();
            this.tabViews.SuspendLayout();
            this.tabLines.SuspendLayout();
            this.tabTexture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackTextureDark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackTextureLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackTextureThreshold)).BeginInit();
            this.tabText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackTextRes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackTextDarkness)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // imageArt
            // 
            this.imageArt.Location = new System.Drawing.Point(1, 12);
            this.imageArt.Name = "imageArt";
            this.imageArt.Size = new System.Drawing.Size(545, 445);
            this.imageArt.TabIndex = 9;
            this.imageArt.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(36, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 50);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabViews
            // 
            this.tabViews.Controls.Add(this.tabCamera);
            this.tabViews.Controls.Add(this.tabLines);
            this.tabViews.Controls.Add(this.tabTexture);
            this.tabViews.Controls.Add(this.tabText);
            this.tabViews.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabViews.Location = new System.Drawing.Point(570, 12);
            this.tabViews.Name = "tabViews";
            this.tabViews.SelectedIndex = 0;
            this.tabViews.Size = new System.Drawing.Size(416, 445);
            this.tabViews.TabIndex = 11;
            this.tabViews.SelectedIndexChanged += new System.EventHandler(this.tabViews_SelectedIndexChanged);
            // 
            // tabCamera
            // 
            this.tabCamera.Location = new System.Drawing.Point(4, 29);
            this.tabCamera.Name = "tabCamera";
            this.tabCamera.Padding = new System.Windows.Forms.Padding(3);
            this.tabCamera.Size = new System.Drawing.Size(408, 412);
            this.tabCamera.TabIndex = 0;
            this.tabCamera.Text = "Camera";
            this.tabCamera.UseVisualStyleBackColor = true;
            // 
            // tabLines
            // 
            this.tabLines.Controls.Add(this.button1);
            this.tabLines.Controls.Add(this.label1);
            this.tabLines.Location = new System.Drawing.Point(4, 29);
            this.tabLines.Name = "tabLines";
            this.tabLines.Padding = new System.Windows.Forms.Padding(3);
            this.tabLines.Size = new System.Drawing.Size(408, 412);
            this.tabLines.TabIndex = 1;
            this.tabLines.Text = "Lines";
            this.tabLines.UseVisualStyleBackColor = true;
            // 
            // tabTexture
            // 
            this.tabTexture.Controls.Add(this.trackTextureDark);
            this.tabTexture.Controls.Add(this.chkTextureDark);
            this.tabTexture.Controls.Add(this.trackTextureLight);
            this.tabTexture.Controls.Add(this.chkTextureLight);
            this.tabTexture.Controls.Add(this.label2);
            this.tabTexture.Controls.Add(this.trackTextureThreshold);
            this.tabTexture.Location = new System.Drawing.Point(4, 29);
            this.tabTexture.Name = "tabTexture";
            this.tabTexture.Padding = new System.Windows.Forms.Padding(3);
            this.tabTexture.Size = new System.Drawing.Size(408, 412);
            this.tabTexture.TabIndex = 2;
            this.tabTexture.Text = "Texture";
            this.tabTexture.UseVisualStyleBackColor = true;
            // 
            // trackTextureDark
            // 
            this.trackTextureDark.Location = new System.Drawing.Point(16, 216);
            this.trackTextureDark.Maximum = 100;
            this.trackTextureDark.Name = "trackTextureDark";
            this.trackTextureDark.Size = new System.Drawing.Size(290, 45);
            this.trackTextureDark.TabIndex = 15;
            this.trackTextureDark.Value = 50;
            this.trackTextureDark.Scroll += new System.EventHandler(this.trackTextureDark_Scroll);
            // 
            // chkTextureDark
            // 
            this.chkTextureDark.AutoSize = true;
            this.chkTextureDark.Location = new System.Drawing.Point(25, 186);
            this.chkTextureDark.Name = "chkTextureDark";
            this.chkTextureDark.Size = new System.Drawing.Size(115, 24);
            this.chkTextureDark.TabIndex = 14;
            this.chkTextureDark.Text = "Dark Squish";
            this.chkTextureDark.UseVisualStyleBackColor = true;
            this.chkTextureDark.CheckedChanged += new System.EventHandler(this.chkTextureDark_CheckedChanged);
            // 
            // trackTextureLight
            // 
            this.trackTextureLight.Location = new System.Drawing.Point(16, 135);
            this.trackTextureLight.Maximum = 100;
            this.trackTextureLight.Name = "trackTextureLight";
            this.trackTextureLight.Size = new System.Drawing.Size(290, 45);
            this.trackTextureLight.TabIndex = 13;
            this.trackTextureLight.Value = 50;
            this.trackTextureLight.Scroll += new System.EventHandler(this.trackTextureLight_Scroll);
            // 
            // chkTextureLight
            // 
            this.chkTextureLight.AutoSize = true;
            this.chkTextureLight.Location = new System.Drawing.Point(25, 105);
            this.chkTextureLight.Name = "chkTextureLight";
            this.chkTextureLight.Size = new System.Drawing.Size(116, 24);
            this.chkTextureLight.TabIndex = 12;
            this.chkTextureLight.Text = "Light Squish";
            this.chkTextureLight.UseVisualStyleBackColor = true;
            this.chkTextureLight.CheckedChanged += new System.EventHandler(this.chkTextureLight_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Threshold";
            // 
            // trackTextureThreshold
            // 
            this.trackTextureThreshold.Location = new System.Drawing.Point(16, 33);
            this.trackTextureThreshold.Maximum = 100;
            this.trackTextureThreshold.Name = "trackTextureThreshold";
            this.trackTextureThreshold.Size = new System.Drawing.Size(290, 45);
            this.trackTextureThreshold.TabIndex = 0;
            this.trackTextureThreshold.Value = 50;
            this.trackTextureThreshold.Scroll += new System.EventHandler(this.trackTextureThreshold_Scroll);
            // 
            // tabText
            // 
            this.tabText.Controls.Add(this.label4);
            this.tabText.Controls.Add(this.trackTextRes);
            this.tabText.Controls.Add(this.label3);
            this.tabText.Controls.Add(this.trackTextDarkness);
            this.tabText.Location = new System.Drawing.Point(4, 29);
            this.tabText.Name = "tabText";
            this.tabText.Padding = new System.Windows.Forms.Padding(3);
            this.tabText.Size = new System.Drawing.Size(408, 412);
            this.tabText.TabIndex = 3;
            this.tabText.Text = "Text";
            this.tabText.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(166, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Resolution";
            // 
            // trackTextRes
            // 
            this.trackTextRes.Location = new System.Drawing.Point(59, 195);
            this.trackTextRes.Maximum = 100;
            this.trackTextRes.Minimum = 1;
            this.trackTextRes.Name = "trackTextRes";
            this.trackTextRes.Size = new System.Drawing.Size(290, 45);
            this.trackTextRes.TabIndex = 16;
            this.trackTextRes.Value = 50;
            this.trackTextRes.Scroll += new System.EventHandler(this.trackTextRes_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(159, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Darkness";
            // 
            // trackTextDarkness
            // 
            this.trackTextDarkness.Location = new System.Drawing.Point(52, 60);
            this.trackTextDarkness.Maximum = 100;
            this.trackTextDarkness.Name = "trackTextDarkness";
            this.trackTextDarkness.Size = new System.Drawing.Size(290, 45);
            this.trackTextDarkness.TabIndex = 14;
            this.trackTextDarkness.Value = 50;
            this.trackTextDarkness.Scroll += new System.EventHandler(this.trackTextDarkness_Scroll);
            // 
            // WebCamTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 510);
            this.Controls.Add(this.tabViews);
            this.Controls.Add(this.imageArt);
            this.Name = "WebCamTest";
            this.Text = "WebCamTest";
            ((System.ComponentModel.ISupportInitialize)(this.imageArt)).EndInit();
            this.tabViews.ResumeLayout(false);
            this.tabLines.ResumeLayout(false);
            this.tabLines.PerformLayout();
            this.tabTexture.ResumeLayout(false);
            this.tabTexture.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackTextureDark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackTextureLight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackTextureThreshold)).EndInit();
            this.tabText.ResumeLayout(false);
            this.tabText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackTextRes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackTextDarkness)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Emgu.CV.UI.ImageBox imageArt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabViews;
        private System.Windows.Forms.TabPage tabCamera;
        private System.Windows.Forms.TabPage tabLines;
        private System.Windows.Forms.TabPage tabTexture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackTextureThreshold;
        private System.Windows.Forms.TrackBar trackTextureDark;
        private System.Windows.Forms.CheckBox chkTextureDark;
        private System.Windows.Forms.TrackBar trackTextureLight;
        private System.Windows.Forms.CheckBox chkTextureLight;
        private System.Windows.Forms.TabPage tabText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackTextRes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackTextDarkness;
    }
}

