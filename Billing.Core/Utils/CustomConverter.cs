using System;
using System.Globalization;

namespace Billing.Core.Utils
{
    public class CustomConverter
    {
        public static string ToHexaStr(byte[] buffer)
        {
            return BitConverter.ToString(buffer).Replace("-", "");
        }

        public static string Encodeb64ToHex(string base64String)
        {
            return ToHexaStr(Convert.FromBase64String(base64String));
        }

        public static string DecodeB64FromHex(string hexaString)
        {
            return Convert.ToBase64String(Convert.FromHexString(hexaString));
        }

        /// <summary>
        /// ToGuid - Parse string to guid
        /// key = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Guid ToGuid(string key)
        {
            return Guid.Parse(key);
        }

        public static string GuidToStr(Guid key)
        {
            return key.ToString("D", CultureInfo.InvariantCulture);
        }

        public static string NewGuid()
        {
            return Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture);
        }
    }
}
