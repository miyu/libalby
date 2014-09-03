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
   public interface IMeshLoaderService
   {
      AssetHandle UnitCubeMesh { get; }
      AssetHandle UnitPlaneXY { get; }
      AssetHandle TransformMesh(AssetHandle meshHandle, Matrix transformation);
   }

   public class MeshLoaderService : GameSystem, IMeshLoaderService
   {
      private readonly IAssetService assetService;
      private AssetHandle unitCubeMesh = AssetHandle.NullHandle;
      private AssetHandle unitPlaneXYMesh = AssetHandle.NullHandle;

      public MeshLoaderService(Game game, IAssetService assetService) : base(game)
      {
         this.assetService = assetService;
         
         game.Services.AddService(typeof(IMeshLoaderService), this);
         game.GameSystems.Add(this);
      }

      protected override void LoadContent()
      {
         base.LoadContent();

         var cube = ToDisposeContent(GeometricPrimitive.Cube.New(GraphicsDevice));
         var cubeBounds = new OrientedBoundingBox(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, 0.5f, 0.5f));
         var cubeMesh = new Mesh(cube.IsIndex32Bits, cube.IndexBuffer, cube.VertexBuffer, cubeBounds);
         this.unitCubeMesh = assetService.AddAsset(cubeMesh);

         var plane = ToDisposeContent(GeometricPrimitive.Plane.New(GraphicsDevice, 1f, 1f));
         var planeBounds = new OrientedBoundingBox(new Vector3(-0.5f, -0.05f, -0.5f), new Vector3(0.5f, 0.05f, 0.5f));
         var planeXYMesh = new Mesh(plane.IsIndex32Bits, plane.IndexBuffer, plane.VertexBuffer, planeBounds, Matrix.RotationX(-MathUtil.PiOverTwo));
         this.unitPlaneXYMesh = assetService.AddAsset(planeXYMesh);
      }

      public AssetHandle TransformMesh(AssetHandle meshHandle, Matrix transformation)
      {
         var oldMesh = assetService.GetAsset<Mesh>(meshHandle);
         var newBoundingBox = oldMesh.BoundingBox;
         newBoundingBox.Transform(transformation);
         var newMesh = new Mesh(oldMesh.IsIndex32Bits, oldMesh.IndexBuffer, oldMesh.VertexBuffer, newBoundingBox, oldMesh.ModelTransform * transformation);
         return assetService.AddAsset(newMesh);
      }

      public AssetHandle UnitCubeMesh { get { return unitCubeMesh; } }
      public AssetHandle UnitPlaneXY { get { return unitPlaneXYMesh; } }
   }
}
