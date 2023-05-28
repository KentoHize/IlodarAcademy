using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritiafel.IlodarAcademy
{
    public class Ar3DArea
    {
        public Ar3DModel[] Models { get; set; }
        public Color BackgroudColor { get; set; } = Color.Black;
        public ArViewport Viewport { get; set; }
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
