using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Entities;
using SharpDX;
using Component = Shade.Entities.Component;

namespace Shade.Helios.Entities
{
   public interface ICameraComponent
   {
      Matrix View { get; }
      Matrix Projection { get; }
   }

   public class PositionedOrientedCameraComponent : Component, ICameraComponent
   {
      private IPositionComponent positionComponent;
      private IOrientationComponent orientationComponent;

      // view
      //private Vector3 destinationOffset = new Vector3(0, -10f, -4f);
      private Vector3 up = new Vector3(0, 1, 0);
      private Matrix viewMatrix;
      private bool viewMatrixDirty = true;

      // projection
      private float zNear = 0.1f;
      private float zFar = 200.0f;
      private float fieldOfViewRadians = 60.0f * (float)Math.PI / 180.0f;
      private float aspectRatio = 16.0f / 9.0f;
      private Matrix projectionMatrix;
      private bool projectionMatrixDirty = true;
      
      public PositionedOrientedCameraComponent() : base(ComponentType.Camera) {
      }

      protected override void OnEntityAttached()
      {
         base.OnEntityAttached();

         positionComponent = (IPositionComponent)Entity.GetComponentOrNull(ComponentType.Position);
         orientationComponent = (IOrientationComponent)Entity.GetComponentOrNull(ComponentType.Orientation);

         positionComponent.PropertyChanged += HandleSetViewMatrixDirty;
         orientationComponent.PropertyChanged += HandleSetViewMatrixDirty;
      }

      private void HandleSetViewMatrixDirty(object sender, PropertyChangedEventArgs propertyChangedEventArgs) { viewMatrixDirty = true; }

      private Matrix GetViewMatrix()
      {
         if (viewMatrixDirty) {
            var position = positionComponent.Position;
            var offset = Vector3.Transform(Vector3.ForwardRH, orientationComponent.Orientation);
            var lookat = new Vector3(position.X + offset.X, position.Y + offset.Y, position.Z + offset.Z);
            viewMatrix = Matrix.LookAtRH(position, lookat, up);
            viewMatrixDirty = false;
         }
         return viewMatrix;
      }

      private Matrix GetProjectionMatrix()
      {
         if (projectionMatrixDirty) {
            projectionMatrix = Matrix.PerspectiveFovRH(fieldOfViewRadians, aspectRatio, 0.1f, 200.0f);
            projectionMatrixDirty = false;
         }
         return projectionMatrix;
      }

      public Matrix View { get { return GetViewMatrix(); } }
      public Matrix Projection { get { return GetProjectionMatrix(); } }
   }
}
