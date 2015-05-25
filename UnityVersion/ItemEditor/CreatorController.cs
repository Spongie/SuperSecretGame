using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Assets.Scripts.Utility;
using System;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ItemEditor
{
    public class CreatorController
    {
        private string ivItemsPath;
        private string ivIconPath;
        private List<string> ivOriginalFiles;

        public ObservableCollection<Item> Items { get; set; }

        public Item SelectedItem { get; set; }

        public ObservableCollection<ItemIcon> Images { get; set; }

        public CreatorController()
        {
            Items = new ObservableCollection<Item>();
            Images = new ObservableCollection<ItemIcon>();
            ivOriginalFiles = new List<string>();

            if (File.Exists("config.ini"))
            {
                var lines = File.ReadAllLines("config.ini");
                ivItemsPath = lines[0];
                ivIconPath = lines[1];
            }

            GetItemsPath();
            GetIcons();
            LoadItems();

            File.WriteAllLines("config.ini", new string[] { ivItemsPath, ivIconPath });
        }

        private void GetItemsPath()
        {
            if (!string.IsNullOrEmpty(ivItemsPath))
                return;

            var folderBroswer = new FolderBrowserDialog();
            folderBroswer.Description = "Select the folder that contains all items";

            if (folderBroswer.ShowDialog() == DialogResult.OK)
            {
                ivItemsPath = folderBroswer.SelectedPath;
            }
            else
                ivItemsPath = Application.ExecutablePath;
        }

        private void LoadItems()
        {
            foreach (var path in Directory.GetFiles(ivItemsPath))
            {
                if (path.EndsWith(".meta"))
                    continue;

                var jsonItem = File.ReadAllText(path);
                var item = JsonConvert.DeserializeObject<Item>(jsonItem);
                Items.Add(item);

                ivOriginalFiles.Add(path);
            }
        }

        private void GetIcons()
        {
            if (!string.IsNullOrEmpty(ivIconPath))
            {
                LoadImages();
            }
            else
            {
                var folderBroswer = new FolderBrowserDialog();
                folderBroswer.Description = "Select the folder that contains all icons";

                if (folderBroswer.ShowDialog() == DialogResult.OK)
                {
                    ivIconPath = folderBroswer.SelectedPath;

                    LoadImages();
                }
            }
        }

        private void LoadImages()
        {
            foreach (var file in Directory.GetFiles(ivIconPath))
            {
                if (file.EndsWith(".meta"))
                    continue;

                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(file);
                image.EndInit();
                Images.Add(new ItemIcon(image));
            }
        }

        public void AddItem()
        {
            var item = new Item()
            {
                Name = "Temp" + new Random().Next(Int16.MaxValue),
                Damage = 0.0f,
                Defense = 0.0f,
                MagicDamage = 0.0f,
                MagicDefense = 0.0f,
                Luck = 0.0f,
                Resistance = 0.0f,
                Slot = 0
            };

            Items.Add(item);
        }

        public void SaveItems(Action<int> piReportProgress)
        {
            int maxProgress = ivOriginalFiles.Count + Items.Count;
            int currentProgress = 0;

            foreach (var file in ivOriginalFiles)
            {
                File.Delete(file);
                
                currentProgress++;
                piReportProgress((int)((currentProgress / maxProgress) * 100));
            }

            foreach (var item in Items)
            {
                var jsonItem = JsonConvert.SerializeObject(item);
                File.WriteAllText(ivItemsPath + "\\" + item.Name + ".txt", jsonItem);
                
                currentProgress++;
                piReportProgress((int)(((float)currentProgress / (float)maxProgress) * 100));
            }
        }

        public void ClearConfig()
        {
            File.Delete("config.ini");
        }

        private void LoadItems(string piPath)
        {
            foreach (var itemFile in Directory.GetFiles(piPath))
            {
                var item = JsonConvert.DeserializeObject<Item>(itemFile);
                Items.Add(item);
            }
        }
    }
}
