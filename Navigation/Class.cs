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

        public double R(double x,double y,double z)
        {
            double R = Math.Sqrt((x - X) * (x - X) + (y - Y) * (y - Y) + (z - Z) * (z - Z));
            return R;
        }
    }
}
