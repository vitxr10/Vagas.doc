using System.Security.Cryptography;
using System.Text;

namespace VagasDoc.Helper
{
    public static class Cripto
    {
        public static string Encrypt(this string valor)
        {
            var hash = SHA1.Create();
            var enconding = new ASCIIEncoding();
            var array = enconding.GetBytes(valor);

            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}
