using Assets.Scripts.Utility;
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
using System.ComponentModel;
using System.IO;

namespace ItemEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CreatorController ivController;
        private BackgroundWorker ivWorker;

        public MainWindow()
        {
            InitializeComponent();
            ivController = new CreatorController();
            ivListboxItems.DataContext = ivController.Items;
            ivListboxIcons.DataContext = ivController.Images;
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            ivController.Items.RemoveAt(ivListboxItems.SelectedIndex);
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ivController.AddItem();
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
            ivController.SelectedItem.IconName = ((ItemIcon)e.AddedItems[0]).Name;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            ivWorker = new BackgroundWorker();
            ivWorker.ProgressChanged += s_ProgressChanged;
            ivWorker.DoWork += s_DoWork;
            ivWorker.WorkerReportsProgress = true;
            ivWorker.RunWorkerAsync();
        }

        void s_DoWork(object sender, DoWorkEventArgs e)
        {
            ivController.SaveItems(ivWorker.ReportProgress);
            Dispatcher.Invoke((Action)delegate()
            {
                ivProgressBar.Value = 0;

            });
        }

        void s_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ivProgressBar.Value = e.ProgressPercentage;
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ivController.ClearConfig();
        }
    }
}
