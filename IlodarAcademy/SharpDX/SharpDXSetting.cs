using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritiafel.IlodarAcademy.SharpDX
{
    public class SharpDXSetting
    {
        public IntPtr Handle { get; set; }
        public ArSwapEffect SwapEffect { get; set; } = ArSwapEffect.FlipDiscard;
    }
}
