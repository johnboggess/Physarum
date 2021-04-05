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
        public AgentSettings AgentSettings;

        Texture2D _textureOut;

        RenderProgram _renderProgram;
        AgentProgram _agentProgram;
        FadeProgram _fadeProgram;

        object _lock = new object();
        int _width = 1000;
        int _height = 1000;
        int _numberOfAgents = 100_000;
        const int _localWorkGroupSize = 1000;
        int _iteration = 0;

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

            AgentSettings = AgentSettings.Default();

            _renderProgram = RenderProgram.Create();

            _textureOut = new Texture2D(SizedInternalFormat.Rgba8, _width, _height);
            _textureOut.SetFilter(TextureMinFilter.Nearest, TextureMagFilter.Nearest);
            _textureOut.Bind(TextureUnit.Texture0);

            _agentProgram = AgentProgram.Create(_width, _height, _textureOut, _numberOfAgents, AgentSettings.Default());

            _fadeProgram = FadeProgram.Create(_width, _height, _textureOut, FadeSettings.Default());

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
            _agentProgram.AgentSettings = AgentSettings;
            AgentProgram.Dispatch(_numberOfAgents / _localWorkGroupSize, 1, 1);
            
            Thread.Sleep(10);

            _fadeProgram.Use();
            _fadeProgram.DeltaTime.Set((float)args.Time);
            FadeProgram.Dispatch(_width / 10, _height / 10, 1);

            _renderProgram.Use();
            _renderProgram.Draw();

            _iteration += 1;
            _iteration %= int.MaxValue;

            SwapBuffers();
        }
    }
}
