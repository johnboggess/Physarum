using System;
using System.Collections.Generic;
using System.Text;

using ObjectTK.Shaders;
using ObjectTK.Shaders.Sources;
using ObjectTK.Shaders.Variables;
namespace Physarum.TK.Shaders
{
    [ComputeShaderSource("DispersionShader.Dispersion")]
    class DispersionProgram : ComputeProgram
    {
        public ImageUniform Texture { get; set; }
        public Uniform<int> Width { get; set; }
        public Uniform<int> Height { get; set; }
        public Uniform<float> Evaporation { get; set; }
    }
}
