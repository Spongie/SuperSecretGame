using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Assets.Scripts.Items;

namespace ItemEditor
{
    public class CreatorController
    {
        private string ivItemsPath;
        private string ivLootTablePath;
        private string ivIconPath;
        private List<string> ivOriginalItemFiles;
        private List<string> ivOriginalLootTableFiles;

        public ObservableCollection<Item> Items { get; set; }

        public Item SelectedItem { get; set; }

        public EditorLootTable SelectedLootTable { get; set; }

        public ObservableCollection<ItemIcon> Images { get; set; }

        public ObservableCollection<EditorLootTable> LootTables { get; set; }

        public CreatorController()
        {
            Items = new ObservableCollection<Item>();
            Images = new ObservableCollection<ItemIcon>();
            LootTables = new ObservableCollection<EditorLootTable>();
            ivOriginalItemFiles = new List<string>();
            ivOriginalLootTableFiles = new List<string>();

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
            {
                ivLootTablePath = ivItemsPath.Replace("Item", "LootTable");
                return;
            }

            var folderBroswer = new FolderBrowserDialog();
            folderBroswer.Description = "Select the folder that contains all items";

            if (folderBroswer.ShowDialog() == DialogResult.OK)
            {
                ivItemsPath = folderBroswer.SelectedPath;
            }
            else
                ivItemsPath = Application.ExecutablePath;

            ivLootTablePath = ivItemsPath.Replace("Item", "LootTable");
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

                ivOriginalItemFiles.Add(path);
            }

            foreach (var path in Directory.GetFiles(ivLootTablePath))
            {
                if (path.EndsWith(".meta"))
                    continue;

                var jsonItem = File.ReadAllText(path);
                var item = JsonConvert.DeserializeObject<LootTable>(jsonItem);
                LootTables.Add(new EditorLootTable(item));

                ivOriginalLootTableFiles.Add(path);
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

        public void AddItemToLootTable(Item piItem)
        {
            var item = new LootTableItem()
            {
                ItemName = piItem.Name,
                DropChance = 0
            };

            if (!SelectedLootTable.LootItems.Any(lootItem => lootItem.ItemName == item.ItemName))
                SelectedLootTable.AddItem(item);
        }

        public void AddLootTable()
        {
            var lootTable = new EditorLootTable()
            {
               Name = "Temp" + new Random().Next(Int16.MaxValue)
            };
            
            LootTables.Add(lootTable);
        }

        public bool CanSave()
        {
            var itemErrors = CheckForItemErrors();

            if (itemErrors)
                return false;

            var lootTableErrors = CheckForLootTableErrors();

            if (lootTableErrors)
                return false;

            return true;
        }

        private bool CheckForLootTableErrors()
        {
            var duplicateIds = LootTables.GroupBy(table => table.Name).SelectMany(grp => grp.Skip(1)).Distinct();

            if (duplicateIds.Any())
            {
                var errorMsg = string.Empty;

                foreach (var item in duplicateIds)
                {
                    errorMsg += string.Format("{0} Has a duplicate Name {1}", item.Name, Environment.NewLine);
                }

                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK);

                return true;
            }

            var wrongDropChance = LootTables.Where(table => table.SumDropChance() != 100 && table.SumDropChance() > 0);

            if(wrongDropChance.Any())
            {
                var errorMsg = string.Empty;

                foreach (var table in wrongDropChance)
                {
                    errorMsg += string.Format("{0} Has an invalid total dropchance {1}", table.Name, Environment.NewLine);
                }

                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK);

                return true;
            }

            return false;
        }

        private bool CheckForItemErrors()
        {
            var duplicateIds = Items.GroupBy(item => item.ID).SelectMany(grp => grp.Skip(1)).Distinct();

            if (duplicateIds.Any())
            {
                var errorMsg = string.Empty;

                foreach (var item in duplicateIds)
                {
                    errorMsg += string.Format("{0} Has a duplicate ID {1}", item.Name, Environment.NewLine);
                }

                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK);

                return true;
            }

            return false;
        }

        public void SaveItems()
        {
            ivOriginalItemFiles.AddRange(ivOriginalLootTableFiles);

            foreach (var file in ivOriginalItemFiles)
            {
                File.Delete(file);
            }

            foreach (var item in Items)
            {
                var jsonItem = JsonConvert.SerializeObject(item);
                File.WriteAllText(ivItemsPath + "\\" + item.Name + ".txt", jsonItem);
            }

            foreach (var lootTable in LootTables)
            {
                var jsonItem = JsonConvert.SerializeObject(lootTable);
                File.WriteAllText(ivLootTablePath + "\\" + lootTable.Name + ".txt", jsonItem);
            }

            MessageBox.Show("Items and LootTables saved!", "Success", MessageBoxButtons.OK);
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
