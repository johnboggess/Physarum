using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL4;

using ObjectTK.Shaders;
using ObjectTK.Shaders.Sources;
using ObjectTK.Shaders.Variables;
using ObjectTK.Textures;
using ObjectTK.Buffers;

namespace PhysarumCore.Shaders
{
    [ComputeShaderSource("FadeShader.Fade")]
    class FadeProgram : ComputeProgram
    {
        public ImageUniform Texture { get; set; }
        public Uniform<int> Width { get; set; }
        public Uniform<int> Height { get; set; }
        public Uniform<float> DeltaTime { get; set; }
        public UniformBuffer Settings { get; set; }


        public FadeSettings FadeSettings;
        internal Buffer<FadeSettings> _FadeSettingsBuffer;

        public static FadeProgram Create(int width, int height, Texture2D texture, FadeSettings settings)
        {
            FadeProgram _fadeProgram = ObjectTK.Shaders.ProgramFactory.Create<FadeProgram>();
            _fadeProgram.Use();
            _fadeProgram.Texture.Bind(0, texture, TextureAccess.ReadWrite);
            _fadeProgram.Width.Set(width);
            _fadeProgram.Height.Set(height);

            _fadeProgram._FadeSettingsBuffer = new Buffer<FadeSettings>();
            _fadeProgram._FadeSettingsBuffer.Init(BufferTarget.UniformBuffer, new FadeSettings[] { settings });
            _fadeProgram.Settings.BindBuffer(_fadeProgram._FadeSettingsBuffer);

            return _fadeProgram;
        }
    }
}
