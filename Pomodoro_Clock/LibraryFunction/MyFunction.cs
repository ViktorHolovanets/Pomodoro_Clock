using System;
using System.IO;
using System.Runtime.InteropServices;

namespace LibraryFunction
{
    public class MyFunction
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int CloseWindow(IntPtr hWnd);



        public static void SetFocusWindow(string WindowTitle)
        {
            IntPtr bl = FindWindowByCaption(IntPtr.Zero, WindowTitle);
            if (bl == IntPtr.Zero) return;
            SetWindowPos(bl, IntPtr.Zero, 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0040);
            SetForegroundWindow(bl);
            SetFocus(bl);
            SetActiveWindow(bl);
        }
        //

        public static string StringConnection(string Path)
        {
            var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"..\\..\\{Path}");
            FileInfo fileInfo = new FileInfo(path);
            return $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={fileInfo.FullName};Integrated Security=True; MultipleActiveResultSets=True";
        }
    }
}
