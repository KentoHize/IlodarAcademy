using Aritiafel.Organizations.RaeriharUniversity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritiafel.Organizations.RaeriharUniversity
{
    public struct ArMatrix44
    {
        double[,] _data;

        public static ArMatrix44 Zero = new ArMatrix44();
        public static ArMatrix44 One = new ArMatrix44(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

        public ArMatrix44()
        {
            _data = new double[4, 4];
        }

        public ArMatrix44(double n11, double n12, double n13, double n14, double n21, double n22, double n23, double n24, double n31, double n32, double n33, double n34, double n41, double n42, double n43, double n44)
        {
            _data = new double[4, 4];
            _data[0, 0] = n11;
            _data[0, 1] = n12;
            _data[0, 2] = n13;
            _data[0, 3] = n14;
            _data[1, 0] = n21;
            _data[1, 1] = n22;
            _data[1, 2] = n23;
            _data[1, 3] = n24;
            _data[2, 0] = n31;
            _data[2, 1] = n32;
            _data[2, 2] = n33;
            _data[2, 3] = n34;
            _data[3, 0] = n41;
            _data[3, 1] = n42;
            _data[3, 2] = n43;
            _data[3, 3] = n44;
        }

        public ArMatrix44(double[,] matrix)
        {
            if (matrix.GetLength(0) != 4 || matrix.GetLength(1) != 4)
                throw new IndexOutOfRangeException(nameof(matrix));
            _data = matrix;
        }

        public double this[int x, int y]
        {
            get => _data[x, y];
            set => _data[x, y] = value;
        }

        public static ArVector4 operator *(ArVector4 a, ArMatrix44 b)
        {
            return new ArVector4(
                a[0] * b[0, 0] + a[0] * b[0, 1] + a[0] * b[0, 2] + a[0] * b[0, 3],
                a[1] * b[1, 0] + a[1] * b[1, 1] + a[1] * b[1, 2] + a[1] * b[1, 3],
                a[2] * b[2, 0] + a[2] * b[2, 1] + a[2] * b[2, 2] + a[2] * b[2, 3],
                a[3] * b[3, 0] + a[3] * b[3, 1] + a[3] * b[3, 2] + a[3] * b[3, 3]);
        }

        public static ArMatrix44 operator *(ArMatrix44 a, ArMatrix44 b)
        {
            ArMatrix44 result = new ArMatrix44();
            //result[0, 0] = a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0] + a[0, 2] * b[2, 0] + a[0, 3] * b[3, 0];
            //result[0, 1] = a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1] + a[0, 2] * b[2, 1] + a[0, 3] * b[3, 1];
            //result[0, 2] = a[0, 0] * b[0, 2] + a[0, 1] * b[1, 2] + a[0, 2] * b[2, 2] + a[0, 3] * b[3, 2];
            //result[0, 3] = a[0, 0] * b[0, 3] + a[0, 1] * b[1, 3] + a[0, 2] * b[2, 3] + a[0, 3] * b[3, 3];
            //result[1, 0] = a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0] + a[1, 2] * b[2, 0] + a[1, 3] * b[3, 0];
            //result[1, 1] = a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1] + a[1, 2] * b[2, 1] + a[1, 3] * b[3, 1];
            for (int i = 0; i < 4; i++)
                for(int j = 0; j < 4; j++)
                    result[i, j] = a[i, 0] * b[0, j] + a[i, 1] * b[1, j] + a[i, 2] * b[2, j] + a[i, 3] * b[3, j];
            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{{0} {1} {2} {3}}\n", _data[0, 0], _data[0, 1], _data[0, 2], _data[0, 3]);
            sb.AppendFormat("{{0} {1} {2} {3}}\n", _data[1, 0], _data[1, 1], _data[1, 2], _data[1, 3]);
            sb.AppendFormat("{{0} {1} {2} {3}}\n", _data[2, 0], _data[2, 1], _data[2, 2], _data[2, 3]);
            sb.AppendFormat("{{0} {1} {2} {3}}", _data[3, 0], _data[3, 1], _data[3, 2], _data[3, 3]);
            return sb.ToString();
        }
    }
}
