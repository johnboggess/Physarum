using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

using ObjectTK.Shaders;
using ObjectTK.Shaders.Sources;
using ObjectTK.Shaders.Variables;
using ObjectTK.Textures;
using ObjectTK.Buffers;

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

        private AgentSettings _agentSettings;

        public AgentSettings AgentSettings
        {
            get { return _agentSettings; }
            set
            {
                _agentSettings = value;
                if (_AgentsSettingsBuffer == null)
                {
                    _AgentsSettingsBuffer = new Buffer<AgentSettings>();
                    _AgentsSettingsBuffer.Init(BufferTarget.UniformBuffer, new AgentSettings[] { _agentSettings });
                    Settings.BindBuffer(_AgentsSettingsBuffer);
                }
                _AgentsSettingsBuffer.SubData(BufferTarget.UniformBuffer, new AgentSettings[] { _agentSettings }, 0);
            }
        }

        internal Buffer<Agent> _AgentsBuffer;
        internal Buffer<AgentSettings> _AgentsSettingsBuffer;

        public static AgentProgram Create(int width, int height, Texture2D texture, int numberOfAgents, AgentSettings settings)
        {
            AgentProgram _agentProgram = ObjectTK.Shaders.ProgramFactory.Create<AgentProgram>();
            _agentProgram.Use();
            _agentProgram.Texture.Bind(0, texture, TextureAccess.ReadWrite);
            _agentProgram.Width.Set(width);
            _agentProgram.Height.Set(height);
            _agentProgram.Iteration.Set(0);


            Agent[] a = Agent.RandomAgents(numberOfAgents, new Vector2(width / 2, height / 2), width / 4);
            _agentProgram._AgentsBuffer = new Buffer<Agent>();
            _agentProgram._AgentsBuffer.Init(BufferTarget.ArrayBuffer, a);
            _agentProgram.Agents.BindBuffer(_agentProgram._AgentsBuffer);


            _agentProgram.AgentSettings = settings;

            return _agentProgram;
        }
    }
}
