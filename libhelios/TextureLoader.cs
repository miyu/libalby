using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Helios.Assets;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;

namespace Shade.Helios
{
   public class TextureLoader : GameSystem, ITextureLoaderService
   {
      private readonly Dictionary<string, AssetHandle> loadedAssetsByPath = new Dictionary<string, AssetHandle>(); 
      private readonly IAssetService assetService;
      private AssetHandle whiteTextureHandle;
      private AssetHandle redTextureHandle;
      private AssetHandle limeTextureHandle;

      public TextureLoader(Game game, IAssetService assetService) : base(game)
      {
         this.assetService = assetService;

         game.Services.AddService(typeof(ITextureLoaderService), this);
         game.GameSystems.Add(this);
      }

      public AssetHandle WhiteTextureHandle { get { return whiteTextureHandle; } }
      public AssetHandle RedTextureHandle { get { return redTextureHandle; } }
      public AssetHandle LimeTextureHandle { get { return limeTextureHandle; } }

      protected override void LoadContent()
      {
         base.LoadContent();

         whiteTextureHandle = LoadTexture("SolidColors/white.jpg");
         redTextureHandle = LoadTexture("SolidColors/red.jpg");
         limeTextureHandle = LoadTexture("SolidColors/lime.jpg");
      }

      public AssetHandle LoadTexture(string path)
      {
         AssetHandle handle;
         if (!loadedAssetsByPath.TryGetValue(path, out handle)) {
            var texture = ToDisposeContent(Content.Load<Texture2D>(path));
            handle = assetService.AddAsset(texture);
            this.loadedAssetsByPath.Add(path, handle);
         }
         return handle;
      }
   }
}
