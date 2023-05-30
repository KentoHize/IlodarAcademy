using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritiafel.Organizations.RaeriharUniversity
{
    public struct ArVector4
    {
        double[] _data = new double[4];
        public static readonly ArVector4 Zero = new ArVector4();
        public static readonly ArVector4 One = new ArVector4(1, 1, 1, 1);
        public ArVector4()
        { }

        public ArVector4(double w, double x, double y, double z)
        {
            _data[0] = w;
            _data[1] = x;
            _data[2] = y;
            _data[3] = z;
        }

        public double this[int index]
        {
            get => _data[index];
            set => _data[index] = value;
        }
        public double W { get => _data[0]; set => _data[0] = value; }
        public double X { get => _data[1]; set => _data[1] = value; }
        public double Y { get => _data[2]; set => _data[2] = value; }
        public double Z { get => _data[3]; set => _data[3] = value; }
        public static ArVector4 operator +(ArVector4 a, ArVector4 b)
            => new ArVector4(a._data[0] + b._data[0], a._data[1] + b._data[1], a._data[2] + b._data[2], a._data[3] + b._data[3]);
        public static ArVector4 operator -(ArVector4 a, ArVector4 b)
            => new ArVector4(a._data[0] - b._data[0], a._data[1] - b._data[1], a._data[2] - b._data[2], a._data[3] - b._data[3]);
        public static ArVector4 operator *(ArVector4 a, double b)
            => new ArVector4(a._data[0] * b, a._data[1] * b, a._data[2] * b, a._data[3] * b);
        public static ArVector4 operator /(ArVector4 a, double b)
            => new ArVector4(a._data[0] / b, a._data[1] / b, a._data[2] / b, a._data[3] / b);

        public static bool operator ==(ArVector4 a, ArVector4 b)
           => a._data[0] == b._data[0] && a._data[1] == b._data[1] && a._data[2] == b._data[2] && a._data[3] == b._data[3];
        public static bool operator !=(ArVector4 a, ArVector4 b)
            => !(a == b);
    }
}
