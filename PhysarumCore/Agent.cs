using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using OpenTK.Mathematics;
namespace PhysarumCore
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Agent
    {
        public Vector2 Position;
        public float Direction;
        public bool Defined;
        public float fs;
        public float ls;
        public float rs;
        public int ID;
        public float debug0;
        public float debug1;
        public float debug2;
        public int xint;

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
    }
}
