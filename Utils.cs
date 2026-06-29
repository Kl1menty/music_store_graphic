using System;

namespace MusicStoreWPF
{
    public static class Utils
    {
        public static bool IsPositiveInt(string text, out int result)
        {
            if (int.TryParse(text, out result))
            {
                return result > 0;
            }
            return false;
        }

        public static bool IsPositiveDouble(string text, out double result)
        {
            if (double.TryParse(text, out result))
            {
                return result > 0;
            }
            return false;
        }
    }
}