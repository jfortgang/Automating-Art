using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMagic.Texture
{
    public class cTextureParameters
    {
       
        public int xsize;
        public int ysize;
        public double ThresholdPercent;
        public bool TextureDark;
        public bool TextureLight;
        public double TextureLightSquish;
        public double TextureDarkSquish;


        public void Initialize(int lxsize, int lysize)
        {
           
            xsize = lxsize;
            ysize = lysize;
            ThresholdPercent = 0.5;
                TextureDark=false;
        TextureLight = false;
         TextureLightSquish = .1;
         TextureDarkSquish = .1;
    }
    }
}
