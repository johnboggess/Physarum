using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;
namespace Physarum.TK
{
    public struct Agent
    {
        public Vector2 Position;
        public float Direction;
        public bool Defined;

        public static int SizeInFloats { get { return 4; } }

        public float[] AsFloats()
        {
            return new float[] { Defined == true ? 1 : 0,  Position.X, Position.Y, Direction };
        }

        public static Agent[] RandomAgents(int numberOfAgents, Vector2 center, int positionRange)
        {
            Agent[] result = new Agent[numberOfAgents];
            Random random = new Random((int)(DateTime.Now.Ticks % int.MaxValue));

            for (int i = 0; i < numberOfAgents; i++)
            {
                double dir = (random.NextDouble() * Math.PI * 2d);
                double dist = random.NextDouble() * positionRange;
                double x = Math.Cos(dir) * dist;
                double y = Math.Sin(dir) * dist;

                Agent a = new Agent() { Defined = true, Direction = (float)dir, Position = new Vector2((float)x, (float)y) + center };
                result[i] = a;
            }

            return result;
        }

        public static float[] AgentArrayToFloatArray(Agent[] a)
        {
            float[] result = new float[Agent.SizeInFloats * a.Length];

            for(int i = 0; i < a.Length; i++)
            {
                int index = i * 4;
                float[] data = a[i].AsFloats();
                Array.Copy(data, 0, result, index, data.Length);
            }

            return result;
        }
    }
}
