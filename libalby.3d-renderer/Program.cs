using System;
using System.Net.Mime;
using System.Windows.Forms;

namespace Alby.Gui
{
   class Program
   {
      private static void Main(string[] args)
      {
         Application.EnableVisualStyles();
         using (var game = new ShadeGame())
            game.Run();
      }
   }
}
