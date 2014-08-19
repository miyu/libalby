using System;
using System.Runtime.CompilerServices;
using libshade.server;
using Shade.Server.Dungeon;

namespace Shade.Server.World
{
   public class LevelServiceImpl : LevelService
   {
      public LevelServiceImpl(ShadeServiceLocator shadeServiceLocator)
      {
         shadeServiceLocator.RegisterService(typeof(LevelService), this);
      }
   }
}