using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using ObjectTK.Textures;

using ObjectTK.Buffers;
using ObjectTK.Shaders.Variables;

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
        AgentProgram _agentProgram;
        DispersionProgram _dispersionProgram;
        Buffer<float> _agents;

        int _width = 500;
        int _height = 500;
        float _speed = 1;
        float _evaporation = .01f;
        int _numberOfAgents = 1000;
        const int _localWorkGroupSize = 100;
        int _iteration = 0;
        float _randomDirection = .25f;

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

            _agentProgram = ObjectTK.Shaders.ProgramFactory.Create<AgentProgram>();
            _agentProgram.Use();
            _agentProgram.Texture.Bind(0, _physarumOut, TextureAccess.ReadWrite);
            _agentProgram.Width.Set(_width);
            _agentProgram.Height.Set(_height);
            _agentProgram.Speed.Set(_speed);
            _agentProgram.Iteration.Set(_iteration);
            _agentProgram.RandomDirection.Set(_randomDirection);

            Agent[] a = Agent.RandomAgents(_numberOfAgents, new Vector2(_width/2, _height/2), _width/4);
            float[] f = Agent.AgentArrayToFloatArray(a);
            _agents = new Buffer<float>();
            _agents.Init(BufferTarget.ArrayBuffer, f);
            _agentProgram.Agents.BindBuffer<float>(_agents);

            _dispersionProgram = ObjectTK.Shaders.ProgramFactory.Create<DispersionProgram>();
            _dispersionProgram.Use();
            _dispersionProgram.Texture.Bind(0, _physarumOut, TextureAccess.ReadWrite);
            _dispersionProgram.Width.Set(_width);
            _dispersionProgram.Height.Set(_height);
            _dispersionProgram.Evaporation.Set(_evaporation);

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
            
            _agentProgram.Use();
            _agentProgram.Iteration.Set(_iteration);
            AgentProgram.Dispatch(_numberOfAgents / _localWorkGroupSize, 1, 1);
            _dispersionProgram.Use();
            DispersionProgram.Dispatch(_width / 10, _height / 10, 1);

            _renderProgram.Use();
            _screen.DrawElements(PrimitiveType.Triangles, indexBuffer.ElementCount);

            _iteration += 1;
            _iteration %= int.MaxValue;

            SwapBuffers();
        }
    }
}
