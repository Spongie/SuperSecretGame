using Assets.Scripts.Attacks;
using Assets.Scripts.Defense;
using Assets.Scripts.Items;
using System.Collections.ObjectModel;

namespace ItemEditor
{
    public class EditorItem : Item
    {
        private ObservableCollection<AttackEffect> ivAttackEffects;

        public EditorItem():base()
        {
            ivAttackEffects = new ObservableCollection<AttackEffect>();
            ivDefenseEffects = new ObservableCollection<DefenseEffect>();
        }

        public EditorItem(Item piItem) :base(piItem)
        {
            ivAttackEffects = new ObservableCollection<AttackEffect>();
            ivDefenseEffects = new ObservableCollection<DefenseEffect>();

            if (piItem.AttackEffects != null)
            {
                foreach (var item in piItem.AttackEffects)
                {
                    ivAttackEffects.Add(item);
                }
            }

            if (piItem.DefenseEffects != null)
            {
                foreach (var item in piItem.DefenseEffects)
                {
                    ivDefenseEffects.Add(item);
                }
            }
        }

        public ObservableCollection<AttackEffect> EAttackEffects
        {
            get { return ivAttackEffects; }
            set
            {
                ivAttackEffects = value;
                FirePropertyChanged("EAttackEffects");
            }
        }

        private ObservableCollection<DefenseEffect> ivDefenseEffects;

        public ObservableCollection<DefenseEffect> EDefenseEffects
        {
            get { return ivDefenseEffects; }
            set
            {
                ivDefenseEffects = value;
                FirePropertyChanged("EDefenseEffects");
            }
        }

        public Item GetAsItem()
        {
            DefenseEffects.Clear();
            AttackEffects.Clear();

            foreach (var item in EDefenseEffects)
            {
                DefenseEffects.Add(item);
            }

            foreach (var item in EAttackEffects)
            {
                AttackEffects.Add(item);
            }

            return (Item)this;
        }
    }
}
