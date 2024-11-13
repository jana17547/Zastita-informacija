using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        ////////////////////////////////////////////////////

        public string CryptBifid(string source, string key)
        {
            Bifid bf = new Bifid(key);
            string res = bf.Encrypt(source);
            return res;
        }


        public string DecryptBifid(string source, string key)
        {
            Bifid bf = new Bifid(key);
            string res = bf.Decrypt(source);
            return res;
        }


        public byte[] CryptRC6(string source)
        {
            byte[] key = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
            RC6 fsc = new RC6(128,key);
            return fsc.EncodeRc6(source);
            
        }

        public byte[] DecryptRC6(byte[] source)
        {
            byte[] key = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            RC6 fsc = new RC6(128, key);
            return fsc.DecodeRc6(source);
        }

        public string EncryptKS(string source)
        {
            int[] priv = { 2, 3, 7, 14, 30, 57, 120, 251 };
            int mod = 351;

            // Public key
            int[] pub = new int[priv.Length];
            for (int i = 0; i < priv.Length; i++)
            {
                pub[i] = priv[i] % mod;
            }
            KnapSack ks = new KnapSack();

            string message = source;
            int[] messageAsInts = message.Select(c => (int)c).ToArray();
            int[] ciphertext = ks.Encrypt(messageAsInts,pub);
            string ciphertextAsString = string.Join(",", ciphertext);
            return ciphertextAsString;
            



        }

        public string DecryptKS(string source)
        {

            int[] priv = { 2, 3, 7, 14, 30, 57, 120, 251 };
            int mod = 351;
            KnapSack ks = new KnapSack();
            string[] ciphertextAsStringArray = source.Split(',');
            int[] ciphertextAsInts = Array.ConvertAll(ciphertextAsStringArray, int.Parse);
            int[] message = ks.Decrypt(ciphertextAsInts,priv, mod);
            string messageAsString = new string(Array.ConvertAll(message, (int i) => (char)i));
            return messageAsString;
            

        }

        public string EncDecKS(string source, int p)
        {
            KnapSack ks = new KnapSack();
            // SuperIncreasing knapsack
            int[] priv = { 2, 3, 7, 14, 30, 57, 120, 251 };
            int mod = 351;

            // Public key
            int[] pub = new int[priv.Length];
            for (int i = 0; i < priv.Length; i++)
            {
                pub[i] = priv[i] % mod;
            }

            //Console.Write("Enter a message to encrypt: ");
            string message = source;

            // convert message to a sequence of numbers
            int[] m = message.Select(c => (int)c).ToArray();

            // encrypt the message
            
            //Console.Write("Encrypted message: ");
            string res=null;
            if (p == 0)
            {
                int[] a = ks.Encrypt(m, pub);
                res = string.Join(" ", a);
            }

            int[] decrypted = ks.Decrypt(source.Select(c => (int)c).ToArray(), priv, mod);
            //Console.Write("Decrypted message: ");
            if(p == 1)
                res = string.Join("", decrypted.Select(x => (char)x));

         return res;

        }

        public byte[] EncryptCTR(byte[] source)
        {
            CTR c = new CTR();
            return c.Encrypt(source);
        }

        public byte[] DecryptCTR(byte[] source)
        {
            CTR c = new CTR();
            return c.Decrypt(source);
        }

        public byte[] TH(string fInfo1)
        {
            TigerHash th = new TigerHash();
            return th.ComputeHash(fInfo1);

        }

    }


}
