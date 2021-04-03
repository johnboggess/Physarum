using System;
using System.Collections.Generic;
using System.Text;

using ObjectTK.Shaders;
using ObjectTK.Shaders.Sources;
using ObjectTK.Shaders.Variables;
namespace PhysarumCore.Shaders
{
    [ComputeShaderSource("FadeShader.Fade")]
    class FadeProgram : ComputeProgram
    {
        public ImageUniform Texture { get; set; }
        public Uniform<int> Width { get; set; }
        public Uniform<int> Height { get; set; }
        public Uniform<float> DeltaTime { get; set; }
        public Uniform<float> FadeRate { get; set; }
        public Uniform<float> DiffusionRate { get; set; }
    }
}
