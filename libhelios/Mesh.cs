using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Toolkit.Graphics;

namespace Shade.Helios
{
   public class Mesh
   {
      private static readonly VertexInputLayout kInputLayout = VertexInputLayout.New<VertexPositionNormalTexture>(0);
      private readonly bool isIndex32Bits;
      private readonly Buffer indexBuffer;
      private readonly Buffer<VertexPositionNormalTexture> vertexBuffer;
      private readonly Matrix modelTransform;

      public Mesh(bool isIndex32Bits, Buffer indexBuffer, Buffer<VertexPositionNormalTexture> vertexBuffer, Matrix? modelTransform = null)
      {
         this.isIndex32Bits = isIndex32Bits;
         this.indexBuffer = indexBuffer;
         this.vertexBuffer = vertexBuffer;
         this.modelTransform = modelTransform ?? Matrix.Identity;
      }

      public VertexInputLayout InputLayout { get { return kInputLayout; } }
      public bool IsIndex32Bits { get { return isIndex32Bits; } }
      public Buffer IndexBuffer { get { return indexBuffer; } }
      public Buffer<VertexPositionNormalTexture> VertexBuffer { get { return vertexBuffer; } }
      public Matrix ModelTransform { get { return modelTransform; } }
   }
}
