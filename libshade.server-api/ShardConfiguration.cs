namespace Shade.Server
{
   public class ShardConfiguration
   {
      private string shardId;

      public ShardConfiguration(string shardId) { this.shardId = shardId; }

      public string ShardId { get { return shardId; } }
   }
}