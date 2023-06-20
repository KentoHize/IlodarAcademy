using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicLibrary.Items
{
    public class ArGeometricPlane
    {
        public ArIntVector3[] Vertices { get; set; }
        public int Length => Vertices.Length;
        public ArGeometricPlane(int verticesCount = 3)
        {
            Vertices = new ArIntVector3[verticesCount];
        }

        public ArGeometricPlane(ArIntVector3[] vertices)
        {
            if (vertices.Length < 3)
                throw new ArgumentException("A plane need at least 3 vertices.");
            if (vertices.Length > int.MaxValue)
                throw new IndexOutOfRangeException("Too many vertices");
            Vertices = new ArIntVector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
                Vertices[i] = vertices[i];
        }

        public ArGeometricPlane(ArIntVector3 v1, ArIntVector3 v2, params ArIntVector3[] otherVertices)
        {
            if (otherVertices.Length > int.MaxValue - 2)
                throw new IndexOutOfRangeException("Too many vertices");
            Vertices = new ArIntVector3[otherVertices.Length + 2];
            Vertices[0] = v1;
            Vertices[1] = v2;
            for (int i = 0; i < otherVertices.Length; i++)
                Vertices[i + 2] = otherVertices[i];
        }
    }
}
