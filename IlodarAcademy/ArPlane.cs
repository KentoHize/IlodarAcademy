using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritiafel.IlodarAcademy
{
    public class ArPlane
    {
        ArVertex[] m_vertices;
        public ArVertex[] Vertices { get => m_vertices; set { if (value.Length > int.MaxValue) throw new IndexOutOfRangeException(); m_vertices = value; } }
        public ArPlane(List<ArVertex> vertices)
            : this(vertices.ToArray())
        { }
        public ArPlane(ArVertex[] vertices)
        {
            Vertices = vertices;
        }
        public ArPlane() { }
    }
}
