using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ps2ls.Forms.Controls
{
    public class CustomGLControl : GLControl
    {
        public CustomGLControl()
            : base(new OpenTK.Graphics.GraphicsMode(32, 24, 8, 8), 2, 0, OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible)
        {
        }
    }
}
