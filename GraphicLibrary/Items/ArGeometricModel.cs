using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicLibrary.Items
{
    public class ArGeometricModel
    {
        ArFloatVector3[] vertices;
        int[] indices;

        public ArFloatVector3[] Vertices { get => vertices; set => vertices = value; }
        public int[] Indices { get => indices; set => indices = value; }

    }
}
