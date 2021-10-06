using System;
using System.Runtime.InteropServices;

namespace getDiscordDefaultAvatar
{
    public static class NativeStuff
    {
        public enum StdHandle
        {
            Input = -10,
            Output = -11,
            Error = -12,
        }

        public enum ConsoleMode
        {
            ENABLE_ECHO_INPUT = 4
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(StdHandle nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out int lpMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int dwMode);
    }
}
