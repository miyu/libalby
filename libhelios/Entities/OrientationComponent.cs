using System.ComponentModel;
using System.Runtime.CompilerServices;
using Shade.Entities;
using Shade.Helios.Annotations;
using SharpDX;
using Component = Shade.Entities.Component;

namespace Shade.Helios.Entities
{
   public class OrientationComponent : Component, IOrientationComponent
   {
      private Quaternion quaternion;

      public OrientationComponent(Quaternion orientation)
         : base(ComponentType.Orientation) { this.Orientation = orientation; }

      public Quaternion Orientation { get { return quaternion; } set { quaternion = value; OnPropertyChanged(); } }
      
      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
      }
   }

   public interface IOrientationComponent : INotifyPropertyChanged
   {
      Quaternion Orientation { get; set; }
   }
}