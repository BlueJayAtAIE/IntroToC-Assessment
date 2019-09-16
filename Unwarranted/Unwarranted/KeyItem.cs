using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UnwarrantedTools
{
    class KeyItem : Item
    {
        public int textStart;
        public int textDurration;
        public bool canPickup;

        public KeyItem(string itemName, int itemIDNumber, int flavorTextStart, int flavorTextDurration, bool pickup)
        {
            name = itemName;
            itemID = itemIDNumber;
            textStart = flavorTextStart;
            textDurration = flavorTextDurration;
            canPickup = pickup;
        }
    }
}
