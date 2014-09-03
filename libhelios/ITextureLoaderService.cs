using Shade.Helios.Assets;

namespace Shade.Helios
{
   public interface ITextureLoaderService
   {
      AssetHandle LoadTexture(string path);

      AssetHandle WhiteTextureHandle { get; }
      AssetHandle RedTextureHandle { get; }
      AssetHandle LimeTextureHandle { get; }
   }
}