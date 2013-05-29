using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using ps2ls.Assets.Dme;
using ps2ls.Assets.Pack;
using ps2ls.Cameras;
using ps2ls.Graphics.Materials;

namespace ps2ls.Forms.Controls
{
    public class GLModelVeiwerControl : GLControl
    {





        #region Mesh Colors
        // a series of nice pastel colors we'll use to color meshes
        Color[] meshColors = {
                                 Color.FromArgb(162, 206, 250),
                                 Color.FromArgb(244, 228, 139),
                                 Color.FromArgb(206, 128, 236),
                                 Color.FromArgb(212, 201, 158),
                                 Color.FromArgb(252, 247, 158),
                                 Color.FromArgb(162, 140, 166),
                                 Color.FromArgb(224, 166, 157),
                                 Color.FromArgb(199, 188, 183),
                                 Color.FromArgb(226, 247, 150),
                                 Color.FromArgb(128, 197, 167),
                                 Color.FromArgb(219, 152, 223),
                                 Color.FromArgb(241, 167, 249),
                                 Color.FromArgb(131, 179, 175),
                                 Color.FromArgb(167, 167, 151),
                                 Color.FromArgb(230, 163, 139),
                                 Color.FromArgb(176, 165, 128),
                                 Color.FromArgb(168, 199, 185),
                                 Color.FromArgb(231, 166, 254),
                                 Color.FromArgb(153, 177, 250),
                                 Color.FromArgb(163, 251, 178),
                                 Color.FromArgb(246, 198, 243),
                                 Color.FromArgb(198, 220, 216),
                                 Color.FromArgb(242, 235, 193),
                                 Color.FromArgb(145, 195, 137),
                                 Color.FromArgb(135, 186, 207),
                                 Color.FromArgb(254, 187, 169),
                                 Color.FromArgb(238, 207, 158),
                                 Color.FromArgb(166, 178, 208),
                                 Color.FromArgb(165, 137, 128),
                                 Color.FromArgb(250, 218, 178),
                                 Color.FromArgb(144, 223, 183),
                                 Color.FromArgb(252, 175, 224)
                             };
        #endregion


        #region Properties
        public Color BackgroundColor
        {
            get;
            set;
        }

        public bool ShowAxis
        {
            get;
            set;
        }

        public bool Wireframe
        {
            get;
            set;
        }
        public Model Model
        {
            get;
            set;
        }
        #endregion

        #region Member Variables


        private Int32 texturedShader = 0;
        private int untexturedShader = 0;
        private int currentShader = 0;

        private List<int> textures = new List<int>(new int[] { 0, 0, 0, 0, 0 });

        public int gray;
        #endregion

        public GLModelVeiwerControl()
            : base(new OpenTK.Graphics.GraphicsMode(32, 24, 8, 8), 2, 0, OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible)
        {
            Camera = new ArcBallCamera();

        }



        #region Camera Controls

                private Point location;
        private bool rotating;
        private bool panning;
        private bool zooming;

        public ArcBallCamera Camera { get; set; }

       
        private void ModelBrowserGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    rotating = true;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    panning = true;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    zooming = true;
                    break;
            }
        }

        private void ModelBrowserGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    rotating = false;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    panning = false;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    zooming = false;
                    break;
            }
        }

        private void ModelBrowserGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (location.X == 0 && location.Y == 0)
            {
                location = e.Location;
            }

            Int32 deltaX = e.Location.X - location.X;
            Int32 deltaY = e.Location.Y - location.Y;

            if (panning)
            {
                Matrix4 world = Matrix4.CreateFromAxisAngle(Vector3.UnitX, Camera.Pitch) * Matrix4.CreateFromAxisAngle(Vector3.UnitY, Camera.Yaw);

                Vector3 forward = Vector3.Transform(Vector3.UnitZ, world);
                forward.Y = 0;
                forward.Normalize();

                Vector3 up = Vector3.UnitY;
                Vector3 left = Vector3.Cross(up, forward);

                Camera.DesiredTarget += (up * deltaY) * 0.00390625f;
                Camera.DesiredTarget += (left * deltaX) * 0.00390625f;
            }
            else if (rotating)
            {
                Camera.DesiredYaw -= MathHelper.DegreesToRadians(0.25f * deltaX);
                Camera.DesiredPitch += MathHelper.DegreesToRadians(0.25f * deltaY);
            }
            else if (zooming)
            {
                Camera.DesiredDistance -= deltaY * 0.015625f;
            }

            location = e.Location;
        }

        private void GLModelVeiwerControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    Wireframe = !Wireframe;
                    break;
              
            }
        }

        #endregion

        #region Shader Compilation
        //TODO: move this elsehwere
        private void compileShader(Int32 shader, String source)
        {
            ErrorCode e;

            GL.ShaderSource(shader, source);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }
            GL.CompileShader(shader);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }

            String info = String.Empty;
            GL.GetShaderInfoLog(shader, out info);
            Console.WriteLine(info);

            Int32 compileResult;
            GL.GetShader(shader, ShaderParameter.CompileStatus, out compileResult);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }

            if (compileResult != 1)
            {
                Console.WriteLine("Compile error!");
                Console.WriteLine(source);
            }
        }




        private void createShaderProgram()
        {
            //TODO: Use external shader source files.
            String vertexShaderSource = @"
void main(void)
{
    gl_Position = ftransform();

    gl_TexCoord[0] = gl_MultiTexCoord0;
}
";
            String fragmentShaderSource = @"
uniform sampler2D colorMap;

void main(void)
{
   vec4 col = texture2D(colorMap, gl_TexCoord[0].st);
   if(col.a <= 0) discard;
   gl_FragColor = texture2D(colorMap, gl_TexCoord[0].st);
}
";

            texturedShader = createShaderProgram(vertexShaderSource, fragmentShaderSource);

            vertexShaderSource = @"
varying vec3 normal;
varying vec3 lightDirection;


void main() 
{ 
    gl_Position = ftransform();

    gl_FrontColor = gl_Color;


   
    normal = gl_NormalMatrix * gl_Normal;

    lightDirection = vec3(1, -1, 1);
}
";
            fragmentShaderSource = @"
varying vec3 normal; 
varying vec3 lightDirection;

void main()
{
	const vec4 ambientColor = vec4(0.25, 0.25, 0.25, 1.0);
    const vec4 diffuseColor = vec4(1.0, 1.0, 1.0, 1.0);

    vec3 normalizedNormal = normalize(normal);
    vec3 noralizedLightDirection = normalize(lightDirection);

    float diffuseTerm = clamp(dot(normalizedNormal, noralizedLightDirection), 0.0, 1.0);

    gl_FragColor = gl_Color * (ambientColor + (diffuseColor * diffuseTerm));
 

}
";
            untexturedShader = createShaderProgram(vertexShaderSource, fragmentShaderSource);

            currentShader = texturedShader;

            gray = LoadTexture("grey.dds");

        }

        //TODO: move this elsehwere
        private int createShaderProgram(string vertexShaderSource, string fragmentShaderSource)
        {
            ErrorCode e;

            GL.GetError(); //clear error

            int shaderProgram = GL.CreateProgram();
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }

            Int32 vertexShader = GL.CreateShader(ShaderType.VertexShader);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }
            Int32 fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }



            compileShader(vertexShader, vertexShaderSource);
            int res = 0;
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out res);
            if ((All)res == All.False)
            {
                MessageBox.Show(GL.GetShaderInfoLog(vertexShader));
                throw new Exception(GL.GetShaderInfoLog(vertexShader));
            }
            compileShader(fragmentShader, fragmentShaderSource);
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out res);
            if ((All)res == All.False)
            {
                MessageBox.Show(GL.GetShaderInfoLog(fragmentShader));
                throw new Exception(GL.GetShaderInfoLog(fragmentShader));
            }

            GL.AttachShader(shaderProgram, fragmentShader);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }
            GL.AttachShader(shaderProgram, vertexShader);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }
            GL.LinkProgram(shaderProgram);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }

            String info;
            GL.GetProgramInfoLog(shaderProgram, out info);

            Console.WriteLine(info);

            if (fragmentShader != 0)
            {
                GL.DeleteShader(fragmentShader);
                if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }
            }

            if (vertexShader != 0)
            {
                GL.DeleteShader(vertexShader);
                if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }
            }

            return shaderProgram;
        }
        #endregion

        #region Render
        public void update()
        {
            Camera.AspectRatio = (Single)ClientSize.Width / (Single)ClientSize.Height;
            Camera.Update();
        }


        public void render()
        {
            this.MakeCurrent();

            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

            //clear
            GL.ClearColor(BackgroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //projection matrix
            Matrix4 projection = Camera.Projection;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            //view matrix
            Matrix4 view = Camera.View;
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref view);

            if (ShowAxis)
            {
                // debug axes
                GL.Begin(BeginMode.Lines);
                //x
                GL.Color3(Color.Red);
                GL.Vertex3(Vector3.Zero);
                GL.Vertex3(Vector3.UnitX);
                GL.Vertex3(Vector3.UnitX); GL.Vertex3(Vector3.UnitX + new Vector3(-0.125f, 0.125f, 0.0f));
                GL.Vertex3(Vector3.UnitX); GL.Vertex3(Vector3.UnitX + new Vector3(-0.125f, -0.125f, 0.0f));

                //y
                GL.Color3(Color.Green);
                GL.Vertex3(Vector3.Zero);
                GL.Vertex3(Vector3.UnitY);
                GL.Vertex3(Vector3.UnitY); GL.Vertex3(Vector3.UnitY + new Vector3(0.125f, -0.125f, 0.0f));
                GL.Vertex3(Vector3.UnitY); GL.Vertex3(Vector3.UnitY + new Vector3(-0.125f, -0.125f, 0.0f));

                //z
                GL.Color3(Color.Blue);
                GL.Vertex3(Vector3.Zero);
                GL.Vertex3(Vector3.UnitZ);
                GL.Vertex3(Vector3.UnitZ); GL.Vertex3(Vector3.UnitZ + new Vector3(0, -0.125f, -0.125f));
                GL.Vertex3(Vector3.UnitZ); GL.Vertex3(Vector3.UnitZ + new Vector3(0, 0.125f, -0.125f));

                GL.End();
            }

            //TODO: Decide what to do with non-version 4 models.
            if (Model != null && Model.Version == 4)
            {
                GL.PushMatrix();

                GL.PushAttrib(AttribMask.PolygonBit | AttribMask.EnableBit | AttribMask.LightingBit | AttribMask.CurrentBit);

                GL.UseProgram(currentShader);

                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);
                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.CullFace(CullFaceMode.Back);
                GL.FrontFace(FrontFaceDirection.Cw);



                for (Int32 i = 0; i < Model.Meshes.Length; ++i)
                {
                    Mesh mesh = Model.Meshes[i];

                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, textures[i]);

                    if (currentShader == texturedShader)
                    {

                        int loc = GL.GetUniformLocation(currentShader, "colorMap");
                        GL.Uniform1(loc, 0);
                    }

                    //pin handles to stream data
                    GCHandle[] streamDataGCHandles = new GCHandle[mesh.VertexStreams.Length];

                    for (Int32 j = 0; j < streamDataGCHandles.Length; ++j)
                    {
                        streamDataGCHandles[j] = GCHandle.Alloc(mesh.VertexStreams[j].Data, GCHandleType.Pinned);
                    }

                    //fetch material definition and vertex layout
                    MaterialDefinition materialDefinition = MaterialDefinitionManager.Instance.MaterialDefinitions[Model.Materials[(Int32)mesh.MaterialIndex].MaterialDefinitionHash];
                    VertexLayout vertexLayout = MaterialDefinitionManager.Instance.VertexLayouts[materialDefinition.DrawStyles[0].VertexLayoutNameHash];

                    GL.Color3(meshColors[i % meshColors.Length]);

                    if (Wireframe)
                    {
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    }
                    else
                    {
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                    }

                    //position
                    VertexLayout.Entry.DataTypes positionDataType = VertexLayout.Entry.DataTypes.None;
                    Int32 positionStream = 0;
                    Int32 positionOffset = 0;
                    bool positionExists = vertexLayout.GetEntryInfoFromDataUsageAndUsageIndex(VertexLayout.Entry.DataUsages.Position, 0, out positionDataType, out positionStream, out positionOffset);

                    if (positionExists)
                    {
                        IntPtr positionData = streamDataGCHandles[positionStream].AddrOfPinnedObject();

                        GL.EnableClientState(ArrayCap.VertexArray);
                        GL.VertexPointer(3, VertexPointerType.Float, mesh.VertexStreams[positionStream].BytesPerVertex, positionData + positionOffset);
                    }

                    //normal
                    VertexLayout.Entry.DataTypes normalDataType = VertexLayout.Entry.DataTypes.None;
                    Int32 normalStream = 0;
                    Int32 normalOffset = 0;
                    bool normalExists = vertexLayout.GetEntryInfoFromDataUsageAndUsageIndex(VertexLayout.Entry.DataUsages.Normal, 0, out normalDataType, out normalStream, out normalOffset);

                    if (normalExists)
                    {
                        IntPtr normalData = streamDataGCHandles[normalStream].AddrOfPinnedObject();

                        GL.EnableClientState(ArrayCap.NormalArray);
                        GL.NormalPointer(NormalPointerType.Float, mesh.VertexStreams[normalStream].BytesPerVertex, normalData + normalOffset);
                    }


                    //texture coordiantes
                    VertexLayout.Entry.DataTypes texCoord0DataType = VertexLayout.Entry.DataTypes.None;
                    Int32 texCoord0Stream = 0;
                    Int32 texCoord0Offset = 0;
                    bool texCoord0Exists = vertexLayout.GetEntryInfoFromDataUsageAndUsageIndex(VertexLayout.Entry.DataUsages.Texcoord, 0, out texCoord0DataType, out texCoord0Stream, out texCoord0Offset);

                    if (texCoord0Exists)
                    {
                        IntPtr texCoord0Data = streamDataGCHandles[texCoord0Stream].AddrOfPinnedObject();

                        GL.EnableClientState(ArrayCap.TextureCoordArray);

                        TexCoordPointerType texCoord0PointerType = TexCoordPointerType.Float;

                        switch (texCoord0DataType)
                        {
                            case VertexLayout.Entry.DataTypes.Float2:
                                texCoord0PointerType = TexCoordPointerType.Float;
                                break;
                            case VertexLayout.Entry.DataTypes.float16_2:
                                texCoord0PointerType = TexCoordPointerType.HalfFloat;
                                break;
                            default:
                                break;
                        }

                        GL.TexCoordPointer(2, texCoord0PointerType, mesh.VertexStreams[texCoord0Stream].BytesPerVertex, texCoord0Data + texCoord0Offset);
                    }



                    //indices
                    GCHandle indexDataHandle = GCHandle.Alloc(mesh.IndexData, GCHandleType.Pinned);
                    IntPtr indexData = indexDataHandle.AddrOfPinnedObject();

                    GL.DrawElements(BeginMode.Triangles, (Int32)mesh.IndexCount, DrawElementsType.UnsignedShort, indexData);

                    indexDataHandle.Free();

                    GL.DisableClientState(ArrayCap.VertexArray);
                    GL.DisableClientState(ArrayCap.NormalArray);
                    GL.DisableClientState(ArrayCap.TextureCoordArray);

                    //free stream data handles
                    for (Int32 j = 0; j < streamDataGCHandles.Length; ++j)
                    {
                        streamDataGCHandles[j].Free();
                    }
                }

                GL.UseProgram(0);

                GL.PopAttrib();

                ////bounding box
                //if (showBoundingBoxButton.Checked)
                //{
                //    GL.PushAttrib(AttribMask.CurrentBit | AttribMask.EnableBit);

                //    GL.Color3(Color.Red);

                //    GL.Enable(EnableCap.DepthTest);

                //    Vector3 min = model.Min;
                //    Vector3 max = model.Max;
                //    Vector3[] vertices = new Vector3[8];
                //    UInt32[] indices = { 0, 1, 1, 2, 2, 3, 3, 0, 0, 4, 1, 5, 2, 6, 3, 7, 4, 5, 5, 6, 6, 7, 7, 4 };

                //    vertices[0] = min;
                //    vertices[1] = new Vector3(max.X, min.Y, min.Z);
                //    vertices[2] = new Vector3(max.X, min.Y, max.Z);
                //    vertices[3] = new Vector3(min.X, min.Y, max.Z);
                //    vertices[4] = new Vector3(min.X, max.Y, min.Z);
                //    vertices[5] = new Vector3(max.X, max.Y, min.Z);
                //    vertices[6] = max;
                //    vertices[7] = new Vector3(min.X, max.Y, max.Z);

                //    GL.EnableClientState(ArrayCap.VertexArray);
                //    GL.VertexPointer(3, VertexPointerType.Float, 0, vertices);
                //    GL.DrawRangeElements(BeginMode.Lines, 0, 23, 24, DrawElementsType.UnsignedInt, indices);

                //    GL.PopAttrib();
                //}
            }

            this.SwapBuffers();
        }
        #endregion

        #region Events
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GLModelVeiwerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "GLModelVeiwerControl";
            this.Load += new System.EventHandler(this.GLModelVeiwerControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GLModelVeiwerControl_Paint);
            this.MouseEnter += new System.EventHandler(this.glControl1_MouseEnter);
            this.Resize += new System.EventHandler(this.GLModelVeiwerControl_Resize);
            this.ResumeLayout(false);

        }

        private void GLModelVeiwerControl_Paint(object sender, PaintEventArgs e)
        {
            render();
        }

        private void GLModelVeiwerControl_Resize(object sender, EventArgs e)
        {
            
            if (this.Height == 0)
            {
                this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, 1);
            }
        }

        private void GLModelVeiwerControl_Load(object sender, EventArgs e)
        {
            CreateGraphics();

            createShaderProgram();
            Application.Idle += applicationIdle;
        }

        private void applicationIdle(object sender, EventArgs e)
        {

            
            while (Context != null && IsIdle)
            {
                update();
                render();
            }
        }

        private void glControl1_MouseEnter(object sender, EventArgs e)
        {
            Focus();
        }
        #endregion

        public void RenderModel(Model m)
        {
            textures.Clear();

            for (int i = 0; i < Model.Meshes.Length; i++)
            {
                textures.Add(gray);
            }


            if (Model.TextureStrings.Count == 0)
            {
                currentShader = untexturedShader;
            }
            else
            {                        
                currentShader = texturedShader;
            }


            snapCameraToModel();
        }

        private void snapCameraToModel()
        {
            if (Model == null)
            {
                return;
            }

            Vector3 center = (Model.Max + Model.Min) / 2.0f;
            Vector3 extents = (Model.Max - Model.Min) / 2.0f;

            Camera.DesiredTarget = center;
            Camera.DesiredDistance = extents.Length * 1.75f;
        }


        public int LoadTexture(string name)
        {
            MemoryStream textureMemoryStream = AssetManager.Instance.CreateAssetMemoryStreamByName(name);
            return TextureManager.LoadFromStream(textureMemoryStream);
        }

        public void SetTextureForMesh(int meshID, string name)
        {
            //if (textures[meshID] != gray) GL.DeleteTexture(textures[meshID]);
            textures[meshID] = LoadTexture(name);
        }


    }
}
