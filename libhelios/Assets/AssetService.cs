using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ItzWarty;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;

namespace Shade.Helios.Assets
{
   public sealed class AssetService : GameSystem, IAssetService
   {
      private readonly Dictionary<AssetHandle, object> assetsByHandle = new Dictionary<AssetHandle, object>(); 
      private uint assetHandleCounter = 0;

      public AssetService(Game game)
         : base(game)
      {
         game.Services.AddService(typeof(IAssetService), this);
      }

      public AssetHandle AddAsset(object asset)
      {
         var handle = GetNextAssetHandle();
         assetsByHandle.Add(handle, asset);
         Console.WriteLine("HANDLE " + handle + " is " + asset);
         return handle;
      }

      public TAsset GetAsset<TAsset>(AssetHandle handle) { return (TAsset)assetsByHandle[handle]; }

      private AssetHandle GetNextAssetHandle() { return new AssetHandle(assetHandleCounter++); }
   }
}
