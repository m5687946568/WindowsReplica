﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsReplica
{
    public partial class WindowsReplica : Form
    {
        public WindowsReplica()
        {
            InitializeComponent();
            Reset();
        }

        IntPtr Thumb;
        int LeftAndTop;
        int RightAndBottom;
        bool ResizeForm = false;
        int OItemHeight, OItemWidth;
        int NFormHeight, NFormWidth;

        #region 取得程式清單
        private void GetWindows()
        {
            FormMenu.Items.Clear();
            FormMenu.Items.Add("--None--");
            Dll_Import.EnumWindows(EnumWindowCallback, 0);
            FormMenu.Items.Add("-");
            FormMenu.Items.Add("Exit");
        }

        //加入程式項目
        private bool EnumWindowCallback(IntPtr hWnd, int lParam)
        {
            //項目篩選
            if (!Function.IsWindowValidForCapture(hWnd)) { return true; }

            //取得ICON
            Icon gIcon = Function.GetAppIcon(hWnd);

            //添加項目
            StringBuilder sb = new StringBuilder(256);
            Dll_Import.GetWindowText(hWnd, sb, sb.Capacity);
            if (gIcon != null)
            {
                ToolStripMenuItem nItem = new ToolStripMenuItem();
                nItem.Text = sb.ToString();
                nItem.Image = gIcon.ToBitmap();
                nItem.Tag = hWnd;
                FormMenu.Items.Add(nItem);
            }
            else
            {
                ToolStripMenuItem nItem = new ToolStripMenuItem();
                nItem.Text = sb.ToString();
                nItem.Tag = hWnd;
                FormMenu.Items.Add(nItem);
            }
            return true;
        }
        #endregion

        #region 點擊穿透
        bool Click_Through_Temp = false;
        private void ClickThrough()
        {
            if (Click_Through_Temp == false)
            {
                Dll_Import.SetWindowLong(this.Handle, (int)Enum.GWL.GWL_EXSTYLE, Dll_Import.GetWindowLong(this.Handle, (int)Enum.GWL.GWL_EXSTYLE) | (long)Enum.WS.WS_EX_LAYERED | (long)Enum.WS.WS_EX_TRANSPARENT);
                ToolStripMenuItem_ClickThrough.Checked = true;
                Click_Through_Temp = true;
            }
            else
            {
                Dll_Import.SetWindowLong(this.Handle, (int)Enum.GWL.GWL_EXSTYLE, Dll_Import.GetWindowLong(this.Handle, (int)Enum.GWL.GWL_EXSTYLE) & ~(long)Enum.WS.WS_EX_LAYERED & ~(long)Enum.WS.WS_EX_TRANSPARENT);
                ToolStripMenuItem_ClickThrough.Checked = false;
                Click_Through_Temp = false;
            }
        }
        #endregion

        #region 重設
        private void Reset()
        {
            if (Thumb != IntPtr.Zero)
            {
                Dll_Import.DwmUnregisterThumbnail(Thumb);
            }
            ResizeForm = false;
            Function.GetScreenSize(out int ScreenWidth, out int ScreenHeight);
            this.MinimumSize = new Size((int)(ScreenWidth * 0.1), (int)(ScreenHeight * 0.1));
            this.MaximumSize = new Size(ScreenWidth, ScreenHeight);
            this.Size = new Size((int)(ScreenWidth * 0.4), (int)(ScreenHeight * 0.4));
            GC.Collect();
        }
        #endregion

        #region 更新thumbnail屬性
        public void UpdateThumb()
        {
            LeftAndTop = 0;
            RightAndBottom = 0;
            if (Thumb != IntPtr.Zero)
            {
                Struct.DWM_THUMBNAIL_PROPERTIES props = new Struct.DWM_THUMBNAIL_PROPERTIES
                {
                    dwFlags = (uint)(Enum.DWM.DWM_TNP_RECTDESTINATION | Enum.DWM.DWM_TNP_OPACITY | Enum.DWM.DWM_TNP_VISIBLE | Enum.DWM.DWM_TNP_SOURCECLIENTAREAONLY),
                    rcDestination = new Struct.ThumbRect(LeftAndTop, LeftAndTop, this.Width + RightAndBottom, this.Height + RightAndBottom),
                    opacity = 255,
                    fVisible = true,
                    fSourceClientAreaOnly = false
                };
                Dll_Import.DwmUpdateThumbnailProperties(Thumb, ref props);
                NFormHeight = this.Height;
                NFormWidth = this.Width;
            }
        }
        #endregion

        #region 改變視窗大小
        //WndProc
        protected override void WndProc(ref Message m)
        {
            if (ResizeForm)
            {
                if (m.Msg == (int)Enum.WM.WM_SIZING)
                {
                    Struct.RECT rc = (Struct.RECT)Marshal.PtrToStructure(m.LParam, typeof(Struct.RECT));
                    switch (m.WParam.ToInt32()) // Resize handle
                    {
                        case (int)Enum.WMSZ.WMSZ_LEFT:
                        case (int)Enum.WMSZ.WMSZ_RIGHT:
                            // Left or right handles, adjust height                        
                            rc.Bottom = rc.Top + (int)(OItemHeight * NFormWidth / OItemWidth);
                            UpdateThumb();
                            break;

                        case (int)Enum.WMSZ.WMSZ_TOP:
                        case (int)Enum.WMSZ.WMSZ_BOTTOM:
                            // Top or bottom handles, adjust width
                            rc.Right = rc.Left + (int)(OItemWidth * NFormHeight / OItemHeight);
                            UpdateThumb();
                            break;
                    }
                    Marshal.StructureToPtr(rc, m.LParam, true);
                }
            }
            base.WndProc(ref m);
        }

        const int _ = 5; //邊距
        Rectangle RectangleCentre { get { return new Rectangle(_, _, this.ClientSize.Width - _ - _, this.ClientSize.Height - _ - _); } }
        Rectangle RectangleTop { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
        Rectangle RectangleLeft { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }
        Rectangle RectangleBottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }
        Rectangle RectangleRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height); } }

        //事件_滑鼠按下
        private void WindowsReplica_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var cursor = this.PointToClient(Cursor.Position);
                if (RectangleCentre.Contains(cursor)) { Dll_Import.ReleaseCapture(); Dll_Import.SendMessage(Handle, (uint)Enum.WM.WM_NCLBUTTONDOWN, (int)Enum.HT.HT_CAPTION, 0); }
                else if (RectangleTop.Contains(cursor)) { Dll_Import.ReleaseCapture(); Dll_Import.SendMessage(Handle, (uint)Enum.WM.WM_NCLBUTTONDOWN, (int)Enum.HT.HT_TOP, 0); }
                else if (RectangleLeft.Contains(cursor)) { Dll_Import.ReleaseCapture(); Dll_Import.SendMessage(Handle, (uint)Enum.WM.WM_NCLBUTTONDOWN, (int)Enum.HT.HT_LEFT, 0); }
                else if (RectangleRight.Contains(cursor)) { Dll_Import.ReleaseCapture(); Dll_Import.SendMessage(Handle, (uint)Enum.WM.WM_NCLBUTTONDOWN, (int)Enum.HT.HT_RIGHT, 0); }
                else if (RectangleBottom.Contains(cursor)) { Dll_Import.ReleaseCapture(); Dll_Import.SendMessage(Handle, (uint)Enum.WM.WM_NCLBUTTONDOWN, (int)Enum.HT.HT_BOTTOM, 0); }
            }
        }

        //事件_滑鼠移動
        private void WindowsReplica_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X <= _ || e.X + _ >= this.Width)
            {
                this.Cursor = Cursors.SizeWE;
            }
            else if (e.Y <= _ || e.Y + _ >= this.Height)
            {
                this.Cursor = Cursors.SizeNS;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region 事件_工具列圖示點擊
        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            {
                ClickThrough();
            }
        }
        #endregion

        #region 事件_工作列選單點選
        private void NotifyIconMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch(e.ClickedItem.Name)
            {
                case "ToolStripMenuItem_Exit": //離開
                    GC.Collect();
                    Close();
                    break;

                case "ToolStripMenuItem_Hide": //隱藏
                    this.Hide();
                    break;

                case "ToolStripMenuItem_Show": //顯示
                    this.Show();
                    if (this.TopMost == false)
                    {
                        this.TopMost = true;
                        this.TopMost = false;
                    }
                    else
                    {
                        this.TopMost = false;
                        this.TopMost = true;
                    }
                    break;

                case "ToolStripMenuItem_Reset": //重設
                    Reset();
                    break;

                case "ToolStripMenuItem_ClickThrough": //點擊穿透
                    ClickThrough();
                    break;

                case "ToolStripMenuItem_OnTop": //置頂
                    if (this.TopMost == true)
                    {
                        this.TopMost = false;
                        ToolStripMenuItem_OnTop.Checked = false;
                    }
                    else
                    {
                        this.TopMost = true;
                        ToolStripMenuItem_OnTop.Checked = true;
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region 事件_開啟Form選單
        private void FormMenu_Opening(object sender, CancelEventArgs e)
        {
            GetWindows();
            GC.Collect();
        }
        #endregion

        #region 事件_Form選單點選
        private void FormMenu_ItemClick(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Exit")
            {
                GC.Collect();
                Close();
            }
            else if (e.ClickedItem.Text == "--None--")
            {
                Reset();
            }
            else
            {
                Reset();
                IntPtr ItemhWnd = (IntPtr)e.ClickedItem.Tag;
                int i = Dll_Import.DwmRegisterThumbnail(this.Handle, ItemhWnd, out Thumb);
                if (i == 0)
                {
                    Dll_Import.DwmQueryThumbnailSourceSize(Thumb, out Struct.ThumbSize CheckItemSize);
                    OItemWidth = CheckItemSize.x;
                    OItemHeight = CheckItemSize.y;
                    this.MinimumSize = new Size((int)(OItemWidth * 0.1), (int)(OItemHeight * 0.1));
                    this.MaximumSize = new Size(OItemWidth, OItemHeight);
                    this.Size = new Size((int)(OItemWidth * 0.4), (int)(OItemHeight * 0.4));
                    UpdateThumb();
                }
                ResizeForm = true;
                GC.Collect();
            }
        }
        #endregion


    }
}
