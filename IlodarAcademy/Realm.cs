using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Aritiafel.IlodarAcademy
{
    public enum CameraMode
    {
        Perspective,
        Orthographic,
        Projection
    }

    public class Realm
    {
        public CameraMode CameraMode
        {
            get {
                if (_Camera is PerspectiveCamera)
                    return CameraMode.Perspective;
                else if (_Camera is OrthographicCamera)
                    return CameraMode.Orthographic;
                else
                    return CameraMode.Projection;
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

        public Canvas Canvas { 
            get => _Canvas;
            set { 
                _Canvas = value;
                RefreshCanvas();
            }
        }

        private Viewport3D _V3D;
        private ModelVisual3D _MV3D;
        private Canvas _Canvas;
        private Camera _Camera;
        //private CameraMode _CameraMode;
        //private PerspectiveCamera _PCamera;
        //private OrthographicCamera _OCamera;
        
        protected void SetCamera(CameraMode mode, Point3D position, Vector3D lookDirection, Vector3D upDirection, double fieldOfView)
        {
            //_CameraMode = mode;
            if (mode == CameraMode.Perspective)
                _Camera = new PerspectiveCamera(position, lookDirection, upDirection, fieldOfView);
            else if (mode == CameraMode.Orthographic)
                _Camera = new OrthographicCamera(position, lookDirection, upDirection, fieldOfView);
            else
                throw new NotImplementedException(); //_Camera = _Canvas.Pro new ProjectionCamera();
            _V3D.Camera = _Camera;
        }
        public Realm(Canvas canvas = null, CameraMode cameraMode = CameraMode.Perspective)
        {
            _Canvas = canvas;   
            RefreshCanvas();
        }

        protected void RefreshCanvas()
        {
            if (_Canvas == null)
                return;
            _V3D = new Viewport3D();
            _Canvas.Children.Add(_V3D);
            _MV3D = new ModelVisual3D();
            _V3D.Children.Add(_MV3D);
            SetCamera(CameraMode, CameraPosition, CameraLookDirection, CameraUpDirection, CameraFieldOfView);
        }
        //public DrawObject
        //CreateCube
        //CreateCuboid 
        //CreateGeometry
    }
}
