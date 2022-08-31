using System;

namespace WindowsReplica
{
    class Struct
    {
        public struct DWM_THUMBNAIL_PROPERTIES
        {
            public uint dwFlags;
            public ThumbRect rcDestination;
            public ThumbRect rcSource;
            public byte opacity;
            public bool fVisible;
            public bool fSourceClientAreaOnly;
        }

        public struct ThumbRect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public ThumbRect(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }
        }

        public struct ThumbSize
        {
            public int x;
            public int y;
        }

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}
