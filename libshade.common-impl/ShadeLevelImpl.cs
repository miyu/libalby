using System.Collections.Generic;
using Shade.Entities;

namespace Shade
{
   public class ShadeLevelImpl : ShadeLevel
   {
      public ICollection<Entity> Entities { get; private set; }
   }
}
