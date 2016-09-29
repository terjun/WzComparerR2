using System;
using System.Collections.Generic;
using System.Text;

namespace WzComparerR2.CharaSim
{
    public class ExclusiveEquipIDList
    {
        public ExclusiveEquipIDList()
        {
            this.Items = new List<int>();
        }

        public List<int> Items { get; private set; }

        public void Add(int itemID)
        {
            this.Items.Add(itemID);
        }

        public void Remove(int itemID)
        {
            this.Items.RemoveAll(p => p == itemID);
        }

        public bool this[int itemID]
        {
            get
            {
                return this.Items.Exists(p => p == itemID);
            }
        }
    }
}
