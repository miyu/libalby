using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shade.Client
{
   public class GameControllerServiceImpl : GameControllerService
   {
      private IShadeClient client;
      private ClientLevelService clientLevelService;

      public GameControllerServiceImpl(IShadeClient client, ClientLevelService clientLevelService)
      {
         this.client = client;
         this.clientLevelService = clientLevelService;
         
         client.Services.AddService(typeof(GameControllerService), this);
      }
   }
}
