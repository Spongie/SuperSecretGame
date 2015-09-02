using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Assets.Scripts.Items;

namespace ItemEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CreatorController ivController;

        public MainWindow()
        {
            InitializeComponent();
            ivController = new CreatorController();

            ivListboxItems.DataContext = ivController.Items;
            ivListboxItemsTable.DataContext = ivController.LootTables;
            ivListboxIcons.DataContext = ivController.Images;
            ivListboxAvailableItems.DataContext = ivController.Items;
            ivListboxItemsInTable.DataContext = ivController.SelectedLootTable;

            if (ivController.Items.Any())
                ivListboxItems.SelectedIndex = 0;
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if(ivListboxItems.SelectedIndex >= 0)
                ivController.Items.RemoveAt(ivListboxItems.SelectedIndex);
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ivController.AddItem();

            ivListboxItems.SelectedIndex = ivListboxItems.Items.Count - 1;
        }

        private void ivListboxItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ivController.SelectedItem = (Item)e.AddedItems[0];
                ivItemEditorStack.DataContext = ivController.SelectedItem;
            }
        }

        private void ivListboxIcons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ivController.SelectedItem != null)
                ivController.SelectedItem.IconName = ((ItemIcon)e.AddedItems[0]).Name;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if(ivController.CanSave())
                ivController.SaveItems();
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ivController.ClearConfig();
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

        private void ivListboxItemsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ivController.SelectedLootTable = (EditorLootTable)e.AddedItems[0];
                ivListboxItemsInTable.DataContext = ivController.SelectedLootTable.LootItems;
                ivTextBoxLootTableName.DataContext = ivController.SelectedLootTable;
                ivTextTotalDrop.DataContext = ivController.SelectedLootTable;
            }
        }

        private void Button_RemoveFromLootTable(object sender, RoutedEventArgs e)
        {
            if (ivListboxItemsInTable.SelectedIndex >= 0)
                ivController.SelectedLootTable.LootItems.RemoveAt(ivListboxItemsInTable.SelectedIndex);
        }

        private void Button_AddToLootTable(object sender, RoutedEventArgs e)
        {
            if(ivController.SelectedLootTable != null && ivListboxAvailableItems.SelectedIndex != -1)
            {
                ivController.AddItemToLootTable((Item)ivListboxAvailableItems.SelectedItem);
            }
        }

        private void Button_AddLootTable(object sender, RoutedEventArgs e)
        {
            ivController.AddLootTable();
        }

        private void Button_RemoveLootTable(object sender, RoutedEventArgs e)
        {
            if (ivListboxItemsTable.SelectedIndex >= 0)
                ivController.LootTables.RemoveAt(ivListboxItemsTable.SelectedIndex);
        }

        private void ivListboxItemsInTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ivStackLootItem.DataContext = ivListboxItemsInTable.SelectedItem;
                ivTextBoxLootTableName.DataContext = ivController.SelectedLootTable;
                ivTextTotalDrop.DataContext = ivController.SelectedLootTable;
            }
        }
    }
}
