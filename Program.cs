using System;
using System.Collections.Generic;
using System.IO;
using inputChecker;

namespace PasswordManager
{
    class MyClass
    {
        const string filePath = @"C:\Users\Public\Documents\passwords.txt";
        static void Main(string[] args)
        {
            //Start up program
            bool runProgram = true;
            firstTime();
            List<string> passwords = File.ReadAllLines(filePath).ToList();
            printPasswords(passwords);
            while (runProgram)
            {
                Console.Clear();
                Console.WriteLine("would you like to:\n1: Add a new password\n2: Change an existing password\n3: Remove an existing password\n4: Print current passwords\n5: Save and exit\n\n(1,2,3,4,5)");
                string option = rejectNull(Console.ReadLine());
                Console.Clear();
                switch (option)
                {
                    case "1":
                        passwords.Add(addPW(false));
                        break;

                    case "2":
                        passwords = changePW(passwords);
                        break;

                    case "3":
                        passwords = removePW(passwords);
                        break;

                    case "4":
                        printPasswords(passwords);
                        break;

                    case "5":
                        File.WriteAllLines(filePath, passwords);
                        Console.WriteLine("Your progress has been saved, press any button to exit the program");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Your input was not an expected value, try again.");
                        break;
                }
            }
        }

        static bool firstTime()
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine("welcome back to my password manager");
                return false;
            }
            Console.WriteLine("Welcome to my password manager, a file has been created so you may leave the program and continue at any time.");
            File.Create(filePath);
            File.Create(@"C:\Users\Public\Documents\letters.txt");
            File.Create(@"C:\Users\Public\Documents\numbers.txt");
            File.Create(@"C:\Users\Public\Documents\symbols.txt");
            List<string> letters = new List<string>() {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
            List<string> numbers = new List<string>() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};
            List<string> symbols = new List<string>() {".", ",", "-", "_", ":", ";", "'", "*", "^", "¨", "~", @"\", "/", "(", ")", "[", "]", "{", "}", "<", ">", "%", "&", "#", "\"", "=", "?"};
            return true;
        }

        static void printPasswords(List<string> passwords)
        {
            Console.WriteLine("Your currnet passwords are:\n");
            foreach (var pWord in passwords)
            {
                Console.WriteLine(pWord);
            }
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        static string rejectNull(string input)
        {
            while (input == "")
            {
                Console.WriteLine("Input was empty, try again");
                input = Console.ReadLine();
            }
            return input;
        }

        static string addPW(bool skipSite)
        {
            string siteName = "";
            string confirm = "";
            bool cont = true;
            string critera = "";
            string password = "";
            object PWLength = "";
            string[] passwordChain = new string[2];
            if (skipSite)
            {
                confirm = "yes";
            }
            while (confirm != "yes")
            {
                Console.WriteLine("What is the name of the site that you want to add a password for?");
                siteName = rejectNull(Console.ReadLine());
                Console.WriteLine("Confirm that the website is called \"" + siteName + "\" (yes/no)");
                confirm = rejectNull(Console.ReadLine().ToLower());
            }
            confirm = "no";
            while (confirm != "yes")
            {
                Console.Clear();
                while (cont)
                {
                    Console.WriteLine("How long would you like your password to be?");
                    bool[] isInt = new bool[6];
                    PWLength = Console.ReadLine();
                    isInt = validator.strInputValidator(PWLength, "12");
                    if (!(isInt.Contains(false)))
                    {
                        Console.WriteLine("Which of the following would you like to be a part of your password?\n1: Lowercase letters\n2: Uppercase letters\n3: Numbers\n4: Symbols\nEnter any combination (ex. 134)");
                        critera = Console.ReadLine();
                        cont = false;
                    }
                }
                int.TryParse((string?)PWLength, out int lengthAsInt);
                password = passwordGen(critera, lengthAsInt);
                Console.WriteLine("Confirm that you are statisfied with the password: \"" + password + "\". (yes/no)");
                confirm = rejectNull(Console.ReadLine().ToLower());
                if (confirm == "no")
                {
                    Console.WriteLine("Would you like to change the peramiters of your password? (yes/no)");
                    string confirm2 = rejectNull(Console.ReadLine().ToLower());
                    if (confirm2 == "yes")
                    {
                        cont = true;
                    }
                }
            }

            passwordChain[0] = siteName;
            passwordChain[1] = password;
            return (passwordChain[0] + " , " + passwordChain[1]);
        }

        static List<string> changePW(List<string> passwords)
        {
            Console.Clear();
            bool match = true;
            int pos = -1;
            while (match)
            {
                Console.WriteLine("What is the name of the website that you want to change the password for?");
                string siteName = Console.ReadLine().ToLower();
                foreach (string password in passwords)
                {
                    pos++;
                    if (password.Contains(siteName))
                    {
                        string replacement = addPW(true);
                        replacement = siteName + replacement;
                        passwords.Remove(password);
                        passwords.Insert(pos, replacement);
                        match = false;
                        break;
                    }
                }
            }
            return passwords;
        }

        static List<string> removePW(List<string> passwords)
        {
            Console.Clear();
            bool match = true;
            int pos = -1;
            while (match)
            {
                Console.WriteLine("What is the name of the website that you want to remove the password for?");
                string siteName = Console.ReadLine().ToLower();
                foreach (string password in passwords)
                {
                    pos++;
                    if (password.Contains(siteName))
                    {
                        passwords.Remove(password);
                        match = false;
                        break;
                    }
                }
            }
            return passwords;
        }

        /*
         * Critera logic:
         * 
         * 1: lowercase letters
         * 2: uppercase letters
         * 3: numbers
         * 4: symbols
        */
        static string passwordGen(string critera, int length)
        {
            char[] critChar = critera.ToCharArray();
            int currentUsage;
            int critLeng = critera.Length;
            char[] passwordHolder = new char[length];
            List<string> letters = File.ReadAllLines(@"C:\Users\Public\Documents\letters.txt").ToList();
            List<string> numbers = File.ReadAllLines(@"C:\Users\Public\Documents\numbers.txt").ToList();
            List<string> symbols = File.ReadAllLines(@"C:\Users\Public\Documents\symbols.txt").ToList();
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                int now = rand.Next(critLeng);
                currentUsage = critChar[now]-49;
                //This is dumb, why -49? I dont know either. But it works now and I'll get someone to help me figure it out later
                if (currentUsage == 0)
                {
                    passwordHolder[i] = char.Parse(letters[rand.Next(letters.Count)]);
                }
                if (currentUsage == 1)
                {
                    passwordHolder[i] = char.Parse(letters[rand.Next(letters.Count)].ToUpper());
                }
                if (currentUsage == 2)
                {
                    passwordHolder[i] = char.Parse(numbers[rand.Next(numbers.Count)]);
                }
                if (currentUsage == 3)
                {
                    passwordHolder[i] = char.Parse(symbols[rand.Next(symbols.Count)]);
                }
            }
            string password = new string(passwordHolder);
            return password;
        }
    }
}