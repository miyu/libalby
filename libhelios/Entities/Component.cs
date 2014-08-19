namespace Shade.Helios.Entities
{
   public abstract class Component
   {
      private ComponentType componentType;

      protected Component(ComponentType componentType) { this.componentType = componentType; }

      public ComponentType ComponentType { get { return componentType; } }
   }
}