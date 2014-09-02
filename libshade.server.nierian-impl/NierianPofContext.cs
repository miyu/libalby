using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.PortableObjects;
using Shade.Server.Nierians.Distributed;
using Shade.Server.Nierians.DTOs;

namespace Shade.Server.Nierians
{
   public class NierianPofContext : PofContext
   {
      public NierianPofContext()
      {
         // api
         RegisterPortableObjectType(30000, typeof(NierianIdV1));
         
         // impl
         RegisterPortableObjectType(31000, typeof(AccountIdFromNierianEntryProjector));
         RegisterPortableObjectType(31001, typeof(NierianEntry));
         RegisterPortableObjectType(31002, typeof(NierianKey));
      }
   }
}
