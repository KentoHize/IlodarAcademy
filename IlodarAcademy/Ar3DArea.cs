using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Aritiafel.IlodarAcademy
{
    public class Ar3DArea
    {
        public Ar3DModel[]? Models { get; set; }
        public Color BackgroudColor { get; set; } = Color.Black;        
        public Vector3 TranslateTransform { get; set; } = Vector3.Zero;
        public Vector3 RotationTransform { get; set; } = Vector3.Zero;
        public int AmplificationFactor { get; set; } = 1;

        public Ar3DArea(List<Ar3DModel> models)
            : this(models.ToArray())
        { }

        public Ar3DArea(Ar3DModel[] models)
        {
            Models = models;
        }

        public Ar3DArea()
        {
            
        }
    }
}
