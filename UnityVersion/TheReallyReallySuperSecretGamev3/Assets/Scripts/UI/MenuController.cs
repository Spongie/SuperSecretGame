using Assets.Scripts.Character;
using Assets.Scripts.Environment.ResourceManagers;
using Assets.Scripts.Items;
using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Assets.Scripts.UI
{
    public enum CurrentPanel
    {
        Items,
        Equipped,
        Spells
    }

    public class MenuController : MonoBehaviour
    {
        public Player Player;
        public GameObject ItemButton;
        public GameObject SpellButton;
        public Transform ParentTransfrom;
        public IconManager ItemIconManager;
        public GameObject firstItemButton;
        public GameObject firstSpellButton;
        private CurrentPanel currentPanel;
        private Item SelectedItem;
        
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
            if (Input.GetButtonDown("RB"))
            {
                if (IsSpellButtonSelected())
                {
                    SelectButton(firstItemButton);
                    currentPanel = CurrentPanel.Items;
                }
                else if (IsItemButtonSelected())
                {
                    SelectButton(firstSpellButton);
                    currentPanel = CurrentPanel.Spells;
                }
            }
            else if (Input.GetButtonDown("A"))
            {
                Player.Controller.PlayerInventory.EquipItem(SelectedItem);             
            }
        }

        private void SelectButton(GameObject button)
        {
            EventSystem.current.SetSelectedGameObject(button);
        }

        private static bool IsSpellButtonSelected()
        {
            return EventSystem.current.currentSelectedGameObject.GetComponent<SpellButton>() != null;
        }

        private static bool IsItemButtonSelected()
        {
            return EventSystem.current.currentSelectedGameObject.GetComponent<ItemButton>() != null;
        }

        private GameObject AddItemButton(Item item, int index)
        {
            var itemButton = Instantiate(ItemButton);
            itemButton.name = item.ID;
            itemButton.transform.SetParent(ParentTransfrom);
            Sprite icon = ItemIconManager.Icons[item.IconName];

            itemButton.GetComponent<Button>().onClick.AddListener(() => EquipSelectedItem(itemButton.name));
            setButtonSelectHandler(item, index, itemButton);

            SetIcon(itemButton, icon);
            SetText(item, itemButton);
            return itemButton;
        }

        private void setButtonSelectHandler(Item item, int index, GameObject itemButton)
        {
            ButtonSelectedHandler selectHandler = itemButton.GetComponent<ButtonSelectedHandler>();
            selectHandler.Index = index;
            selectHandler.SourceObject = item;
            selectHandler.onButtonSelected += SelectHandler_onButtonSelected;
        }

        private void SelectHandler_onButtonSelected(object sender, System.EventArgs e)
        {
            Item item = sender as Item;

            if(item != null)
            {
                SelectedItem = item;
            }
        }

        public void EquipSelectedItem(string piItemID)
        {
            Utility.Logger.Log(string.Format("Equpping item with id {0}", piItemID));
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
    }
}
