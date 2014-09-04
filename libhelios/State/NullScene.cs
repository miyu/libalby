using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Entities;
using Shade.Helios.Entities;
using SharpDX.Toolkit;

namespace Shade.Helios.State
{
   public class NullScene : IScene
   {
      public void AddEntity(Entity entity) { throw new InvalidOperationException("Can't add entities to null scene!"); }

      public IEnumerable<Entity> EnumerateEntities() { yield break; }
      public void SetCamera(Entity entity) { }
      public Entity GetCamera() { return null; }
      public void Update(GameTime gameTime) { }
   }
}
