using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMagic.Text
{
    public class cTextParameters
    {
        public int xsize;
        public int ysize;
        public double Darkness;
        public int Resolution;
  

    public void Initialize(int lxsize, int lysize)
    {
            Resolution = 50;
            Darkness = 0.5;
        xsize = lxsize;
        ysize = lysize;
    }
}
}

