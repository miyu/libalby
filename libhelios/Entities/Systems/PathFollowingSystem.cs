using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItzWarty.Geometry;
using Shade.Entities;
using Shade.Helios.Pathfinding;
using SharpDX;
using SharpDX.Toolkit;

namespace Shade.Helios.Entities.Systems
{
   public class PathFollowingSystem : EntitySystem
   {
      private readonly ISet<EntityContext> trackedEntityContexts = new HashSet<EntityContext>();

      protected override void OnEntityHostSet()
      {
         base.OnEntityHostSet();

         this.EntityHost.EntityAdded += EntityHostOnEntityAdded;
      }

      private void EntityHostOnEntityAdded(IEntityHost sender, EntityAddedEventArgs e)
      {
         var entity = e.Entity;
         var positionComponent = entity.GetComponentOrNull<IPositionComponent>(ComponentType.Position);
         var pathingComponent = entity.GetComponentOrNull<IPathingComponent>(ComponentType.Pathing);
         var speedComponent = entity.GetComponentOrNull<ISpeedComponent>(ComponentType.Speed);

         if (positionComponent != null && pathingComponent != null && speedComponent != null) {
            trackedEntityContexts.Add(new EntityContext(entity, pathingComponent, positionComponent, speedComponent));
            Console.WriteLine("Tracking entity " + entity);
         }
      }

      public override void Update(GameTime gameTime)
      {
         base.Update(gameTime);

         foreach (var entityContext in trackedEntityContexts) {
            var position = entityContext.PositionComponent.Position;
            var speed = entityContext.SpeedComponent.Speed;
            var route = entityContext.PathingComponent.Route;

            if (route == null) 
               return;

            // deque pls
            var path = new List<Vector3>(route.Path);
            var distanceRemainingSquared = Math.Pow(gameTime.ElapsedGameTime.TotalSeconds * speed, 2);
            bool done = false;
            while (distanceRemainingSquared > 0 && !done) {
               var nextPoint = path.First();
               var currentPointToNextPoint = nextPoint - position;
               var distanceToNextPointSquared = currentPointToNextPoint.LengthSquared();
               if (distanceRemainingSquared >= distanceToNextPointSquared) {
                  position = nextPoint;
                  distanceRemainingSquared -= distanceToNextPointSquared;
                  path.RemoveAt(0);
                  if (!path.Any()) {
                     done = true;
                  }
               } else {
                  var progress = Math.Sqrt(distanceRemainingSquared) / Math.Sqrt(distanceToNextPointSquared);
                  position += currentPointToNextPoint * (float)progress;
                  distanceRemainingSquared = 0;
                  done = true;
               }
            }
            
            entityContext.PositionComponent.Position = position;
            if (path.Count == 0) {
               entityContext.PathingComponent.Route = null;
            } else {
               entityContext.PathingComponent.Route = new PathingRoute(position, route.EndPoint, path);
            }
         }
      }

      private class EntityContext
      {
         private readonly Entity entity;
         private readonly IPathingComponent pathingComponent;
         private readonly IPositionComponent positionComponent;
         private readonly ISpeedComponent speedComponent;

         public EntityContext(Entity entity, IPathingComponent pathingComponent, IPositionComponent positionComponent, ISpeedComponent speedComponent)
         {
            this.entity = entity;
            this.pathingComponent = pathingComponent;
            this.positionComponent = positionComponent;
            this.speedComponent = speedComponent;
         }

         public Entity Entity { get { return entity; } }
         public IPathingComponent PathingComponent { get { return pathingComponent; } }
         public IPositionComponent PositionComponent { get { return positionComponent; } }
         public ISpeedComponent SpeedComponent { get { return speedComponent; } }
      }
   }
}
