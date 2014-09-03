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
   public class TransformComponent : Component
   {
      private Matrix worldTransform;

      public TransformComponent(Matrix? worldTransform = null)
         :base (ComponentType.Transform)
      {
         this.worldTransform = worldTransform ?? Matrix.Identity;
      }
      public Matrix WorldTransform { get { return worldTransform; } set { worldTransform = value; } }
   }
}
