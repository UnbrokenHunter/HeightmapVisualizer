
namespace HeightmapVisualizer.Scene
{
    public class Scene
    {
        public Camera Camera { get; set; }
        public Gameobject[] Gameobjects { get; set; }

        public Scene(Camera camera, Gameobject[] gameobjects)
        {
            this.Camera = camera;
            this.Gameobjects = gameobjects;
        }

        public void Init()
        {
            foreach (var gameobject in Gameobjects)
            {
                gameobject.Init();
            }
        }

        public void Update(Graphics g)
        {
            UpdateGameobjects();

            RenderCamera(g);
        }

        private void UpdateGameobjects()
        {
            foreach (var gameobject in Gameobjects)
            {
                gameobject.Update();
            }
        }

        private void RenderCamera(Graphics g)
        {
            foreach (var gameobject in Gameobjects)
            {
                var renderable = gameobject.GetRenderable();
                if (renderable != null)
                {
                    renderable.Render(g, Camera);
                }
            }
        }
    }
}
