using System;
using System.Diagnostics;
using System.Text;

namespace SymmetricalEncryption
{
    public class Program
    {
        private static Encrypter encrypter;

        private static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Symmectric Encryption & Decryption\n");

                Console.WriteLine(@"
                         +------------------------------------+
                         | +------------------------------------+
                         | |                                  | |
                         | |        Press 1 for DES           | |
                         | |      Press 2 for TripleDES       | |
                         | |         Press 3 for AES          | |
                         | |         Press 4 to EXIT          | |
                         | |    ==========================    | |
                         | |      Remember to press enter     | |
                         | |                                  | |
                         +------------------------------------+ |
                           +------------------------------------+ ");

                // Note to self : Console.Clear();

                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    encrypter = new Encrypter("DES");
                    GetInput("DES");
                }
                else if (userInput == "2")
                {
                    encrypter = new Encrypter("TRIPLEDES");
                    GetInput("TrippleDES");
                }
                else if (userInput == "3")
                {
                    encrypter = new Encrypter("AES");
                    GetInput("AES");
                }
                else if (userInput == "4")
                {
                    isRunning = false;
                }                         
            }
        }

        private static void GetInput(string algorithm)
        {
            string message;
            byte[] key;
            byte[] iv;

            Console.Clear();
            Console.WriteLine($"{algorithm} Encryption & Decryption\n");

            Console.WriteLine(@"
                         +------------------------------------+
                         | +------------------------------------+
                         | |                                  | |
                         | |        Press 1 to Encrypt        | |
                         | |        Press 2 to Decrypt        | |  
                         | |        Press 4 to go back        | |
                         | |    ==========================    | |
                         | |      Remember to press enter     | |
                         | |                                  | |
                         +------------------------------------+ |
                           +------------------------------------+ ");
            string choosenUserInput = Console.ReadLine();


            if (choosenUserInput == "1")
            {
                Console.Clear();
                Console.Write("Messsage: ");
                message = Console.ReadLine();

                key = encrypter.myCipherAlg.Key;
                iv = encrypter.myCipherAlg.IV;

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                Console.WriteLine($"\nYour encrypted message: {encrypter.Encrypt(message, key, iv)}");
                stopWatch.Stop();
                Console.WriteLine("Time for encryption :" + stopWatch.ElapsedMilliseconds + "milliseconds");
                Console.WriteLine($"\nYour key: {Convert.ToBase64String(key)}");
                Console.WriteLine($"The IV: {Convert.ToBase64String(iv)}");
                Console.WriteLine("Plz press enter for continuing");
                Console.ReadLine();
            }

            else if (choosenUserInput == "2")
            {
                Console.Clear();
                Console.Write("messsage: ");
                message = Console.ReadLine();

                Console.Write("\nThe keey: ");
                key = Convert.FromBase64String(Console.ReadLine());
                Console.WriteLine(key.Length);
                Console.Write("\nThe IV: ");
                iv = Convert.FromBase64String(Console.ReadLine());

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                Console.WriteLine($"\r\nYour decrypted message: {encrypter.Decrypt(message, key, iv)}");
                stopWatch.Stop();
                Console.WriteLine("Time for decryption :" + stopWatch.ElapsedMilliseconds + "milliseconds");
                Console.WriteLine("Plz press enter for continuing");
                Console.ReadLine();
            }         
        }
    }
}


/*
 * Reasons why the error came with lenght error:
 * 
 * - the first problem was that the alg(algorithm) functions in blocks and if the msg isnt long enough it wont decrypt, it can encrypt but not the other way,
 * thats why we use flush final block, so it finishes the encryption by putting "something" on it so it fills out the block (as far as i understand)
 * 
 * - the lenght for the key is dif for the dif methods : des, triple and aes, so the alg makes the key depending on the type instead of us meanualy doin so.
 * 
 * - the way we converted the msg was wrong basically, it had to be a string reader as well as we put it in memorysteam --> streareader.read instead of write
 * 
 */