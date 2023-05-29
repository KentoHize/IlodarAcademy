﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Aritiafel.Organizations.RaeriharUniversity;
using System.Diagnostics;

namespace Aritiafel.IlodarAcademy
{
    public static class Ar3DMahine
    {
        public static long StaticScaleFactor = 1000;
        //public static ArVertex TraslateTransform(ArVertex av, Vector3 vector)
        //{
        //    av.Position = new Vector3(av.Position.X + vector.X, av.Position.Y + vector.Y, av.Position.Z + vector.Z);
        //    return av;
        //}

        //public static ArVertex RotateTransform(ArVertex av, Vector3 vector)
        //{   
        //    return av;
        //}

        //public static ArVertex AmplificationTransform(ArVertex av, int factor)
        //{
        //    av.Position = new Vector3(av.Position.X * factor, av.Position.Y * factor, av.Position.Z * factor);
        //    return av;
        //}

        //public Point TraslateTransformInverse(Point p, int transformX, int transformY)
        //{
        //    return new Point(p.X - transformX, p.Y - transformY);
        //}

        //public Point RotateTransform(Point p, double multipierY, double multipierZ1, double multipierZ2)
        //{
        //    return new Point((int)(p.X * multipierZ1 - p.Y * multipierZ2), (int)((p.Y * multipierZ1 + p.X * multipierZ2) * multipierY));
        //}

        //public Point RotateTransformInverse(Point p, double multipierY, double multipierZ1, double multipierZ2)
        //{
        //    Point r = new Point(p.X, (int)(p.Y / multipierY));
        //    return new Point((int)((r.X * multipierZ1 + r.Y * multipierZ2) / (multipierZ1 * multipierZ1 + multipierZ2 * multipierZ2)),
        //        (int)((r.Y * multipierZ1 - r.X * multipierZ2) / (multipierZ1 * multipierZ1 + multipierZ2 * multipierZ2)));
        //}

        //public Point AmplificationTransform(Point p, int amplificationFactor)
        //{
        //    return new Point(p.X * amplificationFactor, p.Y * amplificationFactor);
        //}

        //public Point AmplificationTransformInverse(Point p, int amplificationFactor)
        //{
        //    return new Point(p.X / amplificationFactor, p.Y / amplificationFactor);
        //}

        public static ArVector3 MultiplyTransformMatrix(ArVector3 position, ArMatrix44 transformMatrix)
        {
            ArVector4 v4 = transformMatrix * new ArVector4(position[0], position[1], position[2], 1);
            return new ArVector3(v4[0], v4[1], v4[2]);
        }

        public static ArMatrix44 ProduceTransformMatrix(ArVector3 translateVector, ArVector3 rotateVector, ArVector3 scaleVector)
        {
            ArMatrix44 result = ArMatrix44.One;
            //Standard Scale
            result[0, 0] = result[0, 0] / StaticScaleFactor;
            result[1, 1] = result[1, 1] / StaticScaleFactor;
            result[2, 2] = result[2, 2] / StaticScaleFactor;
            //Scale
            result[0, 0] = result[0, 0] * scaleVector[0];
            result[1, 1] = result[1, 1] * scaleVector[1];
            result[2, 2] = result[2, 2] * scaleVector[2];
            //Rotate            
            double cos = Math.Cos(rotateVector[0]);
            double sin = Math.Sin(rotateVector[0]);
            result = result * new ArMatrix44(
                cos, sin * -1, 0, 0,
                sin, cos, 0, 0,
                0, 0, 1, 0,
                0 ,0 ,0, 1);
            cos = Math.Cos(rotateVector[1]);
            sin = Math.Sin(rotateVector[1]);
            result = result * new ArMatrix44(
                cos, 0, sin * -1, 0,
                0, 1, 0, 0,
                sin, 0, cos, 0,
                0, 0, 0, 1);
            cos = Math.Cos(rotateVector[2]);
            sin = Math.Sin(rotateVector[2]);
            result = result * new ArMatrix44(
                1, 0, 0, 0,
                0, cos, sin * -1, 0,
                0, sin, cos, 0,
                0, 0, 0, 1);
            //Translate
            result[0, 3] = result[0, 3] + translateVector[0];
            result[1, 3] = result[1, 3] + translateVector[1];
            result[2, 3] = result[2, 3] + translateVector[2];
            return result;
        }

        public static long PlaneCount(Ar3DArea area)
        {
            if (area.Models == null)
                throw new NullReferenceException(nameof(area.Models));
            long result = 0;
            for (long i = 0; i < area.Models.LongLength; i++)
                result += area.Models[i].Planes.Length;
            return result;
        }

        public static ArVertex[][] ProduceDrawingVertices(Ar3DArea area)
        {
            if (area.Models == null)
                throw new NullReferenceException(nameof(area.Models));
            ArVertex[][] result = new ArVertex[PlaneCount(area)][];
            ArMatrix44 transformMatrix = ProduceTransformMatrix(area.TranslateTransform, area.RotateTransform, area.ScaleTransform);
            
            long index = 0;
            for(long i = 0; i < area.Models.Length; i++)
            {
                for(int j = 0; j < area.Models[i].Planes.Length; j++)
                {
                    List<ArVertex> vertices = new List<ArVertex>();
                    for (int k = 0; k < area.Models[i].Planes[j].Vertices.Length; k++)
                    {
                        vertices.Add(new ArVertex(MultiplyTransformMatrix(area.Models[i].Planes[j].Vertices[k].Position, transformMatrix),
                            area.Models[i].Planes[j].Vertices[k].Color));
                    }
                    result[index++] = vertices.ToArray();                    
                }
            }
            return result;
        }
    }
}
