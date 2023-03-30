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
        private List<char> _code = new List<char>();
        private Difficulty _difficulty;
        private List<string> _words;
        private List<char> _guesses;

        public Word(Difficulty difficulty)
        {
            _difficulty = difficulty;
            _words = File.ReadAllLines($"{difficulty.ToString()}Words.txt").ToList<string>();
            _word = _words[_generator.Next(0, _words.Count)];
            string underscores = new string('_', _word.Length);
            _code.AddRange(underscores.ToCharArray());
        }

        public string SecretWord { get { return _word; } }

        public void PrintWord()
        {
            int index;
            if (_guesses.Count > 0)
            {
                foreach (char guess in _guesses)
                {
                    index = _word.IndexOf(guess);
                    if (index > -1)
                    {
                        _code.RemoveAt(index);
                        _code.Insert(index, guess);
                    }
                }
            }
            
            Console.WriteLine(_code);
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

        public void GuessWord(char c)
        {
            if (_word.Contains(c))
            {
                _guesses.Add(c);
                Console.WriteLine("Good Guess!");
            }
            else
            {
                Console.WriteLine("Bad Guess!");
            }
        }
    }
}
