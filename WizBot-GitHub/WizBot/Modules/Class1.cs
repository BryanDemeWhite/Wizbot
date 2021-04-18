using System;
using System.Collections.Generic;
using System.Text;

namespace WizBot.Modules
{
    class Player
    {
        public ulong Id { get; set; }
            public string Name { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }

        public List<string> Abilities { get; set; }
/*
        public class Item
        {
            public string name { get; set; }
            public string index { get; set; }
            public string optional { get; set; }
        }

        public class RootObject
        {
            public List<Item> items { get; set; }
        }
*/


    }
}
