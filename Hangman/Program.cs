using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Hangman
{
    internal class Program
    {
        enum Screen
        {
            title,
            main,
            settings
        }

        static void Main(string[] args)
        {
            // initialize main vars
            Screen screen = Screen.title;
            drawTitle();
            Console.ReadLine();

        }

        static void drawTitle()
        {
            List<ConsoleColor> colors = new List<ConsoleColor>() { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Magenta };
            string[] lines = File.ReadAllLines(@"Title.txt");
            StringAnimation pressStart2Play = new StringAnimation("Press ENTER to start!");

            // write letters
            foreach (string line in lines)
            {
                Console.SetCursorPosition((Console.WindowWidth / 2) - 24, Console.CursorTop);
                Console.WriteLine(line);
                Thread.Sleep(20);
            }
            
            //Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 11, Console.CursorTop + 5);
            pressStart2Play.FlashAnimation(400, colors);
        }
    }
}
