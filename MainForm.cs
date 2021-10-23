using System;
//using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
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
            //GetScreenSize(out int ScreenWidth, out int ScreenHeight);
            //this.Size = new Size(ScreenWidth / 5, ScreenHeight / 5);
            //this.MinimumSize = new Size(ScreenWidth / 10, ScreenHeight / 10);
            //this.MaximumSize = new Size(ScreenWidth, ScreenHeight);
        }

        //WndProc
        protected override void WndProc(ref Message m)
        {
            if (ResizeForm)
            {
                if (m.Msg == WM_SIZING)
                {
                    RECT rc = (RECT)Marshal.PtrToStructure(m.LParam, typeof(RECT));

                    int w = rc.Right - rc.Left;
                    int h = rc.Bottom - rc.Top;

                    switch (m.WParam.ToInt32()) // Resize handle
                    {
                        case WMSZ_LEFT:
                        case WMSZ_RIGHT:
                            // Left or right handles, adjust height                        
                            rc.Bottom = rc.Top + (int)(OrininalHeight * w / OrininalWidth);
                            break;

                        case WMSZ_TOP:
                        case WMSZ_BOTTOM:
                            // Top or bottom handles, adjust width
                            rc.Right = rc.Left + (int)(OrininalWidth * h / OrininalHeight);
                            break;
                    }
                    Marshal.StructureToPtr(rc, m.LParam, true);
                }
            }
            base.WndProc(ref m);
        }

        #region 常數及結構
        //Win32 helper 功能
        const int GWL_EXSTYLE = (-20);
        const int WS_EX_LAYERED = 0x00080000;
        const int WS_EX_TRANSPARENT = 0x00000020;
        const int WM_GETICON = 0x7F;
        const int ICON_SMALL = 0;
        const int ICON_BIG = 1;
        const int ICON_SMALL2 = 2;
        const int GCL_HICONSM = (-34);
        const int GCL_HICON = -14;
        //private static readonly int LWA_ALPHA = 0x2;

        //DWM功能
        const int DWM_TNP_VISIBLE = 0x8;
        const int DWM_TNP_OPACITY = 0x4;
        const int DWM_TNP_RECTDESTINATION = 0x1;

        //WndProc功能
        const int WM_SIZING = 0x214;
        const int WM_NCLBUTTONDOWN = 0xA1; //游標位於視窗的非工作區內按下滑鼠左鍵
        const int HT_CAPTION = 2; //在標題列中
        const int HT_LEFT = 10; //在可調整大小之視窗的左框線中 
        const int HT_RIGHT = 11; //在可調整大小之視窗的右框線中 
        const int HT_TOP = 12; //在可調整大小之視窗的上框線中 
        const int HT_BOTTOM = 15; //在可調整大小之視窗的下框線中 
        const int WMSZ_LEFT = 1; //改變視窗左大小
        const int WMSZ_RIGHT = 2; //改變視窗右大小
        const int WMSZ_TOP = 3; //改變視窗頂部大小
        const int WMSZ_BOTTOM = 6; //改變視窗底部大小


        //鼠標位置
        const int _ = 4; //邊距
        Rectangle RectangleCentre { get { return new Rectangle(_, _, this.ClientSize.Width - _ - _, this.ClientSize.Height - _ - _); } }
        Rectangle RectangleTop { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
        Rectangle RectangleLeft { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }
        Rectangle RectangleBottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }
        Rectangle RectangleRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height); } }

        //功能開關
        bool Click_Through_Temp = false;
        bool ResizeForm = false;

        //Window
        IntPtr ItemhWnd;
        double OrininalHeight, OrininalWidth;

        //Thumb
        IntPtr Thumb;
        int LeftAndTop;
        int RightAndBottom;
        internal struct DWM_THUMBNAIL_PROPERTIES
        {
            public int dwFlags;
            public ThumbRect rcDestination;
            public ThumbRect rcSource;
            public byte opacity;
            public bool fVisible;
            public bool fSourceClientAreaOnly;
        }
        internal struct ThumbRect
        {
            internal ThumbRect(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        internal struct ThumbSize
        {
            public int x;
            public int y;
        }
        internal struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        #endregion

        #region Win32 helper 功能
        [DllImport("user32", CharSet = CharSet.Unicode)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32", CharSet = CharSet.Unicode)]
        static extern void GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32")]
        static extern int EnumWindows(EnumWindowsProc lpEnumFunc, int lParam);
        delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        [DllImport("user32")]
        static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32")]
        static extern bool IsImmersiveProcess(IntPtr hWnd);

        [DllImport("user32")]
        static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32")]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32")]
        static extern IntPtr GetShellWindow();

        [DllImport("user32")]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32", SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32", EntryPoint = "GetClassLong")]
        static extern int GetClassLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32", EntryPoint = "GetClassLongPtr")]
        static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32")]
        static extern bool ReleaseCapture();

        //[DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        //static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, int bAlpha, int dwFlags);

        //透明度
        //SetLayeredWindowAttributes(this.Handle, 0, 225, 2);

        //[DllImport("user32", SetLastError = false)]
        //static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

        //取得視窗大小
        //private static void GetWindowSize(IntPtr hWnd, out int Width, out int Height)
        //{
        //    if (hWnd != null)
        //    {
        //        GetWindowRect(hWnd, out Rectangle size);
        //        Width = size.Right - size.Left;
        //        Height = size.Bottom - size.Top;
        //    }
        //    else
        //    {
        //        Width = 1;
        //        Height = 1;
        //    }
        //}

        #endregion

        #region DWM功能
        [DllImport("dwmapi")]
        static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

        [DllImport("dwmapi")]
        static extern int DwmUnregisterThumbnail(IntPtr thumb);

        [DllImport("dwmapi")]
        static extern int DwmQueryThumbnailSourceSize(IntPtr thumb, out ThumbSize size);

        [DllImport("dwmapi")]
        static extern int DwmUpdateThumbnailProperties(IntPtr hThumb, ref DWM_THUMBNAIL_PROPERTIES props);

        #endregion

        #region 副程式
        private static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
            {
                return GetClassLongPtr64(hWnd, nIndex);
            }
            else
            {
                return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
            }
        }

        //點擊穿透
        private void ClickThrough()
        {
            if (Click_Through_Temp == false)
            {
                SetWindowLong(this.Handle, GWL_EXSTYLE, GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT);
                ToolStripMenuItem_ClickThrough.Checked = true;
                Click_Through_Temp = true;
            }
            else
            {
                SetWindowLong(this.Handle, GWL_EXSTYLE, GetWindowLong(this.Handle, GWL_EXSTYLE) & ~WS_EX_LAYERED & ~WS_EX_TRANSPARENT);
                ToolStripMenuItem_ClickThrough.Checked = false;
                Click_Through_Temp = false;
            }
        }

        //置頂
        private void ShowWindow()
        {
            if (this.TopMost == false)
            {
                this.TopMost = true;
                this.WindowState = FormWindowState.Normal;
                this.TopMost = false;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        //取得工作區大小
        private static void GetScreenSize(out int Width,out int Height)
        {
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        //取得Form大小
        private void GetFormSize()
        {
            OrininalWidth = this.Width;
            OrininalHeight = this.Height;
        }

        //取得Icon
        private Icon GetAppIcon(IntPtr hWnd)
        {
            IntPtr iconHandle = SendMessage(hWnd, WM_GETICON, ICON_SMALL2, 0);
            if (iconHandle == IntPtr.Zero) { iconHandle = SendMessage(hWnd, WM_GETICON, ICON_SMALL, 0); }
            if (iconHandle == IntPtr.Zero) { iconHandle = SendMessage(hWnd, WM_GETICON, ICON_BIG, 0); }
            if (iconHandle == IntPtr.Zero) { iconHandle = GetClassLongPtr(hWnd, GCL_HICON); }
            if (iconHandle == IntPtr.Zero) { iconHandle = GetClassLongPtr(hWnd, GCL_HICONSM); }
            if (iconHandle == IntPtr.Zero) { return null; }
            Icon gIicon = Icon.FromHandle(iconHandle);
            return gIicon;
        }

        //取得程式清單
        private void GetWindows()
        {
            FormMenu.Items.Clear();
            FormMenu.Items.Add("--None--");
            EnumWindows(EnumWindowCallback, 0);
            FormMenu.Items.Add("-");
            FormMenu.Items.Add("Exit");
        }

        //加入程式項目
        private bool EnumWindowCallback(IntPtr hWnd, int lParam)
        {
            //項目篩選
            if (this.Handle == hWnd) { return true; }
            if (!IsWindow(hWnd)) { return true; }
            if (IsImmersiveProcess(hWnd)) { return true; }
            if (!IsWindowVisible(hWnd)) { return true; }
            if (IsIconic(hWnd)) { return true; }
            if (hWnd == GetShellWindow()) { return true; }
            if (GetWindowTextLength(hWnd) == 0) { return true; }
            
            //添加項目
            Icon gIcon = GetAppIcon(hWnd);
            StringBuilder sb = new StringBuilder(256);
            GetWindowText(hWnd, sb, sb.Capacity);
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

        //更新thumbnail屬性
        private void UpdateThumb()
        {
            LeftAndTop = 1;
            RightAndBottom = -1;
            if (Thumb != IntPtr.Zero)
            {
                DWM_THUMBNAIL_PROPERTIES props = new DWM_THUMBNAIL_PROPERTIES
                {
                    fVisible = true,
                    dwFlags = DWM_TNP_VISIBLE | DWM_TNP_RECTDESTINATION | DWM_TNP_OPACITY,
                    opacity = 255,
                    rcDestination = new ThumbRect(LeftAndTop, LeftAndTop, this.Width + RightAndBottom, this.Height + RightAndBottom)
                };
                DwmUpdateThumbnailProperties(Thumb, ref props);
            }
        }

        //重設
        private void Reset()
        {
            if (Thumb != IntPtr.Zero)
            {
                DwmUnregisterThumbnail(Thumb);
            }
            ResizeForm = false;
            GetScreenSize(out int ScreenWidth, out int ScreenHeight);
            this.MinimumSize = new Size(ScreenWidth / 10, ScreenHeight / 10);
            this.MaximumSize = new Size(ScreenWidth, ScreenHeight);
            this.Size = new Size(ScreenWidth / 5, ScreenHeight / 5);
        }



        #endregion

        #region 事件
        //視窗移動及調整視窗大小功能
        private void WindowsReplica_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var cursor = this.PointToClient(Cursor.Position);
                if (RectangleCentre.Contains(cursor)) { ReleaseCapture(); SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); }
                else if (RectangleTop.Contains(cursor)) { ReleaseCapture(); SendMessage(Handle, WM_NCLBUTTONDOWN, HT_TOP, 0); }
                else if (RectangleLeft.Contains(cursor)) { ReleaseCapture(); SendMessage(Handle, WM_NCLBUTTONDOWN, HT_LEFT, 0); }
                else if (RectangleRight.Contains(cursor)) { ReleaseCapture(); SendMessage(Handle, WM_NCLBUTTONDOWN, HT_RIGHT, 0); }
                else if (RectangleBottom.Contains(cursor)) { ReleaseCapture(); SendMessage(Handle, WM_NCLBUTTONDOWN, HT_BOTTOM, 0); }
            }
        }

        //視窗鼠標
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

        //視窗大小變更
        private void WindowsReplica_Resize(object sender, EventArgs e)
        {
            UpdateThumb();
            GC.Collect();
        }

        //點擊穿透功能開關
        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            {
                ClickThrough();
            }
            GC.Collect();
        }

        //工作列選單點選
        private void NotifyIconMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch(e.ClickedItem.Name)
            {
                case "ToolStripMenuItem_Exit": //離開
                    GC.Collect();
                    Close();
                    break;

                case "ToolStripMenuItem_Minimized": //縮小
                    this.WindowState = FormWindowState.Minimized;
                    break;

                case "ToolStripMenuItem_Show": //顯示
                    ShowWindow();
                    break;

                case "ToolStripMenuItem_Reset": //重設
                    Reset();
                    break;

                case "ToolStripMenuItem_ClickThrough": //置頂
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
            GC.Collect();
        }

        //程式清單
        private void FormMenu_Opening(object sender, CancelEventArgs e)
        {
            GetWindows();
            GC.Collect();
        }

        //程式清單點選
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
                GC.Collect();
            }
            else
            {
                if (Thumb != IntPtr.Zero) { DwmUnregisterThumbnail(Thumb); }
                ItemhWnd = (IntPtr)e.ClickedItem.Tag;
                int i = DwmRegisterThumbnail(this.Handle, ItemhWnd, out Thumb);
                if (i == 0)
                {
                    ResizeForm = false;
                    DwmQueryThumbnailSourceSize(Thumb, out ThumbSize size);
                    this.MinimumSize = new Size(0, 0);
                    this.MaximumSize = new Size(0, 0);
                    this.Width = size.x / 5;
                    this.Height = size.y / 5;
                    this.MinimumSize = new Size(size.x / 10, size.y / 10);
                    this.MaximumSize = new Size(size.x, size.y);
                    UpdateThumb();
                    GetFormSize();
                    ResizeForm = true;
                }
                GC.Collect();
            }
        }

        #endregion

    }
}
