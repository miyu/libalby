using System.Collections.Generic;
using ItzWarty;
using SharpDX;

namespace Shade.Helios.Entities
{
   public class Entity
   {
      private Dictionary<ComponentType, Component> componentsByType = new Dictionary<ComponentType, Component>();

      public Entity()
      {
      }

      public void AddComponent(Component component) { this.componentsByType.Add(component.ComponentType, component); }

      public Component GetComponentOrNull(ComponentType componentType) { return this.componentsByType.GetValueOrDefault(componentType); }
   }
}
