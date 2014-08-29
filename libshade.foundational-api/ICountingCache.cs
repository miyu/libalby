using Dargon.Distributed;

namespace Shade.Server.SpecializedCache
{
   public interface ICountingCache : ICache
   {
      ulong Next();
   }
}