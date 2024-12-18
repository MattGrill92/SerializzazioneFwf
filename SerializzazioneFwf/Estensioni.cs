using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SerializzazioneFwf
{
    internal static class Estensioni
    {
        public static void ReplaceAt(this char[] sb, int index, int size, string str, char? filler = null)
        {
            for (int i = 0, pos = index; i < size; i++, pos++)
            {
                if (pos >= sb.Length)
                    return;

                sb[pos] = !filler.HasValue || i < str.Length
                    ? str[i]
                    : filler.Value;
            }
        }

        public static void Populate<T>(this T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = value;
            }
        }
    }
}
