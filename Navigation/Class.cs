using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation
{
    class KoorSput
    {
        public double X;
        public double Y;
        public double Z;

     
        /// <summary>
        /// Расстояние от i-го спутника до источкина радиоизлучения
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public double R(double x,double y,double z)
        {
            double R = Math.Sqrt((x - X) * (x - X) + (y - Y) * (y - Y) + (z - Z) * (z - Z));
            return R;
        }
    }
}
