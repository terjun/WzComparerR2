using System;
using System.Collections.Generic;
using System.Text;

using WzComparerR2.WzLib;

namespace WzComparerR2.CharaSim
{
    public class ExclusiveEquip
    {
        public ExclusiveEquip()
        {
            itemIDs = new ExclusiveEquipIDList();
        }
        public ExclusiveEquipIDList itemIDs;
        public string msg;

        public static ExclusiveEquip CreateFromNode(Wz_Node exclusiveEquipNode)
        {
            if (exclusiveEquipNode == null)
                return null;

            ExclusiveEquip exclusiveEquip = new ExclusiveEquip();

            Dictionary<string, string> desc = new Dictionary<string, string>();

            foreach (Wz_Node subNode in exclusiveEquipNode.Nodes)
            {
                switch (subNode.Text)
                {
                    case "item":
                        foreach (Wz_Node itemNode in subNode.Nodes)
                        {
                            int idx = Convert.ToInt32(itemNode.Text);
                            int itemID = Convert.ToInt32(itemNode.Value);
                            exclusiveEquip.itemIDs.Add(itemID);
                        }
                        break;
                    case "msg":
                        exclusiveEquip.msg = Convert.ToString(subNode.Value);
                        break;
                }
            }

            return exclusiveEquip;
        }
    }
}
