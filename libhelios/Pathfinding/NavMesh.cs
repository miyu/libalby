using ItzWarty;
using ItzWarty.Geometry;
using SharpDX;
using System;
using System.Collections.Generic;

namespace Shade.Helios.Pathfinding
{
   public class NavMesh
   {
      private readonly ISet<ConvexPolygonNode> nodes = new HashSet<ConvexPolygonNode>();

      public void AddNode(ConvexPolygonNode node) { nodes.Add(node); }

      public void Connect(ConvexPolygonNode a, ConvexPolygonNode b)
      {
         if (!nodes.Contains(a) || !nodes.Contains(b))
            throw new InvalidOperationException("Either A or B wasn't in the navmesh graph!");

         a.Peers.Add(b);
         b.Peers.Add(a);
      }

      public ConvexPolygonNode FindNode(Point3D point)
      {
         foreach (var node in nodes) {
            if (node.ContainsPoint(point)) {
               return node;
            }
         }
         return null;
      }
   }

   public class ConvexPolygonNode
   {
      private readonly List<ConvexPolygonNode> peers = new List<ConvexPolygonNode>();
      private readonly Point3D origin;
      private readonly Vector3D normal;
      private readonly Vector3D basis0;
      private readonly Vector3D basis1;
      private readonly IReadOnlyList<Point2D> pointsInBasis;
      private const double POSITIVE_EPSILON = 0.00001;
      private const double NEGATIVE_EPSILON = -0.00001;

      public ConvexPolygonNode(Point3D[] vertices)
      {
         var p0 = vertices[0];
         var p1 = vertices[1];
         var p2 = vertices[2];
         var p1p0 = p0 - p1;
         var p1p2 = p2 - p1;
         this.origin = p0;
         this.normal = p1p0.Cross(p1p2);

         var matrix = new Matrix3x3((float)p1p0.X, (float)p1p0.Y, (float)p1p0.Z, (float)p1p2.X, (float)p1p2.Y, (float)p1p2.Z, 0, 0, 0);
         matrix.Orthonormalize();

         this.basis0 = new Vector3D(matrix.M11, matrix.M12, matrix.M13);
         this.basis1 = new Vector3D(matrix.M21, matrix.M22, matrix.M23);

         this.pointsInBasis = Util.Generate(vertices.Length, i => WorldToPlanar(vertices[i]));
      }

      public List<ConvexPolygonNode> Peers { get { return peers; } }

      public Point3D Raycast(Ray3D ray)
      {
         var pointOnPlane = ProjectRayToSurface(ray);
         if (pointOnPlane == null) {
            return null;
         } else {
            if (ContainsPoint(pointOnPlane)) {
               return pointOnPlane;
            } else {
               return null;
            }
         }
      }

      public bool ContainsPoint(Point3D pointInWorldSpace) { return ContainsPoint(WorldToPlanar(pointInWorldSpace)); }

      private bool ContainsPoint(Point2D pointInPlaneSpace)
      {
         var clockness = GeometryUtilities.GetClockness(pointsInBasis[0], pointsInBasis[1], pointInPlaneSpace);
         for (var i = 1; i < pointsInBasis.Count; i++) {
            var p0 = pointsInBasis[i];
            var p1 = pointsInBasis[(i + 1) % pointsInBasis.Count];
            if (clockness != GeometryUtilities.GetClockness(p0, p1, pointInPlaneSpace)) {
               return false;
            }
         }
         return true;
      }

      public Point3D ProjectRayToSurface(Ray3D ray)
      {
         var denominator = ray.Direction.Dot(normal);
         if (Util.IsBetween(NEGATIVE_EPSILON, denominator, POSITIVE_EPSILON)) {
            return null;
         } else {
            var numerator = (origin.ToVector3D() - ray.Position.ToVector3D()).Dot(normal);
            return ray.Position + ray.Direction * (numerator / denominator);
         }
      }

      private Point2D WorldToPlanar(Point3D vertex)
      {
         var popv = vertex - origin;
         if (popv.IsZeroVector()) {
            return new Point2D(0, 0);
         } else {
            var a = popv.ScalarProjectionOnto(basis0);
            var b = popv.ScalarProjectionOnto(basis1);
            return new Point2D(a, b);
         }
      }

      public Point3D PlanarToWorld(Point2D vertex)
      {
         var result = origin + basis0 * vertex.X + basis1 * vertex.Y;
         return new Point3D(result.X, result.Y, result.Z);
      }
   }
}
