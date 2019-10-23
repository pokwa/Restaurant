using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant
{
    public class Tab
    {
        public enum Status
        {
            NoSuchTable,
            Settled,
            Open
        };

        public Status TabStatus;
        public decimal Amount;

    }
}
