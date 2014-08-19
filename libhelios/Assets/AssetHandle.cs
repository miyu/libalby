namespace Shade.Helios.Assets
{
   public struct AssetHandle
   {
      private uint value;

      public AssetHandle(uint value) { this.value = value; }

      public override bool Equals(object obj)
      {
         if (obj is AssetHandle)
            return ((AssetHandle)obj).value == this.value;
         else 
            return false;
      }

      public override int GetHashCode() { return value.GetHashCode(); }
   }
}