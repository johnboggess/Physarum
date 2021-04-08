using System;

using PhysarumCore;
namespace PhysarumConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TKWindow window = new TKWindow();
            window.AgentSettings = AgentSettings.Default();
            window.FadeSettings = FadeSettings.Default();
            window.Run();
            Console.Read();
        }
    }
}
