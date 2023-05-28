using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Aritiafel.IlodarAcademy
{
    public struct ArVertex
    {
        public Vector3 Position { get; set; }
        public Vector4 Color { get; set; }
        public static ArVertex Empty => new ArVertex(0, 0, 0, 0, 0, 0, 0);
        public ArVertex(Vector3 position, Color color)
            : this(position.X, position.Y, position.Z, color.A, color.R, color.G, color.B)
        { }
        public ArVertex(Vector3 position, Vector4 color)
            : this(position.X, position.Y, position.Z, color.X, color.Y, color.Z, color.W)
        { }

        public ArVertex(float x, float y, float z, Vector4 color)
            : this(x, y, z, color.X, color.Y, color.Z, color.W)
        { }

        public ArVertex(float x, float y, float z, Color color)
            : this(x, y, z, color.A, color.R, color.G, color.B)
        { }

        public ArVertex(float x, float y, float z, float alpha, float red, float green, float blue)
        {
            Position = new Vector3(x, y, z);
            Color = new Vector4(alpha, red, green, blue);
        }
    }
}
