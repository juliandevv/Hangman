using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Hangman
{
    internal class StringAnimation
    {
        private string _text;
        private static Random _generator = new Random();

        public StringAnimation(string text)
        {
            _text = text;   
        }

        public string Text 
        { 
            get { return _text; }
            set { _text = value; }
        }

        public void Write()
        {
            Console.WriteLine(_text);
        }

        public void TypewriterAnimation()
        {
            TypewriterAnimation(20, ConsoleColor.White);
        }

        public void TypewriterAnimation(int delay, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            foreach (char c in _text)
            {
                Console.Write(c.ToString());
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        public void FlashAnimation()
        {
            FlashAnimation(300, 3, new List<ConsoleColor> { ConsoleColor.White });
        }

        public void FlashAnimation(int delay, int iterations, List<ConsoleColor> colors)
        {
            int[] cursorCoords = { Console.CursorLeft, Console.CursorTop };
            for (int i = 0; i < iterations; i++)
            {
                Console.ForegroundColor = colors[_generator.Next(0, colors.Count)];
                Console.SetCursorPosition(cursorCoords[0], cursorCoords[1] - 1);
                Console.Write(_text);
                Thread.Sleep(delay);
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(cursorCoords[0], cursorCoords[1] - 1);
                Thread.Sleep(delay/5);
            }
            Console.WriteLine(_text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
