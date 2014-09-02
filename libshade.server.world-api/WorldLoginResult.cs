using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.PortableObjects;
using Shade.Server.Level;

namespace Shade.Server.World
{
   public class WorldLoginResult : IPortableObject
   {
      private string sessionToken;
      private WorldLocationV1 worldLocation;

      public WorldLoginResult(string sessionToken, WorldLocationV1 worldLocation)
      {
         this.sessionToken = sessionToken;
         this.worldLocation = worldLocation;
      }

      public string SessionToken { get { return sessionToken; } }
      public WorldLocationV1 WorldLocation { get { return worldLocation; } }

      public void Serialize(IPofWriter writer)
      {
         writer.WriteString(0, sessionToken);
         writer.WriteObject(1, worldLocation);
      }

      public void Deserialize(IPofReader reader)
      {
         sessionToken = reader.ReadString(0);
         worldLocation = reader.ReadObject<WorldLocationV1>(1);
      }
   }
}