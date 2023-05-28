﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace Aritiafel.IlodarAcademy.SharpDX
{
    public struct Vertex
    {
        public Vector3 Position;
        public Vector4 Color;
    };

    public static class Extension
    {
        public static Vector4 ToSharpDXVector4(this System.Drawing.Color a)
            => new Vector4(a.R / 255f, a.G / 255f, a.B / 255f, a.A / 255f);
        public static Color4 ToSharpDXColor4(this System.Drawing.Color a)
            => new Color4(a.R / 255f, a.G / 255f, a.B / 255f, a.A / 255f);
        public static Vertex ToSharpDXVertex(this ArVertex a)
            => new Vertex { Position = new Vector3(a.Position.X, a.Position.Y, a.Position.Z), Color = a.Color.ToSharpDXVector4() };
        public static Vertex[] ToSharpDXModel(this Ar3DModel a)
        {
            Vertex[] result = new Vertex[a.Vertices.LongLength];
            for (long i = 0; i < a.Vertices.LongLength; i++)
                result[i] = a.Vertices[i].ToSharpDXVertex();
            return result;
        }
    }
}