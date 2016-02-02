using Assets.Scripts.Attacks;
using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ItemEditor
{
    /// <summary>
    /// Interaction logic for EffectWindow.xaml
    /// </summary>
    public partial class EffectWindow : Window
    {
        public EffectWindow()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetCurrentItem().SelectedAttackEfffect = (AttackEffect)ivListbox.SelectedItem;
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            GetCurrentItem().EAttackEffects.Add(new AttackEffect());
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if(ivListbox.SelectedIndex >= 0)
                GetCurrentItem().EAttackEffects.RemoveAt(ivListbox.SelectedIndex);
        }

        private EditorItem GetCurrentItem()
        {
            return (EditorItem)DataContext;
        }
    }
}
