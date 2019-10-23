using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant
{
    public enum MoveChairErrorCodes
    {
        Ok,
        NoSuchChair,
        ChairAlreadyAtThatTable,
        NoSuchTable
    }
}
