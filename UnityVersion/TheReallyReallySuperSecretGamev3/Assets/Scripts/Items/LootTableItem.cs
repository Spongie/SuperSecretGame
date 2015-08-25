﻿using System;
using System.ComponentModel;

namespace Assets.Scripts.Items
{
    public class LootTableItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler DropChanceChanged;

        private string ivItemName;

        public string ItemName
        {
            get { return ivItemName; }
            set 
            { 
                ivItemName = value;
                FirePropertyChanged("ItemName");
            }
        }

        private int ivDropChange;

        public int DropChance
        {
            get { return ivDropChange; }
            set 
            { 
                ivDropChange = value;
                FirePropertyChanged("DropChance");

                if (DropChanceChanged != null)
                    DropChanceChanged(this, new EventArgs());
            }
        }


        private void FirePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}