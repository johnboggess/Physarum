using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;

namespace PhysarumCore
{
    public struct FadeSettings
    {
        public float FadeRate;
        public float DiffusionRate;
        public Vector2i Kernel;

        public static FadeSettings Default() { return new FadeSettings() { FadeRate = .01f, DiffusionRate = .7f/*10f*/, Kernel = new Vector2i(3, 3) }; }
    }
}
