using System;
using System.Collections.Generic;
using System.Text;

namespace PhysarumCore
{
    public struct FadeSettings
    {
        public float FadeRate;
        public float DiffusionRate;
        public float Padding0;
        public float Padding1;

        public static FadeSettings Default() { return new FadeSettings() { FadeRate = .01f, DiffusionRate = .7f/*10f*/ }; }
    }
}
