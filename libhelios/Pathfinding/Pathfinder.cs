using System.Collections.Generic;
using ItzWarty.Geometry;
using SharpDX;

namespace Shade.Helios.Pathfinding
{
   public class Pathfinder
   {
      private readonly NavMesh navmesh;

      public Pathfinder(NavMesh navmesh) {
         this.navmesh = navmesh;
      }

      public PathingRoute FindPath(Vector3 start, Vector3 end)
      {
         var startNode = this.navmesh.FindNode(new Point3D(start.X, start.Y, start.Z));
         var endNode = this.navmesh.FindNode(new Point3D(end.X, end.Y, end.Z));
         if (startNode == null || endNode == null)
            return null;
     
         return new PathingRoute(start, end, new List<Vector3> { end });
      }
   }

   public class PathingRoute
   {
      private readonly Vector3 startPoint;
      private readonly Vector3 endPoint;
      private readonly IReadOnlyList<Vector3> path;

      public PathingRoute(Vector3 startPoint, Vector3 endPoint, IReadOnlyList<Vector3> path)
      {
         this.startPoint = startPoint;
         this.endPoint = endPoint;
         this.path = path;
      }

      public Vector3 StartPoint { get { return startPoint; } }
      public Vector3 EndPoint { get { return endPoint; } }
      public IReadOnlyList<Vector3> Path { get { return path; } }
   }
}
