using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace WcfServiceLibrary
{
    class CTR
    {
        byte[] key = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        byte[] iv = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        public  byte[] Encrypt(byte[] plainText)
        {
            // Provera argumenata

            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Kreiranje novog Aes objekta da bi se izvrsilo sifrovanje
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                //aes.Mode = CipherMode.CTR;
                // Kreiranje enkriptora da bi se izvrsila transformacija toka.
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                // Kreiranje novog memorijskog toka za čuvanje sifrovanih podataka
                using (MemoryStream ms = new MemoryStream())
                {
                    // Kreiranje novog kripto toka da bi se  izvrsilo sifrovanje
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        // Upis plaintText u kripto tok
                        cs.Write(plainText, 0, plainText.Length);
                    }

                    // Vracamo sifrovane podatke
                    encrypted = ms.ToArray();
                }
            }
            // Vracanje sifrovanih bajtova iz memorijskog toka.
            return encrypted;

        }

        public byte[] Decrypt(byte[] cipherText)
        {
            // Provera argumenata.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("IV");

            //Deklarisanje niza koji se koristi za cuvanje desifrovanog teksta
            
            byte[] plaintext;
            // Kreiranje novog Aes objekat da bi se izvrsilo desifrovanje
            using (Aes aes = Aes.Create())

            {
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.None;

                // duzina niza bajtova mora biti deljiva sa velicinom blokova (=16) ukoliko nije dodajemo prazne bajtove
                int blockSize = 16;
                if ((cipherText.Length % blockSize) != 0)
                {
                    int paddingLength = blockSize - (cipherText.Length % blockSize);
                    if (paddingLength == 0)
                    {
                        paddingLength = blockSize;
                    }
                    byte[] padding = new byte[paddingLength];
                    byte[] paddedPlaintext = new byte[cipherText.Length + paddingLength];
                    Buffer.BlockCopy(cipherText, 0, paddedPlaintext, 0, cipherText.Length);
                    Buffer.BlockCopy(padding, 0, paddedPlaintext, cipherText.Length, padding.Length);

                    using (var decryptor1 = aes.CreateDecryptor())
                    {
                        var plaintext1 = decryptor1.TransformFinalBlock(paddedPlaintext, 0, paddedPlaintext.Length);

                        int unpaddedLength = plaintext1.Length;
                        for (int i = plaintext1.Length - 1; i >= 0; i--)
                        {
                            if (plaintext1[i] != 0)
                            {
                                break;
                            }
                            unpaddedLength--;
                        }
                        byte[] unpaddedPlaintext = new byte[unpaddedLength];
                        Buffer.BlockCopy(plaintext1, 0, unpaddedPlaintext, 0, unpaddedLength);

                        return unpaddedPlaintext;
                    }
                }
                // Kreiranje dekriptora da bi se izvrsila transformacija toka
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                // Kreiranje novog memorijskog toka za cuvanje desifrovanih podataka
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Kreiranje novog kripto toka da bi se izvrsilo desifrovanje
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {

                        //int paddingSize = (int)cipherText[cipherText.Count - 1];
                        byte[] plaintext1 = new byte[cipherText.Length];
                        // Get the number of padding bytes

                        int decryptedByteCount = cs.Read(plaintext1, 0, plaintext1.Length);
                        int paddingSize = (int)plaintext1[decryptedByteCount - 1];
                        // Kreiranje novog bafera da bi se zadrzali podaci bez dodatka
                        byte[] unpaddedPlainText = new byte[decryptedByteCount - paddingSize];
                        Array.Copy(plaintext1, 0, unpaddedPlainText, 0, decryptedByteCount - paddingSize);

                        return unpaddedPlainText;

                        // Write the ciphertext to the crypto stream
                       // cs.Write(ciphertext, 0, ciphertext.Length);
                    }

                    // Return the decrypted data
                   // return ms.ToArray();
                }
            }
        }
    }
}
