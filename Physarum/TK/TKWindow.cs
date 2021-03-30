using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;

namespace Physarum.TK
{
    public class TKWindow : GameWindow
    {
        public TKWindow() : base(GameWindowSettings.Default, NativeWindowSettings.Default) { }

        public TKWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
        }
    }
}
