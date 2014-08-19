using Shade.Helios.Assets;

namespace Shade.Helios
{
   public interface ITextureLoaderService
   {
      AssetHandle LoadTexture(string path);
   }
}