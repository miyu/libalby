using System.Collections.Generic;
using Shade.Entities;

namespace Shade
{
   public interface ShadeLevel
   {
      ICollection<Entity> Entities { get; }
   }
}
