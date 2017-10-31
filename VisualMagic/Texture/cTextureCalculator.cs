using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMagic.Texture
{
   public class cTextureCalculator
    {
        public cTextureParameters Parameters;
        public cContentImage SourceImage;
        public cContentImage ArtImage;
        Byte[,,] mSourceBytes;
        Byte[,,] mArtBytes;

        public cTextureCalculator()
        {
            Parameters = new cTextureParameters();
            SourceImage = new cContentImage();
            ArtImage = new cContentImage();
          
        }

        public void CreateImage()
        {
            mSourceBytes = SourceImage.GrayVal.Data;
            mArtBytes = ArtImage.ColorVal.Data;

            int BinSize =256;
            Emgu.CV.DenseHistogram Histo = new  Emgu.CV.DenseHistogram(255, new Emgu.CV.Structure.RangeF(0, BinSize-1));

            float[] GrayHist;
            GrayHist = new float[BinSize];

            Histo.Calculate(new Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte>[] { SourceImage.GrayVal }, true, null);
           
            GrayHist = Histo.GetBinValues();

            int k;
            double Total=0;
            for (k=0; k< BinSize-1; k++)
            {
                Total = Total + GrayHist[k];
            }
            double SwtichPoint = Total *Parameters.ThresholdPercent;
            k = 0;
            Total = 0;
            while (Total< SwtichPoint)
            {
                Total = Total + GrayHist[k];
                k = k + 1;
            }
            double TotalReverse = 0;
            int kReverse = BinSize - 2;
            while (TotalReverse < SwtichPoint)
            {
                TotalReverse = TotalReverse + GrayHist[kReverse];
                kReverse = kReverse -1;
            }

            //The data is here
            //Histo.MatND.ManagedArray
            Byte HistSwitch = Convert.ToByte(k);
          
            //First Determine if the original Image meets the requirements
            int i;
            int j;

          
            for (i = 0; i < Parameters.ysize; i++)
            {
                for (j = 0; j < Parameters.xsize; j++)
                {
                    Byte GrayVal = (Byte)(mSourceBytes[i, j, 0]);
                    Byte SetVal = 0;
                    if (GrayVal < HistSwitch)
                    {
                        if (Parameters.TextureDark==true)
                        {
                            double Val = Parameters.TextureDarkSquish * Convert.ToDouble(GrayVal) / Convert.ToDouble(HistSwitch);
                            SetVal = Convert.ToByte(128 *(Val));
                        }
                        else
                        {
                            SetVal = 0;
                        }
                    
                    }
                    else
                    {
                        if (Parameters.TextureLight == true)
                        {
                            double Val = Parameters.TextureLightSquish * (255 -Convert.ToDouble(GrayVal)) /(255- Convert.ToDouble(HistSwitch));
                            SetVal =  Convert.ToByte(255 - Val* (255 - Convert.ToDouble(HistSwitch)));
                        }
                        else
                        {
                            SetVal = 255;
                        }
                    }

                    //ApplyMaskFast(i, j);
                    mArtBytes[i, j, 0] = SetVal;
                    mArtBytes[i, j, 1] = SetVal;
                    mArtBytes[i, j, 2] = SetVal;
                }

            }
        }
    }
}
