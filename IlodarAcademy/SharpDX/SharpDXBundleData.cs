using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D;

namespace Aritiafel.IlodarAcademy.SharpDX
{
    public class SharpDXBundleData
    {
        public ArVertex[] Data { get; set; }
        public ArDrawingMethod PrimitiveTopology { get; set; }
    }
}
