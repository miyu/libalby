using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Entities;
using Shade.Helios.Pathfinding;
using SharpDX;
using Component = Shade.Entities.Component;

namespace Shade.Helios.Entities
{
   public class PathingComponent : Component, IPathingComponent
   {
      private PathingRoute route;

      public PathingComponent(PathingRoute route = null)
         : base(ComponentType.Pathing)
      {
         this.route = route;
      }

      public PathingRoute Route { get { return route; } set { route = value; } }
   }

   public interface IPathingComponent
   {
      PathingRoute Route { get; set; }
   }
}
