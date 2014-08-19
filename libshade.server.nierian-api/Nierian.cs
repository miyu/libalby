using System.Runtime.InteropServices;
using Shade.Server.Accounts;

namespace Shade.Server.Nierians
{
   public interface Nierian
   {
      NierianKey Key { get; }
      string Name { get; }
   }
}