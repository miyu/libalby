using System.Security.Cryptography.X509Certificates;

namespace Shade.Server.Dungeon
{
   public interface DungeonService
   {
      Dungeon CreateDebugDungeon();
   }
}
