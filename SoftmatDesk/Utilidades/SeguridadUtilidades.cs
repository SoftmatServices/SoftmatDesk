using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionPersonalizada.Utilidades
{
   public class SeguridadUtilidades
    {
       public static String GetSha1(String texto)
       {
           var sha = SHA1.Create();
            UTF8Encoding encoding=new UTF8Encoding();
           byte[] datos;
            StringBuilder builder=new StringBuilder();
           datos = sha.ComputeHash(encoding.GetBytes(texto));
           for (int i = 0; i < datos.Length; i++)
           {
               builder.AppendFormat("{0:x2}", datos[i]);
           }

           return builder.ToString();
       }

       public static byte[] GetKey(String txt)
       {
            

           return new PasswordDeriveBytes(txt,null).GetBytes(32);
       }
       public static string Cifrar(String contenido, String clave)
       {
           var encoding=new UTF8Encoding();
           var cripto=new RijndaelManaged();
            
           byte[] cifrado;
           byte[] retorno;
           byte[] key=GetKey(clave);

           cripto.Key = key;
           cripto.GenerateIV();
           byte[] aEncriptar = encoding.GetBytes(contenido);

           cifrado = cripto.CreateEncryptor().
                TransformFinalBlock(aEncriptar, 0, aEncriptar.Length);

            retorno=new byte[cripto.IV.Length + cifrado.Length];

            cripto.IV.CopyTo(retorno,0);
            cifrado.CopyTo(retorno,cripto.IV.Length);

           return Convert.ToBase64String(retorno);
       }
        public static String DesCifrar(byte[] contenido, String clave)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            var cripto=new RijndaelManaged();
            var ivTemp=new byte[cripto.IV.Length];
            
            var key = GetKey(clave);
            var cifrado=new byte[contenido.Length-ivTemp.Length];

            cripto.Key = key;

            Array.Copy(contenido,ivTemp,ivTemp.Length);
            Array.Copy(contenido,ivTemp.Length,cifrado,0,cifrado.Length);

            cripto.IV = ivTemp;
            var descifrado = cripto.CreateDecryptor().
                TransformFinalBlock(cifrado, 0, cifrado.Length);
            return encoding.GetString(descifrado);
        }


    }
}
