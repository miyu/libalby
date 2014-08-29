using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shade.Server.Level;

namespace Shade.Server.World
{
   public interface WorldLoginResult
   {
      string SessionToken { get; }
   }
}