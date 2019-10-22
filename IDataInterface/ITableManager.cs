using System;
using System.Collections.Generic;
using System.Text;

namespace IDataInterface
{
    public interface ITableManager
    {
        Table GetTableByTableNumber(int tableNumber);
        void AddTable(int tableNumber);
    }
}
