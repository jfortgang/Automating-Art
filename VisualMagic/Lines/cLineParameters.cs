using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMagic.Lines
{
    public class cLineParameters
    {
        public bool[,,] MaskArray;
        public int xsize;
        public int ysize;
        public int NumMasks;
        public int[] OnNums;
        public int[] OffNums;
        public bool[] IsHorizontal;
        public Byte[] RedColors;
        public Byte[] BlueColors;
        public Byte[] GreenColors;
        public int[] MaskLimits;
        public double SegmentLength;
        public double[] RandThreshold;
        Random RandomGenerator;
        public void Initialize(int lNumMasks, int lxsize, int lysize, int RandVal)
        {
            int k;
            xsize = lxsize;
            ysize = lysize;

            NumMasks = lNumMasks;
            OnNums = new int[NumMasks];
            OffNums = new int[NumMasks];
            IsHorizontal = new bool[NumMasks];
            RedColors = new Byte[NumMasks];
            BlueColors = new Byte[NumMasks];
            GreenColors = new Byte[NumMasks];
            MaskArray = new bool[xsize, ysize, NumMasks];
            MaskLimits = new int[NumMasks];
            RandThreshold = new double[NumMasks];
            RandomGenerator = new Random(RandVal);
            GenColors();
            for (k = 0; k < NumMasks; k++)
            {

                OnNums[k] = RandomGenerator.Next(1, 20);
                OffNums[k] = RandomGenerator.Next(1, 20);
                if (RandomGenerator.NextDouble() >= 0.5)
                {
                    IsHorizontal[k] = true;
                }
                else
                {
                    IsHorizontal[k] = false;
                }
                MaskLimits[k] = (int)(k * 255.0 / NumMasks);
                RandThreshold[k] = 0;// (RandomGenerator.NextDouble())/5;
            }
            SegmentLength = 255 / (NumMasks + 1);
            //Now Build all the masks
            int TotalNum;
            int icount;

            bool UseVal;
            for (k = 0; k < NumMasks; k++)
            {

                TotalNum = OnNums[k] + OffNums[k];

                for (int i = 0; i < ysize; i++)
                {

                    for (int j = 0; j < xsize; j++)
                    {
                        if (IsHorizontal[k] == false)
                        {

                            icount = i % TotalNum;
                            if (icount < OnNums[k])
                            {
                                UseVal = true;
                            }
                            else
                            {
                                UseVal = false;
                            }
                            if (RandomGenerator.NextDouble() < RandThreshold[k])
                            {
                                UseVal = false;
                            }
                            MaskArray[j, i, k] = UseVal;
                        }
                        else
                        {
                            icount = j % TotalNum;
                            if (icount < OnNums[k])
                            {
                                UseVal = true;
                            }
                            else
                            {
                                UseVal = false;
                            }
                            if (RandomGenerator.NextDouble() < RandThreshold[k])
                            {
                                UseVal = false;
                            }
                            MaskArray[j, i, k] = UseVal;
                        }

                    }
                }
            }
        }
        enum ColorGenAlg
        {
            RandomRGB,
            RandomOffset,
            Gradient,
            Harmony,
            TriadMixing

        }
        void GenColors()
        {
            int k;
            ColorGenAlg Alg = ColorGenAlg.RandomOffset;
            switch (Alg)
            {
                case ColorGenAlg.RandomRGB:
                    {
                        for (k = 0; k < NumMasks; k++)
                        {

                            RedColors[k] = (Byte)(RandomGenerator.Next(0, 255));
                            BlueColors[k] = (Byte)(RandomGenerator.Next(0, 255));
                            GreenColors[k] = (Byte)(RandomGenerator.Next(0, 255));
                        }
                        break;
                    }
                case ColorGenAlg.RandomOffset:
                    {
                        RedColors[0] = (Byte)(RandomGenerator.Next(0, 255));
                        BlueColors[0] = (Byte)(RandomGenerator.Next(0, 255));
                        GreenColors[0] = (Byte)(RandomGenerator.Next(0, 255));
                        float offset = RandomGenerator.Next(0, 255);
                        offset = offset % 255; //Make 0 to 255
                        for (k = 1; k < NumMasks; k++)
                        {

                            RedColors[k] = (Byte)((RedColors[k - 1] + offset) % 255);
                            BlueColors[k] = (Byte)((BlueColors[k - 1] + offset) % 255);
                            GreenColors[k] = (Byte)((GreenColors[k - 1] + offset) % 255);
                        }

                        break;
                    }
                default:
                    {

                        break;
                    }
            }

        }
    }
}
