using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    public enum SpellSlot
    {
        Normal,
        Up,
        Down,
        Forward
    }

    public class SpellManager : MonoBehaviour
    {
        public List<GameObject> Spells;
        public List<string> UnlockedSpells;
    }
}
