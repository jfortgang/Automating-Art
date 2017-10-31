using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VisualMagic.Text
{
    public class cTextCalculator
    {
        public cTextParameters Parameters;
        public cContentImage SourceImage;
        public cContentImage ArtImage;
        Byte[,,] mSourceBytes;
        Byte[,,] mArtBytes;
        List<string> TextToUse;
        int TextIterator;
        Random rnd = new Random();

        public cTextCalculator()
        {
            Parameters = new cTextParameters();
             SourceImage = new cContentImage();
            ArtImage = new cContentImage();
            TextToUse = new List<string>();
            FillStringList();
            TextIterator = 0;
        }

        private string GetNextText()
        {
            string ReturnString="";
            ReturnString = TextToUse[TextIterator];
            TextIterator += 1;
            if (TextIterator >= TextToUse.Count())
            {
                TextIterator = 0;
            }
            return ReturnString;

        }

        double GetArrayAverages(int IVal, int JVal, int AverageSize, int YSize, int XSize, ref Byte[,,] PassedBytes)
         {
        
            int XRes = Convert.ToInt32(Parameters.xsize / AverageSize);
            int YRes = Convert.ToInt32(Parameters.ysize / AverageSize);
            int i;
            int j;
            double SumVal=0;
            int NumPoints=0;

            for (i = YRes*IVal; i < YRes * (IVal+1); i++)
            {
               

                for (j = XRes*JVal; j < XRes *( JVal+1); j++)
                {
                    int GrayVal = (int)(PassedBytes[i, j, 0]);


                    SumVal += GrayVal;
                    NumPoints += 1;
                }

            }
            double ReturnVal;
            ReturnVal = SumVal / Convert.ToDouble(NumPoints);
            return ReturnVal;
              
        }
        void ResetSection(int IVal, int JVal, int AverageSize, int YSize, int XSize, double AverageValue, ref Byte[,,] PassedBytes)
        {
            int XRes = Convert.ToInt32(Parameters.xsize / AverageSize);
            int YRes = Convert.ToInt32(Parameters.ysize / AverageSize);
            int i;
            int j;
            //First Make all Ones
            for (i = YRes * IVal; i < YRes * (IVal + 1); i++)
            {


                for (j = XRes * JVal; j < XRes * (JVal + 1); j++)
                {
                    PassedBytes[i, j, 0] = Convert.ToByte(255);
                    PassedBytes[i, j, 1] = Convert.ToByte(255);
                    PassedBytes[i, j, 2] = Convert.ToByte(255);
                }

            }

        }


        void AddTextTillAverage(int IVal, int JVal, int AverageSize, int YSize, int XSize, double AverageValue,  ref Byte[,,] PassedBytes)
        {
            int XRes = Convert.ToInt32(Parameters.xsize / AverageSize);
            int YRes = Convert.ToInt32(Parameters.ysize / AverageSize);
            int i;
            int j;
          
            bool BreakLoop = false;
            double CurAverage=0;
            int count = 0;
            while (BreakLoop==false)
            {
                ++count;
                if (count>10000)
                {
                    BreakLoop = true;
                }
                CurAverage = 255;

                //Now Add Text Till the Average is met
                string AddString = GetNextText();

                //Create the font
                double ScaleVal = rnd.NextDouble();
                int YLocation = rnd.Next(0, YRes);
                int XLocation = rnd.Next(0, XRes);
                int TextGrayVal = rnd.Next(0, Convert.ToInt32(AverageValue));
                //Now Compute the new average value
                CurAverage = GetArrayAverages(IVal, JVal, AverageSize, YSize, XSize, ref PassedBytes);
                System.Drawing.Point AddLocation = new System.Drawing.Point(XRes * JVal+XLocation, YRes * IVal+YLocation);
                Emgu.CV.CvInvoke.PutText(ArtImage.ColorVal, AddString, AddLocation, Emgu.CV.CvEnum.FontFace.HersheyComplex, ScaleVal, new Emgu.CV.Structure.Bgr(TextGrayVal, TextGrayVal, TextGrayVal).MCvScalar);

                //Now Compute the new average value
                CurAverage = GetArrayAverages(IVal, JVal, AverageSize, YSize, XSize, ref PassedBytes);
                CurAverage = CurAverage * Parameters.Darkness;
                if (CurAverage< (AverageValue))
                {
                    BreakLoop = true;
                }
            }


        }


        void AddRandomTextInBlock(int IVal, int JVal, int YRes, int XRes, int AverageSize, int YSize, int XSize, double AverageValue, ref Byte[,,] mArtBytes)
        {
            //Now Add Text Till the Average is met
            string AddString = GetNextText();

            //Create the font
            double ScaleVal = rnd.NextDouble();
            int YLocation = rnd.Next(0, YRes);
            int XLocation = rnd.Next(0, XRes);
            System.Drawing.Point AddLocation = new System.Drawing.Point(XRes * JVal + XLocation, YRes * IVal + YLocation);
            Emgu.CV.CvInvoke.PutText(ArtImage.ColorVal, AddString, AddLocation, Emgu.CV.CvEnum.FontFace.HersheyComplex, ScaleVal, new Emgu.CV.Structure.Bgr(0, 0, 0).MCvScalar);


        }
        public void CreateImage()
        {
            mSourceBytes = SourceImage.GrayVal.Data;
            mArtBytes = ArtImage.ColorVal.Data;
            int AverageSize =Parameters.Resolution;
       
            double [,] AverageArray = new double[AverageSize, AverageSize] ;
         

            //Loop Through each section and build add the average value and num Points
            //First Determine if the original Image meets the requirements
            int i;
            int j;
            int XRes = Convert.ToInt32(Parameters.xsize / AverageSize);
            int YRes = Convert.ToInt32(Parameters.ysize / AverageSize);
          

        
            for (i = 0; i < AverageSize; i++)
            {
                for (j = 0; j < AverageSize; j++)
                {
                    double TestVal;
                    TestVal = GetArrayAverages(j, i, AverageSize, Parameters.ysize, Parameters.xsize, ref mSourceBytes);
                    AverageArray[i, j] = TestVal;
                }

            }
            //REset the image
            for (i = 0; i < AverageSize; i++)
            {
                for (j = 0; j < AverageSize; j++)
                {
                    //Add Text Until It Reaches the Average
                    ResetSection(j, i, AverageSize, Parameters.ysize, Parameters.xsize, AverageArray[i, j], ref mArtBytes);
                }

            }

            for (i = 0; i < AverageSize; i++)
            {
                for (j = 0; j < AverageSize; j++)
                {
                    //Add Text Until It Reaches the Average
                    AddTextTillAverage(j, i, AverageSize, Parameters.ysize, Parameters.xsize, AverageArray[i, j], ref mArtBytes);
                }

            }

            TextIterator = 0;
            //for (i = 0; i < Parameters.ysize; i++)
            //{
            //    Yindex = i / YRes;

            //    for (j = 0; j < Parameters.xsize; j++)
            //    {

            //        Xindex = j / XRes;
            //        Byte SetVal = Convert.ToByte(AverageArray[Xindex, Yindex]);
            //        mArtBytes[i, j, 0] = SetVal;
            //        mArtBytes[i, j, 1] = SetVal;
            //        mArtBytes[i, j, 2] = SetVal;
            //    }

            //}

            ////Create the font
            //Emgu.CV.CvInvoke.PutText(ArtImage.ColorVal, "w",new System.Drawing.Point(10, 80), Emgu.CV.CvEnum.FontFace.HersheyComplex,  1.0, new Emgu.CV.Structure.Bgr(255, 255, 255).MCvScalar);



        }


      


        private void FillStringList()
        {
            TextToUse.Add("John");
            TextToUse.Add("Henry");
            TextToUse.Add("was");
            TextToUse.Add("a");
            TextToUse.Add("railroad");
            TextToUse.Add("man,");
            TextToUse.Add("He");
            TextToUse.Add("worked");
            TextToUse.Add("from");
            TextToUse.Add("six");
            TextToUse.Add("'till");
            TextToUse.Add("five,");
            TextToUse.Add("Raise");
            TextToUse.Add("'em");
            TextToUse.Add("up");
            TextToUse.Add("bullies");
            TextToUse.Add("and");
            TextToUse.Add("let");
            TextToUse.Add("'em");
            TextToUse.Add("drop");
            TextToUse.Add("down,");
            TextToUse.Add("I'll");
            TextToUse.Add("beat");
            TextToUse.Add("you");
            TextToUse.Add("to");
            TextToUse.Add("the");
            TextToUse.Add("bottom");
            TextToUse.Add("or");
            TextToUse.Add("die.");
            TextToUse.Add("John");
            TextToUse.Add("Henry");
            TextToUse.Add("said");
            TextToUse.Add("to");
            TextToUse.Add("his");
            TextToUse.Add("captain:");
            TextToUse.Add("You");
            TextToUse.Add("are");
            TextToUse.Add("nothing");
            TextToUse.Add("but");
            TextToUse.Add("a");
            TextToUse.Add("common");
            TextToUse.Add("man,");
            TextToUse.Add("Before");
            TextToUse.Add("that");
            TextToUse.Add("steam");
            TextToUse.Add("drill");
            TextToUse.Add("shall");
            TextToUse.Add("beat");
            TextToUse.Add("me");
            TextToUse.Add("down,");
            TextToUse.Add("I'll");
            TextToUse.Add("die");
            TextToUse.Add("with");
            TextToUse.Add("my");
            TextToUse.Add("hammer");
            TextToUse.Add("in");
            TextToUse.Add("my");
            TextToUse.Add("hand.");
            TextToUse.Add("John");
            TextToUse.Add("Henry");
            TextToUse.Add("said");
            TextToUse.Add("to");
            TextToUse.Add("the");
            TextToUse.Add("Shakers:");
            TextToUse.Add("You");
            TextToUse.Add("must");
            TextToUse.Add("listen");
            TextToUse.Add("to");
            TextToUse.Add("my");
            TextToUse.Add("call,");
            TextToUse.Add("Before");
            TextToUse.Add("that");
            TextToUse.Add("steam");
            TextToUse.Add("drill");
            TextToUse.Add("shall");
            TextToUse.Add("beat");
            TextToUse.Add("me");
            TextToUse.Add("down,");
            TextToUse.Add("I'll");
            TextToUse.Add("jar");
            TextToUse.Add("these");
            TextToUse.Add("mountains");
            TextToUse.Add("till");
            TextToUse.Add("they");
            TextToUse.Add("fall.");
            TextToUse.Add("John");
            TextToUse.Add("Henry's");
            TextToUse.Add("captain");
            TextToUse.Add("said");
            TextToUse.Add("to");
            TextToUse.Add("him:");
            TextToUse.Add("I");
            TextToUse.Add("believe");
            TextToUse.Add("these");
            TextToUse.Add("mountains");
            TextToUse.Add("are");
            TextToUse.Add("caving");
            TextToUse.Add("in.");
            TextToUse.Add("John");
            TextToUse.Add("Henry");
            TextToUse.Add("said");
            TextToUse.Add("to");
            TextToUse.Add("his");
            TextToUse.Add("captain:");
            TextToUse.Add("Oh,");
            TextToUse.Add("Lord!");
            TextToUse.Add("That's");
            TextToUse.Add("my");
            TextToUse.Add("hammer");
            TextToUse.Add("you");
            TextToUse.Add("hear");
            TextToUse.Add("in");
            TextToUse.Add("the");
            TextToUse.Add("wind.");
            TextToUse.Add("John");
            TextToUse.Add("Henry");
            TextToUse.Add("he");
            TextToUse.Add("said");
            TextToUse.Add("to");
            TextToUse.Add("his");
            TextToUse.Add("captain:");
            TextToUse.Add("Your");
            TextToUse.Add("money");
            TextToUse.Add("is");
            TextToUse.Add("getting");
            TextToUse.Add("mighty");
            TextToUse.Add("slim,");
            TextToUse.Add("When");
            TextToUse.Add("I");
            TextToUse.Add("hammer");
            TextToUse.Add("through");
            TextToUse.Add("this");
            TextToUse.Add("old");
            TextToUse.Add("mountain,");
            TextToUse.Add("Oh");
            TextToUse.Add("Captain");
            TextToUse.Add("will");
            TextToUse.Add("you");
            TextToUse.Add("walk");
            TextToUse.Add("in?");
            TextToUse.Add("John");
            TextToUse.Add("Henry's");
            TextToUse.Add("captain");
            TextToUse.Add("came");
            TextToUse.Add("to");
            TextToUse.Add("him");
            TextToUse.Add("With");
            TextToUse.Add("fifty");
            TextToUse.Add("dollars");
            TextToUse.Add("in");
            TextToUse.Add("his");
            TextToUse.Add("hand,");
            TextToUse.Add("He");
            TextToUse.Add("laid");
            TextToUse.Add("his");
            TextToUse.Add("hand");
            TextToUse.Add("on");
            TextToUse.Add("his");
            TextToUse.Add("shoulder");
            TextToUse.Add("and");
            TextToUse.Add("said:");
            TextToUse.Add("This");
            TextToUse.Add("belongs");
            TextToUse.Add("to");
            TextToUse.Add("a");
            TextToUse.Add("steel");
            TextToUse.Add("driving");
            TextToUse.Add("man.");
            TextToUse.Add("John");
            TextToUse.Add("Henry");
            TextToUse.Add("was");
            TextToUse.Add("hammering");
            TextToUse.Add("on");
            TextToUse.Add("the");
            TextToUse.Add("right");
            TextToUse.Add("side,");
            TextToUse.Add("The");
            TextToUse.Add("big");
            TextToUse.Add("steam");
            TextToUse.Add("drill");
            TextToUse.Add("on");
            TextToUse.Add("the");
            TextToUse.Add("left,");
            TextToUse.Add("Before");
            TextToUse.Add("that");
            TextToUse.Add("steam");
            TextToUse.Add("drill");
            TextToUse.Add("could");
            TextToUse.Add("beat");
            TextToUse.Add("him");
            TextToUse.Add("down,");
            TextToUse.Add("He");
            TextToUse.Add("hammered");
            TextToUse.Add("his");
            TextToUse.Add("fool");
            TextToUse.Add("self");
            TextToUse.Add("to");
            TextToUse.Add("death.");
            TextToUse.Add("They");
            TextToUse.Add("carried");
            TextToUse.Add("John");
            TextToUse.Add("Henry");
            TextToUse.Add("to");
            TextToUse.Add("the");
            TextToUse.Add("mountains,");
            TextToUse.Add("From");
            TextToUse.Add("his");
            TextToUse.Add("shoulder");
            TextToUse.Add("his");
            TextToUse.Add("hammer");
            TextToUse.Add("would");
            TextToUse.Add("ring,");
            TextToUse.Add("She");
            TextToUse.Add("caught");
            TextToUse.Add("on");
            TextToUse.Add("fire");
            TextToUse.Add("by");
            TextToUse.Add("a");
            TextToUse.Add("little");
            TextToUse.Add("blue");
            TextToUse.Add("blaze");
            TextToUse.Add("I");
            TextToUse.Add("believe");
            TextToUse.Add("these");
            TextToUse.Add("old");
            TextToUse.Add("mountains");
            TextToUse.Add("are");
            TextToUse.Add("caving");
            TextToUse.Add("in.");
            TextToUse.Add("John");
            TextToUse.Add("Henry");
            TextToUse.Add("was");
            TextToUse.Add("lying");
            TextToUse.Add("on");
            TextToUse.Add("his");
            TextToUse.Add("death");
            TextToUse.Add("bed,");
            TextToUse.Add("He");
            TextToUse.Add("turned");
            TextToUse.Add("over");
            TextToUse.Add("on");
            TextToUse.Add("his");
            TextToUse.Add("side,");
            TextToUse.Add("And");
            TextToUse.Add("these");
            TextToUse.Add("were");
            TextToUse.Add("the");
            TextToUse.Add("last");
            TextToUse.Add("words");
            TextToUse.Add("John");
            TextToUse.Add("Henry");
            TextToUse.Add("said");
            TextToUse.Add("Bring");
            TextToUse.Add("me");
            TextToUse.Add("a");
            TextToUse.Add("cool");
            TextToUse.Add("drink");
            TextToUse.Add("of");
            TextToUse.Add("water");
            TextToUse.Add("before");
            TextToUse.Add("I");
            TextToUse.Add("die.");
            TextToUse.Add("John");
            TextToUse.Add("Henry");
            TextToUse.Add("had");
            TextToUse.Add("a");
            TextToUse.Add("little");
            TextToUse.Add("woman,");
            TextToUse.Add("Her");
            TextToUse.Add("name");
            TextToUse.Add("was");
            TextToUse.Add("Pollie");
            TextToUse.Add("Ann,");
            TextToUse.Add("He");
            TextToUse.Add("hugged");
            TextToUse.Add("and");
            TextToUse.Add("kissed");
            TextToUse.Add("her");
            TextToUse.Add("just");
            TextToUse.Add("before");
            TextToUse.Add("he");
            TextToUse.Add("died,");
            TextToUse.Add("Saying,");
            TextToUse.Add("Pollie,");
            TextToUse.Add("do");
            TextToUse.Add("the");
            TextToUse.Add("very");
            TextToUse.Add("best");
            TextToUse.Add("you");
            TextToUse.Add("can.");
            TextToUse.Add("John");
            TextToUse.Add("Henry's");
            TextToUse.Add("woman");
            TextToUse.Add("heard");
            TextToUse.Add("he");
            TextToUse.Add("was");
            TextToUse.Add("dead,");
            TextToUse.Add("She");
            TextToUse.Add("could");
            TextToUse.Add("not");
            TextToUse.Add("rest");
            TextToUse.Add("on");
            TextToUse.Add("her");
            TextToUse.Add("bed,");
            TextToUse.Add("She");
            TextToUse.Add("got");
            TextToUse.Add("up");
            TextToUse.Add("at");
            TextToUse.Add("midnight,");
            TextToUse.Add("caught");
            TextToUse.Add("that");
            TextToUse.Add("No.");
            TextToUse.Add("4");
            TextToUse.Add("train,");
            TextToUse.Add("I");
            TextToUse.Add("am");
            TextToUse.Add("going");
            TextToUse.Add("where");
            TextToUse.Add("John");
            TextToUse.Add("Henry");
            TextToUse.Add("fell");
            TextToUse.Add("dead.");
            TextToUse.Add("They");
            TextToUse.Add("carried");
            TextToUse.Add("John");
            TextToUse.Add("Henry");
            TextToUse.Add("to");
            TextToUse.Add("that");
            TextToUse.Add("new");
            TextToUse.Add("burying");
            TextToUse.Add("ground");
            TextToUse.Add("His");
            TextToUse.Add("wife");
            TextToUse.Add("all");
            TextToUse.Add("dressed");
            TextToUse.Add("in");
            TextToUse.Add("blue,");
            TextToUse.Add("She");
            TextToUse.Add("laid");
            TextToUse.Add("her");
            TextToUse.Add("hand");
            TextToUse.Add("on");
            TextToUse.Add("John");
            TextToUse.Add("Henry's");
            TextToUse.Add("cold");
            TextToUse.Add("face,");
            TextToUse.Add("John");
            TextToUse.Add("Henry");
            TextToUse.Add("I've");
            TextToUse.Add("been");
            TextToUse.Add("true");
            TextToUse.Add("to");
            TextToUse.Add("you.");

        }
    }
}
