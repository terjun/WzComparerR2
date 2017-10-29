using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Res = CharaSimResource.Resource;
using WzComparerR2.MapRender.Patches2;
using WzComparerR2.Common;
using WzComparerR2.Rendering;
using WzComparerR2.Animation;
using static WzComparerR2.MapRender.UI.TooltipHelper;

namespace WzComparerR2.MapRender.UI
{
    class Tooltip2
    {
        public Tooltip2(GraphicsDevice graphicsDevice)
        {
            this.LoadContent(graphicsDevice);
        }

        public NineFormResource Resource { get; private set; }
        public StringLinker StringLinker { get; set; }
        public object TooltipTarget { get; set; }

        public void Draw(GameTime gameTime, RenderEnv env)
        {
            if (this.TooltipTarget == null)
            {
                return;
            }

            var content = Draw(gameTime, env, this.TooltipTarget);
            if (content.blocks != null)
            {
                var pos = env.Input.MousePosition;
                DrawContent(env, content, new Vector2(pos.X + 16, pos.Y + 16), true);
            }
        }

        public void Draw(GameTime gameTime, RenderEnv env, object item, Vector2 centerPosition)
        {
            if (item == null)
            {
                return;
            }

            var content = Draw(gameTime, env, item);
            if (content.blocks != null)
            {
                var pos = new Vector2(centerPosition.X - (int)(content.size.X / 2), centerPosition.Y - (int)(content.size.Y / 2));
                DrawContent(env, content, pos, false);
            }
        }

        private void LoadContent(GraphicsDevice graphicsDevice)
        {
            var res = new NineFormResource();
            res.N = Res.UIToolTip_img_Item_Frame2_n.ToTexture(graphicsDevice);
            res.NE = Res.UIToolTip_img_Item_Frame2_ne.ToTexture(graphicsDevice);
            res.E = Res.UIToolTip_img_Item_Frame2_e.ToTexture(graphicsDevice);
            res.SE = Res.UIToolTip_img_Item_Frame2_se.ToTexture(graphicsDevice);
            res.S = Res.UIToolTip_img_Item_Frame2_s.ToTexture(graphicsDevice);
            res.SW = Res.UIToolTip_img_Item_Frame2_sw.ToTexture(graphicsDevice);
            res.W = Res.UIToolTip_img_Item_Frame2_w.ToTexture(graphicsDevice);
            res.NW = Res.UIToolTip_img_Item_Frame2_nw.ToTexture(graphicsDevice);
            res.C = Res.UIToolTip_img_Item_Frame2_c.ToTexture(graphicsDevice);
            this.Resource = res;
        }

        private TooltipContent Draw(GameTime gameTime, RenderEnv env, object target)
        {
            if (target is LifeItem)
            {
                return DrawItem(gameTime, env, (LifeItem)target);
            }
            else if (target is PortalItem)
            {
                return DrawItem(gameTime, env, (PortalItem)target);
            }
            else if (target is ReactorItem)
            {
                return DrawItem(gameTime, env, (ReactorItem)target);
            }
            else if (target is TooltipItem)
            {
                return DrawItem(gameTime, env, (TooltipItem)target);
            }
            else if (target is PortalItem.ItemTooltip)
            {
                return DrawItem(gameTime, env, (PortalItem.ItemTooltip)target);
            }
            else if (target is UIWorldMap.Tooltip)
            {
                return DrawItem(gameTime, env, (UIWorldMap.Tooltip)target);
            }
            else if (target is string)
            {
                return DrawString(gameTime, env, (string)target);
            }
            else if (target is KeyValuePair<string, string>)
            {
                return DrawPair(gameTime, env, (KeyValuePair<string, string>)target);
            }
            return new TooltipContent();
        }

        private TooltipContent DrawItem(GameTime gameTime, RenderEnv env, LifeItem item)
        {
            var blocks = new List<object>();
            Vector2 size = Vector2.Zero;

            blocks = new List<object>();
            StringResult sr = null;
            Vector2 current = Vector2.Zero;

            switch (item.Type)
            {
                case LifeItem.LifeType.Mob:
                    {
                        this.StringLinker?.StringMob.TryGetValue(item.ID, out sr);
                        blocks.Add(PrepareTextBlock(env.Fonts.TooltipTitleFont, sr == null ? "(null)" : sr.Name, ref current, Color.LightYellow));
                        current += new Vector2(4, 4);
                        blocks.Add(PrepareTextBlock(env.Fonts.TooltipContentFont, "ID: " + item.ID.ToString("d7"), ref current, Color.White));
                        size.X = Math.Max(size.X, current.X);
                        current = new Vector2(0, current.Y + 16);

                        Vector2 size2;
                        var blocks2 = TooltipHelper.Prepare(item.LifeInfo, env.Fonts, out size2);
                        for (int i = 0; i < blocks2.Length; i++)
                        {
                            blocks2[i].Position.Y += current.Y;
                            blocks.Add(blocks2[i]);
                        }
                        size.X = Math.Max(size.X, size2.X);
                        size.Y = current.Y + size2.Y;
                    }
                    break;

                case LifeItem.LifeType.Npc:
                    {
                        this.StringLinker?.StringNpc.TryGetValue(item.ID, out sr);
                        blocks.Add(PrepareTextBlock(env.Fonts.TooltipTitleFont, sr == null ? "(null)" : sr.Name, ref current, Color.LightYellow));
                        current += new Vector2(4, 4);
                        blocks.Add(PrepareTextBlock(env.Fonts.TooltipContentFont, "ID: " + item.ID.ToString("d7"), ref current, Color.White));
                        size.X = Math.Max(size.X, current.X);
                        current = new Vector2(0, current.Y + 16);

                        var aniName = (item.View?.Animator as StateMachineAnimator)?.GetCurrent();
                        if (aniName != null)
                        {
                            blocks.Add(PrepareTextLine(env.Fonts.TooltipContentFont, "동작: " + aniName, ref current, Color.White, ref size.X));
                        }

                        size.Y = current.Y;
                    }
                    break;
            }

            return new TooltipContent() { blocks = blocks, size = size };
        }

        private TooltipContent DrawItem(GameTime gameTime, RenderEnv env, PortalItem item)
        {
            var blocks = new List<object>();
            Vector2 size = Vector2.Zero;
            StringResult sr = null;
            Vector2 current = Vector2.Zero;

            var sb = new StringBuilder();
            sb.Append("이름: ").AppendLine(item.PName);

            string pTypeName = GetPortalTypeString(item.Type);
            sb.Append("유형: ").Append(item.Type);
            if (pTypeName != null)
            {
                sb.Append("(").Append(pTypeName).Append(")");
            }
            sb.AppendLine();

            sb.Append("이동 맵: ").Append(item.ToMap);
            if (item.ToMap != null)
            {
                this.StringLinker?.StringMap.TryGetValue(item.ToMap.Value, out sr);
                string toMapName = sr?.Name;
                sb.Append("(").Append(sr?.Name ?? "null").Append(")");
            }
            sb.AppendLine();

            sb.Append("이동 포탈: ").AppendLine(item.ToName);

            if (!string.IsNullOrEmpty(item.Script))
            {
                sb.Append("스크립트: ").AppendLine(item.Script);
            }

            sb.Length -= 2;

            blocks.Add(PrepareTextLine(env.Fonts.TooltipContentFont, sb.ToString(), ref current, Color.White, ref size.X));
            size.Y = current.Y;
            return new TooltipContent() { blocks = blocks, size = size };
        }

        private TooltipContent DrawItem(GameTime gameTime, RenderEnv env, ReactorItem item)
        {
            var blocks = new List<object>();
            Vector2 size = Vector2.Zero;
            Vector2 current = Vector2.Zero;

            var sb = new StringBuilder();
            sb.Append("ID: ").Append(item.ID).AppendLine();
            sb.Append("이름: ").AppendLine(item.ReactorName);
            sb.Append("시간: ").Append(item.ReactorTime).AppendLine();

            sb.Append("상태: ").Append(item.View.Stage);
            var ani = item.View.Animator as StateMachineAnimator;
            if (ani != null)
            {
                sb.Append(" (").Append(ani.Data.SelectedState).Append(")");
            }
            sb.AppendLine();

            sb.Length -= 2;
            blocks.Add(PrepareTextLine(env.Fonts.TooltipContentFont, sb.ToString(), ref current, Color.White, ref size.X));
            size.Y = current.Y;
            return new TooltipContent() { blocks = blocks, size = size };
        }

        private TooltipContent DrawItem(GameTime gameTime, RenderEnv env, TooltipItem item)
        {
            var blocks = new List<object>();
            Vector2 size = Vector2.Zero;
            Vector2 current = Vector2.Zero;

            if (!string.IsNullOrEmpty(item.Title))
            {
                bool hasDesc = !string.IsNullOrEmpty(item.Desc) || !string.IsNullOrEmpty(item.ItemEU);
                var titleFont = hasDesc ? env.Fonts.TooltipTitleFont : env.Fonts.TooltipContentFont;
                blocks.Add(PrepareTextLine(titleFont, item.Title, ref current, Color.White, ref size.X));
            }
            if (!string.IsNullOrEmpty(item.Desc))
            {
                blocks.Add(PrepareTextLine(env.Fonts.TooltipContentFont, item.Desc, ref current, Color.White, ref size.X));
            }
            if (!string.IsNullOrEmpty(item.ItemEU))
            {
                blocks.Add(PrepareTextLine(env.Fonts.TooltipContentFont, item.ItemEU, ref current, Color.White, ref size.X));
            }

            size.Y = current.Y;
            return new TooltipContent() { blocks = blocks, size = size };
        }

        private TooltipContent DrawItem(GameTime gameTime, RenderEnv env, PortalItem.ItemTooltip item)
        {
            var blocks = new List<object>();
            Vector2 size = new Vector2(160, 0);
            Vector2 current = new Vector2(0, 2);
            TextBlock textBlock = PrepareTextLine(env.Fonts.TooltipContentFont, item.Title, ref current, Color.White, ref size.X);
            textBlock.Align = Alignment.Center;
            blocks.Add(textBlock);
            size.Y = current.Y;
            return new TooltipContent() { blocks = blocks, size = size };
        }

        private TooltipContent DrawItem(GameTime gameTime, RenderEnv env, UIWorldMap.Tooltip item)
        {
            var blocks = new List<object>();
            Vector2 size = new Vector2(160, 0);
            Vector2 current = new Vector2(0, 2);
            StringResult sr = null;

            if (item.MapID != null)
            {
                this.StringLinker?.StringMap.TryGetValue(item.MapID.Value, out sr);
                string title = item.Title ?? (sr != null ? string.Format("{0} : {1}", sr["streetName"], sr["mapName"]) : item.MapID.ToString());
                string desc = item.Desc ?? sr?["mapDesc"];
                if (item.Barrier > 0 || item.BarrierArc > 0)
                {
                    TextBlock barrierBlock;
                    current.X += 20;
                    if (item.Barrier > 0)
                    {
                        barrierBlock = PrepareTextLine(env.Fonts.MapBarrierFont, item.Barrier.ToString(), ref current, new Color(255, 204, 0), ref size.X);
                    }
                    else
                    {
                        barrierBlock = PrepareTextLine(env.Fonts.MapBarrierFont, item.BarrierArc.ToString(), ref current, new Color(221, 170, 255), ref size.X);
                    }
                    barrierBlock.Align = Alignment.Center;
                    blocks.Add(barrierBlock);
                    current.Y += 1;
                }
                var titleFont = string.IsNullOrEmpty(desc) ? env.Fonts.TooltipContentFont : env.Fonts.TooltipTitleFont;
                TextBlock titleBlock = PrepareTextLine(titleFont, title, ref current, Color.White, ref size.X);
                titleBlock.Align = Alignment.Center;
                blocks.Add(titleBlock);
                bool hasPart2 = false;
                List<float> lineY = new List<float>();
                float maxWidth = 0;
                if (item.Mob.Count + item.Npc.Count >= 19 && item.Npc.Count > 0)
                {
                    foreach (var npc in item.Npc)
                    {
                        maxWidth = Math.Max(maxWidth, env.Fonts.TooltipContentFont.MeasureString(npc).X);
                    }
                    size.X = Math.Max(size.X, 25 + maxWidth * 2);
                }
                if (!string.IsNullOrEmpty(desc))
                {
                    size.X = Math.Max(size.X, 230);
                    current.Y += 5;
                    blocks.AddRange(PrepareFormatText(env.Fonts.TooltipContentFont, desc, ref current, (int)size.X - 7, ref size.X, env.Fonts.TooltipContentFont.Height + 2).Cast<object>());
                }
                if (item.Mob.Count > 0)
                {
                    if (!hasPart2)
                    {
                        current.Y += 5;
                        hasPart2 = true;
                    }
                    lineY.Add(current.Y);
                    current.Y += 8;
                    blocks.Add(new UIGraphics.RenderBlock<Texture2D>(item.Mob[0].Value ? Properties.Resources.UIWindow_img_ToolTip_WorldMap_enchantMob.ToTexture(env.GraphicsDevice) : Properties.Resources.UIWindow_img_ToolTip_WorldMap_Mob.ToTexture(env.GraphicsDevice), new Rectangle((int)current.X, (int)current.Y + 1, 0, 0)));
                    foreach (var mob in item.Mob)
                    {
                        this.StringLinker?.StringMob.TryGetValue(mob.Key, out sr);
                        var levelNode = PluginBase.PluginManager.FindWz(string.Format("Mob/{0:D7}.img/info/level", mob.Key));
                        int level = levelNode != null ? Convert.ToInt32(levelNode.Value) : 0;
                        string mobText = sr != null ? string.Format("{0}(Lv.{1})", sr.Name, level) : mob.Key.ToString();
                        current.X = 15;
                        blocks.Add(PrepareTextLine(env.Fonts.TooltipContentFont, mobText, ref current, mob.Value ? new Color(255, 0, 102) : new Color(119, 255, 0), ref size.X));
                        current.Y += 4;
                    }
                }
                if (item.Npc.Count > 0)
                {
                    if (!hasPart2)
                    {
                        current.Y += 5;
                        hasPart2 = true;
                    }
                    lineY.Add(current.Y);
                    current.Y += 8;
                    blocks.Add(new UIGraphics.RenderBlock<Texture2D>(Properties.Resources.UIWindow_img_ToolTip_WorldMap_Npc.ToTexture(env.GraphicsDevice), new Rectangle((int)current.X, (int)current.Y + 1, 0, 0)));
                    if (item.Mob.Count + item.Npc.Count >= 19)
                    {
                        for (int i = 0; i < (item.Npc.Count + 1) / 2; i++)
                        {
                            float y = current.Y;
                            current.X += 15;
                            blocks.Add(PrepareTextLine(env.Fonts.TooltipContentFont, item.Npc[i * 2], ref current, new Color(119, 204, 255), ref size.X));
                            if (i * 2 + 1 < item.Npc.Count)
                            {
                                current.X += 25 + maxWidth;
                                current.Y = y;
                                blocks.Add(PrepareTextLine(env.Fonts.TooltipContentFont, item.Npc[i * 2 + 1], ref current, new Color(119, 204, 255), ref size.X));
                            }
                            current.Y += 4;
                        }
                    }
                    else
                    {
                        foreach (var npc in item.Npc)
                        {
                            current.X += 15;
                            blocks.Add(PrepareTextLine(env.Fonts.TooltipContentFont, npc, ref current, new Color(119, 204, 255), ref size.X));
                            current.Y += 4;
                        }
                    }
                }
                if (hasPart2)
                {
                    current.Y -= 4;
                }
                if (item.Barrier > 0)
                {
                    blocks.Add(new UIGraphics.RenderBlock<Texture2D>(Properties.Resources.UIWindow_img_ToolTip_WorldMap_StarForce.ToTexture(env.GraphicsDevice), new Rectangle((int)(size.X / 2 - (20 + env.Fonts.MapBarrierFont.MeasureString(item.Barrier.ToString()).X) / 2), 2, 0, 0)));
                }
                if (item.BarrierArc > 0)
                {
                    blocks.Add(new UIGraphics.RenderBlock<Texture2D>(Properties.Resources.UIWindow_img_ToolTip_WorldMap_ArcaneForce.ToTexture(env.GraphicsDevice), new Rectangle((int)(size.X / 2 - (20 + env.Fonts.MapBarrierFont.MeasureString(item.BarrierArc.ToString()).X) / 2), 2, 0, 0)));
                }
                foreach (var y in lineY)
                {
                    blocks.Add(new UIGraphics.RenderBlock<Texture2D>(Properties.Resources.UIWindow_img_ToolTip_WorldMap_Line.ToTexture(env.GraphicsDevice), new Rectangle((int)current.X, (int)y, (int)size.X, 1)));
                }
            }

            size.Y = current.Y;
            return new TooltipContent() { blocks = blocks, size = size };
        }

        private TooltipContent DrawString(GameTime gameTime, RenderEnv env, string text)
        {
            var blocks = new List<object>();
            Vector2 size = Vector2.Zero;
            Vector2 current = Vector2.Zero;
            blocks.Add(PrepareTextLine(env.Fonts.TooltipContentFont, text, ref current, Color.White, ref size.X));
            size.Y = current.Y;
            return new TooltipContent() { blocks = blocks, size = size };
        }

        private TooltipContent DrawPair(GameTime gameTime, RenderEnv env, KeyValuePair<string, string> pair)
        {
            var blocks = new List<object>();
            Vector2 size = Vector2.Zero;
            Vector2 current = new Vector2(-2, -2);

            TextBlock nameBlock = PrepareTextLine(env.Fonts.TooltipContentFont, pair.Key, ref current, Color.White, ref size.X);
            nameBlock.Align = Alignment.Center;
            blocks.Add(nameBlock);
            float lineY = current.Y;
            current.X -= 2;
            current.Y += 3;
            TextBlock descBlock = PrepareTextLine(env.Fonts.TooltipContentFont, pair.Value, ref current, Color.White, ref size.X);
            descBlock.Align = Alignment.Center;
            blocks.Add(descBlock);
            current.X -= 2;
            blocks.Add(new UIGraphics.RenderBlock<Texture2D>(Properties.Resources.UIWindow_img_ToolTip_WorldMap_Line.ToTexture(env.GraphicsDevice), new Rectangle((int)current.X, (int)lineY, (int)size.X + 2, 1)));

            size.X -= 2;
            size.Y = current.Y - 4;
            return new TooltipContent() { blocks = blocks, size = size };
        }

        private void DrawContent(RenderEnv env, TooltipContent content, Vector2 position, bool adjustToWindow)
        {
            Vector2 padding = new Vector2(10, 8);
            Vector2 preferSize = new Vector2(
                Math.Max(content.size.X + padding.X * 2, 26),
                Math.Max(content.size.Y + padding.Y * 2, 26));

            if (adjustToWindow)
            {
                position.X = Math.Max(0, Math.Min(position.X, env.Camera.Width - preferSize.X));
                position.Y = Math.Max(0, Math.Min(position.Y, env.Camera.Height - preferSize.Y));
            }

            env.Sprite.Begin(blendState: BlendState.NonPremultiplied);
            var background = UIGraphics.LayoutNinePatch(this.Resource, new Point((int)preferSize.X, (int)preferSize.Y));
            foreach (var block in background)
            {
                if (block.Rectangle.Width > 0 && block.Rectangle.Height > 0 && block.Texture != null)
                {
                    var rect = new Rectangle((int)position.X + block.Rectangle.X,
                        (int)position.Y + block.Rectangle.Y,
                        block.Rectangle.Width,
                        block.Rectangle.Height);
                    env.Sprite.Draw(block.Texture, rect, Color.White);
                }
            }
            var cover = Res.UIToolTip_img_Item_Frame2_cover.ToTexture(env.GraphicsDevice);
            var coverRect = new Rectangle((int)position.X + 3,
                (int)position.Y + 3,
                Math.Min((int)preferSize.X - 6, cover.Width),
                Math.Min((int)preferSize.Y - 6, cover.Height));
            var sourceRect = new Rectangle(0,
                0,
                Math.Min((int)preferSize.X - 6, cover.Width),
                Math.Min((int)preferSize.Y - 6, cover.Height));
            env.Sprite.Draw(cover, coverRect, sourceRect, Color.White);

            foreach (var block in content.blocks.OfType<TextBlock>())
            {
                var pos = new Vector2(position.X + padding.X + block.Position.X,
                    position.Y + padding.Y + block.Position.Y);
                if (block.Align == Alignment.Center)
                {
                    float maxWidth = 0;
                    float width = 0;
                    foreach (char c in block.Text)
                    {
                        if (c == '\r')
                        {
                            continue;
                        }
                        else if (c == '\n') //换行符
                        {
                            width = 0;
                            continue;
                        }
                        else
                        {
                            width += block.Font.TryGetRect(c).Width;
                            if (width > maxWidth)
                            {
                                maxWidth = width;
                            }
                        }
                    }
                    pos.X += (int)((content.size.X - block.Position.X) / 2 - maxWidth / 2);
                }
                env.Sprite.DrawStringEx(block.Font, block.Text, pos, block.ForeColor);
            }
            foreach (var block in content.blocks.OfType<UIGraphics.RenderBlock<Texture2D>>())
            {
                var rect = new Rectangle((int)position.X + (int)padding.X + block.Rectangle.X,
                    (int)position.Y + (int)padding.Y + block.Rectangle.Y,
                    block.Rectangle.Width == 0 ? block.Texture.Width : block.Rectangle.Width,
                    block.Rectangle.Height == 0 ? block.Texture.Height : block.Rectangle.Height);
                env.Sprite.Draw(block.Texture, rect, Color.White);
            }
            env.Sprite.End();
        }

        private struct TooltipContent
        {
            public List<object> blocks;
            public Vector2 size;
        }
    }
}
