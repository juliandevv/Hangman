using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    internal class Word
    {
        private static Random _generator = new Random();
        private string _word;
        private bool _solved;
        private List<char> _code;
        private Difficulty _difficulty;
        private List<string> _words;
        private List<char> _guesses = new List<char>();

        public Word(Difficulty difficulty)
        {
            _difficulty = difficulty;
            _words = File.ReadAllLines($"{difficulty.ToString()}Words.txt").ToList<string>();
            _word = _words[_generator.Next(0, _words.Count)];
            string underscores = new string('_', _word.Length);
            _code = underscores.ToCharArray().ToList();
            _solved = false;
        }

        public string SecretWord { get { return _word; } }

        public List<char> Guesses { get { return _guesses; } }

        public bool Solved { get { return _solved; } }

        public void PrintWord()
        {
            foreach (char c in _code)
            {
                Console.Write(c);
            }
            Console.WriteLine();
        }

        public void NewWord()
        {
            _word = _words[_generator.Next(0, _words.Count)];
            string underscores = new string('_', _word.Length);
            _code.AddRange(underscores.ToCharArray());
        }

        public void ChangeDifficulty(Difficulty newDifficulty)
        {
            _difficulty = newDifficulty;
            _words = File.ReadAllLines($"{newDifficulty.ToString()}Words.txt").ToList<string>();
        }

        public void GuessLetter(char c)
        {
            StringAnimation stringAnimation = new StringAnimation("Good Guess!");
            if (_word.Contains(c))
            {
                _guesses.Add(c);

                for (int i = 0; i < _word.Length; i++)
                {
                    foreach (char guess in _guesses)
                    {
                        if (_word.ToCharArray()[i] == guess)
                        {
                            _code.RemoveAt(i);
                            _code.Insert(i, guess);
                        }
                    }
                }
                stringAnimation.Text = "Good Guess!";
                stringAnimation.FlashAnimation();
            }
            else
            {
                stringAnimation.Text = "Bad Guess!";
                stringAnimation.FlashAnimation();
            }

            if (!(_code.Contains('_')))
            {
                Console.SetCursorPosition(25, Console.CursorTop - 3);
                PrintWord();
                //Console.WriteLine("You guessed the word!");
                Console.SetCursorPosition(25, Console.CursorTop + 4);
                stringAnimation.Text = "You guessed the word!";
                stringAnimation.FlashAnimation(250, 10, new List<ConsoleColor> { ConsoleColor.DarkGreen, ConsoleColor.DarkRed, ConsoleColor.DarkYellow });
                _solved = true;
            }
        }
    }
}
