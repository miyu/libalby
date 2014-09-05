using System.Collections;
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

      public Point3D Raycast(Ray3D ray)
      {
         foreach (var node in nodes) {
            var result = node.Raycast(ray);
            if (result != null) {
               return result;
            }
         }
         return null;
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

      public IEnumerable<ConvexPolygonNode> EnumerateNodes() { return nodes; } 
   }

   public class ConvexPolygonNode
   {
      private readonly List<ConvexPolygonNode> peers = new List<ConvexPolygonNode>();

      public List<ConvexPolygonNode> Peers { get { return peers; } }

   }
}
