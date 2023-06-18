using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraphicLibrary.Items
{
    //DirectX Compatible Float Vector2
    public class ArFloatVector2 : ArVector, IComparable<ArFloatVector2>, IEquatable<ArFloatVector2>, IAdditionOperators<ArFloatVector2, ArFloatVector2, ArFloatVector2>,
        ISubtractionOperators<ArFloatVector2, ArFloatVector2, ArFloatVector2>, IMultiplyOperators<ArFloatVector2, ArFloatVector2, float>
    {
        float _x, _y;
        public static ArFloatVector2 Zero => new ArFloatVector2();
        public static ArFloatVector2 One => new ArFloatVector2(1, 1);
        public static ArFloatVector2 UnitX { get => new ArFloatVector2(1, 0); }
        public static ArFloatVector2 UnitY { get => new ArFloatVector2(0, 1); }

        public ArFloatVector2()
        { }

        public ArFloatVector2(float x, float y)
        {
            _x = x;
            _y = y;
        }

        public float this[int index]
        {
            get => index switch { 0 => _x, 1 => _y, _ => throw new IndexOutOfRangeException(nameof(index)) };
            set
            {
                switch (index)
                {
                    case 0:
                        _x = value;
                        break;
                    case 1:
                        _y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException(nameof(index));
                }
            }
        }

        public override object Clone()
            => new ArFloatVector2(_x, _y);

        public override string ToString()
            => $"({_x}, {_y})";

        public bool Equals(ArFloatVector2? other)
            => _x == other._x && _y == other._y;
        public int CompareTo(ArFloatVector2? other)
            => _x > other._x ? 1 : _x < other._x ? -1 : _y > other._y ? 1 : _y < other._y ? -1 : 0;
        public static ArFloatVector2 operator +(ArFloatVector2 left, ArFloatVector2 right)
            => new ArFloatVector2(left._x + right._x, left._y + right._y);
        public static ArFloatVector2 operator -(ArFloatVector2 left, ArFloatVector2 right)
            => new ArFloatVector2(left._x - right._x, left._y - right._y);
        public static ArFloatVector2 operator *(ArFloatVector2 a, int b)
            => new ArFloatVector2(a._x * b, a._y * b);        
        public static ArFloatVector2 operator /(ArFloatVector2 a, double b)
            => new ArFloatVector2((float)(a._x / b), (float)(a._y / b));
        public double GetLength() => Math.Sqrt(_x * _x + _y * _y);
        public double AngleBetween(ArFloatVector2 a)
            => Math.Acos(DotProduct(a) / (GetLength() * a.GetLength()));
        public ArFloatVector2 Normalize()
        {
            double l = GetLength();
            return new ArFloatVector2((float)(_x / l),(float)(_y / l));
        }
        
        public static float operator *(ArFloatVector2 left, ArFloatVector2 right)
            => left.DotProduct(right);
        public float CrossProduct(ArFloatVector2 a)
            => _x * a._y - _y * a._x;
        public float DotProduct(ArFloatVector2 a)
            => _x * a._x + _y * a._y;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            else if (ReferenceEquals(obj, null) || !(obj is ArFloatVector2))
                return false;
            return Equals((ArFloatVector2)obj);
        }

        public override int GetHashCode()
            => (_x, _y).GetHashCode();
        public static bool operator <(ArFloatVector2 left, ArFloatVector2 right)
            => ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
        public static bool operator <=(ArFloatVector2 left, ArFloatVector2 right)
            => ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
        public static bool operator >(ArFloatVector2 left, ArFloatVector2 right)
            => !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
        public static bool operator >=(ArFloatVector2 left, ArFloatVector2 right)
            => ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
    }
}
