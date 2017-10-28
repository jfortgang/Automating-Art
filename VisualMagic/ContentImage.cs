using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMagic
{
    public class cContentImage
    {
        public string Creator;
        public DateTime CreationDate;
        public string Source;
        public Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> GrayVal;
        public Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> ColorVal;

    }
}
