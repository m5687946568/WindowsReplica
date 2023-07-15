using System;

namespace WindowsReplica
{
    class Enum
    {
        public enum GetAncestorFlags
        {
            // Retrieves the parent window. This does not include the owner, as it does with the GetParent function.
            GetParent = 1,
            // Retrieves the root window by walking the chain of parent windows.
            GetRoot = 2,
            // Retrieves the owned root window by walking the chain of parent and owner windows returned by GetParent.
            GetRootOwner = 3
        }

        public enum GWL
        {
            GWL_WNDPROC = (-4),
            GWL_HINSTANCE = (-6),
            GWL_HWNDPARENT = (-8),
            GWL_STYLE = (-16),
            GWL_EXSTYLE = (-20),
            GWL_USERDATA = (-21),
            GWL_ID = (-12)
        }

        public enum WS : uint
        {
            WS_BORDER = 0x800000,
            WS_CAPTION = 0xc00000,
            WS_CHILD = 0x40000000,
            WS_CLIPCHILDREN = 0x2000000,
            WS_CLIPSIBLINGS = 0x4000000,
            WS_DISABLED = 0x8000000,
            WS_DLGFRAME = 0x400000,
            WS_GROUP = 0x20000,
            WS_HSCROLL = 0x100000,
            WS_MAXIMIZE = 0x1000000,
            WS_MAXIMIZEBOX = 0x10000,
            WS_MINIMIZE = 0x20000000,
            WS_MINIMIZEBOX = 0x20000,
            WS_OVERLAPPED = 0x0,
            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_POPUP = 0x80000000u,
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
            WS_SIZEFRAME = 0x40000,
            WS_SYSMENU = 0x80000,
            WS_TABSTOP = 0x10000,
            WS_VISIBLE = 0x10000000,
            WS_VSCROLL = 0x200000,
            WS_EX_TRANSPARENT = 0x00000020,
            WS_EX_LAYERED = 0x00080000,
            WS_EX_TOOLWINDOW = 0x00000080
        }

        public enum WM : uint
        {
            WM_GETICON = 0x0000007F,
            WM_SIZING = 0x214,
            WM_NCLBUTTONDOWN = 0xA1 //游標位於視窗的非工作區內按下滑鼠左鍵
        }

        public enum WMSZ : uint
        {
            WMSZ_LEFT = 1, //改變視窗左大小
            WMSZ_RIGHT = 2, //改變視窗右大小
            WMSZ_TOP = 3, //改變視窗頂部大小
            WMSZ_BOTTOM = 6 //改變視窗底部大小
    }

        public enum HT : uint
        {
            HT_CAPTION = 2, //在標題列中
            HT_LEFT = 10, //在可調整大小之視窗的左框線中 
            HT_RIGHT = 11, //在可調整大小之視窗的右框線中 
            HT_TOP = 12, //在可調整大小之視窗的上框線中 
            HT_BOTTOM = 15 //在可調整大小之視窗的下框線中 
        }

        public enum DWM : uint
        {
            DWM_TNP_RECTDESTINATION = 0x00000001,
            DWM_TNP_RECTSOURCE = 0x00000002,
            DWM_TNP_OPACITY = 0x00000004,
            DWM_TNP_VISIBLE = 0x00000008,
            DWM_TNP_SOURCECLIENTAREAONLY = 0x00000010,
            DWM_CLOAKED_APP = 0x0000001,
            DWM_CLOAKED_SHELL = 0x0000002,
            DWM_CLOAKED_INHERITED = 0x0000004
        }

        public enum Icon : uint
        {
            ICON_SMALL = 0,
            ICON_BIG = 1,
            ICON_SMALL2 = 2
        }

        public enum GCL : int
        {
            GCLP_HICONSM = (-34),
            GCLP_HICON = (-14)
        }

        public enum IDI : uint
        {
            IDI_APPLICATION = 32512
        }

        public enum DWMWINDOWATTRIBUTE : uint
        {
            NCRenderingEnabled = 1,
            NCRenderingPolicy,
            TransitionsForceDisabled,
            AllowNCPaint,
            CaptionButtonBounds,
            NonClientRtlLayout,
            ForceIconicRepresentation,
            Flip3DPolicy,
            ExtendedFrameBounds,
            HasIconicBitmap,
            DisallowPeek,
            ExcludedFromPeek,
            Cloak,
            Cloaked,
            FreezeRepresentation
        }

    }
}
