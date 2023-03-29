﻿using System;
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
            FlashAnimation(400, new List<ConsoleColor> { ConsoleColor.White });
        }

        public void FlashAnimation(int delay, List<ConsoleColor> colors)
        {
            int[] cursorCoords = { Console.CursorLeft, Console.CursorTop };
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = colors[_generator.Next(0, colors.Count)];
                Console.SetCursorPosition(cursorCoords[0], cursorCoords[1] - 1);
                Console.Write(_text);
                Thread.Sleep(delay);
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(cursorCoords[0], cursorCoords[1] - 1);
                Thread.Sleep(delay/2);
            }
            Console.WriteLine(_text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
