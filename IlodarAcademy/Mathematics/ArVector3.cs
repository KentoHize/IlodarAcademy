using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Aritiafel.Organizations.RaeriharUniversity
{
    public struct ArVector3
    {
        public ArVector3(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static ArVector3 Zero = new ArVector3();

        public ArVector3(double x, double y, double z)
            : this((long)x, (long)y, (long)z)
        { }

        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public static ArVector3 operator +(ArVector3 a, ArVector3 b) 
            => new ArVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static ArVector3 operator -(ArVector3 a, ArVector3 b)
            => new ArVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static ArVector3 operator *(ArVector3 a, double b)
            => new ArVector3(a.X * b, a.Y * b, a.Z * b);
        public static ArVector3 operator /(ArVector3 a, double b)
            => new ArVector3((double)a.X / b, (double)a.Y / b, (double)a.Z / b);


        //public static ArVector3 operator *(ArVector3 a, ArVector3 b)
        //    => new ArVector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        //public static ArVector3 operator /(ArVector3 a, ArVector3 b)
        //    => new ArVector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
    }
}
