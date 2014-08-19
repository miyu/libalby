using System.Security.Cryptography.X509Certificates;
using Shade.Helios.Entities;
using System.Collections.Generic;

namespace Shade
{
   public interface ShadeLevel
   {
      ICollection<Entity> Entities { get; }
   }
}
