using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoMMSpell
{
    public class Variable
    {
        private int id;
        private string name;
        private string syn;
        private int val;

        public Variable(int id, string name, string syn, int value) 
        { 
            this.id = id;
            this.name = name;
            this.syn = syn;
            this.val = value;
        }
        public int Id { get { return id; } set { id=value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Syn { get { return syn; } set { syn = value; } }
        public int Value { get { return val; } set { val = value; } }
    }
}
