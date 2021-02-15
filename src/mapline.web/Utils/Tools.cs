using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Utils
{
    public static class Tools
    {
        public static int ConvertNegativeIntoPositive(int value)
        {
            // https://stackoverflow.com/questions/46930513/how-to-convert-negative-number-to-positive-in-c/46930615
            // Fastest option
            return (value + (value >> 31)) ^ (value >> 31);
        }
    }
}
