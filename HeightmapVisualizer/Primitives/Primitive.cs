using HeightmapVisualizer.Scene;

namespace HeightmapVisualizer.Primitives
{
    /// <summary>
    /// Represents the base class for all geometric primitives in a mesh.
    /// Each primitive is associated with a mesh and can be drawn.
    /// </summary>
    public abstract class Primitive
    {
        /// <summary>
        /// The mesh to which this primitive belongs.
        /// </summary>
        protected readonly Mesh mesh;

        /// <summary>
        /// The color to draw this Primitive with with
        /// </summary>
        protected Color color;

        /// <summary>
        /// Initializes a new instance of the <see cref="Primitive"/> class with the specified mesh.
        /// </summary>
        /// <param name="mesh">The mesh to which this primitive belongs.</param>
        internal Primitive(Mesh mesh, Color color)
        {
            this.mesh = mesh;
            this.color = color;
        }

        /// <summary>
        /// Draws the primitive using the provided graphics context and camera.
        /// </summary>
        /// <param name="g">The graphics context used for drawing.</param>
        /// <param name="cam">The camera used for projecting the primitive into view space.</param>
        public abstract void Draw(Graphics g, Camera cam);

        /// <summary>
        /// Returns the drawing color of the primitive
        /// </summary>
        /// <returns>The drawing color of the primitive</returns>
        internal Color GetColor()
        {
            return color;
        }

        /// <summary>
        /// Changes the color of this Primitive to the newly selected one
        /// </summary>
        /// <param name="color">The new color</param>
        /// <returns></returns>
        public Primitive SetColor(Color color)
        {
            this.color = color;
            return this;
        }
    }
}
