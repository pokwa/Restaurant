using IDataInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant
{
    public class FurnitureAPI
    {
        private ITableManager tableManager;
        private IChairManager chairManager;

        public FurnitureAPI(ITableManager tableManager, IChairManager chairManager)
        {
            this.tableManager = tableManager;
            this.chairManager = chairManager;
        }
        public bool AddTable(int tableNumber)
        {
            var existingTable = tableManager.GetTableByTableNumber(tableNumber);
            if (existingTable != null)
                return false;
            tableManager.AddTable(tableNumber);
            return true;
        }

        public MoveChairErrorCodes MoveChair(int chairNumber, int tableNumber)
        {
            var newTable = tableManager.GetTableByTableNumber(tableNumber);
            if (newTable == null)
                return MoveChairErrorCodes.NoSuchTable;

            var chair = chairManager.GetChairByChairNumber(chairNumber);
            if (chair == null)
                return MoveChairErrorCodes.NoSuchChair;
            if (chair.Table.TableNumber == tableNumber)
                return MoveChairErrorCodes.ChairAlreadyAtThatTable;

            chairManager.MoveChair(chair.ChairID, newTable.TableID);

            return MoveChairErrorCodes.Ok;
        }
    }
}
