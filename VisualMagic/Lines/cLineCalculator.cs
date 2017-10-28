using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMagic.Lines
{
    class cTriplet
    {
        public Byte Red;
        public Byte Blue;
        public Byte Green;
        public cTriplet(Byte R, Byte B, Byte G)
        {
            Red = R;
            Blue = B;
            Green = G;
        }
    }

    public class cLineCalculator
    {
        public cLineParameters Parameters;
        public cContentImage SourceImage;
        public cContentImage ArtImage;
        Byte[,,] mSourceBytes;
        Byte[,,] mArtBytes;
        double Percent = 0.2;
        Dictionary<int, cTriplet> DColors;


        public cLineCalculator()
        {
            Parameters = new cLineParameters();
            SourceImage = new cContentImage();
            ArtImage = new cContentImage();
            DColors = new Dictionary<int, cTriplet>();
        }


        public void CreateImage()
        {
            mSourceBytes = SourceImage.GrayVal.Data;
            mArtBytes = ArtImage.ColorVal.Data;

            //First Determine if the original Image meets the requirements
            int i;
            int j;


            for (i = 0; i < Parameters.ysize; i++)
            {
                for (j = 0; j < Parameters.xsize; j++)
                {

                    ApplyMaskFast(i, j);

                }

            }


        }


        void ApplyMaskFast(int i, int j)
        {
            int k;

            Byte GrayVal = (Byte)(255 - mSourceBytes[i, j, 0]);
            int NumSegements = (int)(GrayVal / Parameters.SegmentLength);
            if (NumSegements > Parameters.NumMasks)
            {
                NumSegements = Parameters.NumMasks;
            }
            else if (NumSegements == 0)
            {

                mArtBytes[i, j, 0] = 255;
                mArtBytes[i, j, 1] = 255;
                mArtBytes[i, j, 2] = 255;
                return;
            }

            Byte SumRed = 255;
            Byte SumGreen = 255;
            Byte SumBlue = 255;
            Byte Red;
            Byte Blue;
            Byte Green;
            int Key = 0;
            for (k = 0; k < NumSegements; k++)
            {
                if (Parameters.MaskArray[j, i, k] == true)
                {
                    Key = Key + (int)Math.Pow(2, (double)k);
                }
            }
            if (DColors.ContainsKey(Key) == true)
            {
                cTriplet ResultColor;
                ResultColor = DColors[Key];
                mArtBytes[i, j, 0] = (ResultColor.Red);
                mArtBytes[i, j, 1] = (ResultColor.Blue);
                mArtBytes[i, j, 2] = ResultColor.Green;

            }
            else
            {
                //Build the Key
                for (k = 0; k < NumSegements; k++)
                {
                    if (Parameters.MaskArray[j, i, k] == true)
                    {
                        Red = Parameters.RedColors[k];
                        Blue = Parameters.BlueColors[k];
                        Green = Parameters.GreenColors[k];
                        //SumRed =SumRed+ Parameters.MaskArray[k].Data[i, j, 0];
                        //SumGreen = SumGreen + Parameters.MaskArray[k].Data[i, j, 1];
                        //SumBlue = SumBlue + Parameters.MaskArray[k].Data[i, j, 2];
                        AddColorVals(ref SumRed, ref SumBlue, ref SumGreen, Red, Blue, Green, Percent);
                    }
                }
                cTriplet NewColor = new cTriplet(SumRed, SumBlue, SumGreen);
                DColors.Add(Key, NewColor);
                mArtBytes[i, j, 0] = (SumRed);
                mArtBytes[i, j, 1] = (SumBlue);
                mArtBytes[i, j, 2] = SumGreen;

            }





        }

        static void AddColorVals(ref Byte RedSource, ref Byte BlueSource, ref Byte GreenSource, Byte RedAdd, Byte BlueAdd, Byte GreenAdd, double Percent)
        {
            if ((RedAdd != 255) || (BlueAdd != 255) || (GreenAdd != 255))
            {
                //  if ((RedSource != 255) || (BlueSource != 255) || (GreenSource != 255))
                //  {

                double RedN = (double)RedSource / 255.0;
                double RedS = Math.Pow((1.0 - RedN), 2.0) / (2.0 * RedN);
                RedN = (double)RedAdd / 255.0;
                double RedA = Math.Pow((1.0 - RedN), 2.0) / (2.0 * RedN);
                double RedSum = (RedS + Percent * RedA) / (1 + Percent);
                double RedR = 255 * (1.0 + RedSum - Math.Sqrt(Math.Pow(RedSum, 2.0) + (2.0 * RedSum)));

                double BlueN = (double)BlueSource / 255.0;
                double BlueS = Math.Pow((1.0 - BlueN), 2.0) / (2.0 * BlueN);
                BlueN = (double)BlueAdd / 255.0;
                double BlueA = Math.Pow((1.0 - BlueN), 2.0) / (2.0 * BlueN);
                double BlueSum = (BlueS + Percent * BlueA) / (1 + Percent);
                double BlueR = 255 * (1.0 + BlueSum - Math.Sqrt(Math.Pow(BlueSum, 2.0) + (2.0 * BlueSum)));

                double GreenN = (double)GreenSource / 255.0;
                double GreenS = Math.Pow((1.0 - GreenN), 2.0) / (2.0 * GreenN);
                GreenN = (double)GreenAdd / 255.0;
                double GreenA = Math.Pow((1.0 - GreenN), 2.0) / (2.0 * GreenN);
                double GreenSum = (GreenS + Percent * GreenA) / (1 + Percent);
                double GreenR = 255 * (1.0 + GreenSum - Math.Sqrt(Math.Pow(GreenSum, 2.0) + (2.0 * GreenSum)));

                RedSource = (Byte)RedR;
                BlueSource = (Byte)BlueR;
                GreenSource = (Byte)GreenR;


            }
        }
    }
}
