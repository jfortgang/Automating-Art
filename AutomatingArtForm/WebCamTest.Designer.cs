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
            this.CamImage = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageArt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CamImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(378, 475);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // imageArt
            // 
            this.imageArt.Location = new System.Drawing.Point(597, 10);
            this.imageArt.Name = "imageArt";
            this.imageArt.Size = new System.Drawing.Size(545, 445);
            this.imageArt.TabIndex = 9;
            this.imageArt.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(297, 470);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CamImage
            // 
            this.CamImage.Location = new System.Drawing.Point(2, 10);
            this.CamImage.Name = "CamImage";
            this.CamImage.Size = new System.Drawing.Size(589, 445);
            this.CamImage.TabIndex = 7;
            this.CamImage.TabStop = false;
            // 
            // WebCamTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 510);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imageArt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CamImage);
            this.Name = "WebCamTest";
            this.Text = "WebCamTest";
            ((System.ComponentModel.ISupportInitialize)(this.imageArt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CamImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Emgu.CV.UI.ImageBox imageArt;
        private System.Windows.Forms.Button button1;
        private Emgu.CV.UI.ImageBox CamImage;
    }
}

