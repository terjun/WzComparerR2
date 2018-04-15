using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Resource = CharaSimResource.Resource;
using WzComparerR2.PluginBase;
using WzComparerR2.WzLib;
using WzComparerR2.Common;
using System.Text.RegularExpressions;
using WzComparerR2.CharaSim;

namespace WzComparerR2.CharaSimControl
{
    public class SetItemTooltipRender : TooltipRender
    {
        public SetItemTooltipRender()
        {
        }

        public SetItem SetItem { get; set; }

        public override object TargetItem
        {
            get { return this.SetItem; }
            set { this.SetItem = value as SetItem; }
        }

        public bool IsCombineProperties { get; set; } = true;

        public override Bitmap Render()
        {
            if (this.SetItem == null)
            {
                return null;
            }

            int width = 261;
            int picHeight1;
            Bitmap originBmp = RenderSetItem(out picHeight1);
            int picHeight2 = 0;
            Bitmap effectBmp = null;

            if (this.SetItem.ExpandToolTip)
            {
                effectBmp = RenderEffectPart(out picHeight2);
                width += 261;
            }

            Bitmap tooltip = new Bitmap(width, Math.Max(picHeight1, picHeight2));
            Graphics g = Graphics.FromImage(tooltip);

            //绘制左侧
            GearGraphics.DrawNewTooltipBack(g, 0, 0, originBmp.Width, picHeight1);
            g.DrawImage(originBmp, 0, 0, new Rectangle(0, 0, originBmp.Width, picHeight1), GraphicsUnit.Pixel);
            
            //绘制右侧
            if(effectBmp != null)
            {
                GearGraphics.DrawNewTooltipBack(g, originBmp.Width, 0, effectBmp.Width, picHeight2);
                g.DrawImage(effectBmp, originBmp.Width, 0, new Rectangle(0, 0, effectBmp.Width, picHeight2), GraphicsUnit.Pixel);
            }

            originBmp?.Dispose();
            effectBmp?.Dispose();
            g.Dispose();
            return tooltip;
        }

        private Bitmap RenderSetItem(out int picHeight)
        {
            Bitmap setBitmap = new Bitmap(261, DefaultPicHeight);
            Graphics g = Graphics.FromImage(setBitmap);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            picHeight = 10;
            TextRenderer.DrawText(g, this.SetItem.SetItemName, GearGraphics.EquipDetailFont2, new Point(261, 10), ((SolidBrush)GearGraphics.GreenBrush2).Color, TextFormatFlags.HorizontalCenter);
            picHeight += 25;

            format.Alignment = StringAlignment.Far;

            if (this.SetItem.SetItemID > 0)
            {
                List<string> partNames = new List<string>();

                foreach (var setItemPart in this.SetItem.ItemIDs.Parts)
                {
                    string itemName = setItemPart.Value.RepresentName;
                    string typeName = setItemPart.Value.TypeName;

                    if (string.IsNullOrEmpty(typeName) && SetItem.Parts)
                    {
                        typeName = "장비";
                    }

                    bool Cash = false;
                    int wonderGrade = 0;
                    BitmapOrigin IconRaw = new BitmapOrigin();

                    foreach (var itemID in setItemPart.Value.ItemIDs)
                    {
                        StringResult sr;
                        if (StringLinker != null)
                        {
                            if (StringLinker.StringEqp.TryGetValue(itemID.Key, out sr))
                            {
                                string[] fullPath = sr.FullPath.Split('\\');
                                Wz_Node itemNode = PluginBase.PluginManager.FindWz(string.Format(@"Character\{0}\{1:D8}.img", String.Join("\\", new ArraySegment<string>(fullPath, 2, fullPath.Length - 3)), itemID.Key));
                                if (itemNode != null)
                                {
                                    Gear gear = Gear.CreateFromNode(itemNode, PluginBase.PluginManager.FindWz);
                                    Cash = gear.Cash;
                                    IconRaw = gear.IconRaw;
                                }
                            }
                            else if (StringLinker.StringItem.TryGetValue(itemID.Key, out sr))
                            {
                                Wz_Node itemNode = PluginBase.PluginManager.FindWz(string.Format(@"Item\Pet\{0:D7}.img", itemID.Key));
                                if (itemNode != null)
                                {
                                    Item item = Item.CreateFromNode(itemNode, PluginBase.PluginManager.FindWz);
                                    Cash = item.Cash;
                                    item.Props.TryGetValue(ItemPropType.wonderGrade, out wonderGrade);
                                    IconRaw = item.IconRaw;
                                }
                            }
                        }

                        break;
                    }

                    if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(typeName))
                    {
                        foreach (var itemID in setItemPart.Value.ItemIDs)
                        {
                            StringResult sr = null; ;
                            if (StringLinker != null)
                            {
                                if (StringLinker.StringEqp.TryGetValue(itemID.Key, out sr))
                                {
                                    itemName = sr.Name;
                                    if (typeName == null)
                                    {
                                        typeName = ItemStringHelper.GetSetItemGearTypeString(Gear.GetGearType(itemID.Key));
                                    }
                                    switch (Gear.GetGender(itemID.Key))
                                    {
                                    case 0: itemName += " (남)"; break;
                                    case 1: itemName += " (여)"; break;
                                    }
                                }
                                else if (StringLinker.StringItem.TryGetValue(itemID.Key, out sr)) //兼容宠物
                                {
                                    itemName = sr.Name;
                                    //if (typeName == null)
                                    {
                                        if (itemID.Key / 10000 == 500)
                                        {
                                            typeName = "펫";
                                        }
                                        else
                                        {
                                            typeName = "";
                                        }
                                    }
                                }
                            }
                            if (sr == null)
                            {
                                itemName = "(null)";
                            }

                            break;
                        }
                    }

                    itemName = itemName ?? string.Empty;
                    typeName = typeName ?? "장비";

                    if (!Regex.IsMatch(typeName, @"^(\(.*\)|（.*）)$") && !Regex.IsMatch(typeName, @"^(\[.*\]|（.*）)$"))
                    {
                        typeName = "(" + typeName + ")";
                    }

                    if (!partNames.Contains(itemName + typeName))
                    {
                        partNames.Add(itemName + typeName);
                        Brush brush = setItemPart.Value.Enabled ? Brushes.White : GearGraphics.GrayBrush2;
                        if (!Cash)
                        {
                            TextRenderer.DrawText(g, itemName, GearGraphics.EquipDetailFont2, new Point(10, picHeight), ((SolidBrush)brush).Color, TextFormatFlags.NoPadding);
                            TextRenderer.DrawText(g, typeName, GearGraphics.EquipDetailFont2, new Point(252 - TextRenderer.MeasureText(g, typeName, GearGraphics.EquipDetailFont2, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Width, picHeight), ((SolidBrush)brush).Color, TextFormatFlags.NoPadding);
                            picHeight += 18;
                        }
                        else
                        {
                            g.FillRectangle(GearGraphics.GearIconBackBrush2, 10, picHeight, 36, 36);
                            g.DrawImage(Resource.Item_shadow, 10 + 2 + 3, picHeight + 2 + 32 - 6);
                            if (IconRaw.Bitmap != null)
                            {
                                g.DrawImage(IconRaw.Bitmap, 10 + 2 - IconRaw.Origin.X, picHeight + 2 + 32 - IconRaw.Origin.Y);
                            }
                            if (wonderGrade > 0)
                            {
                                Image label = Resource.ResourceManager.GetObject("CashItem_label_" + (wonderGrade + 3)) as Bitmap;
                                if (label != null)
                                {
                                    g.DrawImage(label, 10 + 2 + 20, picHeight + 2 + 32 - 12);
                                }
                            }
                            else
                            {
                                g.DrawImage(Resource.CashItem_0, 10 + 2 + 20, picHeight + 2 + 32 - 12);
                            }
                            TextRenderer.DrawText(g, itemName, GearGraphics.EquipDetailFont2, new Point(50, picHeight), ((SolidBrush)brush).Color, TextFormatFlags.NoPadding);
                            TextRenderer.DrawText(g, typeName, GearGraphics.EquipDetailFont2, new Point(252 - TextRenderer.MeasureText(g, typeName, GearGraphics.EquipDetailFont2, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Width, picHeight), ((SolidBrush)brush).Color, TextFormatFlags.NoPadding);
                            picHeight += 40;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.SetItem.CompleteCount; ++i)
                {
                    TextRenderer.DrawText(g, "(없음)", GearGraphics.EquipDetailFont2, new Point(10, picHeight), ((SolidBrush)GearGraphics.GrayBrush2).Color, TextFormatFlags.NoPadding);
                    TextRenderer.DrawText(g, "미착용", GearGraphics.EquipDetailFont2, new Point(252 - TextRenderer.MeasureText(g, "미착용", GearGraphics.EquipDetailFont2, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Width, picHeight), ((SolidBrush)GearGraphics.GrayBrush2).Color, TextFormatFlags.NoPadding);
                    picHeight += 18;
                }
            }

            if (!this.SetItem.ExpandToolTip)
            {
                picHeight += 5;
                g.DrawLine(Pens.White, 6, picHeight, 254, picHeight);//分割线
                picHeight += 9;
                RenderEffect(g, ref picHeight);
            }
            picHeight += 11;

            format.Dispose();
            g.Dispose();
            return setBitmap;
        }

        private Bitmap RenderEffectPart(out int picHeight)
        {
            Bitmap effBitmap = new Bitmap(261, DefaultPicHeight);
            Graphics g = Graphics.FromImage(effBitmap);
            picHeight = 9;
            RenderEffect(g, ref picHeight);
            picHeight += 11;
            g.Dispose();
            return effBitmap;
        }

        /// <summary>
        /// 绘制套装属性。
        /// </summary>
        private void RenderEffect(Graphics g, ref int picHeight)
        {
            foreach (KeyValuePair<int, SetItemEffect> effect in this.SetItem.Effects)
            {
                string effTitle;
                if (this.SetItem.SetItemID < 0)
                {
                    effTitle = $"월드 내 중복 착용 효과({effect.Key} / {this.SetItem.CompleteCount})";
                }
                else
                {
                    effTitle = effect.Key + "세트효과";
                }
                TextRenderer.DrawText(g, effTitle, GearGraphics.EquipDetailFont, new Point(10, picHeight), ((SolidBrush)GearGraphics.GreenBrush2).Color, TextFormatFlags.NoPadding);
                picHeight += 15;
                //Brush brush = effect.Value.Enabled ? Brushes.White : GearGraphics.GrayBrush2;
                var color = effect.Value.Enabled ? Color.White : GearGraphics.GrayColor2;

                //T116 合并套装
                var props = IsCombineProperties ? Gear.CombineProperties(effect.Value.PropsV5) : effect.Value.PropsV5;
                foreach (KeyValuePair<GearPropType, object> prop in props)
                {
                    if (prop.Key == GearPropType.Option)
                    {
                        List<Potential> ops = (List<Potential>)prop.Value;
                        foreach (Potential p in ops)
                        {
                            GearGraphics.DrawPlainText(g, p.ConvertSummary(), GearGraphics.EquipDetailFont2, color, 10, 244, ref picHeight, 15);
                        }
                    }
                    else if (prop.Key == GearPropType.OptionToMob)
                    {
                        List<SetItemOptionToMob> ops = (List<SetItemOptionToMob>)prop.Value;
                        foreach (SetItemOptionToMob p in ops)
                        {
                            GearGraphics.DrawPlainText(g, p.ConvertSummary(), GearGraphics.EquipDetailFont2, color, 10, 244, ref picHeight, 15);
                        }
                    }
                    else if (prop.Key == GearPropType.activeSkill)
                    {
                        List<SetItemActiveSkill> ops = (List<SetItemActiveSkill>)prop.Value;
                        foreach (SetItemActiveSkill p in ops)
                        {
                            StringResult sr;
                            if (StringLinker == null || !StringLinker.StringSkill.TryGetValue(p.SkillID, out sr))
                            {
                                sr = new StringResult();
                                sr.Name = p.SkillID.ToString();
                            }
                            string summary = "<" + sr.Name.Replace(Environment.NewLine, "") + "> 스킬 사용 가능";
                            GearGraphics.DrawPlainText(g, summary, GearGraphics.EquipDetailFont2, color, 10, 244, ref picHeight, 15);
                        }
                    }
                    else
                    {
                        var summary = ItemStringHelper.GetGearPropString(prop.Key, Convert.ToInt32(prop.Value));
                        GearGraphics.DrawPlainText(g, summary, GearGraphics.EquipDetailFont2, color, 10, 244, ref picHeight, 15);
                    }
                }
            }
        }
    }
}
