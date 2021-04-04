using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;

using ObjectTK.Shaders;
using ObjectTK.Shaders.Sources;
using ObjectTK.Shaders.Variables;
namespace PhysarumCore.Shaders
{
    [ComputeShaderSource("AgentShader.Agent")]
    public class AgentProgram : ComputeProgram
    {
        public ImageUniform Texture { get; set; }
        public Uniform<int> Width { get; set; }
        public Uniform<int> Height { get; set; }
        public Uniform<int> Iteration { get; set; }
        public Uniform<float> DeltaTime { get; set; }
        public UniformBuffer Settings { get; set; }
        public ShaderStorage Agents { get; set; }
    }
}
