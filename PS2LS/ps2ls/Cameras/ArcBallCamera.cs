using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ps2ls.Cameras
{
    public class ArcBallCamera : Camera
    {
        private Single distance;
        private Vector3 target;
        private Single desiredPitch = 0;

        public Single DesiredDistance { get; set; }
        public Vector3 DesiredTarget{ get; set; }
        public Single DesiredYaw { get; set; }
        public Single DesiredPitch
        {
            get { return desiredPitch; }
            set
            {
                if (value > MathHelper.DegreesToRadians(89.9f))
                {
                    desiredPitch = MathHelper.DegreesToRadians(89.9f);
                }
                else if (value < -MathHelper.DegreesToRadians(89.9f))
                {
                    desiredPitch = -MathHelper.DegreesToRadians(89.9f);
                }
                else
                {
                    desiredPitch = value;
                }
            }
        }

        public ArcBallCamera()
            : base(Camera.Types.ArcBall)
        {
            DesiredYaw = Yaw = MathHelper.DegreesToRadians(-45.0f);
            DesiredPitch = Pitch = MathHelper.DegreesToRadians(45.0f);
            DesiredDistance = distance = 10.0f;
        }

        public override void Update()
        {
            distance = DesiredDistance;

            if (distance < 0.0f)
            {
                distance = 0.0f;
            }

            target = DesiredTarget;

            Yaw = DesiredYaw;

            Pitch = DesiredPitch;

            Matrix4 world = Matrix4.CreateRotationX(Pitch) * Matrix4.CreateRotationY(Yaw);
            Vector3 forward = Vector3.Transform(Vector3.UnitZ, world);

            Position = target - (forward * distance);

            base.Update();
        }
    }
}
