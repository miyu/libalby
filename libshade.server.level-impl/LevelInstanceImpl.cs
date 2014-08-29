using System;
using Shade.Server.Nierians;

namespace Shade.Server.Level
{
   public class LevelInstanceImpl : LevelInstance
   {
      public LevelInstanceImpl(ShadeServiceLocator shadeServiceLocator)
      {
      }

//      public void Enter(NierianKey nierianKey)
//      {
//         throw new NotImplementedException();
//      }
//
//      public void Leave(NierianKey nierianKey)
//      {
//         throw new NotImplementedException();
//      }

      public bool IsTransient { get; private set; }
   }
}