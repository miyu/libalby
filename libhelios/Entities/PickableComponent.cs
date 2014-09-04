using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Entities;
using SharpDX;
using Component = Shade.Entities.Component;

namespace Shade.Helios.Entities
{
   public class PickableComponent : Component
   {
      private readonly OrientedBoundingBox hbb;

      public PickableComponent(OrientedBoundingBox hbb)
         : base(ComponentType.Pickable) {
         this.hbb = hbb;
      }

      public OrientedBoundingBox BoundingBox
      {
         get
         {
            return hbb;
         }
      }

      public bool IsTargetable { get; private set; }

      public bool IsHovered { get; private set; }
   }
}
