using System;
using System.IO;
using System.Security.Cryptography;

namespace SymmetricalEncryption
{
    public class Encrypter
    {
        
    public SymmetricAlgorithm MyCipherAlg { get; private set; }

    public Encrypter(string cipher)
        {
            switch (cipher)
            {
                case "DES":
                    MyCipherAlg = DES.Create();
                    break;

                case "TRIPLEDES":
                    MyCipherAlg = TripleDES.Create();
                    break;

                case "AES":
                    MyCipherAlg = Rijndael.Create();
                    break;

            }
            MyCipherAlg.GenerateKey();
            MyCipherAlg.GenerateIV();
        }


        //Create an aesmanaged object with the specified key/iv
        public byte[] Encrypt(byte[] input)
        {

                //myCipherAlg.Key = key;
                //myCipherAlg.IV = iv;

                //create an encryptor to perform the stream transform
                ICryptoTransform cryptoTransform = MyCipherAlg.CreateEncryptor(); //(myCipherAlg.Key, myCipherAlg.IV);

            // As used in the example in the document of the assignment i have choosen to use using here as well
            // the using statement defines our scope at the end of which an object will be disposed
            //create te streams used for encrypption
            using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                    {
                    cryptoStream.Write(input, 0, input.Length);
                    }
                return memoryStream.ToArray();
                }
            }
        
        // legit the sane but with decrypt instead
        public byte[] Decryption(byte[] input, byte[] key, byte[] iv)
        {
            byte[]plaintext = new byte[input.Length];
            MyCipherAlg.Key = key;
            MyCipherAlg.IV = iv;

                ICryptoTransform cryptoTransform = MyCipherAlg.CreateDecryptor();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
                    {
                        cryptoStream.Read(plaintext, 0,  input.Length);
                        cryptoStream.FlushFinalBlock();

                    }
                        return plaintext;
                }
            }
        
        //Can be used if custom keys are used in the future
        //public byte[] GenRNG(int length)
        //{
        //    using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        //    {
        //        var randomNumber = new byte[length];
        //        randomNumberGenerator.GetBytes(randomNumber);

        //        return randomNumber;
        //    }
        //}
    }
}
