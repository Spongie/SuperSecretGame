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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItemEditor.Controls
{
    /// <summary>
    /// Interaction logic for DefenseEffect.xaml
    /// </summary>
    public partial class DefenseEffect : UserControl
    {
        public DefenseEffect()
        {
            InitializeComponent();
        }

        private void EffectName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool enabled = e.AddedItems[0].ToString() != "None";

            ivTextboxEffectvalue.IsEnabled = enabled;
            ivTextboxEffectDuration.IsEnabled = enabled;
            ivTextboxEffectTicks.IsEnabled = enabled;
            ivTextboxEffectStatsDamage.IsEnabled = enabled;
            ivTextboxEffectStatsDefense.IsEnabled = enabled;
            ivTextboxEffectStatsLuck.IsEnabled = enabled;
            ivTextboxEffectStatsMagicDamage.IsEnabled = enabled;
            ivTextboxEffectStatsMagicDefense.IsEnabled = enabled;
            ivTextboxEffectStatsResistance.IsEnabled = enabled;
        }
    }
}
