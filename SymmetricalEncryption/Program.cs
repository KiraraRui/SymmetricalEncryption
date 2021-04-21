using System;
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
                //this one is never shown, fix
                else
                {
                    Console.WriteLine("Please choose an option");
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

               byte[] encrypted = encrypter.Encrypt(Encoding.UTF8.GetBytes(message));
                Console.WriteLine($"\r\nYour decrypted message: {encrypter.Decryption(encrypted, encrypter.MyCipherAlg.Key, encrypter.MyCipherAlg.IV)}");
                Console.WriteLine($"\nYour encrypted message: {Convert.ToBase64String(encrypted)}");
                Console.WriteLine($"\nYour key: {Convert.ToBase64String(encrypter.MyCipherAlg.Key)}");
                Console.WriteLine($"The IV: {Convert.ToBase64String(encrypter.MyCipherAlg.IV)}");
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

                Console.WriteLine($"\r\nYour decrypted message: {encrypter.Decryption(Convert.FromBase64String(message), key, iv)}");
                Console.WriteLine("Plz press enter for continuing");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Please choose an option");
            }

        }
    }
}


