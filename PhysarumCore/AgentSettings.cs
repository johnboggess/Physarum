using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;
namespace PhysarumCore
{
    public struct AgentSettings
    {
        public float Speed;
        public float SteerStrength;
        public float Jitter;
        public float SensorDistance;

        public float SensorAngle;
        public float Padding0;
        public float Padding1;
        public float Padding2;

        public Vector4 AgentColor;

        public static AgentSettings Default()
        {
            return new AgentSettings { Speed = 1/*50*/, SteerStrength = 1, Jitter = .5f, SensorDistance = 20, SensorAngle=MathF.PI/4f, AgentColor = new Vector4(Color4.Magenta.R, Color4.Magenta.G, Color4.Magenta.B, Color4.Magenta.A) };
        }
    }
}
