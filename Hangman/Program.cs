using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace Hangman
{
    enum Difficulty 
    { 
        Easy,
        Medium,
        Hard
    }

    enum Screen
    {
        Title,
        Main,
        Settings
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // initialize main vars
            Screen screen;
            Difficulty difficulty = Difficulty.Easy;
            while (true)
            {
                drawTitle();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Spacebar:
                        screen = Screen.Settings;
                        break;
                    default:
                        screen = Screen.Main;
                        break;
                }

                switch (screen)
                {
                    case Screen.Title:
                        drawTitle();
                        break;
                    case Screen.Main:
                        game(difficulty);
                        break;
                    default:
                        break;
                }
            }
        }

        static void game(Difficulty difficulty)
        {
            string secretWord = customWord();
            Word word = new Word(difficulty, secretWord);
            if (secretWord == "")
            {
                word.NewWord();
            }
            Man hangman = new Man();
            StringAnimation stringAnimation = new StringAnimation("You Failed!!");
            char[] guess;
            List<char> alpha = "abcdefghijklmnopqrstuvwxyz".ToCharArray().ToList();

            while (!word.Solved)
            {
                Console.Clear();

                hangman.Draw();
                Console.SetCursorPosition(Console.CursorLeft + 25, Console.CursorTop);
                word.PrintWord();
                Console.SetCursorPosition(Console.CursorLeft + 37, Console.CursorTop - 1);

                foreach (char c in alpha)
                {
                    Console.Write(c);
                    Console.Write(' ');
                }

                //Console.WriteLine(hangman.Strikes);

                if (hangman.Fail)
                {
                    Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                    Console.SetCursorPosition(45, Console.CursorTop + 1);
                    Console.WriteLine("The word was: " + word.SecretWord);
                    Console.SetCursorPosition(45, Console.CursorTop + 1);
                    stringAnimation.FlashAnimation(250, 10, new List<ConsoleColor> { ConsoleColor.DarkRed });
                    Console.SetCursorPosition(45, Console.CursorTop + 1);
                    Console.WriteLine("Press ENTER to play again!");
                    break;
                }

                Console.SetCursorPosition(25, Console.CursorTop + 1);
                //Thread.Sleep(20);
                guess = Console.ReadLine().ToLower().ToCharArray();
                if (guess.Length > 0)
                {
                    Console.SetCursorPosition(25, Console.CursorTop + 1);
                    word.GuessLetter(guess[0], hangman);
                    alpha.Remove(guess[0]);
                }
            }
            Console.SetCursorPosition(45, Console.CursorTop + 1);
            Console.WriteLine("Press ENTER to play again!");
            Console.ReadLine();
            Console.Clear();
        }

        static string customWord()
        {
            Console.Clear();
            string word;
            Console.SetCursorPosition(25, 10);
            Console.WriteLine("Enter a secret word to guess!");
            Console.SetCursorPosition(25, Console.CursorTop);
            Console.WriteLine("The word will be randomly generated if you enter nothing.");
            Console.SetCursorPosition(25, Console.CursorTop);

            while (true)
            {
                string response = anonymousEntry();

                if (response.All(Char.IsLetter) || response == "")
                {
                    word = response.ToLower();
                    break;
                }
                else
                {
                    Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                    Console.SetCursorPosition(25, Console.CursorTop);
                    Console.WriteLine("Not a valid word!");
                    Console.SetCursorPosition(25, Console.CursorTop - 2);
                }
            }

            return word;
        }

        static string anonymousEntry()
        {
            char c;
            List<char> letters = new List<char>();
            ConsoleKey key;
            string word;

            while (true)
            {
                key = Console.ReadKey().Key;
                if (key != ConsoleKey.Enter)
                {
                    c = (char)key;
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write('*');
                    letters.Add(c);
                }
                else
                {
                    break;
                }
            }

            word = new string(letters.ToArray());
            return word;
        }

        static void drawTitle()
        {
            List<ConsoleColor> colors = new List<ConsoleColor>() { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Magenta };
            string[] lines = File.ReadAllLines(@"Title.txt");
            StringAnimation pressStart2Play = new StringAnimation("Press ENTER to start!");
           
            Console.CursorVisible = false;

            // write letters
            foreach (string line in lines)
            {
                Console.SetCursorPosition((Console.WindowWidth / 2) - 24, Console.CursorTop);
                Console.WriteLine(line);
                Thread.Sleep(20);
            }
            
            //Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 11, Console.CursorTop + 5);
            //pressStart2Play.Write();
            pressStart2Play.FlashAnimation(50, 10);
        }

        static void settings()
        {
            Console.Clear();
            Console.WriteLine("Which setting would you like to change?: Colour Theme[1] Difficulty[2]");
            while (true)
            {
                string response = Console.ReadLine();
                if (int.TryParse(response, out int selection))
                {
                    if (selection == 1)
                    {
                        break;
                    }
                    else if (selection == 2)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Not a valid input!");
                    }
                }
                else
                {
                    Console.WriteLine("Not a valid input!");
                }
            }

            Console.Clear();
        }
    }
}
