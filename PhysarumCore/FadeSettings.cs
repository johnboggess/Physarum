using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;

namespace PhysarumCore
{
    public struct FadeSettings
    {
        public float FadeRate;
        public bool AdditiveFade;
        public float DiffusionRate;
        public float Padding0;
        public Vector2i Kernel;
        public float Padding1;
        public float Padding2;

        public static FadeSettings Default() { return new FadeSettings() { FadeRate = .01f, AdditiveFade = true, DiffusionRate = .7f/*10f*/, Kernel = new Vector2i(3, 3) }; }
    }
}
