using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Aritiafel.IlodarAcademy.Mathematics;

namespace Aritiafel.IlodarAcademy
{
    public struct ArVertex
    {
        public ArVector3 Position { get; set; }
        public Color Color { get; set; }
        public static ArVertex Empty => new ArVertex(0, 0, 0, Color.Empty);
        public ArVertex(ArVector3 position, Color color)
            : this(position.X, position.Y, position.Z, color)
        { }
        //public ArVertex(Vector3 position, Vector4 color)
        //    : this(position.X, position.Y, position.Z, color.X, color.Y, color.Z, color.W)
        //{ }

        //public ArVertex(float x, float y, float z, Vector4 color)
        //    : this(x, y, z, color.X, color.Y, color.Z, color.W)
        //{ }

        //public ArVertex(float x, float y, float z, Color color)
        //    : this(x, y, z, color.A, color.R, color.G, color.B)
        //{ }

        public ArVertex(long x, long y, long z, int alpha, int red, int green, int blue)
            : this(x, y, z, Color.FromArgb(alpha, red, green, blue))
        { }
        public ArVertex(long x, long y, long z, Color color)
        {
            Position = new ArVector3(x, y, z);
            Color = color;
        }        
    }
}
