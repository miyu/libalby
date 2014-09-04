
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Entities;

namespace Shade.Helios.Entities
{
   public class SpeedComponent : Component, ISpeedComponent
   {
      private float speed;

      public SpeedComponent(float speed = 0.0f) : base(ComponentType.Speed) { this.speed = speed; }

      public float Speed { get { return this.speed; } set { this.speed = value; } }
   }

   public interface ISpeedComponent
   {
      float Speed { get; }
   }
}
