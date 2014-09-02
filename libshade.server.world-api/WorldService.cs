using Shade.Server.Nierians;

namespace Shade.Server.World
{
   public interface WorldService
   {
      WorldLoginResult Enter(string shardId, ulong accountId, ulong nierianId);
      void Leave(string shardId, ulong accountId, ulong nierianId);
   }
}
