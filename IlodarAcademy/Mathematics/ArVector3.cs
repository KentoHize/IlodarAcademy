using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Aritiafel.Organizations.RaeriharUniversity;

namespace Aritiafel.Organizations.RaeriharUniversity
{
    public struct ArVector3 : IEquatable<ArVector3>, IFormattable
    {
        double[] _data = new double[3];

        public static readonly ArVector3 Zero = new ArVector3();
        public static readonly ArVector3 One = new ArVector3(1, 1, 1);
        public static readonly ArVector3 UnitX = new ArVector3(1, 0, 0);
        public static readonly ArVector3 UnitY = new ArVector3(0, 1, 0);
        public static readonly ArVector3 UnitZ = new ArVector3(0, 0, 1);
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

        public ArVector3 CrossProduct(ArVector3 a)
            => new ArVector3(_data[1] * a._data[2] - _data[2] * a._data[1],
                _data[2] * a._data[0] - _data[0] * a._data[2],
                _data[0] * a._data[1] - _data[1] * a._data[0]);
        public double DotProduct(ArVector3 a)
            => _data[0] * a._data[0] + _data[1] * a._data[1] + _data[2] * a._data[2];
        public double GetLength()
            => Math.Sqrt(_data[0] * _data[0] + _data[1] * _data[1] + _data[2] * _data[2]);
        public override string ToString()
            => $"({_data[0]}, {_data[1]}, {_data[2]})";
        public bool Equals(ArVector3 other)
            => _data[0] == other._data[0] && _data[1] == other._data[1] && _data[2] == other._data[2];
        public string ToString(string? format, IFormatProvider? formatProvider)        
            => string.Format(formatProvider, "({0}, {1}, {2})", _data[0], _data[1], _data[2]);
    }
}
