using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ps2ls.Forms.Controls;
using ps2ls.Cameras;

namespace ps2ls.Forms
{
    public class MaterialBrowserGLControl : CustomGLControl
    {
        public ArcBallCamera Camera { get; set; }

        public MaterialBrowserGLControl()
        {
            Camera = new ArcBallCamera();
            Camera.DesiredPitch = 0;
            Camera.DesiredYaw = 0;
            Camera.DesiredDistance = 2.0f;
        }
    }
}
