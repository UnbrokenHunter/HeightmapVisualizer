
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace HeightmapVisualizer.src
{
    public class Gameloop
    {
        public static Gameloop? Instance;

        public readonly double TargetFPS;
        public double FPS {  get; private set; }
        public double DeltaTime { get; private set; }


        private const double fpsSamplePeriod = 1.0;

        private readonly Timer timer;
        private readonly Stopwatch stopwatch;
        private int frameCount = 0;
        private double fpsTimer = 0.0;
        private double previousTime;

        private readonly Action Update;
        private readonly Action Render;

        public Gameloop(Action update, Action render, double targetFPS = 60.0) 
        {
            if (Instance != null)
                throw new Exception("Two Gameloops have been created");
            else
                Instance = this;

            this.Update = update;
            this.Render = render;

            this.TargetFPS = targetFPS;


            timer = new Timer();
            timer.Interval = (int)(1000 * (1 / TargetFPS)); // ~60 FPS
            timer.Tick += Loop;

            stopwatch = new Stopwatch();
        }

        public void Start()
        {
            stopwatch.Start();
            previousTime = stopwatch.Elapsed.TotalSeconds;

            // Starts the looping
            timer.Start();
        }

        private void Loop(object? sender, EventArgs e)
        {
            // Calculate delta time
            double currentTime = stopwatch.Elapsed.TotalSeconds;
            DeltaTime = currentTime - previousTime;
            previousTime = currentTime;

            // Update game logic
            Update();

            // Calculate FPS over a second
            fpsTimer += DeltaTime;
            frameCount++;
            if (fpsTimer >= fpsSamplePeriod)
            {
                FPS = frameCount / fpsTimer;
                Console.WriteLine($"Actual Displayed FPS: {FPS:F2}");

                // Reset counters for the next second
                fpsTimer -= fpsSamplePeriod;
                frameCount = 0;
            }

            Render();
        }
    }
}
