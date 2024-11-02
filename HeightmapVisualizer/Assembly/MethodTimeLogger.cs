
using System.Diagnostics;
using System.Reflection;

namespace HeightmapVisualizer.Assembly
{
    public static class MethodTimeLogger
    {
        private const int sampleSize = 10;
        private static Dictionary<string, Queue<double>> averageTimes = new();
        public static void Log(MethodBase method, TimeSpan timeSpan)
        {
            string key = method.DeclaringType + method.Name;
            double value = timeSpan.TotalMilliseconds;

            UpdateAverages(value, key);
            Trace.WriteLine("Method: " + method.Name + "\tAvg: " + GetAverage(key).ToString("0.####") + "\tTime: " + value);
        }

        private static void UpdateAverages(double time, string key)
        {
            // Already in dictionary
            if (averageTimes.TryGetValue(key, out var value))
            {
                AddValue(value, time);
            }
            else
            {
                if (!averageTimes.ContainsKey(key))
                {
                    averageTimes[key] = new Queue<double>();
                }
            }
        }

        public static void AddValue(Queue<double> queue, double newValue)
        {
            // If the queue is at maxSize, remove the oldest value
            if (queue.Count == sampleSize)
            {
                queue.Dequeue();
            }

            // Add the new value to the queue
            queue.Enqueue(newValue);
        }

        public static double GetAverage(string key)
        {
            // Check if the key exists in the dictionary
            if (averageTimes.ContainsKey(key) && averageTimes[key].Count > 0)
            {
                // Calculate the average of values in the queue
                return averageTimes[key].Average();
            }

            // Return 0 if no values are present for the specified key
            return 0;
        }
    }
}
