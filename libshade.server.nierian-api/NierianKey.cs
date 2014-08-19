using System.Security.Cryptography.X509Certificates;
using Shade.Server.Accounts;

namespace Shade.Server.Nierians
{
   public interface NierianKey
   {
      string ShardId { get; }
      uint AccountId { get; }
      uint NierianId { get; }
   }
}