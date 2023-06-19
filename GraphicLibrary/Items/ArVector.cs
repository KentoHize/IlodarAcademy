using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraphicLibrary.Items
{
    //All Vector
    public abstract class ArVector : IDisposable, ICloneable, IEqualityOperators<ArVector, ArVector, bool>
    {
        public abstract object Clone();
        public void Dispose()
            => Dispose();
        public static bool operator ==(ArVector? left, ArVector? right)
            => left.Equals(right);
        public static bool operator !=(ArVector? left, ArVector? right)
            => !left.Equals(right);
        public abstract override bool Equals(object? obj);
        public abstract override int GetHashCode();
        public abstract override string ToString();

    }
}
