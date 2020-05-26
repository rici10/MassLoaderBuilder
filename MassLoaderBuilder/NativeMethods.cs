namespace MassLoaderBuilder
{
    #region References

    using System;
    using System.Runtime.InteropServices;

    #endregion

    internal static partial class NativeMethods
    {
        #region Для удаления фокуса с кнопок

        [DllImport("user32.dll")]
        internal extern static IntPtr SetFocus(IntPtr hWnd);

        #endregion
    }
}