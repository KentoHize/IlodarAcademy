using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace Aritiafel.IlodarAcademy.SharpDX
{
    public struct Vertex
    {
        public Vector3 Position;
        public Vector4 Color;
    };

    public static class Extension
    {
        public static Vertex ToSharpDXVertex(this ArVertex a)
            => new Vertex { Position = new Vector3(a.Position.X, a.Position.Y, a.Position.Z), Color = new Vector4(a.Color.Y / 255f, a.Color.Z / 255f, a.Color.W / 255f, a.Color.X / 255f) };
        public static Vertex[] ToSharpDXModel(this Ar3DModel a)
        {
            Vertex[] result = new Vertex[a.Vertices.LongLength];
            for (long i = 0; i < a.Vertices.LongLength; i++)
                result[i] = a.Vertices[i].ToSharpDXVertex();
            return result;
        }

        //public static Vertex[][] ToSharpDXModels
    }
}