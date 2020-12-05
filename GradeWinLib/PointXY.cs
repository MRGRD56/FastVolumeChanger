namespace GradeWinLib
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct PointXY
    {
        public int X;
        public int Y;
        
        public PointXY(int x, int y)
        {
            (X, Y) = (x, y);
        }

        public static bool operator ==(PointXY p1, PointXY p2) => 
            p1.X == p2.X && p1.Y == p2.Y;

        public static bool operator !=(PointXY p1, PointXY p2) => 
            !(p1 == p2);
    }

}
