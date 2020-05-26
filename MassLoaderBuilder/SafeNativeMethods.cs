namespace MassLoaderBuilder
{
    using System;
    using System.Runtime.InteropServices;

    internal static partial class SafeNativeMethods
    {
        #region Для AntiDump Защиты 

        [DllImport("kernel32.dll")]
        internal static extern IntPtr ZeroMemory(IntPtr addr, IntPtr size);

        [DllImport("kernel32.dll")]
        internal static extern unsafe bool VirtualProtect(byte* lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        #endregion
    }
}