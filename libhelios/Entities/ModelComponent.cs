using Shade.Entities;
using Shade.Helios.Assets;
using SharpDX;
using Component = Shade.Entities.Component;

namespace Shade.Helios.Entities
{
   public class ModelComponent : Component
   {
      private readonly AssetHandle diffuseTexture;
      private readonly AssetHandle mesh;

      public ModelComponent(AssetHandle diffuseTexture, AssetHandle mesh)
         : base(ComponentType.Model)
      {
         this.diffuseTexture = diffuseTexture;
         this.mesh = mesh;
      }

      public AssetHandle DiffuseTexture { get { return diffuseTexture; } }
      public AssetHandle Mesh { get { return mesh; } }
   }
}
