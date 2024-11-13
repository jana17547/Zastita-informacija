using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    class Bifid
    {
        
        private const string Alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
        private int[] _keySquare;

        public Bifid(string key)
        {
            _keySquare = BuildKeySquare(key);
        }

        private int[] BuildKeySquare(string key)
        {
            var square = new int[25];
            var keyChars = key.ToUpper().Where(c => Char.IsLetter(c)).Distinct().ToArray();
            var alphabetChars = Alphabet.ToCharArray();
            var mergedChars = keyChars.Concat(alphabetChars).Distinct().ToArray();
            for (int i = 0; i < mergedChars.Length; i++)
            {
                square[i] = Alphabet.IndexOf(mergedChars[i]);
            }
            return square;
        }


        //NAPOMENA: metoda Encript uklanja sve razmake iz ulaznog niza i takođe zamenjuje slovo 'J' slovom 'I'
        //jer ovaj algoritam radi samo sa 25 slova i ne uključuje J
        public string Encrypt(string plaintext)
        {
            plaintext = plaintext.ToUpper().Replace("J", "I").Replace(" ", "");
            var plaintextPairs = new int[plaintext.Length][];
            for (int i = 0; i < plaintext.Length; i++)
            {
                var index = Alphabet.IndexOf(plaintext[i]);
                var row = index / 5;
                var col = index % 5;
                plaintextPairs[i] = new[] { row, col };
            }
            var ciphertext = "";
            for (int i = 0; i < plaintextPairs.Length; i++)
            {
                var index = plaintextPairs[i][0] * 5 + plaintextPairs[i][1];
                ciphertext += Alphabet[_keySquare[index]];
            }
            return ciphertext;
        }

        public string Decrypt(string ciphertext)
        {
            var ciphertextPairs = new int[ciphertext.Length][];
            for (int i = 0; i < ciphertext.Length; i++)
            {
                var index = Alphabet.IndexOf(ciphertext[i]);
                var row = Array.IndexOf(_keySquare, index) / 5;
                var col = Array.IndexOf(_keySquare, index) % 5;
                ciphertextPairs[i] = new[] { row, col };
            }
            var plaintext = "";
            for (int i = 0; i < ciphertextPairs.Length; i++)
            {
                var index = ciphertextPairs[i][0] * 5 + ciphertextPairs[i][1];
                plaintext += Alphabet[index];
            }
            plaintext = plaintext.Replace("I", "J").Replace(" ", "");
            return plaintext;
        }
    }
}

