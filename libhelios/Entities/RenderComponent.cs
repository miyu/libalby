using Shade.Entities;
using Shade.Helios.Assets;
using SharpDX;
using Component = Shade.Entities.Component;

namespace Shade.Helios.Entities
{
   public class RenderComponent : Component
   {
      private readonly AssetHandle diffuseTexture;
      private readonly AssetHandle mesh;
      private readonly Matrix worldTransform;

      public RenderComponent(AssetHandle diffuseTexture, AssetHandle mesh, Matrix? modelTransform = null)
         : base(ComponentType.Renderable)
      {
         this.diffuseTexture = diffuseTexture;
         this.mesh = mesh;
         this.worldTransform = modelTransform ?? Matrix.Identity;
      }

      public AssetHandle DiffuseTexture { get { return diffuseTexture; } }
      public AssetHandle Mesh { get { return mesh; } }
      public Matrix WorldTransform { get { return worldTransform; } }
   }
}
