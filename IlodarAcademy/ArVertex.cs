using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Aritiafel.Organizations.RaeriharUniversity;

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

        public ArVertex(double x, double y, double z, int alpha, int red, int green, int blue)
            : this(x, y, z, Color.FromArgb(alpha, red, green, blue))
        { }
        public ArVertex(double x, double y, double z, Color color)
        {
            Position = new ArVector3(x, y, z);
            Color = color;
        }        
    }
}
