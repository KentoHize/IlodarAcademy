﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicLibrary.Items
{
    //DirectX Compatible FloatMatrix44
    public class ArFloatMatrix44 : ArMatrix
    {
        float _11, _12, _13, _14, _21, _22, _23, _24, _31, _32, _33, _34, _41, _42, _43, _44;
        public static ArFloatMatrix44 Zero { get => new ArFloatMatrix44(); }
        public static ArFloatMatrix44 One { get => new ArFloatMatrix44(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1); }

        public float this[int x, int y]
        {
            get
            {
                return x switch
                {
                    0 => y switch
                    {
                        0 => _11,
                        1 => _12,
                        2 => _13,
                        3 => _14,
                        _ => throw new IndexOutOfRangeException(nameof(y))
                    },
                    1 => y switch
                    {
                        0 => _21,
                        1 => _22,
                        2 => _23,
                        3 => _24,
                        _ => throw new IndexOutOfRangeException(nameof(y))
                    },
                    2 => y switch
                    {
                        0 => _31,
                        1 => _32,
                        2 => _33,
                        3 => _34,
                        _ => throw new IndexOutOfRangeException(nameof(y))
                    },
                    3 => y switch
                    {
                        0 => _41,
                        1 => _42,
                        2 => _43,
                        3 => _44,
                        _ => throw new IndexOutOfRangeException(nameof(y))
                    },
                    _ => throw new IndexOutOfRangeException(nameof(x))
                };
            }
            set
            {
                switch (x)
                {
                    case 0:
                        switch (y)
                        {
                            case 0:
                                _11 = value;
                                break;
                            case 1:
                                _12 = value;
                                break;
                            case 2:
                                _13 = value;
                                break;
                            case 3:
                                _14 = value;
                                break;
                            default:
                                throw new IndexOutOfRangeException(nameof(y));
                        }
                        break;
                    case 1:
                        switch (y)
                        {
                            case 0:
                                _21 = value;
                                break;
                            case 1:
                                _22 = value;
                                break;
                            case 2:
                                _23 = value;
                                break;
                            case 3:
                                _24 = value;
                                break;
                            default:
                                throw new IndexOutOfRangeException(nameof(y));
                        }
                        break;
                    case 2:
                        switch (y)
                        {
                            case 0:
                                _31 = value;
                                break;
                            case 1:
                                _32 = value;
                                break;
                            case 2:
                                _33 = value;
                                break;
                            case 3:
                                _34 = value;
                                break;
                            default:
                                throw new IndexOutOfRangeException(nameof(y));
                        }
                        break;
                    case 3:
                        switch (y)
                        {
                            case 0:
                                _41 = value;
                                break;
                            case 1:
                                _42 = value;
                                break;
                            case 2:
                                _43 = value;
                                break;
                            case 3:
                                _44 = value;
                                break;
                            default:
                                throw new IndexOutOfRangeException(nameof(y));
                        }
                        break;
                    default:
                        throw new IndexOutOfRangeException(nameof(x));
                }
            }
        }

        public ArFloatMatrix44()
            : this(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
        { }

        public ArFloatMatrix44(float _11, float _12, float _13, float _14, float _21, float _22, float _23, float _24, float _31, float _32, float _33, float _34, float _41, float _42, float _43, float _44)
        {
            this._11 = _11;
            this._12 = _12;
            this._13 = _13;
            this._14 = _14;
            this._21 = _21;
            this._22 = _22;
            this._23 = _23;
            this._24 = _24;
            this._31 = _31;
            this._32 = _32;
            this._33 = _33;
            this._34 = _34;
            this._41 = _41;
            this._42 = _42;
            this._43 = _43;
            this._44 = _44;
        }

        public ArFloatMatrix44(float[,] matrix)
        {
            if (matrix.GetLength(0) != 4 || matrix.GetLength(1) != 4)
                throw new IndexOutOfRangeException(nameof(matrix));
            _11 = matrix[0, 0];
            _12 = matrix[0, 1];
            _13 = matrix[0, 2];
            _14 = matrix[0, 3];
            _21 = matrix[1, 0];
            _22 = matrix[1, 1];
            _23 = matrix[1, 2];
            _24 = matrix[1, 3];
            _31 = matrix[2, 0];
            _32 = matrix[2, 1];
            _33 = matrix[2, 2];
            _34 = matrix[2, 3];
            _41 = matrix[3, 0];
            _42 = matrix[3, 1];
            _43 = matrix[3, 2];
            _44 = matrix[3, 3];
        }

        public static ArFloatVector4 operator *(ArFloatMatrix44 a, ArFloatVector4 b)
        {
            return new ArFloatVector4(
                a[0, 0] * b[0] + a[0, 1] * b[1] + a[0, 2] * b[2] + a[0, 3] * b[3],
                a[1, 0] * b[0] + a[1, 1] * b[1] + a[1, 2] * b[2] + a[1, 3] * b[3],
                a[2, 0] * b[0] + a[2, 1] * b[1] + a[2, 2] * b[2] + a[2, 3] * b[3],
                a[3, 0] * b[0] + a[3, 1] * b[1] + a[3, 2] * b[2] + a[3, 3] * b[3]);
        }

        public static ArFloatMatrix44 operator *(ArFloatMatrix44 a, ArFloatMatrix44 b)
        {
            ArFloatMatrix44 result = new ArFloatMatrix44();
            //result[0, 0] = a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0] + a[0, 2] * b[2, 0] + a[0, 3] * b[3, 0];
            //result[0, 1] = a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1] + a[0, 2] * b[2, 1] + a[0, 3] * b[3, 1];
            //result[0, 2] = a[0, 0] * b[0, 2] + a[0, 1] * b[1, 2] + a[0, 2] * b[2, 2] + a[0, 3] * b[3, 2];
            //result[0, 3] = a[0, 0] * b[0, 3] + a[0, 1] * b[1, 3] + a[0, 2] * b[2, 3] + a[0, 3] * b[3, 3];
            //result[1, 0] = a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0] + a[1, 2] * b[2, 0] + a[1, 3] * b[3, 0];
            //result[1, 1] = a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1] + a[1, 2] * b[2, 1] + a[1, 3] * b[3, 1];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    result[i, j] = a[i, 0] * b[0, j] + a[i, 1] * b[1, j] + a[i, 2] * b[2, j] + a[i, 3] * b[3, j];
            return result;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            else if (ReferenceEquals(obj, null) || !(obj is ArFloatMatrix44))
                return false;
            return Equals((ArFloatMatrix44)obj);
        }

        public bool Equals(ArFloatMatrix44 other)
            => _11 == other._11 && _12 == other._12 && _13 == other._13 && _14 == other._14 &&
            _21 == other._21 && _22 == other._22 && _23 == other._23 && _24 == other._24 &&
            _31 == other._31 && _32 == other._32 && _33 == other._33 && _34 == other._34 &&
            _41 == other._41 && _42 == other._42 && _43 == other._43 && _44 == other._44;

        public string ToString(string format)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{{{0} {1} {2} {3}}}\n", _11.ToString(format), _12.ToString(format), _13.ToString(format), _14.ToString(format));
            sb.AppendFormat("{{{0} {1} {2} {3}}}\n", _21.ToString(format), _22.ToString(format), _23.ToString(format), _24.ToString(format));
            sb.AppendFormat("{{{0} {1} {2} {3}}}\n", _31.ToString(format), _32.ToString(format), _33.ToString(format), _34.ToString(format));
            sb.AppendFormat("{{{0} {1} {2} {3}}}", _41.ToString(format), _42.ToString(format), _43.ToString(format), _44.ToString(format));
            return sb.ToString();
        }  

        public override string ToString()
            => ToString("G");

        public override object Clone()
            => new ArFloatMatrix44(_11, _12, _13, _14, _21, _22, _23, _24, _31, _32, _33, _34, _41, _42, _43, _44);
        public override int GetHashCode()
            => (_11, _12, _13, _14, _21, _22, _23, _24, _31, _32, _33, _34, _41, _42, _43, _44).GetHashCode();
    }
}
