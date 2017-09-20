using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper
{
    public class Graphic
    {
        public static int GetPixel(IntPtr hdc, int nXPos, int nYPos)
        {
            return Api.GetPixel(hdc, nXPos, nYPos);
        }
    }
}
