using System.Collections.Generic;
using Shade.Entities;
using Shade.Helios.Entities;
using Shade.Helios.Entities.Systems;
using Shade.Helios.Pathfinding;

namespace Shade.Helios.State
{
   public class Scene : EntityHost, IScene
   {
      private Entity cameraEntity = null;
      private NavMesh navigationMesh;

      public Scene()
      {
         AddSystem(new PositionOrientationScaleToTransformSystem());
         AddSystem(new PathFollowingSystem());
      }

      public void SetCamera(Entity cameraEntity) { this.cameraEntity = cameraEntity; }
      public Entity GetCamera() { return this.cameraEntity; }

      public void SetNavigationMesh(NavMesh navigationMesh) { this.navigationMesh = navigationMesh; }
      public NavMesh GetNavigationMesh() { return this.navigationMesh; }
   }
}