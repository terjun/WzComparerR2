using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using WzComparerR2.WzLib;
using WzComparerR2.PluginBase;

namespace WzComparerR2.CharaSim
{
    public static class CharaSimLoader
    {
        static CharaSimLoader()
        {
            loadedSetItems = new Dictionary<int, SetItem>();
            loadedExclusiveEquips = new Dictionary<int, ExclusiveEquip>();
            loadedCommoditiesBySN = new Dictionary<int, Commodity>();
            loadedCommoditiesByItemId = new Dictionary<int, Commodity>();
        }

        private static Dictionary<int, SetItem> loadedSetItems;
        private static Dictionary<int, ExclusiveEquip> loadedExclusiveEquips;
        private static Dictionary<int, Commodity> loadedCommoditiesBySN;
        private static Dictionary<int, Commodity> loadedCommoditiesByItemId;

        public static Dictionary<int, SetItem> LoadedSetItems
        {
            get { return loadedSetItems; }
        } 

        public static Dictionary<int, ExclusiveEquip> LoadedExclusiveEquips
        {
            get { return loadedExclusiveEquips; }
        }

        public static Dictionary<int, Commodity> LoadedCommoditiesBySN
        {
            get { return loadedCommoditiesBySN; }
        }

        public static Dictionary<int, Commodity> LoadedCommoditiesByItemId
        {
            get { return loadedCommoditiesByItemId; }
        }

        public static void LoadSetItems()
        {
            //搜索setItemInfo.img
            Wz_Node etcWz = PluginManager.FindWz(Wz_Type.Etc);
            if (etcWz == null)
                return;
            Wz_Node setItemNode = etcWz.FindNodeByPath("SetItemInfo.img", true);
            if (setItemNode == null)
                return;

            //搜索ItemOption.img
            Wz_Node itemWz = PluginManager.FindWz(Wz_Type.Item);
            if (itemWz == null)
                return;
            Wz_Node optionNode = itemWz.FindNodeByPath("ItemOption.img", true);
            if (optionNode == null)
                return;

            loadedSetItems.Clear();
            foreach (Wz_Node node in setItemNode.Nodes)
            {
                int setItemIndex;
                if (Int32.TryParse(node.Text, out setItemIndex))
                {
                    SetItem setItem = SetItem.CreateFromNode(node, optionNode);
                    if (setItem != null)
                        loadedSetItems[setItemIndex] = setItem;
                }
            }
        }

        public static void LoadExclusiveEquips()
        {
            Wz_Node etcWz = PluginManager.FindWz(Wz_Type.Etc);
            if (etcWz == null)
                return;
            Wz_Node exclusiveEquipNode = etcWz.FindNodeByPath("ExclusiveEquip.img", true);
            if (exclusiveEquipNode == null)
                return;

            loadedExclusiveEquips.Clear();
            foreach (Wz_Node node in exclusiveEquipNode.Nodes)
            {
                int exclusiveEquipIndex;
                if (Int32.TryParse(node.Text, out exclusiveEquipIndex))
                {
                    ExclusiveEquip exclusiveEquip = ExclusiveEquip.CreateFromNode(node);
                    if (exclusiveEquip != null)
                        loadedExclusiveEquips[exclusiveEquipIndex] = exclusiveEquip;
                }
            }
        }

        public static void LoadCommodities()
        {
            Wz_Node etcWz = PluginManager.FindWz(Wz_Type.Etc);
            if (etcWz == null)
                return;
            Wz_Node commodityNode = etcWz.FindNodeByPath("Commodity.img", true);
            if (commodityNode == null)
                return;

            loadedCommoditiesBySN.Clear();
            foreach (Wz_Node node in commodityNode.Nodes)
            {
                int commodityIndex;
                if (Int32.TryParse(node.Text, out commodityIndex))
                {
                    Commodity commodity = Commodity.CreateFromNode(node);
                    if (commodity != null)
                    {
                        loadedCommoditiesBySN[commodity.SN] = commodity;
                        if (commodity.ItemId / 10000 == 910)
                            loadedCommoditiesByItemId[commodity.ItemId] = commodity;
                    }
                }
            }
        }

        public static int GetActionDelay(string actionName)
        {
            if (string.IsNullOrEmpty(actionName))
            {
                return 0;
            }
            Wz_Node actionNode = PluginManager.FindWz("Character/00002000.img/" + actionName);
            if (actionNode == null)
            {
                return 0;
            }

            int delay = 0;
            foreach (Wz_Node frameNode in actionNode.Nodes)
            {
                Wz_Node delayNode = frameNode.Nodes["delay"];
                if (delayNode != null)
                {
                    delay += Math.Abs(delayNode.GetValue<int>());
                }
            }

            return delay;
        }
    }
}
