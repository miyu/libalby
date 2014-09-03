using System.Collections.Generic;
using Shade.Entities;
using Shade.Helios.Entities;

namespace Shade.Helios.State
{
   public interface IScene
   {
      void AddEntity(Entity entity);
      IEnumerable<Entity> EnumerateEntities();
      void SetCamera(Entity entity);
      Entity GetCamera();
   }
}
