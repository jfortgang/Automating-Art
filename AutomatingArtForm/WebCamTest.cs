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
     
        BackgroundWorker bw;
        Emgu.CV.VideoCapture capture;
        bool BreakLoop = false;
        VisualMagic.Lines.cLineCalculator LineCalculator;
       
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




            CamImage.Image = CurrentFrame; //draw the image obtained from camera

            //Image<Gray, byte> SourceImage = CurrentFrame.ToImage<Gray, byte>();
            //SourceImage = SourceImage.Resize(mMaskValues.xsize, mMaskValues.ysize, Emgu.CV.CvEnum.Inter.Cubic);
            //imageArt.Image = SourceImage;
            //return;

            LineCalculator.SourceImage.GrayVal = CurrentFrame.ToImage<Emgu.CV.Structure.Gray, byte>();
            LineCalculator.SourceImage.GrayVal = LineCalculator.SourceImage.GrayVal.Resize(LineCalculator.Parameters.xsize, LineCalculator.Parameters.ysize, Emgu.CV.CvEnum.Inter.Cubic);
            LineCalculator.ArtImage.ColorVal = new Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>(LineCalculator.Parameters.xsize, LineCalculator.Parameters.ysize);


            LineCalculator.CreateImage();
         

            Emgu.CV.Mat ArtFrame = new Emgu.CV.Mat();
            // ArtFrame = CvInvoke.CvArrToMat(ResultImage, false, true, 0);

            imageArt.Image = LineCalculator.ArtImage.ColorVal;
        }

      
    }
}
