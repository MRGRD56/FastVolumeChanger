using System;
using System.Runtime.InteropServices;

namespace GradeWinLib
{
    public static class Screen
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out PointXY lpPoint);

        public static PointXY CursorPosition
        {
            get
            {
                PointXY p;
                if (GetCursorPos(out p))
                {
                    return p;
                }
                
                return new PointXY(0, 0);
            }
        }
    }
}
