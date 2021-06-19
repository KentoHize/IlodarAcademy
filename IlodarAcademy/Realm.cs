using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Aritiafel.IlodarAcademy
{
    public enum CameraMode
    {
        None,
        Perspective,
        Orthographic,
        Projection
    }

    public enum LightMode
    {
        Ambient,
        Point,
        Directional,
        Spot
    }

    public class Realm
    {
        public Viewport3D Viewport
        {
            get => _V3D;
            set => _V3D = value;
        }

        public CameraMode CameraMode
        {
            get {
                if (_Camera is PerspectiveCamera)
                    return CameraMode.Perspective;
                else if (_Camera is OrthographicCamera)
                    return CameraMode.Orthographic;
                else if (_Camera is ProjectionCamera)
                    return CameraMode.Projection;
                else
                    return CameraMode.None;
            }
            set
                => SetCamera(value, CameraPosition, CameraLookDirection, CameraUpDirection, CameraFieldOfView);            
        }

        public Point3D CameraPosition 
        {
            get {
                switch(_Camera)
                {
                    case PerspectiveCamera pc:
                        return pc.Position;
                    case OrthographicCamera oc:
                        return oc.Position;
                    default:
                        return new Point3D();
                }
            }
            set
            {
                switch (_Camera)
                {
                    case PerspectiveCamera pc:
                        pc.Position = value;
                        break;
                    case OrthographicCamera oc:
                        oc.Position = value;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public Vector3D CameraLookDirection
        {
            get
            {
                switch (_Camera)
                {
                    case PerspectiveCamera pc:
                        return pc.LookDirection;
                    case OrthographicCamera oc:
                        return oc.LookDirection;
                    default:
                        return new Vector3D();
                }
            }
            set
            {
                switch (_Camera)
                {
                    case PerspectiveCamera pc:
                        pc.LookDirection = value;
                        break;
                    case OrthographicCamera oc:
                        oc.LookDirection = value;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public Vector3D CameraUpDirection 
        {
            get
            {
                switch (_Camera)
                {
                    case PerspectiveCamera pc:
                        return pc.UpDirection;
                    case OrthographicCamera oc:
                        return oc.UpDirection;
                    default:
                        return new Vector3D();
                }
            }
            set
            {
                switch (_Camera)
                {
                    case PerspectiveCamera pc:
                        pc.UpDirection = value;
                        break;
                    case OrthographicCamera oc:
                        oc.UpDirection = value;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public double CameraFieldOfView 
        {
            get
            {
                switch (_Camera)
                {
                    case PerspectiveCamera pc:
                        return pc.FieldOfView;
                    case OrthographicCamera oc:
                        return oc.Width;
                    default:
                        return 0;
                }
            }
            set
            {
                switch (_Camera)
                {
                    case PerspectiveCamera pc:
                        pc.FieldOfView = value;
                        break;
                    case OrthographicCamera oc:
                        oc.Width = value;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        //public Canvas Canvas { 
        //    get => _Canvas;
        //    set { 
        //        _Canvas = value;
        //        RefreshCanvas();
        //    }
        //}

        //LightMode研究
        //public LightMode LightMode
        //{
        //    get
        //    {
        //        if (_Camera is PerspectiveCamera)
        //            return CameraMode.Perspective;
        //        else if (_Camera is OrthographicCamera)
        //            return CameraMode.Orthographic;
        //        else
        //            return CameraMode.Projection;
        //    }
        //    set
        //        => SetCamera(value, CameraPosition, CameraLookDirection, CameraUpDirection, CameraFieldOfView);
        //}

        //public LightDirection 

        //public DirectionalLight DL
        //{

        //}

        private Viewport3D _V3D;
        private ModelVisual3D _MV3D;
        private Model3DGroup _M3G;
        //private Canvas _Canvas;
        private Camera _Camera;
        private List<Light> _LightList = new List<Light>();
        private List<GeometryModel3D> _GM3DList = new List<GeometryModel3D>();
        //private CameraMode _CameraMode;
        //private PerspectiveCamera _PCamera;
        //private OrthographicCamera _OCamera;

        protected void SetCamera(CameraMode mode, Point3D position, Vector3D lookDirection, Vector3D upDirection, double fieldOfView)
        {   
            if (mode == CameraMode.None)
            {
                _V3D.Camera = null;
                return;
            }   
            
            if (mode == CameraMode.Perspective)
                //_Camera = new PerspectiveCamera(position, lookDirection, upDirection, fieldOfView);
                _Camera = new PerspectiveCamera { Position = position, LookDirection = lookDirection };
            else if (mode == CameraMode.Orthographic)
                _Camera = new OrthographicCamera(position, lookDirection, upDirection, fieldOfView);
            else
                throw new NotImplementedException(); //_Camera = _Canvas.Pro new ProjectionCamera();
            _V3D.Camera = _Camera;
        }

        public Realm(Viewport3D viewport3D, CameraMode cameraMode = CameraMode.Perspective)
        {
            _V3D = viewport3D;
            //CameraMode = cameraMode;
            RefreshCanvas();
        }
        //public Realm(Canvas canvas = null, CameraMode cameraMode = CameraMode.Perspective)
        //{
        //    _Canvas = canvas;            
        //}

        protected void RefreshCanvas()
        {
            //if (_Canvas == null)
            //    return;

            if(_V3D == null)
            {
                _V3D = new Viewport3D();              
            }

            if(_MV3D == null)
            {
                _MV3D = new ModelVisual3D();
                _V3D.Children.Add(_MV3D);
            }

            if (CameraMode == CameraMode.None)
            {
                CameraMode = CameraMode.Perspective;
                CameraPosition = new Point3D(0, -5, 5);
                CameraLookDirection = new Vector3D(0, 1, -1);

                CameraPosition = new Point3D(0, 0, 2);
                CameraLookDirection = new Vector3D(0, 0, -1);

                CameraFieldOfView = 60;
            }
            SetCamera(CameraMode, CameraPosition, CameraLookDirection, CameraUpDirection, CameraFieldOfView);

            _M3G = new Model3DGroup();
               

            _M3G.Children.Clear();
            foreach (GeometryModel3D gm3d in _GM3DList)
                _M3G.Children.Add(gm3d);

            foreach (Light l in _LightList)
                _M3G.Children.Add(l);

            _MV3D.Content = _M3G;
        }


        public void CreateSample()
        {
            //AmbientLight light = new AmbientLight(Colors.White);
            //_LightList.Add(light);

            DirectionalLight light = new DirectionalLight();
            _LightList.Clear();
            light.Color = Colors.White;
            light.Direction = new Vector3D(-0.61, -0.5, -0.61);
            _LightList.Add(light);

            GeometryModel3D sample = new GeometryModel3D();
            MeshGeometry3D mg3d = new MeshGeometry3D();
            Vector3DCollection myNormalCollection = new Vector3DCollection();
            myNormalCollection.Add(new Vector3D(0, 0, 1));
            myNormalCollection.Add(new Vector3D(0, 0, 1));
            myNormalCollection.Add(new Vector3D(0, 0, 1));
            myNormalCollection.Add(new Vector3D(0, 0, 1));
            myNormalCollection.Add(new Vector3D(0, 0, 1));
            myNormalCollection.Add(new Vector3D(0, 0, 1));
            mg3d.Normals = myNormalCollection;

            // Create a collection of vertex positions for the MeshGeometry3D.
            Point3DCollection myPositionCollection = new Point3DCollection();
            myPositionCollection.Add(new Point3D(-0.5, -0.5, 0.5));
            myPositionCollection.Add(new Point3D(0.5, -0.5, 0.5));
            myPositionCollection.Add(new Point3D(0.5, 0.5, 0.5));
            myPositionCollection.Add(new Point3D(0.5, 0.5, 0.5));
            myPositionCollection.Add(new Point3D(-0.5, 0.5, 0.5));
            myPositionCollection.Add(new Point3D(-0.5, -0.5, 0.5));
            mg3d.Positions = myPositionCollection;

            // Create a collection of texture coordinates for the MeshGeometry3D.
            PointCollection myTextureCoordinatesCollection = new PointCollection();
            myTextureCoordinatesCollection.Add(new Point(0, 0));
            myTextureCoordinatesCollection.Add(new Point(1, 0));
            myTextureCoordinatesCollection.Add(new Point(1, 1));
            myTextureCoordinatesCollection.Add(new Point(1, 1));
            myTextureCoordinatesCollection.Add(new Point(0, 1));
            myTextureCoordinatesCollection.Add(new Point(0, 0));
            mg3d.TextureCoordinates = myTextureCoordinatesCollection;

            // Create a collection of triangle indices for the MeshGeometry3D.
            Int32Collection myTriangleIndicesCollection = new Int32Collection();
            myTriangleIndicesCollection.Add(0);
            myTriangleIndicesCollection.Add(1);
            myTriangleIndicesCollection.Add(2);
            myTriangleIndicesCollection.Add(3);
            myTriangleIndicesCollection.Add(4);
            myTriangleIndicesCollection.Add(5);
            mg3d.TriangleIndices = myTriangleIndicesCollection;

            // Apply the mesh to the geometry model.
            sample.Geometry = mg3d;

            // The material specifies the material applied to the 3D object. In this sample a
            // linear gradient covers the surface of the 3D object.

            // Create a horizontal linear gradient with four stops.
            LinearGradientBrush myHorizontalGradient = new LinearGradientBrush();
            myHorizontalGradient.StartPoint = new Point(0, 0.5);
            myHorizontalGradient.EndPoint = new Point(1, 0.5);
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Yellow, 0.0));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Red, 0.25));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.Blue, 0.75));
            myHorizontalGradient.GradientStops.Add(new GradientStop(Colors.LimeGreen, 1.0));

            // Define material and apply to the mesh geometries.
            DiffuseMaterial myMaterial = new DiffuseMaterial(myHorizontalGradient);
            sample.Material = myMaterial;

            // Apply a transform to the object. In this sample, a rotation transform is applied,
            // rendering the 3D object rotated.
            RotateTransform3D myRotateTransform3D = new RotateTransform3D();
            AxisAngleRotation3D myAxisAngleRotation3d = new AxisAngleRotation3D();
            myAxisAngleRotation3d.Axis = new Vector3D(0, 3, 0);
            myAxisAngleRotation3d.Angle = 40;
            myRotateTransform3D.Rotation = myAxisAngleRotation3d;
            sample.Transform = myRotateTransform3D;

            _GM3DList.Clear();
            _GM3DList.Add(sample);
            RefreshCanvas();            
        }
        //public DrawObject
        //CreateCube
        //CreateCuboid 
        //CreateGeometry
    }
}
