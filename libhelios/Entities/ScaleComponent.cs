using System.ComponentModel;
using System.Runtime.CompilerServices;
using Shade.Entities;
using Shade.Helios.Annotations;
using SharpDX;
using Component = Shade.Entities.Component;

namespace Shade.Helios.Entities
{
   public class ScaleComponent : Component, IScaleComponent
   {
      private Vector3 scale;

      public ScaleComponent(Vector3 scale)
         : base(ComponentType.Scale)
      {
         this.Scale = scale;
      }

      public Vector3 Scale { get { return scale; } set { scale = value; OnPropertyChanged(); } }
      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
      }
   }

   public interface IScaleComponent : INotifyPropertyChanged
   {
      Vector3 Scale { get; set; }
   }
}