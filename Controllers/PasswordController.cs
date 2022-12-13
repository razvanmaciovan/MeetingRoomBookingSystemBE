using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Locus.Controllers
{
    internal class PasswordController
    {
        // saltSize = 16
        // hashSize = 20
        // iterations = 10000
        public static string Hash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            var hash = pbkdf2.GetBytes(20);

            // combine salt and hash
            var hashBytes = new byte[16 + 20];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            return String.Format("$MAYA$V1${0}${1}", 10000, base64Hash);
        }

        public static bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$MAYA$V1$");
        }

        public static bool Verify(string password, string hashedPassword)
        {
            // check hash
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("Hash is not supported!");
            }

            var splittedHashString = hashedPassword.Replace("$MAYA$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // get salt
            var salt = new Byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(20);

            // get result
            for (var index = 0; index < 20; index++)
            {
                if (hashBytes[index + 16] != hash[index])
                {
                    return false;
                }
            }

            return true;
        }

        static void Main(string[] args)
        {
            string password = "parola123";
            string typedPassword_ok = "parola123";
            string typedPassword_nok = "123paroal";

            var hash = Hash(password);
            var result = Verify(typedPassword_ok, hash);

            if (result)
            {
                Console.WriteLine("CORRECT PASSWORD!");
            }
            else
            {
                Console.WriteLine("INCORRECT PASSWORD!");
            }

            Console.Write("Press  to exit... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                //run loop until Enter is press
            }
        }
    }
}
