using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Shade.Entities;
using Shade.Helios.Annotations;
using SharpDX;
using Component = Shade.Entities.Component;

namespace Shade.Helios.Entities
{
   public class PositionComponent : Component, IPositionComponent
   {
      private Vector3 position;

      public PositionComponent(Vector3 position)
         : base(ComponentType.Position)
      {
         this.Position = position;
      }

      public Vector3 Position { get { return position; } set { position = value; OnPropertyChanged(); } }

      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
      }
   }

   public interface IPositionComponent : INotifyPropertyChanged
   {
      Vector3 Position { get; set; }
   }
}
