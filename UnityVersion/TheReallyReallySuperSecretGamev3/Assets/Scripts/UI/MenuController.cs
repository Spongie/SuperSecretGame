﻿using Assets.Scripts.Character;
using Assets.Scripts.Environment.ResourceManagers;
using Assets.Scripts.Items;
using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.UI
{
    public enum CurrentPanel
    {
        Items,
        Equipped
    }

    public class MenuController : MonoBehaviour, ISelectHandler
    {
        public Player Player;
        public GameObject ItemButton;
        public Transform ParentTransfrom;
        public IconManager ItemIconManager;
        public GameObject firstItemButton;
        private CurrentPanel currentPanel;

        void Awake()
        {
            //Show();
        }

        public void Show()
        {
            int index = 0;
            bool first = true;
            foreach (Item item in Player.Controller.PlayerInventory.Items.Values)
            {
                GameObject itemButton = AddItemButton(item, index);

                if (first)
                {
                    firstItemButton = itemButton;
                    EventSystem.current.SetSelectedGameObject(itemButton);
                    first = false;
                }

                index++;
            }

            currentPanel = CurrentPanel.Items;
        }

        void Update()
        {
            if(Input.GetButtonDown("RB"))
            {

            }
            else if(Input.GetButtonDown("LB") && currentPanel != CurrentPanel.Items)
            {
                if (firstItemButton != null)
                {
                    EventSystem.current.SetSelectedGameObject(firstItemButton);
                    currentPanel = CurrentPanel.Equipped;
                }
            }
        }

        private GameObject AddItemButton(Item item, int index)
        {
            var itemButton = Instantiate(ItemButton);
            itemButton.name = item.ID;
            itemButton.transform.SetParent(ParentTransfrom);
            Sprite icon = ItemIconManager.Icons[item.IconName];

            itemButton.GetComponent<Button>().onClick.AddListener(() => EquipSelectedItem(itemButton.name));
            itemButton.GetComponent<ButtonSelectedHandler>().Index = index;

            SetIcon(itemButton, icon);
            SetText(item, itemButton);
            return itemButton;
        }

        public void EquipSelectedItem(string piItemID)
        {
            Logger.Log(string.Format("Equpping item with id {0}", piItemID));
            var buttonList = new List<GameObject>();
            Player.Controller.PlayerInventory.EquipItem(piItemID);
            var button = GameObject.Find(piItemID);
            int buttonIndex = button.GetComponent<ButtonSelectedHandler>().Index;

            GameObject buttonBefore = null;
            GameObject buttonAfter = null;
            GameObject fallbackButton = null;

            bool found = false;
            bool first = true;
            foreach (Item item in Player.Controller.PlayerInventory.Items.Values)
            {
                var itemButton = GameObject.Find(item.ID);

                if (itemButton == null)
                {
                    var newItemButton = AddItemButton(item, buttonIndex);
                    buttonList.Add(newItemButton);
                }
                else if (item.StackSize > 0)
                {
                    SetText(item, itemButton);

                    if (item.ID == piItemID)
                        found = true;

                    if (itemButton.GetComponent<ButtonSelectedHandler>().Index == buttonIndex - 1)
                        buttonBefore = itemButton;
                    else if (itemButton.GetComponent<ButtonSelectedHandler>().Index == buttonIndex + 1)
                        buttonAfter = itemButton;

                    if(first)
                    {
                        fallbackButton = itemButton;
                        first = false;
                    }

                    buttonList.Add(itemButton);
                }
            }

            if (!found)
            {
                if (buttonBefore != null)
                    EventSystem.current.SetSelectedGameObject(buttonBefore);
                else if (buttonAfter != null)
                    EventSystem.current.SetSelectedGameObject(buttonAfter);
                else
                    EventSystem.current.SetSelectedGameObject(fallbackButton);

                Destroy(button);
            }

            NormalizeIndexes(buttonList);
        }

        private static void NormalizeIndexes(List<GameObject> buttonList)
        {
            int index = 0;
            foreach (var itemButton in buttonList)
            {
                itemButton.GetComponent<ButtonSelectedHandler>().Index = index;
                index++;
            }
        }

        private static void SetText(Item item, GameObject itemButton)
        {
            foreach (var buttonText in itemButton.GetComponentsInChildren<Text>())
            {
                if (buttonText.name == "Text")
                    buttonText.text = item.GetListString();
            }
        }

        private static void SetIcon(GameObject itemButton, Sprite icon)
        {
            foreach (var image in itemButton.GetComponentsInChildren<Image>())
            {
                if (image.name == "ItemIconImage")
                    image.sprite = icon;
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}
