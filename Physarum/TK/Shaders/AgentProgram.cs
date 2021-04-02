﻿using System;
using System.Collections.Generic;
using System.Text;

using ObjectTK.Shaders;
using ObjectTK.Shaders.Sources;
using ObjectTK.Shaders.Variables;
namespace Physarum.TK.Shaders
{
    [ComputeShaderSource("AgentShader.Agent")]
    public class AgentProgram : ComputeProgram
    {
        public ImageUniform Texture { get; set; }
        public Uniform<int> Width { get; set; }
        public Uniform<int> Height { get; set; }
        public Uniform<float> Speed { get; set; }
        public ShaderStorage Agents { get; set; }
    }
}