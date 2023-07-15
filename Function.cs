using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


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
            if (hWnd == Dll_Import.GetShellWindow()) { return false; }
            if (!Dll_Import.IsWindowVisible(hWnd)) { return false; }
            if (Dll_Import.GetAncestor(hWnd, Enum.GetAncestorFlags.GetRoot) != hWnd) { return false; }
            if (Dll_Import.GetWindowTextLength(hWnd) == 0) { return false; }
            if (CkeckBackgroundAppWindow(hWnd)) { return false; }
            return true;
        }

        static bool CkeckBackgroundAppWindow(IntPtr hWnd)
        {
            Dll_Import.DwmGetWindowAttribute(hWnd, Enum.DWMWINDOWATTRIBUTE.Cloaked, out int cloaked, Marshal.SizeOf(typeof(bool)));
            if (cloaked == (int)Enum.DWM.DWM_CLOAKED_APP)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 取得Icon
        public static Icon GetAppIcon(IntPtr hWnd)
        {
            try
            {
                IntPtr iconHandle = Dll_Import.SendMessage(hWnd, (uint)Enum.WM.WM_GETICON, (int)Enum.Icon.ICON_SMALL2, 0);
                if (iconHandle == IntPtr.Zero) { iconHandle = Dll_Import.SendMessage(hWnd, (uint)Enum.WM.WM_GETICON, (int)Enum.Icon.ICON_SMALL, 0); }
                if (iconHandle == IntPtr.Zero) { iconHandle = Dll_Import.SendMessage(hWnd, (uint)Enum.WM.WM_GETICON, (int)Enum.Icon.ICON_BIG, 0); }
                if (iconHandle == IntPtr.Zero) { iconHandle = Dll_Import.GetClassLongPtr(hWnd, (int)Enum.GCL.GCLP_HICON); }
                if (iconHandle == IntPtr.Zero) { iconHandle = Dll_Import.GetClassLongPtr(hWnd, (int)Enum.GCL.GCLP_HICONSM); }
                if (iconHandle == IntPtr.Zero) { iconHandle = Dll_Import.LoadIcon(IntPtr.Zero, (IntPtr)Enum.IDI.IDI_APPLICATION); }
                if (iconHandle != IntPtr.Zero)
                {
                    Icon gIcon = Icon.FromHandle(iconHandle);
                    return gIcon;
                }
                else
                { 
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

    }
}
