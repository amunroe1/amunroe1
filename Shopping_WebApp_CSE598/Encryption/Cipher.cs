using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEncryption
{
    public class Cipher
    {
        public static string Encrypt(string input)
        {
            // encrypt with a caesar cipher (shift each letter by three)
            StringBuilder encrypted = new StringBuilder();
            foreach (char c in input)
            {
                encrypted.Append((char)(c + 3));
            }
            return encrypted.ToString();
        }

        public static string Decrypt(string input)
        {
            // Undo the simple encryption logic
            StringBuilder decrypted = new StringBuilder();
            foreach (char c in input)
            {
                decrypted.Append((char)(c - 3));
            }
            return decrypted.ToString();
        }
    }
}
