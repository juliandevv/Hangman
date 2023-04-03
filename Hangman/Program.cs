using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

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
            Screen screen = Screen.Title;
            while (true)
            {
                switch (screen)
                {
                    case Screen.Title:
                        drawTitle();
                        Console.ReadLine();
                        screen = Screen.Main;
                        break;
                    case Screen.Main:
                        game();
                        break;
                    case Screen.Settings:
                        break;
                }
            }
        }

        static void game()
        {
            Word word = new Word(Difficulty.Easy);
            char[] guess;
            while (!word.Solved)
            {
                word.PrintWord();
                Console.WriteLine();
                guess = Console.ReadLine().ToCharArray();
                if (guess.Length > 0)
                {
                    word.GuessLetter(guess[0]);
                }
            }
            Console.ReadLine();
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
            pressStart2Play.Write();
            //pressStart2Play.FlashAnimation(400, colors);
        }
    }
}
