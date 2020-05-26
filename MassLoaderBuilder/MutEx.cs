namespace MassLoaderBuilder
{
    using System.Reflection;
    using System.Runtime.InteropServices;

    /*
        Created by r3xq1
        https://github.com/r3xq1/
        Telegram: https://t.me/r3xq1
    */

    public static class MutEx
    {
        public static string GetGUID()
        {
            string result;
            try
            {
                Assembly assembly = typeof(Program).Assembly;
                var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
                result = attribute.Value;
            }
            catch { result = "CF2D4313-33DE-489D-9721-6AFF69841DEA"; }
            return result?.ToUpper();
        }
    }
}