using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using ObjectTK.Textures;

using ObjectTK.Buffers;
using ObjectTK.Shaders.Variables;

using PhysarumCore.Shaders;

namespace PhysarumCore
{
    public class TKWindow : GameWindow
    {
        VertexArray _screen;
        Buffer<Vector2> uvBuffer;
        Buffer<Vector3> vertexBuffer;
        Buffer<int> indexBuffer;

        Texture2D _textureOut;

        RenderProgram _renderProgram;
        AgentProgram _agentProgram;
        FadeProgram _fadeProgram;
        Buffer<Agent> _agents;
        Buffer<AgentSettings> _settings;

        int _width = 1000;
        int _height = 1000;
        float _speed = 50;
        float _fadeRate = .2f;
        float _diffusionRate = 10f;
        int _numberOfAgents = 100_000;
        const int _localWorkGroupSize = 1000;
        int _iteration = 0;
        float _steerStrength = 1;
        float _jitter = .5f;
        Color4 _color = Color4.Magenta;

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

            _textureOut = new Texture2D(SizedInternalFormat.Rgba8, _width, _height);
            _textureOut.SetFilter(TextureMinFilter.Nearest, TextureMagFilter.Nearest);
            _textureOut.Bind(TextureUnit.Texture0);

            _agentProgram = ObjectTK.Shaders.ProgramFactory.Create<AgentProgram>();
            _agentProgram.Use();
            _agentProgram.Texture.Bind(0, _textureOut, TextureAccess.ReadWrite);
            _agentProgram.Width.Set(_width);
            _agentProgram.Height.Set(_height);
            _agentProgram.Iteration.Set(_iteration);

            Agent[] a = Agent.RandomAgents(_numberOfAgents, new Vector2(_width/2, _height/2), _width/4);

            _agents = new Buffer<Agent>();
            _agents.Init(BufferTarget.ArrayBuffer, a);
            _agentProgram.Agents.BindBuffer(_agents);

            _settings = new Buffer<AgentSettings>();
            _settings.Init(BufferTarget.UniformBuffer, new AgentSettings[] { AgentSettings.Default() });
            _agentProgram.Settings.BindBuffer(_settings);

            _fadeProgram = ObjectTK.Shaders.ProgramFactory.Create<FadeProgram>();
            _fadeProgram.Use();
            _fadeProgram.Texture.Bind(0, _textureOut, TextureAccess.ReadWrite);
            _fadeProgram.Width.Set(_width);
            _fadeProgram.Height.Set(_height);
            _fadeProgram.FadeRate.Set(_fadeRate);
            _fadeProgram.DiffusionRate.Set(_diffusionRate);

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
            _agentProgram.DeltaTime.Set((float)args.Time);
            AgentProgram.Dispatch(_numberOfAgents / _localWorkGroupSize, 1, 1);
            
            Thread.Sleep(10);

            _fadeProgram.Use();
            _fadeProgram.DeltaTime.Set((float)args.Time);
            FadeProgram.Dispatch(_width / 10, _height / 10, 1);

            _renderProgram.Use();
            _screen.DrawElements(PrimitiveType.Triangles, indexBuffer.ElementCount);

            _iteration += 1;
            _iteration %= int.MaxValue;

            SwapBuffers();
        }
    }
}
