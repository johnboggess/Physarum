using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using ObjectTK.Textures;

using ObjectTK.Buffers;

using Physarum.TK.Shaders;

namespace Physarum.TK
{
    public class TKWindow : GameWindow
    {
        VertexArray _screen;
        Buffer<Vector2> uvBuffer;
        Buffer<Vector3> vertexBuffer;
        Buffer<int> indexBuffer;

        Texture2D _physarumOut;

        RenderProgram _renderProgram;
        PhysarumProgram _physarumProgram;

        int _width = 500;
        int _height = 500;
        int _numberOfAgents = 100;
        const int _localWorkGroupSize = 100;

        public TKWindow() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            log4net.Config.BasicConfigurator.Configure();
        }

        public TKWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            GL.ClearColor(Color4.CornflowerBlue);

            Vector3[] vertices = new Vector3[] { new Vector3(-1, -1, 0), new Vector3(-1, 1, 0), new Vector3(1, -1, 0), new Vector3(1, 1, 0) };
            vertexBuffer = new Buffer<Vector3>();
            vertexBuffer.Init(BufferTarget.ArrayBuffer, vertices);

            Vector2[] uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };
            uvBuffer = new Buffer<Vector2>();
            uvBuffer.Init(BufferTarget.ArrayBuffer, uv);

            indexBuffer = new Buffer<int>();
            indexBuffer.Init(BufferTarget.ElementArrayBuffer, new int[] { 0, 1, 2, 3, 2, 1 });

            _physarumOut = new Texture2D(SizedInternalFormat.Rgba8, _width, _height);
            _physarumOut.SetFilter(TextureMinFilter.Nearest, TextureMagFilter.Nearest);
            _physarumOut.Bind(TextureUnit.Texture0);

            _physarumProgram = ObjectTK.Shaders.ProgramFactory.Create<PhysarumProgram>();
            _physarumProgram.Use();
            _physarumProgram.Texture.Bind(0, _physarumOut, TextureAccess.ReadWrite);
            _physarumProgram.Width.Set(_width);
            _physarumProgram.Height.Set(_height);

            Agent[] a = Agent.RandomAgents(_numberOfAgents, new Vector2(_width/2, _height/2), _width/4);
            float[] f = Agent.AgentArrayToFloatArray(a);

            _physarumProgram.Agents.Set(f);

            _renderProgram = ObjectTK.Shaders.ProgramFactory.Create<RenderProgram>();
            _renderProgram.Use();
            _renderProgram.Texture.Set(TextureUnit.Texture0);

            _screen = new VertexArray();
            _screen.Bind();
            _screen.BindAttribute(_renderProgram.InPosition, vertexBuffer);
            _screen.BindAttribute(_renderProgram.InUV, uvBuffer);
            _screen.BindElementBuffer(indexBuffer);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            _physarumProgram.Use();
            PhysarumProgram.Dispatch(_localWorkGroupSize / 100, 1, 1);

            _renderProgram.Use();
            _screen.DrawElements(PrimitiveType.Triangles, indexBuffer.ElementCount);
            SwapBuffers();
        }
    }
}
