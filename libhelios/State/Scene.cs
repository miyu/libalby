using System.Collections.Generic;
using Shade.Entities;
using Shade.Helios.Entities;
using Shade.Helios.Entities.Systems;

namespace Shade.Helios.State
{
   public class Scene : EntityHost, IScene
   {
      public Scene()
      {
         AddSystem(new PositionOrientationScaleToTransformSystem());
      }
   }
}