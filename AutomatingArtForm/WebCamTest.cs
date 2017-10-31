using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace AutomatingArtForm
{
    public partial class WebCamTest : Form
    {

        enum eDrawType { Camera, Lines, Texture, Text};

        BackgroundWorker bw;
        Emgu.CV.VideoCapture capture;
        bool BreakLoop = false;
        VisualMagic.Lines.cLineCalculator LineCalculator;
        VisualMagic.Texture.cTextureCalculator TextureCalculator;

        VisualMagic.Text.cTextCalculator TextCalculator;
        eDrawType DrawType = eDrawType.Camera;

        //Texture
        int TextureThresholdValue = 50;
        int TextureLightValue = 50;
        int TextureDarkValue = 50;
        bool TextureLightEnable = false;
        bool TextureDarkEnable = false;
        double TextDarkness = 0.5;
        int TextResolution = 50;

        int RandVal = 31;
        int NumMasks = 10;
        public WebCamTest()
        {
            InitializeComponent();

            capture = new Emgu.CV.VideoCapture(); //create a camera capture
            int width = capture.Width;
            int height = capture.Height;
            LineCalculator = new VisualMagic.Lines.cLineCalculator();
            LineCalculator.Parameters.Initialize(NumMasks, width, height, RandVal);

            TextureCalculator = new VisualMagic.Texture.cTextureCalculator();
            TextureCalculator.Parameters.Initialize(width, height);

         
            TextCalculator = new VisualMagic.Text.cTextCalculator();
            TextCalculator.Parameters.Initialize(width, height);
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
            label1.Text = RandVal.ToString();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (BreakLoop == false)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    DoDraw();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bw.CancelAsync();
            LineCalculator.Parameters = new VisualMagic.Lines.cLineParameters();
          
            int width = capture.Width;
            int height = capture.Height;
            RandVal = RandVal + 1;
            LineCalculator.Parameters.Initialize(NumMasks, width, height, RandVal);
          
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
            label1.Text = RandVal.ToString();

        }

        private void DoDraw()
        {
            Emgu.CV.Mat CurrentFrame = capture.QueryFrame();





            switch (DrawType)
            {
                case eDrawType.Camera:

                    imageArt.Image = CurrentFrame; //draw the image obtained from camera
                    break;
                case eDrawType.Lines:
                    LineCalculator.SourceImage.GrayVal = CurrentFrame.ToImage<Emgu.CV.Structure.Gray, byte>();
                    LineCalculator.SourceImage.GrayVal = LineCalculator.SourceImage.GrayVal.Resize(LineCalculator.Parameters.xsize, LineCalculator.Parameters.ysize, Emgu.CV.CvEnum.Inter.Cubic);
                    LineCalculator.ArtImage.ColorVal = new Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>(LineCalculator.Parameters.xsize, LineCalculator.Parameters.ysize);


                    LineCalculator.CreateImage();


                    
                    // ArtFrame = CvInvoke.CvArrToMat(ResultImage, false, true, 0);

                    imageArt.Image = LineCalculator.ArtImage.ColorVal;
                    break;
                case eDrawType.Texture:
                    //Texture
                    TextureCalculator.SourceImage.GrayVal = CurrentFrame.ToImage<Emgu.CV.Structure.Gray, byte>();
                    TextureCalculator.SourceImage.GrayVal = TextureCalculator.SourceImage.GrayVal.Resize(TextureCalculator.Parameters.xsize, TextureCalculator.Parameters.ysize, Emgu.CV.CvEnum.Inter.Cubic);
                    TextureCalculator.ArtImage.ColorVal = new Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>(TextureCalculator.Parameters.xsize, TextureCalculator.Parameters.ysize);
                    TextureCalculator.Parameters.ThresholdPercent =Convert.ToDouble(TextureThresholdValue) / 100;
                    TextureCalculator.Parameters.TextureLight = TextureLightEnable;
                    TextureCalculator.Parameters.TextureLightSquish = Convert.ToDouble(TextureLightValue) / 100;
                    TextureCalculator.Parameters.TextureDark = TextureDarkEnable;
                    TextureCalculator.Parameters.TextureDarkSquish = Convert.ToDouble(TextureDarkValue) / 100;

                    TextureCalculator.CreateImage();


                 
                    // ArtFrame = CvInvoke.CvArrToMat(ResultImage, false, true, 0);

                    imageArt.Image = TextureCalculator.ArtImage.ColorVal;
                    break;
                case eDrawType.Text:
                    TextCalculator.SourceImage.GrayVal = CurrentFrame.ToImage<Emgu.CV.Structure.Gray, byte>();
                    TextCalculator.SourceImage.GrayVal = TextCalculator.SourceImage.GrayVal.Resize(TextCalculator.Parameters.xsize, TextCalculator.Parameters.ysize, Emgu.CV.CvEnum.Inter.Cubic);
                    TextCalculator.ArtImage.ColorVal = new Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>(TextCalculator.Parameters.xsize, TextCalculator.Parameters.ysize);

                    TextCalculator.Parameters.Resolution = TextResolution;
                    TextCalculator.Parameters.Darkness = TextDarkness;
                    TextCalculator.CreateImage();
                    imageArt.Image = TextCalculator.ArtImage.ColorVal;
                    break;

            }
         
        }
        private void TabControl1_SelectedIndexChanged(Object sender, EventArgs e)
        {

            MessageBox.Show("You are in the TabControl.SelectedIndexChanged event.");

        }

        private void tabViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            bw.CancelAsync();
            if (tabViews.SelectedTab.Text== "Lines")
            {
                DrawType = eDrawType.Lines;
            }
            else if (tabViews.SelectedTab.Text == "Texture")
            {
               
                DrawType = eDrawType.Texture;
            }
            else if (tabViews.SelectedTab.Text == "Camera")
            {
                DrawType = eDrawType.Camera;
            }
            else
            {
                DrawType = eDrawType.Text;
            }
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();

        }

        private void trackTextureThreshold_Scroll(object sender, EventArgs e)
        {
           TextureThresholdValue = trackTextureThreshold.Value;

        }

        private void chkTextureLight_CheckedChanged(object sender, EventArgs e)
        {
            TextureLightEnable = chkTextureLight.Checked;
        }

        private void chkTextureDark_CheckedChanged(object sender, EventArgs e)
        {
            TextureDarkEnable = chkTextureDark.Checked;
        }

        private void trackTextureLight_Scroll(object sender, EventArgs e)
        {
            TextureLightValue = trackTextureLight.Value;
        }

        private void trackTextureDark_Scroll(object sender, EventArgs e)
        {
            TextureDarkValue = trackTextureDark.Value;
        }

        private void trackTextRes_Scroll(object sender, EventArgs e)
        {
            TextResolution = trackTextRes.Value;
        }

        private void trackTextDarkness_Scroll(object sender, EventArgs e)
        {
            TextDarkness = trackTextDarkness.Value/100;
        }



        //private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    bw.CancelAsync();
        //    if (cboType.Text=="Lines")
        //    {
        //        DrawType = eDrawType.Lines;
        //    }
        //    else
        //    {
        //        DrawType = eDrawType.Texture;
        //    }

        //    bw = new BackgroundWorker();
        //    bw.WorkerSupportsCancellation = true;
        //    bw.WorkerReportsProgress = true;
        //    bw.DoWork += bw_DoWork;
        //    bw.RunWorkerAsync();
        //    label1.Text = RandVal.ToString();

        //}


    }
}
