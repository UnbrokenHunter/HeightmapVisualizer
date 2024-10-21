using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeightmapVisualizer.Scene;

namespace HeightmapVisualizer.Components
{
    public interface IComponent
    {
        public void Init(Gameobject gameobject);

        public void Update();
    }
}
