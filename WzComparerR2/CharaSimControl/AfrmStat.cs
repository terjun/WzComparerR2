using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Text;
using CharaSimResource;
using WzComparerR2.CharaSim;
using WzComparerR2.Common;
using WzComparerR2.Controls;

namespace WzComparerR2.CharaSimControl
{
    public class AfrmStat : AlphaForm
    {
        public AfrmStat()
        {
            sec = new int[2];
            for (int i = 0; i < sec.Length; i++)
                sec[i] = 1 << i;

            initCtrl();
        }

        private BitVector32 partVisible;
        private int[] sec;
        private Point baseOffset;
        private Point newLocation;
        private Character character;

        private ACtrlButton btnHPUp;
        private ACtrlButton btnMPUp;
        private ACtrlButton btnStrUp;
        private ACtrlButton btnDexUp;
        private ACtrlButton btnIntUp;
        private ACtrlButton btnLukUp;
        private ACtrlButton btnAuto;
        private ACtrlButton btnClose;
        private ACtrlButton btnDetailOpen;
        private ACtrlButton btnDetailClose;
        private ACtrlButton btnHyperStatOpen;
        private ACtrlButton btnHyperStatClose;
        private ACtrlButton btnAbility;
        private ACtrlButton btnHpUp;
        private ACtrlVScroll vScroll;
        private ACtrlButton btnLVUp1;
        private ACtrlButton btnLVUp2;
        private ACtrlButton btnLVUp3;
        private ACtrlButton btnLVUp4;
        private ACtrlButton btnLVUp5;
        private ACtrlButton btnLVUp6;
        private ACtrlButton btnLVUp7;
        private ACtrlButton btnLVUp8;
        private ACtrlButton btnLVUp9;
        private ACtrlButton btnLVUp10;
        private ACtrlButton btnLVUp11;
        private ACtrlButton btnLVUp12;
        private ACtrlButton btnReset;
        private ACtrlButton btnReduce;
        private bool waitForRefresh;

        private int HyperStatScrollValue;
        private Bitmap[] HyperStatList = new Bitmap[] { Resource.HyperStat_Window_statList_80000400, Resource.HyperStat_Window_statList_80000401, Resource.HyperStat_Window_statList_80000402, Resource.HyperStat_Window_statList_80000403, Resource.HyperStat_Window_statList_80000404, Resource.HyperStat_Window_statList_80000405, Resource.HyperStat_Window_statList_80000406, Resource.HyperStat_Window_statList_80000407, Resource.HyperStat_Window_statList_80000408, Resource.HyperStat_Window_statList_80000409, Resource.HyperStat_Window_statList_80000410, Resource.HyperStat_Window_statList_80000412, Resource.HyperStat_Window_statList_80000413, Resource.HyperStat_Window_statList_80000414, Resource.HyperStat_Window_statList_80000415, Resource.HyperStat_Window_statList_80000416, Resource.HyperStat_Window_statList_80000417 };

        public Character Character
        {
            get { return character; }
            set { character = value; }
        }

        public bool DetailVisible
        {
            get { return partVisible[sec[0]]; }
            private set { partVisible[sec[0]] = value; }
        }

        public bool HyperStatVisible
        {
            get { return partVisible[sec[1]]; }
            private set { partVisible[sec[1]] = value; }
        }

        private Rectangle DetailRect
        {
            get
            {
                return new Rectangle(
                    new Point(baseOffset.X + Resource.Stat_main_backgrnd.Width, baseOffset.Y),
                    Resource.Stat_detail_backgrnd.Size);
            }
        }

        private Rectangle HyperStatRect
        {
            get
            {
                return new Rectangle(
                    new Point(baseOffset.X - Resource.HyperStat_Window_backgrnd.Width, baseOffset.Y),
                    Resource.HyperStat_Window_backgrnd.Size);
            }
        }

        private void initCtrl()
        {
            this.btnHPUp = new ACtrlButton();
            this.btnHPUp.Location = new Point(187, 121);

            this.btnMPUp = new ACtrlButton();
            this.btnMPUp.Location = new Point(187, 139);

            this.btnStrUp = new ACtrlButton();
            this.btnStrUp.Location = new Point(187, 208);

            this.btnDexUp = new ACtrlButton();
            this.btnDexUp.Location = new Point(187, 226);

            this.btnIntUp = new ACtrlButton();
            this.btnIntUp.Location = new Point(187, 244);

            this.btnLukUp = new ACtrlButton();
            this.btnLukUp.Location = new Point(187, 262);

            this.btnLVUp1 = new ACtrlButton();
            this.btnLVUp1.Location = new Point(147, 43);

            this.btnLVUp2 = new ACtrlButton();
            this.btnLVUp2.Location = new Point(147, 61);

            this.btnLVUp3 = new ACtrlButton();
            this.btnLVUp3.Location = new Point(147, 79);

            this.btnLVUp4 = new ACtrlButton();
            this.btnLVUp4.Location = new Point(147, 97);

            this.btnLVUp5 = new ACtrlButton();
            this.btnLVUp5.Location = new Point(147, 115);

            this.btnLVUp6 = new ACtrlButton();
            this.btnLVUp6.Location = new Point(147, 133);

            this.btnLVUp7 = new ACtrlButton();
            this.btnLVUp7.Location = new Point(147, 151);

            this.btnLVUp8 = new ACtrlButton();
            this.btnLVUp8.Location = new Point(147, 169);

            this.btnLVUp9 = new ACtrlButton();
            this.btnLVUp9.Location = new Point(147, 187);

            this.btnLVUp10 = new ACtrlButton();
            this.btnLVUp10.Location = new Point(147, 205);

            this.btnLVUp11 = new ACtrlButton();
            this.btnLVUp11.Location = new Point(147, 223);

            this.btnLVUp12 = new ACtrlButton();
            this.btnLVUp12.Location = new Point(147, 241);

            ACtrlButton[] addBtnList = new ACtrlButton[] { btnHPUp, btnMPUp, btnStrUp, btnDexUp, btnIntUp, btnLukUp, btnLVUp1, btnLVUp2, btnLVUp3, btnLVUp4, btnLVUp5, btnLVUp6, btnLVUp7, btnLVUp8, btnLVUp9, btnLVUp10, btnLVUp11, btnLVUp12 };
            for (int i = 0; i < addBtnList.Length; i++)
            {
                addBtnList[i].Normal = new BitmapOrigin(Resource.Stat_main_BtUp_normal_0);
                addBtnList[i].MouseOver = new BitmapOrigin(Resource.Stat_main_BtUp_mouseOver_0);
                addBtnList[i].Pressed = new BitmapOrigin(Resource.Stat_main_BtUp_pressed_0);
                addBtnList[i].Disabled = new BitmapOrigin(Resource.Stat_main_BtUp_disabled_0);
                addBtnList[i].Size = new Size(12, 12);
                addBtnList[i].ButtonStateChanged += new EventHandler(aCtrl_RefreshCall);
            }

            this.btnClose = new ACtrlButton();
            this.btnClose.Normal = new BitmapOrigin(Resource.BtClose3_normal_0);
            this.btnClose.Pressed = new BitmapOrigin(Resource.BtClose3_pressed_0);
            this.btnClose.MouseOver = new BitmapOrigin(Resource.BtClose3_mouseOver_0);
            this.btnClose.Disabled = new BitmapOrigin(Resource.BtClose3_disabled_0);
            this.btnClose.Location = new Point(190, 6);
            this.btnClose.Size = new Size(13, 13);
            this.btnClose.ButtonStateChanged += new EventHandler(aCtrl_RefreshCall);
            this.btnClose.MouseClick += new MouseEventHandler(btnClose_MouseClick);

            this.btnDetailOpen = new ACtrlButton();
            this.btnDetailOpen.Normal = new BitmapOrigin(Resource.Stat_main_BtDetailOpen_normal_0);
            this.btnDetailOpen.Pressed = new BitmapOrigin(Resource.Stat_main_BtDetailOpen_pressed_0);
            this.btnDetailOpen.MouseOver = new BitmapOrigin(Resource.Stat_main_BtDetailOpen_mouseOver_0);
            this.btnDetailOpen.Disabled = new BitmapOrigin(Resource.Stat_main_BtDetailOpen_disabled_0);
            this.btnDetailOpen.Location = new Point(132,288);
            this.btnDetailOpen.Size = new Size(68, 16);
            this.btnDetailOpen.ButtonStateChanged += new EventHandler(aCtrl_RefreshCall);
            this.btnDetailOpen.MouseClick += new MouseEventHandler(btnDetailOpen_MouseClick);

            this.btnDetailClose = new ACtrlButton();
            this.btnDetailClose.Normal = new BitmapOrigin(Resource.Stat_main_BtDetailClose_normal_0);
            this.btnDetailClose.Pressed = new BitmapOrigin(Resource.Stat_main_BtDetailClose_pressed_0);
            this.btnDetailClose.MouseOver = new BitmapOrigin(Resource.Stat_main_BtDetailClose_mouseOver_0);
            this.btnDetailClose.Disabled = new BitmapOrigin(Resource.Stat_main_BtDetailClose_disabled_0);
            this.btnDetailClose.Location = new Point(132, 288);
            this.btnDetailClose.Size = new Size(68, 16);
            this.btnDetailClose.ButtonStateChanged += new EventHandler(aCtrl_RefreshCall);
            this.btnDetailClose.MouseClick += new MouseEventHandler(btnDetailClose_MouseClick);

            this.btnHyperStatOpen = new ACtrlButton();
            this.btnHyperStatOpen.Normal = new BitmapOrigin(Resource.Stat_main_BtHyperStatOpen_normal_0);
            this.btnHyperStatOpen.Pressed = new BitmapOrigin(Resource.Stat_main_BtHyperStatOpen_pressed_0);
            this.btnHyperStatOpen.MouseOver = new BitmapOrigin(Resource.Stat_main_BtHyperStatOpen_mouseOver_0);
            this.btnHyperStatOpen.Disabled = new BitmapOrigin(Resource.Stat_main_BtHyperStatOpen_disabled_0);
            this.btnHyperStatOpen.Location = new Point(12, 288);
            this.btnHyperStatOpen.Size = new Size(71, 16);
            this.btnHyperStatOpen.ButtonStateChanged += new EventHandler(aCtrl_RefreshCall);
            this.btnHyperStatOpen.MouseClick += new MouseEventHandler(btnHyperStatOpen_MouseClick);

            this.btnHyperStatClose = new ACtrlButton();
            this.btnHyperStatClose.Normal = new BitmapOrigin(Resource.Stat_main_BtHyperStatClose_normal_0);
            this.btnHyperStatClose.Pressed = new BitmapOrigin(Resource.Stat_main_BtHyperStatClose_pressed_0);
            this.btnHyperStatClose.MouseOver = new BitmapOrigin(Resource.Stat_main_BtHyperStatClose_mouseOver_0);
            this.btnHyperStatClose.Disabled = new BitmapOrigin(Resource.Stat_main_BtHyperStatClose_disabled_0);
            this.btnHyperStatClose.Location = new Point(12, 288);
            this.btnHyperStatClose.Size = new Size(71, 16);
            this.btnHyperStatClose.ButtonStateChanged += new EventHandler(aCtrl_RefreshCall);
            this.btnHyperStatClose.MouseClick += new MouseEventHandler(btnHyperStatClose_MouseClick);

            this.btnAuto = new ACtrlButton();
            this.btnAuto.Normal = new BitmapOrigin(Resource.Stat_main_BtAuto_normal_3);
            this.btnAuto.Pressed = new BitmapOrigin(Resource.Stat_main_BtAuto_pressed_0);
            this.btnAuto.MouseOver = new BitmapOrigin(Resource.Stat_main_BtAuto_mouseOver_0);
            this.btnAuto.Disabled = new BitmapOrigin(Resource.Stat_main_BtAuto_disabled_0);
            this.btnAuto.Location = new Point(128, 162);
            this.btnAuto.Size = new Size(67, 34);
            this.btnAuto.ButtonStateChanged += new EventHandler(aCtrl_RefreshCall);

            this.btnAbility = new ACtrlButton();
            this.btnAbility.Normal = new BitmapOrigin(Resource.Stat_detail_BtAbility_normal_0);
            this.btnAbility.Pressed = new BitmapOrigin(Resource.Stat_detail_BtAbility_pressed_0);
            this.btnAbility.MouseOver = new BitmapOrigin(Resource.Stat_detail_BtAbility_mouseOver_0);
            this.btnAbility.Disabled = new BitmapOrigin(Resource.Stat_detail_BtAbility_disabled_0);
            this.btnAbility.Location = new Point(152, 286);
            this.btnAbility.Size = new Size(50, 16);
            this.btnAbility.ButtonStateChanged += new EventHandler(aCtrl_RefreshCall);

            this.btnHpUp = new ACtrlButton();
            this.btnHpUp.Normal = new BitmapOrigin(Resource.Stat_detail_BtHpUp_normal_0);
            this.btnHpUp.Pressed = new BitmapOrigin(Resource.Stat_detail_BtHpUp_pressed_0);
            this.btnHpUp.MouseOver = new BitmapOrigin(Resource.Stat_detail_BtHpUp_mouseOver_0);
            this.btnHpUp.Disabled = new BitmapOrigin(Resource.Stat_detail_BtHpUp_disabled_0);
            this.btnHpUp.Location = new Point(185, 312);
            this.btnHpUp.Size = new Size(17, 16);
            this.btnHpUp.ButtonStateChanged += new EventHandler(aCtrl_RefreshCall);
            this.btnHpUp.MouseClick += new MouseEventHandler(btnDetailClose_MouseClick);

            this.vScroll = new ACtrlVScroll();

            this.vScroll.PicBase.Normal = new BitmapOrigin(Resource.VScr9_enabled_base);
            this.vScroll.PicBase.Disabled = new BitmapOrigin(Resource.VScr9_disabled_base);

            this.vScroll.BtnPrev.Normal = new BitmapOrigin(Resource.VScr9_enabled_prev0);
            this.vScroll.BtnPrev.Pressed = new BitmapOrigin(Resource.VScr9_enabled_prev1);
            this.vScroll.BtnPrev.MouseOver = new BitmapOrigin(Resource.VScr9_enabled_prev2);
            this.vScroll.BtnPrev.Disabled = new BitmapOrigin(Resource.VScr9_disabled_prev);
            this.vScroll.BtnPrev.Size = this.vScroll.BtnPrev.Normal.Bitmap.Size;
            this.vScroll.BtnPrev.Location = new Point(0, 0);

            this.vScroll.BtnNext.Normal = new BitmapOrigin(Resource.VScr9_enabled_next0);
            this.vScroll.BtnNext.Pressed = new BitmapOrigin(Resource.VScr9_enabled_next1);
            this.vScroll.BtnNext.MouseOver = new BitmapOrigin(Resource.VScr9_enabled_next2);
            this.vScroll.BtnNext.Disabled = new BitmapOrigin(Resource.VScr9_disabled_next);
            this.vScroll.BtnNext.Size = this.vScroll.BtnNext.Normal.Bitmap.Size;
            this.vScroll.BtnNext.Location = new Point(0, 202);

            this.vScroll.BtnThumb.Normal = new BitmapOrigin(Resource.VScr9_enabled_thumb0);
            this.vScroll.BtnThumb.Pressed = new BitmapOrigin(Resource.VScr9_enabled_thumb1);
            this.vScroll.BtnThumb.MouseOver = new BitmapOrigin(Resource.VScr9_enabled_thumb0);
            this.vScroll.BtnThumb.Size = this.vScroll.BtnThumb.Normal.Bitmap.Size;

            this.vScroll.Location = new Point(163, 41);
            this.vScroll.Size = new Size(11, 214);
            this.vScroll.ScrollableLocation = new Point(11, 41);
            this.vScroll.ScrollableSize = new Size(163, 214);
            this.vScroll.ValueChanged += new EventHandler(vScroll_ValueChanged);
            this.vScroll.ChildButtonStateChanged += new EventHandler(aCtrl_RefreshCall);

            this.btnReset = new ACtrlButton();
            this.btnReset.Normal = new BitmapOrigin(Resource.Stat_detail_BtAbility_normal_0);
            this.btnReset.Pressed = new BitmapOrigin(Resource.Stat_detail_BtAbility_pressed_0);
            this.btnReset.MouseOver = new BitmapOrigin(Resource.Stat_detail_BtAbility_mouseOver_0);
            this.btnReset.Disabled = new BitmapOrigin(Resource.Stat_detail_BtAbility_disabled_0);
            this.btnReset.Location = new Point(123, 288);
            this.btnReset.Size = new Size(50, 16);
            this.btnReset.ButtonStateChanged += new EventHandler(aCtrl_RefreshCall);

            this.btnReduce = new ACtrlButton();
            this.btnReduce.Normal = new BitmapOrigin(Resource.HyperStat_Window_BtReduce_normal_0);
            this.btnReduce.Pressed = new BitmapOrigin(Resource.HyperStat_Window_BtReduce_pressed_0);
            this.btnReduce.MouseOver = new BitmapOrigin(Resource.HyperStat_Window_BtReduce_mouseOver_0);
            this.btnReduce.Disabled = new BitmapOrigin(Resource.HyperStat_Window_BtReduce_disabled_0);
            this.btnReduce.Location = new Point(11, 288);
            this.btnReduce.Size = new Size(17, 16);
            this.btnReduce.ButtonStateChanged += new EventHandler(aCtrl_RefreshCall);
            this.btnReduce.MouseClick += new MouseEventHandler(btnHyperStatOpen_MouseClick);
        }

        private IEnumerable<AControl> aControls
        {
            get
            {
                yield return btnHPUp;
                yield return btnMPUp;
                yield return btnStrUp;
                yield return btnDexUp;
                yield return btnIntUp;
                yield return btnLukUp;
                yield return btnAuto;
                yield return btnClose;
                yield return btnDetailOpen;
                yield return btnDetailClose;
                yield return btnHyperStatOpen;
                yield return btnHyperStatClose;
            }
        }

        private IEnumerable<AControl> aDetailControls
        {
            get
            {
                yield return btnAbility;
                yield return btnHpUp;
            }
        }

        private IEnumerable<AControl> aHyperStatControls
        {
            get
            {
                yield return vScroll;
                yield return btnLVUp1;
                yield return btnLVUp2;
                yield return btnLVUp3;
                yield return btnLVUp4;
                yield return btnLVUp5;
                yield return btnLVUp6;
                yield return btnLVUp7;
                yield return btnLVUp8;
                yield return btnLVUp9;
                yield return btnLVUp10;
                yield return btnLVUp11;
                yield return btnLVUp12;
                yield return btnReset;
                yield return btnReduce;
            }
        }

        public override void Refresh()
        {
            this.preRender();
            this.SetBitmap(this.Bitmap);
            this.CaptionRectangle = new Rectangle(this.baseOffset, new Size(Resource.Stat_main_backgrnd.Width, 24));
            this.Location = newLocation;
            base.Refresh();
        }

        protected override bool captionHitTest(Point point)
        {
            Rectangle rect = this.btnClose.Rectangle;
            rect.Offset(this.baseOffset);
            if (rect.Contains(point))
                return false;
            return base.captionHitTest(point);
        }

        private void preRender()
        {
            if (Bitmap != null)
                Bitmap.Dispose();

            setControlState();

            Point baseOffsetnew = calcRenderBaseOffset();
            Size size = Resource.Stat_main_backgrnd.Size;
            size.Width += baseOffsetnew.X;
            if (this.DetailVisible)
                size = new Size(size.Width + Resource.Stat_detail_backgrnd.Width, Resource.Stat_detail_backgrnd.Height);

            this.newLocation = new Point(this.Location.X + this.baseOffset.X - baseOffsetnew.X,
                this.Location.Y + this.baseOffset.Y - baseOffsetnew.Y);
            this.baseOffset = baseOffsetnew;

            //绘制背景
            Bitmap stat = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(stat);
            renderBase(g);
            if (this.DetailVisible)
                renderDetail(g);
            if (this.HyperStatVisible)
                renderHyperStat(g);

            //绘制按钮
            g.TranslateTransform(baseOffset.X, baseOffset.Y);
            foreach (AControl ctrl in this.aControls)
            {
                ctrl.Draw(g);
            }
            g.ResetTransform();

            g.TranslateTransform(this.DetailRect.X, this.DetailRect.Y);
            foreach (AControl ctrl in this.aDetailControls)
            {
                ctrl.Draw(g);
            }
            g.ResetTransform();

            g.TranslateTransform(this.HyperStatRect.X, this.HyperStatRect.Y);
            foreach (AControl ctrl in this.aHyperStatControls)
            {
                ctrl.Draw(g);
            }
            g.ResetTransform();

            g.Dispose();
            this.Bitmap = stat;
        }

        private Point calcRenderBaseOffset()
        {
            if (this.HyperStatVisible)
                return new Point(Resource.HyperStat_Window_backgrnd.Width, 0);
            else
                return new Point(0, 0);
        }

        private void setControlState()
        {
            if (this.DetailVisible)
            {
                this.btnDetailOpen.Visible = false;
                this.btnDetailClose.Visible = true;
                this.btnAbility.Visible = true;
                this.btnHpUp.Visible = true;
            }
            else
            {
                this.btnDetailOpen.Visible = true;
                this.btnDetailClose.Visible = false;
                this.btnAbility.Visible = true;
                this.btnHpUp.Visible = false;
            }

            if (this.HyperStatVisible)
            {
                this.btnHyperStatOpen.Visible = true;
                this.btnHyperStatClose.Visible = false;
                this.vScroll.Visible = true;
                this.vScroll.Maximum = 5;
                this.vScroll.Value = this.HyperStatScrollValue;
                this.btnLVUp1.Visible = true;
                this.btnLVUp2.Visible = true;
                this.btnLVUp3.Visible = true;
                this.btnLVUp4.Visible = true;
                this.btnLVUp5.Visible = true;
                this.btnLVUp6.Visible = true;
                this.btnLVUp7.Visible = true;
                this.btnLVUp8.Visible = true;
                this.btnLVUp9.Visible = true;
                this.btnLVUp10.Visible = true;
                this.btnLVUp11.Visible = true;
                this.btnLVUp12.Visible = true;
                this.btnReset.Visible = true;
                this.btnReduce.Visible = true;
            }
            else
            {
                this.btnHyperStatOpen.Visible = false;
                this.btnHyperStatClose.Visible = true;
                this.vScroll.Visible = false;
                this.btnLVUp1.Visible = false;
                this.btnLVUp2.Visible = false;
                this.btnLVUp3.Visible = false;
                this.btnLVUp4.Visible = false;
                this.btnLVUp5.Visible = false;
                this.btnLVUp6.Visible = false;
                this.btnLVUp7.Visible = false;
                this.btnLVUp8.Visible = false;
                this.btnLVUp9.Visible = false;
                this.btnLVUp10.Visible = false;
                this.btnLVUp11.Visible = false;
                this.btnLVUp12.Visible = false;
                this.btnReset.Visible = false;
                this.btnReduce.Visible = false;
            }

            if (this.character != null)
            {
                CharacterStatus charStat = this.character.Status;
                setButtonEnabled(this.btnHPUp, charStat.Ap > 0 && charStat.MaxHP.BaseVal < charStat.MaxHP.TotalMax);
                setButtonEnabled(this.btnMPUp, charStat.Ap > 0 && charStat.MaxMP.BaseVal < charStat.MaxMP.TotalMax);
                setButtonEnabled(this.btnStrUp, charStat.Ap > 0/* && charStat.Strength.BaseVal <= 999*/);
                setButtonEnabled(this.btnDexUp, charStat.Ap > 0/* && charStat.Dexterity.BaseVal <= 999*/);
                setButtonEnabled(this.btnIntUp, charStat.Ap > 0/* && charStat.Intelligence.BaseVal <= 999*/);
                setButtonEnabled(this.btnLukUp, charStat.Ap > 0/* && charStat.Luck.BaseVal <= 999*/);
                setButtonEnabled(this.btnAuto, charStat.Ap > 0);
            }
            else
            {
                foreach (AControl ctrl in this.aControls)
                {
                    setButtonEnabled(ctrl as ACtrlButton, true);
                }
            }
        }

        private void setButtonEnabled(ACtrlButton button, bool enabled)
        {
            if (button == null)
                return;
            if (enabled)
            {
                if (button.State == ButtonState.Disabled)
                {
                    button.State = ButtonState.Normal;
                }
            }
            else
            {
                if (button.State != ButtonState.Disabled)
                {
                    button.State = ButtonState.Disabled;
                }
            }
        }

        private void renderBase(Graphics g)
        {
            g.TranslateTransform(baseOffset.X, baseOffset.Y);
            g.DrawImage(Resource.Stat_main_backgrnd, 0, 0);
            g.DrawImage(Resource.Stat_main_backgrnd2, 6, 22);
            g.DrawImage(Resource.Stat_main_backgrnd3, 7, 156);

            if (this.character != null)
            {
                CharacterStatus charStat = this.character.Status;
                //绘制自动分配
               // g.DrawImage(charStat.Ap > 0 ? Resource.Stat_main_BtAuto_normal_3 : Resource.Stat_main_BtAuto_disabled_0, 94, 180);
                /*switch (charStat.Job / 100 % 10)//绘制角色属性灰色背景
                {
                    case 0:
                    case 1:
                    case 3:
                    case 5:
                        g.DrawImage(Resource.Stat_main_Disabled_INT, 11, 296);
                        g.DrawImage(Resource.Stat_main_Disabled_LUK, 11, 314);
                        break;
                    case 2:
                        g.DrawImage(Resource.Stat_main_Disabled_STR, 11, 260);
                        g.DrawImage(Resource.Stat_main_Disabled_DEX, 11, 278);
                        break;
                    case 4:
                        g.DrawImage(Resource.Stat_main_Disabled_STR, 11, 260);
                        g.DrawImage(Resource.Stat_main_Disabled_INT, 11, 296);
                        break;
                }*/
                g.DrawString(this.character.Name, GearGraphics.EquipDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 32f);
                g.DrawString(ItemStringHelper.GetJobName(charStat.Job), GearGraphics.EquipDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 50f);
                /*g.DrawString(charStat.Level.ToString(), GearGraphics.ItemDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 68f);
                g.DrawString(charStat.Exp + " (" + (charStat.Exp == -1 ? 0 : ((long)charStat.Exp * 100 / charStat.Exptnl)) + "%)",
                    GearGraphics.ItemDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 86f);
                g.DrawString("1", GearGraphics.ItemDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 104f);
                g.DrawString("0 (0%)", GearGraphics.ItemDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 122f);*/
                g.DrawString(string.IsNullOrEmpty(this.character.Guild) ? "-" : this.character.Guild, GearGraphics.EquipDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 68f);
                g.DrawString(charStat.Pop.ToString(), GearGraphics.EquipDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 86f);
                int brushSign;

                double max, min;
                this.character.CalcAttack(out max, out min, out brushSign);
                g.DrawString(max == 0 ? "0" : Math.Round(min) + " ~ " + Math.Round(max), GearGraphics.EquipDetailFont, getDetailBrush(brushSign), 72f, 104f);
                g.DrawString(charStat.HP + " / " + charStat.MaxHP.GetSum(), GearGraphics.EquipDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 122f);
                g.DrawString(charStat.MP + " / " + charStat.MaxMP.GetSum(), GearGraphics.EquipDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 140f);

                g.DrawString(charStat.Ap.ToString().PadLeft(4), GearGraphics.EquipDetailFont, GearGraphics.StatDetailGrayBrush, 73f, 181f);
                g.DrawString(charStat.Strength.ToString(), GearGraphics.EquipDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 210f);
                g.DrawString(charStat.Dexterity.ToString(), GearGraphics.EquipDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 228f);
                g.DrawString(charStat.Intelligence.ToString(), GearGraphics.EquipDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 246f);
                g.DrawString(charStat.Luck.ToString(), GearGraphics.EquipDetailFont, GearGraphics.StatDetailGrayBrush, 72f, 264f);
            }
            g.ResetTransform();
        }

        private void renderDetail(Graphics g)
        {
            Rectangle rect = this.DetailRect;
            g.TranslateTransform(rect.X, rect.Y);
            g.DrawImage(Resource.Stat_detail_backgrnd, 0, 1);
            g.DrawImage(Resource.Stat_detail_backgrnd2, 7, 8);
            g.DrawImage(Resource.Stat_detail_backgrnd3, 12, 13);
            g.DrawImage(Resource.Stat_detail_backgrnd4, 12, 222);

            g.DrawImage(Resource.Stat_detail_abilityTitle_normal_0, 12, 195);
            g.DrawImage(Resource.Stat_detail_metierLine_disabled_0, 15, 225);
            g.DrawImage(Resource.Stat_detail_metierLine_disabled_1, 15, 244);
            g.DrawImage(Resource.Stat_detail_metierLine_disabled_1, 15, 263);

            if (this.character != null)
            {
                CharacterStatus charStat = this.character.Status;
                //g.DrawString("0 ( 0% )", GearGraphics.GearDetailFont, getDetailBrush(0), 72f, 16f);
                //g.DrawString("0", GearGraphics.GearDetailFont, getDetailBrush(0), 72f, 34f);
                int brushSign;

                double max, min;
                this.character.CalcAttack(out max, out min, out brushSign);
                float y = 26f;
                g.DrawString(max == 0 ? "0" : Math.Round(min) + " ~ " + Math.Round(max), GearGraphics.EquipDetailFont, getDetailBrush(brushSign), 72f, (y += 18f));
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Far;
                g.DrawString(charStat.DamageRate.GetSum() + "%", GearGraphics.EquipDetailFont, charStat.DamageRate.BuffAdd > 0 ? Brushes.Red : GearGraphics.StatDetailGrayBrush, 104f, (y += 18f), format);
                g.DrawString(charStat.BossDamageRate.GetSum() + "%", GearGraphics.EquipDetailFont, charStat.BossDamageRate.BuffAdd > 0 ? Brushes.Red : GearGraphics.StatDetailGrayBrush, 200f, y, format);
                g.DrawString(charStat.FinalDamageRate.GetSum() + "%", GearGraphics.EquipDetailFont, charStat.FinalDamageRate.BuffAdd > 0 ? Brushes.Red : GearGraphics.StatDetailGrayBrush, 104f, (y += 18f), format);
                g.DrawString(charStat.IgnoreMobDefenceRate.GetSum() + "%", GearGraphics.EquipDetailFont, charStat.IgnoreMobDefenceRate.BuffAdd > 0 ? Brushes.Red : GearGraphics.StatDetailGrayBrush, 200f, y, format);
                g.DrawString(charStat.CriticalRate.GetSum() + "%", GearGraphics.EquipDetailFont, charStat.CriticalRate.BuffAdd > 0 ? Brushes.Red : GearGraphics.StatDetailGrayBrush, 104f, (y += 18f), format);
                g.DrawString(charStat.CriticalDamage.GetSum() + ".00%", GearGraphics.EquipDetailFont, charStat.CriticalDamage.BuffAdd > 0 ? Brushes.Red : GearGraphics.StatDetailGrayBrush, 72f, (y += 18f));
                g.DrawString(charStat.StatusResistance.GetSum().ToString(), GearGraphics.EquipDetailFont, charStat.StatusResistance.BuffAdd > 0 ? Brushes.Red : GearGraphics.StatDetailGrayBrush, 103f, (y += 18f), format);
                g.DrawString(charStat.KnockbackResistance.GetSum() + "%", GearGraphics.EquipDetailFont, charStat.KnockbackResistance.BuffAdd > 0 ? Brushes.Red : GearGraphics.StatDetailGrayBrush, 200f, y, format);
                g.DrawString(charStat.PDDamage.ToStringDetail(out brushSign), GearGraphics.EquipDetailFont, getDetailBrush(brushSign), 72f, (y += 18f));
                /*g.DrawString(charStat.MDDamage.ToStringDetail(out brushSign), GearGraphics.ItemDetailFont, getDetailBrush(brushSign), 72f, (y += 18f));
                g.DrawString(charStat.PAccurate.ToStringDetail(out brushSign), GearGraphics.ItemDetailFont, getDetailBrush(brushSign), 72f, (y += 18f));
                g.DrawString(charStat.MAccurate.ToStringDetail(out brushSign), GearGraphics.ItemDetailFont, getDetailBrush(brushSign), 72f, (y += 18f));
                g.DrawString(charStat.PEvasion.ToStringDetail(out brushSign), GearGraphics.ItemDetailFont, getDetailBrush(brushSign), 72f, (y += 18f));
                g.DrawString(charStat.MEvasion.ToStringDetail(out brushSign), GearGraphics.ItemDetailFont, getDetailBrush(brushSign), 72f, (y += 18f));*/
                g.DrawString(charStat.MoveSpeed.GetSum() + "%", GearGraphics.EquipDetailFont, getDetailBrush(0), 104f, (y += 18f), format);
                g.DrawString(charStat.Jump.GetSum() + "%", GearGraphics.EquipDetailFont, getDetailBrush(0), 200f, y, format);
                g.DrawString("0", GearGraphics.EquipDetailFont, getDetailBrush(0), 72f, 289f);
            }

            g.ResetTransform();
        }

        private void renderHyperStat(Graphics g)
        {
            Rectangle rect = this.HyperStatRect;
            g.TranslateTransform(rect.X, rect.Y);
            g.DrawImage(Resource.HyperStat_Window_backgrnd, 0, 0);
            g.DrawImage(Resource.HyperStat_Window_backgrnd2, 6, 7);
            g.DrawImage(Resource.HyperStat_Window_backgrnd3, 11, 41);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Far;
            for (int i = 0; i < 12; i++)
            {
                g.DrawImage(HyperStatList[HyperStatScrollValue + i], 16, 43 + 18 * i);
                g.DrawString("0", GearGraphics.EquipDetailFont, getDetailBrush(0), 139f, 44f + 18f * i, format);
            }
            g.DrawString("0", GearGraphics.EquipDetailFont, getDetailBrush(0), 169f, 269f, format);

            g.ResetTransform();
        }

        private Brush getDetailBrush(int sign)
        {
            switch (sign)
            {
                case 1: return Brushes.Red;
                case -1: return Brushes.Blue;
                case 0:
                default: return GearGraphics.StatDetailGrayBrush;
            }

        }

        private void btnClose_MouseClick(object sender, MouseEventArgs e)
        {
            this.Visible = false;
        }

        private void aCtrl_RefreshCall(object sender, EventArgs e)
        {
            this.waitForRefresh = true;
        }

        private void btnDetailOpen_MouseClick(object sender, MouseEventArgs e)
        {
            this.DetailVisible = true;
            this.waitForRefresh = true;
        }

        private void btnDetailClose_MouseClick(object sender, MouseEventArgs e)
        {
            this.DetailVisible = false;
            this.waitForRefresh = true;
        }

        private void btnHyperStatOpen_MouseClick(object sender, MouseEventArgs e)
        {
            this.HyperStatVisible = false;
            this.waitForRefresh = true;
        }

        private void btnHyperStatClose_MouseClick(object sender, MouseEventArgs e)
        {
            this.HyperStatVisible = true;
            this.waitForRefresh = true;
        }

        private void vScroll_ValueChanged(object sender, EventArgs e)
        {
            this.HyperStatScrollValue = this.vScroll.Value;
            this.waitForRefresh = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            MouseEventArgs childArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - baseOffset.X, e.Y - baseOffset.Y, e.Delta);

            foreach (AControl ctrl in this.aControls)
            {
                ctrl.OnMouseMove(childArgs);
            }

            MouseEventArgs detailChildArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - DetailRect.X, e.Y - DetailRect.Y, e.Delta);

            foreach (AControl ctrl in this.aDetailControls)
            {
                ctrl.OnMouseMove(detailChildArgs);
            }

            MouseEventArgs hyperStatChildArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - HyperStatRect.X, e.Y - HyperStatRect.Y, e.Delta);

            foreach (AControl ctrl in this.aHyperStatControls)
            {
                ctrl.OnMouseMove(hyperStatChildArgs);
            }

            if (this.waitForRefresh)
            {
                this.Refresh();
                waitForRefresh = false;
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            MouseEventArgs childArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - baseOffset.X, e.Y - baseOffset.Y, e.Delta);

            foreach (AControl ctrl in this.aControls)
            {
                ctrl.OnMouseDown(childArgs);
            }

            MouseEventArgs detailChildArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - DetailRect.X, e.Y - DetailRect.Y, e.Delta);

            foreach (AControl ctrl in this.aDetailControls)
            {
                ctrl.OnMouseDown(detailChildArgs);
            }

            MouseEventArgs hyperStatChildArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - HyperStatRect.X, e.Y - HyperStatRect.Y, e.Delta);

            foreach (AControl ctrl in this.aHyperStatControls)
            {
                ctrl.OnMouseDown(hyperStatChildArgs);
            }

            if (this.waitForRefresh)
            {
                this.Refresh();
                waitForRefresh = false;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            MouseEventArgs childArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - baseOffset.X, e.Y - baseOffset.Y, e.Delta);

            foreach (AControl ctrl in this.aControls)
            {
                ctrl.OnMouseUp(childArgs);
            }

            MouseEventArgs detailChildArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - DetailRect.X, e.Y - DetailRect.Y, e.Delta);

            foreach (AControl ctrl in this.aDetailControls)
            {
                ctrl.OnMouseUp(detailChildArgs);
            }

            MouseEventArgs hyperStatChildArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - HyperStatRect.X, e.Y - HyperStatRect.Y, e.Delta);

            foreach (AControl ctrl in this.aHyperStatControls)
            {
                ctrl.OnMouseUp(hyperStatChildArgs);
            }

            if (this.waitForRefresh)
            {
                this.Refresh();
                waitForRefresh = false;
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            MouseEventArgs childArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - baseOffset.X, e.Y - baseOffset.Y, e.Delta);

            foreach (AControl ctrl in this.aControls)
            {
                ctrl.OnMouseClick(childArgs);
            }

            MouseEventArgs detailChildArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - DetailRect.X, e.Y - DetailRect.Y, e.Delta);

            foreach (AControl ctrl in this.aDetailControls)
            {
                ctrl.OnMouseClick(detailChildArgs);
            }

            MouseEventArgs hyperStatChildArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - HyperStatRect.X, e.Y - HyperStatRect.Y, e.Delta);

            foreach (AControl ctrl in this.aHyperStatControls)
            {
                ctrl.OnMouseClick(hyperStatChildArgs);
            }

            if (this.waitForRefresh)
            {
                this.Refresh();
                waitForRefresh = false;
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            MouseEventArgs childArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - baseOffset.X, e.Y - baseOffset.Y, e.Delta);

            foreach (AControl ctrl in this.aControls)
            {
                ctrl.OnMouseWheel(childArgs);
            }

            MouseEventArgs detailChildArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - DetailRect.X, e.Y - DetailRect.Y, e.Delta);

            foreach (AControl ctrl in this.aDetailControls)
            {
                ctrl.OnMouseWheel(detailChildArgs);
            }

            MouseEventArgs hyperStatChildArgs = new MouseEventArgs(e.Button, e.Clicks, e.X - HyperStatRect.X, e.Y - HyperStatRect.Y, e.Delta);

            foreach (AControl ctrl in this.aHyperStatControls)
            {
                ctrl.OnMouseWheel(hyperStatChildArgs);
            }

            if (this.waitForRefresh)
            {
                this.Refresh();
                waitForRefresh = false;
            }

            base.OnMouseWheel(e);
        }
    }
}
