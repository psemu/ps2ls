using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ps2ls.Cameras
{
    public class Camera
    {
        public enum Types
        {
            ArcBall,
            Free
        };

        public Matrix4 View { get; protected set; }
        public Matrix4 Projection { get; protected set; }
        public Single AspectRatio { get; set; }
        public Single FieldOfView { get; set; }
        public Single NearPlaneDistance { get; set; }
        public Single FarPlaneDistance { get; set; }
        public Vector3 Position { get; set; }
        public Single Pitch { get; set; }
        public Single Yaw { get; set; }

        public Camera.Types CameraType { get; private set; }

        protected Camera(Camera.Types type)
        {
            CameraType = type;
            FieldOfView = MathHelper.DegreesToRadians(74.0f);
            NearPlaneDistance = (Single)Math.Pow(2, -8);
            FarPlaneDistance = (Single)Math.Pow(2, 8);
        }

        public virtual void Update()
        {
            Projection = Matrix4.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearPlaneDistance, FarPlaneDistance);
            
            Matrix4 world = Matrix4.CreateRotationX(Pitch) * Matrix4.CreateRotationY(Yaw);
            Vector3 forward = Vector3.Transform(Vector3.UnitZ, world);

            View = Matrix4.LookAt(Position, Position + forward, Vector3.UnitY);
        }
    }
}