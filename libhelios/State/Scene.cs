using System.Collections.Generic;
using Shade.Entities;
using Shade.Helios.Entities;

namespace Shade.Helios.State
{
   public class Scene : IScene
   {
      private readonly HashSet<Entity> entities = new HashSet<Entity>();

      public void AddEntity(Entity entity) { this.entities.Add(entity); }

      public IEnumerable<Entity> EnumerateEntities() { return entities; }
   }
}