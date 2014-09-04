using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Entities;
using Shade.Helios.Entities;
using Shade.Helios.Pathfinding;
using SharpDX.Toolkit;

namespace Shade.Helios.State
{
   public class NullScene : IScene
   {
      public void AddEntity(Entity entity) { throw new InvalidOperationException("Can't add entities to null scene!"); }

      public IEnumerable<Entity> EnumerateEntities() { yield break; }
      public Entity GetCamera() { return null; }
      public void SetCamera(Entity entity) { }
      public NavMesh GetNavigationMesh() { return null; }
      public void SetNavigationMesh(NavMesh navigationMesh) { }
      public void Update(GameTime gameTime) { }
   }
}
