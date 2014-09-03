using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Entities;
using SharpDX;
using Component = Shade.Entities.Component;

namespace Shade.Helios.Entities.Systems
{
   public class PositionOrientationScaleToTransformSystem : EntitySystem
   {
      protected override void OnEntityHostSet()
      {
         base.OnEntityHostSet();
         EntityHost.EntityAdded += EntityHostOnEntityAdded;
      }

      private void EntityHostOnEntityAdded(IEntityHost sender, EntityAddedEventArgs e)
      {
         var entity = e.Entity;
         var positionComponent = (IPositionComponent)entity.GetComponentOrNull(ComponentType.Position);
         var scaleComponent = (IScaleComponent)entity.GetComponentOrNull(ComponentType.Scale);
         var orientationComponent = (IOrientationComponent)entity.GetComponentOrNull(ComponentType.Orientation);
         var transformComponent = (ITransformComponent)entity.GetComponentOrNull(ComponentType.Transform);

         if (transformComponent == null && (positionComponent != null || scaleComponent != null || orientationComponent != null)) {
            entity.AddComponent(new LazyTransformComponent(positionComponent, scaleComponent, orientationComponent));
         }
      }

      private class LazyTransformComponent : Component, ITransformComponent
      {
         private readonly IPositionComponent positionComponent;
         private readonly IScaleComponent scaleComponent;
         private readonly IOrientationComponent orientationComponent;

         private bool isDirty = true;
         private Matrix result = Matrix.Identity;

         public LazyTransformComponent(IPositionComponent positionComponent, IScaleComponent scaleComponent, IOrientationComponent orientationComponent) : base(ComponentType.Transform)
         {
            this.positionComponent = positionComponent;
            this.scaleComponent = scaleComponent;
            this.orientationComponent = orientationComponent;

            if (positionComponent != null) {
               positionComponent.PropertyChanged += HandlePropertyChangedMarkDirty;
            }

            if (scaleComponent != null) {
               scaleComponent.PropertyChanged += HandlePropertyChangedMarkDirty;
            }

            if (orientationComponent != null) {
               orientationComponent.PropertyChanged += HandlePropertyChangedMarkDirty;
            }
         }

         private void HandlePropertyChangedMarkDirty(object sender, PropertyChangedEventArgs propertyChangedEventArgs) { isDirty = true; }

         private Matrix GetWorldTransform()
         {
            if (isDirty) {
               result = Matrix.Identity;
               if (orientationComponent != null) {
                  result = Matrix.RotationQuaternion(orientationComponent.Orientation);
               }
               if (scaleComponent != null) {
                  result = result * Matrix.Scaling(scaleComponent.Scale);
               }
               if (positionComponent != null) {
                  result = result * Matrix.Translation(positionComponent.Position);
               }
            }
            return result;
         }

         private void SetWorldTransform(Matrix matrix)
         {
            Vector3 position;
            Vector3 scale;
            Quaternion orientation;
            if (matrix.Decompose(out scale, out orientation, out position)) {
               if (positionComponent != null) {
                  positionComponent.Position = position;
               }
               if (scaleComponent != null) {
                  scaleComponent.Scale = scale;
               }
               if (orientationComponent != null) {
                  orientationComponent.Orientation = orientation;
               }
               result = matrix;
               isDirty = false;
            }
         }

         public Matrix WorldTransform { get { return GetWorldTransform(); } set { SetWorldTransform(value); } }
      }
   }
}
