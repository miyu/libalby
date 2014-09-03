using System.Collections.Generic;
using Shade.Entities;
using Shade.Helios.Entities;
using Shade.Helios.Entities.Systems;

namespace Shade.Helios.State
{
   public class Scene : EntityHost, IScene
   {
      private Entity cameraEntity = null;

      public Scene()
      {
         AddSystem(new PositionOrientationScaleToTransformSystem());
      }

      public void SetCamera(Entity cameraEntity) { this.cameraEntity = cameraEntity; }
      public Entity GetCamera() { return this.cameraEntity; }
   }
}