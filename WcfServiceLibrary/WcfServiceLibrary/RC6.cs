using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    public class RC6
    {
        private const int R = 20; // broj krugova
        private static uint[] RoundKey = new uint[2 * R + 4];  // okrugli kljuc
        private const int W = 32; // dužina mašinske reči u bitovima
        private static byte[] MainKey; // kljuc
        private const uint P32 = 0xB7E15163; // eksponentne konstante zlatnog preseka
        private const uint Q32 = 0x9E3779B9;
        /*Generisanje kljuca*/
        //Коnstruktor za generisanje kljuca
        public RC6(int keyLong)
        {
            GenerateKey(keyLong, null);
        }
        
        public RC6(int keyLong, byte[] key)
        {
            GenerateKey(keyLong, key);
        }
        
        private static uint RightShift(uint value, int shift)
        {
            return (value >> shift) | (value << (W - shift));
        }
        
        private static uint LeftShift(uint value, int shift)
        {
            return (value << shift) | (value >> (W - shift));
        }
        //Generisanje glavnog kljuca i okruglog kljuca
        private static void GenerateKey(int Long, byte[] keyCheck)
        {
            //Ako glavni ključ nije unapred podešen, koristi se generator slučajnih ključeva
            if (keyCheck == null)
            {
                AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider //ключи самому генерировать не очень
                {
                    
                    KeySize = Long
                };
                aesCrypto.GenerateKey();
                MainKey = aesCrypto.Key;
            }
            else MainKey = keyCheck;
            int c = 0;
            int i, j;
            //U zavisnosti od veličine ključa, izabere se na koliko blokova ce se podeliti glavni ključ
            switch (Long)
            {
                case 128:
                    c = 4; 
                    break;
                case 192: 
                    c = 6; 
                    break;
                case 256: 
                    c = 8; 
                    break;
            }
            uint[] L = new uint[c];
            for (i = 0; i < c; i++)
            {
                L[i] = BitConverter.ToUInt32(MainKey, i * 4); // razviti kljuc u reci
            }
            //Generisanje okruglic kljuceva
            RoundKey[0] = P32;
            for (i = 1; i < 2 * R + 4; i++)
                RoundKey[i] = RoundKey[i - 1] + Q32; // dodavanje konstante okruglom kljucu
            uint A, B; // registri
            A = B = 0;
            i = j = 0;
            int V = 3 * Math.Max(c, 2 * R + 4);  // маksimalno krugova ili broj reci u kljucu
            for (int s = 1; s <= V; s++)
            {
                A = RoundKey[i] = LeftShift((RoundKey[i] + A + B), 3); // pomeraj levo za 3
                B = L[j] = LeftShift((L[j] + A + B), (int)(A + B)); // pomeraj levo za A+B
                i = (i + 1) % (2 * R + 4);
                j = (j + 1) % c;
            }
        }
        // niz bajtova
        private static byte[] ToArrayBytes(uint[] uints, int Long)
        {
            byte[] arrayBytes = new byte[Long * 4];
            for (int i = 0; i < Long; i++)
            {
                byte[] temp = BitConverter.GetBytes(uints[i]);
                temp.CopyTo(arrayBytes, i * 4);
            }
            return arrayBytes;
        }
        public byte[] EncodeRc6(string plaintext)
        {
            uint A, B, C, D;
            //Pretvaranje primljenog teksta u niz bajtova
            byte[] byteText = Encoding.UTF8.GetBytes(plaintext);
            int i = byteText.Length;
            while (i % 16 != 0)
                i++;
            //Kreiramo novi niz, čiji je višestruki broj 16, pošto algoritam opisuje rad sa četiri bloka od 4 bajta.
            byte[] text = new byte[i];
            
            byteText.CopyTo(text, 0);
            byte[] cipherText = new byte[i];
            //Prolazak kroz svaki blok od 16 bajtova
            for (i = 0; i < text.Length; i = i + 16)
            {
                //Dobijeni blok od 16 bajtova podeljen je na 4 mašinske reči (svaka po 32 bita)
                A = BitConverter.ToUInt32(text, i);
                B = BitConverter.ToUInt32(text, i + 4);
                C = BitConverter.ToUInt32(text, i + 8);
                D = BitConverter.ToUInt32(text, i + 12);
                //Sam algoritam šifrovanja prema dokumentaciji
                B = B + RoundKey[0];
                D = D + RoundKey[1];
                for (int j = 1; j <= R; j++)
                {
                    uint t = LeftShift((B * (2 * B + 1)), (int)(Math.Log(W, 2)));
                    uint u = LeftShift((D * (2 * D + 1)), (int)(Math.Log(W, 2)));
                    A = (LeftShift((A ^ t), (int)u)) + RoundKey[j * 2];
                    C = (LeftShift((C ^ u), (int)t)) + RoundKey[j * 2 + 1];
                    uint temp = A;
                    A = B;
                    B = C;
                    C = D;
                    D = temp;
                }
                A = A + RoundKey[2 * R + 2];
                C = C + RoundKey[2 * R + 3];
                //Obratna konverzija mašinskih reči u niz bajtova
                uint[] tempWords = new uint[4] { A, B, C, D };
                byte[] block = ToArrayBytes(tempWords, 4);
                //Zapis konvertovanih 16 bajtova u niz bajtova šifrovanog teksta
                block.CopyTo(cipherText, i);
            }
            return cipherText;
        }
        public byte[] DecodeRc6(byte[] cipherText)
        {
            uint A, B, C, D;
            int i;
            byte[] plainText = new byte[cipherText.Length];
            //Podela šifrovanog teksta na blokove od 16 bajtova
            for (i = 0; i < cipherText.Length; i = i + 16)
            {
                //razdvajanje bloka na 4 mašinske reči od 32 bita
                A = BitConverter.ToUInt32(cipherText, i);
                B = BitConverter.ToUInt32(cipherText, i + 4);
                C = BitConverter.ToUInt32(cipherText, i + 8);
                D = BitConverter.ToUInt32(cipherText, i + 12);
                //Sam proces dešifrovanja u skladu sa dokumentacijom
                C = C - RoundKey[2 * R + 3];
                A = A - RoundKey[2 * R + 2];
                for (int j = R; j >= 1; j--)
                {
                    uint temp = D;
                    D = C;
                    C = B;
                    B = A;
                    A = temp;
                    uint u = LeftShift((D * (2 * D + 1)), (int)Math.Log(W, 2));
                    uint t = LeftShift((B * (2 * B + 1)), (int)Math.Log(W, 2));
                    C = RightShift((C - RoundKey[2 * j + 1]), (int)t) ^ u;
                    A = RightShift((A - RoundKey[2 * j]), (int)u) ^ t;
                }
                D = D - RoundKey[1];
                B = B - RoundKey[0];
                //Pretvaranje mašinskih reči u niz bajtova
                uint[] tempWords = new uint[4] { A, B, C, D };
                byte[] block = ToArrayBytes(tempWords, 4);
                //Upis dešifrovanih bajtova u niz bajtova dešifrovanog teksta
                block.CopyTo(plainText, i);
            }
            return plainText;
        }
    }

}

