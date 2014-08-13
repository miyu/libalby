using System;
using System.Configuration;

namespace Shade.Alby
{
   public class CellConnector
   {
      private readonly Cell first;
      private readonly Cell second;
      public ConnectorState connectorState;

      public CellConnector(Cell first, Cell second, ConnectorState connectorState = ConnectorState.Disconnected)
      {
         this.first = first;
         this.second = second;
         this.connectorState = connectorState;
      }

      public void Connect() { connectorState = ConnectorState.Connected; }
      public void Disconnect() { connectorState = ConnectorState.Disconnected; }
      public void Break() { connectorState = ConnectorState.Broken; }

      public ConnectorState State { get { return connectorState; } set { connectorState = value; } }
      
      public Cell Other(Cell cell)
      {
         if (cell == first) {
            return second;
         } else if (cell == second) {
            return first;
         } else {
            throw new InvalidOperationException();
         }
      }
   }

   public enum ConnectorState
   {
      Broken,
      Disconnected,
      Connected
   }
}