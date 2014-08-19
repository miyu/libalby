using System.Collections.Generic;
using Shade.Helios.Entities;

namespace Shade.Helios.State
{
   public interface IScene
   {
      void AddEntity(Entity entity);
      IEnumerable<Entity> EnumerateEntities();  
   }
}
