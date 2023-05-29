using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Aritiafel.Organizations.RaeriharUniversity;

namespace Aritiafel.Organizations.RaeriharUniversity
{
    public struct ArVector3
    {
        double[] _data = new double[3];

        public static ArVector3 Zero { get => new ArVector3(); }
        public static ArVector3 One { get => new ArVector3(1, 1, 1); }
        public ArVector3()
        { }

        public ArVector3(double x, double y, double z)
        {
            _data[0] = x;
            _data[1] = y;
            _data[2] = z;
        }

        public double this[int index]
        {
            get => _data[index];
            set => _data[index] = value;
        }
        public double X { get => _data[0]; set => _data[0] = value; }
        public double Y { get => _data[1]; set => _data[1] = value; }
        public double Z { get => _data[2]; set => _data[2] = value; }
        public static ArVector3 operator +(ArVector3 a, ArVector3 b) 
            => new ArVector3(a._data[0] + b._data[0], a._data[1] + b._data[1], a._data[2] + b._data[2]);
        public static ArVector3 operator -(ArVector3 a, ArVector3 b)
            => new ArVector3(a._data[0] - b._data[0], a._data[1] - b._data[1], a._data[2] - b._data[2]);
        public static ArVector3 operator *(ArVector3 a, double b)
            => new ArVector3(a._data[0] * b, a._data[1] * b, a._data[2] * b);
        public static ArVector3 operator /(ArVector3 a, double b)
            => new ArVector3(a._data[0] / b, a._data[1] / b, a._data[2] / b);
        public static bool operator ==(ArVector3 a, ArVector3 b)
            => a._data[0] == b._data[0] && a._data[1] == b._data[1] && a._data[2] == b._data[2];
        public static bool operator !=(ArVector3 a, ArVector3 b)
            => !(a == b);
    }
}
