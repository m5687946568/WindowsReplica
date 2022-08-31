using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsReplica
{
    class Function
    {
        #region 工作區大小
        public static void GetScreenSize(out int Width, out int Height)
        {
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
        }
        #endregion

        #region 項目篩選
        public static bool IsWindowValidForCapture(IntPtr hWnd)
        {
            if (hWnd.ToInt32() == 0) { return false; }
            if (hWnd == Dll_Import.GetShellWindow()) { return false; }
            if (!Dll_Import.IsWindow(hWnd)) { return false; }
            if (!Dll_Import.IsWindowVisible(hWnd)) { return false; }
            if (Dll_Import.GetAncestor(hWnd, Enum.GetAncestorFlags.GetRoot) != hWnd) { return false; }
            if (Process.GetCurrentProcess().MainWindowHandle == hWnd) { return false; }
            if (Dll_Import.GetWindowTextLength(hWnd) == 0) { return false; }
            if (CkeckBackgroundAppWindow(hWnd)) { return false; }
            return true;
        }

        static bool CkeckBackgroundAppWindow(IntPtr hWnd)
        {
            var cloaked = false;
            Dll_Import.DwmGetWindowAttribute(hWnd, Enum.DWMWINDOWATTRIBUTE.Cloaked, out cloaked, 4);
            return cloaked;
        }
        #endregion

        #region 取得Icon
        public static Icon GetAppIcon(IntPtr hWnd)
        {
            IntPtr iconHandle = Dll_Import.SendMessage(hWnd, (uint)Enum.WM.WM_GETICON, (int)Enum.Icon.ICON_SMALL2, 0);
            if (iconHandle == IntPtr.Zero) { iconHandle = Dll_Import.SendMessage(hWnd, (uint)Enum.WM.WM_GETICON, (int)Enum.Icon.ICON_SMALL, 0); }
            if (iconHandle == IntPtr.Zero) { iconHandle = Dll_Import.SendMessage(hWnd, (uint)Enum.WM.WM_GETICON, (int)Enum.Icon.ICON_BIG, 0); }
            if (iconHandle == IntPtr.Zero) { iconHandle = Dll_Import.GetWindowLongPtr(hWnd, (int)Enum.GCL.GCL_HICON); }
            if (iconHandle == IntPtr.Zero) { iconHandle = Dll_Import.GetWindowLongPtr(hWnd, (int)Enum.GCL.GCL_HICONSM); }
            if (iconHandle == IntPtr.Zero) { return null; }
            Icon gIicon = Icon.FromHandle(iconHandle);
            return gIicon;
        }
        #endregion


    }
}
