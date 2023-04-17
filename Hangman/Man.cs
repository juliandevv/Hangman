using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hangman
{
    internal class Man
    {
        private List<string> _stand;
        private List<string> _head;
        private List<string> _body;
        private List<string> _legs;
        private List<string> _feet;
        private List<string> _drawing;
        private int _strikes;
        private bool _fail;

        public Man()
        {
            _stand = File.ReadAllLines(@"Assets\Stand.txt").ToList();
            _head = File.ReadAllLines(@"Assets\Head.txt").ToList();
            _body = File.ReadAllLines(@"Assets\Body.txt").ToList();
            _legs = File.ReadAllLines(@"Assets\Legs.txt").ToList();
            _feet = File.ReadAllLines(@"Assets\Feet.txt").ToList();
            _strikes = 0;
            _fail = false;
        }

        public int Strikes 
        { 
            get { return _strikes; }
            set { _strikes = value; }
        }

        public bool Fail { get { return _fail; } }

        public void Draw()
        {
            WriteLines(_stand, 0);
            switch (_strikes)
            {
                case 0:
                    break;
                case 1:
                    WriteLines(_head, 4);
                    break;
                case 2:
                    WriteLines(_head, 4);
                    WriteLines(_body, 8);
                    break;
                case 3:
                    WriteLines(_head, 4);
                    WriteLines(_body, 8);
                    WriteLines(_legs, 13);
                    break;
                case 4:
                    WriteLines(_head, 4);
                    WriteLines(_body, 8);
                    WriteLines(_legs, 13);
                    WriteLines(_feet, 17);
                    _fail = true;
                    break;
                default:
                    _fail = true;
                    break;

            }
            Console.SetCursorPosition(0, 23);
            Console.WriteLine();
        }

        private void WriteLines(List<string> lines, int cursorTop)
        {
            Console.SetCursorPosition(0, cursorTop);

            foreach (string line in lines)
            {
                Console.SetCursorPosition(Console.CursorLeft + 45, Console.CursorTop);
                Console.WriteLine(line);
            }
        }

    }
}
