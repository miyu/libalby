using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shade.Alby;

namespace Alby.Gui
{
   public partial class MainForm : Form
   {
      private readonly GridDisplay gridDisplay;
      private SimpleRendererController controller;

      public MainForm()
      {
         InitializeComponent();

         this.gridDisplay = new GridDisplay();
         this.gridContainer.Controls.Add(this.gridDisplay);
         this.gridDisplay.Zoom = 3.0f;
      }

      public void SetController(SimpleRendererController controller) { this.controller = controller; }
      public void SetGrid(SquareGrid grid) { this.gridDisplay.SetGrid(grid); }
      public void SetDebugGraphicsContext(DebugGraphicsContext context) { this.gridDisplay.SetDebugGraphicsContext(context); }
   }
}
