using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Aritiafel.IlodarAcademy
{
    public class Ar3DModel
    {
        public ArVertex[] Vertices { get; set; }
        public Ar3DModel(List<ArVertex> vertices)
            : this(vertices.ToArray())
        { }

        public Ar3DModel(ArVertex[] vertices)
        {
            Vertices = vertices;
        }

        public Ar3DModel()
        {           
            
        }

        
    }
}
