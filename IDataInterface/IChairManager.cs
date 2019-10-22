using System;
using System.Collections.Generic;
using System.Text;

namespace IDataInterface
{
    public interface IChairManager
    {
        void MoveChair(int chairID, int tableID);
        Chair GetChairByChairNumber(int chairNumber);
    }
}
