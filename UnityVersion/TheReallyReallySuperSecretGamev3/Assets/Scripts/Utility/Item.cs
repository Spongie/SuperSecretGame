using CVCommon.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utility
{
    public class Item
    {
        public string Name { get; set; }
        public float Damage { get; set; }
        public float Defense { get; set; }
        public float MagicDamage { get; set; }
        public float MagicDefense { get; set; }
        public float Luck { get; set; }
        public float Resistance { get; set; }
    }
}
