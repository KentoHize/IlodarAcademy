using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritiafel.Organizations.RaeriharUniversity
{
    public struct ArMatrix33
    {
        double[,] _data;

        public static ArMatrix33 Zero = new ArMatrix33();
        public static ArMatrix33 One = new ArMatrix33(1, 0, 0, 0, 1, 0, 0, 0, 1);

        public ArMatrix33()
        { 
            _data = new double[3, 3];
        }

        public ArMatrix33(double n11, double n12, double n13, double n21, double n22, double n23, double n31, double n32, double n33)
        {
            _data = new double[3, 3];
            _data[0, 0] = n11;
            _data[0, 1] = n12;
            _data[0, 2] = n13;
            _data[1, 0] = n21;
            _data[1, 1] = n22;
            _data[1, 2] = n23;
            _data[2, 0] = n31;
            _data[2, 1] = n32;
            _data[2, 2] = n33;
        }

        public ArMatrix33(double[,] matrix)
        {
            if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
                throw new IndexOutOfRangeException(nameof(matrix));
            _data = matrix;
        }

        public double this[int x, int y]
        {
            get => _data[x, y];
            set => _data[x, y] = value;
        }

        public static ArVector3 operator *(ArVector3 a, ArMatrix33 b)
        {
            return new ArVector3(a[0] * b[0, 0] + a[0] * b[0, 1] + a[0] * b[0, 2],
                a[1] * b[1, 0] + a[1] * b[1, 1] + a[1] * b[1, 2],
                a[2] * b[2, 0] + a[2] * b[2, 1] + a[2] * b[2, 2]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();            
            sb.AppendFormat("{{0} {1} {2}}\n", _data[0, 0], _data[0, 1], _data[0, 2]);
            sb.AppendFormat("{{0} {1} {2}}\n", _data[1, 0], _data[1, 1], _data[1, 2]);
            sb.AppendFormat("{{0} {1} {2}}", _data[2, 0], _data[2, 1], _data[2, 2]);
            return sb.ToString();
        }
    }
}
