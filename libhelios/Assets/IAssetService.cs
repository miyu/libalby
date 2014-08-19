namespace Shade.Helios.Assets
{
   public interface IAssetService
   {
      AssetHandle AddAsset(object asset);
      TAsset GetAsset<TAsset>(AssetHandle handle);
   }
}
