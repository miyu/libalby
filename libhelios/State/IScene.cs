using System.Collections.Generic;
using Shade.Entities;
using Shade.Helios.Entities;
using Shade.Helios.Pathfinding;
using SharpDX.Toolkit;

namespace Shade.Helios.State
{
   public interface IScene
   {
      void AddEntity(Entity entity);
      IEnumerable<Entity> EnumerateEntities();
      Entity GetCamera();
      void SetCamera(Entity entity);
      NavMesh GetNavigationMesh();
      void SetNavigationMesh(NavMesh navigationMesh);
      void Update(GameTime gameTime);
   }
}
