using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraphicLibrary.Items
{
    //All Matrix
    public abstract class ArMatrix : IDisposable, ICloneable, IEqualityOperators<ArMatrix, ArMatrix, bool>
    {
        public abstract object Clone();
        public void Dispose()
            => Dispose();
        public static bool operator ==(ArMatrix? left, ArMatrix? right)
            => left.Equals(right);

        public static bool operator !=(ArMatrix? left, ArMatrix? right)
            => !left.Equals(right);

        public abstract override bool Equals(object? obj);
        public abstract override int GetHashCode();
        public abstract override string ToString();
    }
}
