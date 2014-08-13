namespace Alby.Gui
{
   partial class MainForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.gridContainer = new System.Windows.Forms.Panel();
         this.SuspendLayout();
         // 
         // gridContainer
         // 
         this.gridContainer.AutoScroll = true;
         this.gridContainer.BackColor = System.Drawing.Color.Black;
         this.gridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.gridContainer.Location = new System.Drawing.Point(0, 0);
         this.gridContainer.Name = "gridContainer";
         this.gridContainer.Size = new System.Drawing.Size(1264, 761);
         this.gridContainer.TabIndex = 0;
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1264, 761);
         this.Controls.Add(this.gridContainer);
         this.Name = "MainForm";
         this.Text = "MainForm";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Panel gridContainer;
   }
}