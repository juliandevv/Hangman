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
        private List<string> _drawing;

        public Man()
        {
            _stand = File.ReadAllLines(@"Hang.txt").ToList();
            _head = File.ReadAllLines(@"Head.txt").ToList();
            _body = File.ReadAllLines(@"Body.txt").ToList();
            _legs = File.ReadAllLines(@"Legs.txt").ToList();
        }

        public void Draw()
        {

        }

    }
}
