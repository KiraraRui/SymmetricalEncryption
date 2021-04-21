using System;
using System.IO;
using System.Security.Cryptography;

namespace SymmetricalEncryption
{
    public class Encrypter
    {
        public SymmetricAlgorithm myCipherAlg;


        public Encrypter(string cipher)
        {
            switch (cipher)
            {
                case "DES":
                    myCipherAlg = DES.Create();
                    break;

                case "TRIPLEDES":
                    myCipherAlg = TripleDES.Create();
                    break;

                case "AES":
                    myCipherAlg = Aes.Create();
                    break;

            }

            myCipherAlg.GenerateKey();
            myCipherAlg.GenerateIV();
        }

        //Create an aesmanaged object with the specified key/iv
        public string Encrypt(string input, byte[] key, byte[] iv)
        {
            myCipherAlg.Key = key;
            myCipherAlg.IV = iv;

            //create an encryptor to perform the stream transform
            ICryptoTransform cryptoTransform = myCipherAlg.CreateEncryptor(myCipherAlg.Key, myCipherAlg.IV); 

            // As used in the example in the document of the assignment i have choosen to use using here as well
            // the using statement defines our scope at the end of which an object will be disposed
            //create te streams used for encrypption
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(input);
                        streamWriter.Flush();
                        cryptoStream.FlushFinalBlock();
                        streamWriter.Flush();

                        return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int) memoryStream.Length);                   
                    }
                }
            }
        }

        // legit the sane but with decrypt instead
        public string Decrypt(string input, byte[] key, byte[] iv)
        {
            myCipherAlg.Key = key;
            myCipherAlg.IV = iv;

            //create an encryptor to perform the stream transform
            ICryptoTransform cryptoTransform = myCipherAlg.CreateDecryptor(myCipherAlg.Key, myCipherAlg.IV);

            // As used in the example in the document of the assignment i have choosen to use using here as well
            // the using statement defines our scope at the end of which an object will be disposed
            //create te streams used for encrypption
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(input)))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                    return streamReader.ReadToEnd();
                    }
                }
            }
        }

        /*
        To create a random number generator, call the Create() method.
        This is preferred over calling the constructor of the derived class RNGCryptoServiceProvider,
        which is not available on all platforms.
        */

        public byte[] GenRNG(int length)
        {
            using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
            {
                byte[] randomNumber = new byte[length];

                generator.GetBytes(randomNumber);

                return randomNumber;
            }
        }
    }
}
