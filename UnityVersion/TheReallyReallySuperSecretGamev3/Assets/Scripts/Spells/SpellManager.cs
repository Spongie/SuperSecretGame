using Assets.Scripts.Attacks;
using System.Collections.Generic;
using System.Linq;
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
        private Dictionary<SpellSlot, string> ivEquippedSpells;

        void Start()
        {
            ivEquippedSpells = new Dictionary<SpellSlot, string>();
            UnlockedSpells = new List<string>()
            {
                "Fireball",
                "Heal"
            };

            SetDefaultSpells();
        }

        private void SetDefaultSpells()
        {
            ivEquippedSpells[SpellSlot.Normal] = Spells.First().name;
            ivEquippedSpells[SpellSlot.Forward] = Spells.First().name;
            ivEquippedSpells[SpellSlot.Up] = Spells.First().name;
            ivEquippedSpells[SpellSlot.Down] = Spells.First().name;
        }

        public List<GameObject> GetAvailableSpells()
        {
            return Spells.Where(spell => UnlockedSpells.Contains(spell.name)).ToList();
        }

        public Dictionary<SpellSlot, string> GetEquippedSpells()
        {
            return ivEquippedSpells;
        }

        public GameObject GetEquippedSpellAtSlot(SpellSlot piSlot)
        {
            if (ivEquippedSpells.ContainsKey(piSlot))
                return GetSpellWithName(ivEquippedSpells[piSlot]);

            return null;
        }

        private GameObject GetSpellWithName(string piSpellName)
        {
            return Spells.First(spell => spell.name == piSpellName);
        }

        public void EquipSpell(SpellSlot piSlot, string piSpellname)
        {
            ivEquippedSpells[piSlot] = piSpellname;
        }

        public bool CanCastSpell(SpellSlot piSlot, int piCurrentMana)
        {
            if (!ivEquippedSpells.ContainsKey(piSlot))
                return false;

            var spell = GetSpellWithName(ivEquippedSpells[piSlot]).GetComponent<Attack>();

            return spell.ManaCost <= piCurrentMana;
        }
    }
}
