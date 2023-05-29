using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Aritiafel.IlodarAcademy
{
    public static class Ar3DMahine
    {
        public static ArVertex TraslateTransform(ArVertex av, Vector3 vector)
        {
            av.Position = new Vector3(av.Position.X + vector.X, av.Position.Y + vector.Y, av.Position.Z + vector.Z);
            return av;            
        }

        public static ArVertex RotateTransform(ArVertex av, Vector3 vector)
        {   
            return av;
        }

        public static ArVertex AmplificationTransform(ArVertex av, int factor)
        {
            av.Position = new Vector3(av.Position.X * factor, av.Position.Y * factor, av.Position.Z * factor);
            return av;
        }

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
            long index = 0;
            for(long i = 0; i < area.Models.Length; i++)
            {
                for(int j = 0; j < area.Models[i].Planes.Length; j++)
                {
                    List<ArVertex> vertices = new List<ArVertex>();
                    for (int k = 0; k < area.Models[i].Planes[j].Vertices.Length; k++)
                    {
                        vertices.Add(area.Models[i].Planes[j].Vertices[k]);
                        AmplificationTransform(vertices[vertices.Count - 1], area.AmplificationFactor);
                        RotateTransform(vertices[vertices.Count - 1], area.RotationTransform);
                        TraslateTransform(vertices[vertices.Count - 1], area.TranslateTransform);
                    }
                    result[index++] = vertices.ToArray();                    
                }
            }
            return result;
        }
    }
}
