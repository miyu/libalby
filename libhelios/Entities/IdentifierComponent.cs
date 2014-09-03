using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Entities;

namespace Shade.Helios.Entities
{
   public class IdentifierComponent : Component
   {
      private readonly string name;
      public IdentifierComponent(string name) : base(ComponentType.Identifier) { this.name = name; }
      public string Name { get { return name; } }
   }
}
