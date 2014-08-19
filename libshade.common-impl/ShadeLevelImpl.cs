using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Helios.Entities;

namespace Shade
{
   public class ShadeLevelImpl : ShadeLevel
   {
      public ICollection<Entity> Entities { get; private set; }
   }
}
