using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicLibrary.Items
{
    public class ArGeometricModel
    {
        ArIntVector3[] vertices;
        int[] indices;
        public ArIntVector3[] Vertices { get => vertices; set => vertices = value; }
        public int[] Indices { get => indices; set => indices = value; }

        public ArGeometricModel()
        {
            vertices = new ArIntVector3[0];
            indices = new int[0];
        }

        public void AddPlane(ArGeometricPlane plane)
        {
            Array.Resize(vertices.Length + plane.Vertices.Length)
        }

    }
}
