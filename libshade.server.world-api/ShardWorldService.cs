namespace Shade.Server.World
{
   public interface ShardWorldService
   {
      WorldLoginResult Enter(ulong accountId, ulong nierianId);
      void Leave(ulong accountId, ulong nierianId);
   }
}