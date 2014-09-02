using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Entities;
using Shade.Helios.Entities;

namespace Shade.Helios.State
{
   public class NullScene : IScene
   {
      public void AddEntity(Entity entity) { throw new InvalidOperationException("Can't add entities to null scene!"); }

      public IEnumerable<Entity> EnumerateEntities() { yield break; }
   }
}
