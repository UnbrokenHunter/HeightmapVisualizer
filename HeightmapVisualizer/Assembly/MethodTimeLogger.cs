
using System.Diagnostics;
using System.Reflection;

namespace HeightmapVisualizer.Assembly
{
    public static class MethodTimeLogger
    {
        public static void Log(MethodBase method, TimeSpan timeSpan)
        {
            Trace.WriteLine("Method: " + method.Name + " Time: " + timeSpan.TotalMilliseconds);
        }
    }
}
