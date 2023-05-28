using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritiafel.IlodarAcademy
{
    public struct ArViewport
    {
        public float X { get; set; }

        public float Y { get; set; } 

        public float Width { get; set; }

        public float Height { get; set; }

        public float MinDepth { get; set; }

        public float MaxDepth { get; set; }

        //public ArViewport(int x, int y, int width, int height, int minDepth, int maxDepth)
        //{
        //    X = x;
        //    Y = y;
        //    Width = width;
        //    Height = height;
        //    MinDepth = minDepth;
        //    MaxDepth = maxDepth;
        //}

        public ArViewport(float x, float y, float width, float height, float minDepth = 0f, float maxDepth = 1.0f)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            MinDepth = minDepth;
            MaxDepth = maxDepth;
        }
    }
}
