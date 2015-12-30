using Assets.Scripts.Character;
using Assets.Scripts.Environment.ResourceManagers;
using Assets.Scripts.Items;
using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

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
        public Transform EquipParentTransform;
        public Transform SpellParentTransform;
        public IconManager ItemIconManager;
        public GameObject firstItemButton;
        public GameObject firstSpellButton;
        private CurrentPanel currentPanel;
        private Item SelectedItem;
        private List<GameObject> equipButtons;
        private List<GameObject> spellButtons;

        void Awake()
        {
            equipButtons = new List<GameObject>();
            spellButtons = new List<GameObject>();
            //Show();
        }

        public void Show()
        {
            int index = 0;
            bool first = true;
            foreach (Item item in Player.Controller.PlayerInventory.Items.Values)
            {
                GameObject itemButton = AddItemButton(item, index, ParentTransfrom);

                if (first)
                {
                    firstItemButton = itemButton;
                    EventSystem.current.SetSelectedGameObject(itemButton);
                    first = false;
                }

                index++;
            }

            currentPanel = CurrentPanel.Items;

            AddEquippedItems();
        }

        private void RefreshEquippedItems()
        {
            foreach (GameObject button in equipButtons)
            {
                Destroy(button);
            }

            AddEquippedItems();
        }

        private void AddEquippedItems()
        {
            int index = 0;

            foreach (Item item in Player.Controller.PlayerInventory.GetEqippedItems())
            {
                GameObject equipButton = AddItemButton(item, index, EquipParentTransform);
                index++;
            }
        }

        private void AddSpellButtons()
        {
            bool first = true;
            var equippedSpells = Player.Controller.SpellController.GetEquippedSpells();

            foreach (var spell in Player.Controller.SpellController.GetAvailableSpells())
            {
                if (equippedSpells.Values.Any(spellName => spellName == spell.name))
                    continue;

                GameObject spellButton = AddSpellButton(spell);

                if (first)
                {
                    firstSpellButton = spellButton;
                    first = false;
                }

                spellButtons.Add(spellButton);
            }

            foreach (var equippedSpell in equippedSpells.OrderBy(spell => (int)spell.Key))
            {

            }
        }

        private GameObject AddSpellButton(GameObject spell)
        {
            GameObject spellButton = Instantiate(SpellButton);
            SpellButton.name = spell.name;
            SpellButton.transform.SetParent(SpellParentTransform);
            Sprite icon = ItemIconManager.Icons[spell.name];

            spellButton.GetComponent<Button>().onClick.AddListener(() => EquipSelectedItem(spellButton.name));

            SetIcon(spellButton, icon);
            SetText(spellButton.name, spellButton);
            return spellButton;
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
            }
            else if (Input.GetButtonDown("LB"))
            {
                if (IsItemButtonSelected())
                {
                    SelectButton(firstSpellButton);
                    currentPanel = CurrentPanel.Spells;
                }
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

        private GameObject AddItemButton(Item item, int index, Transform parent)
        {
            var itemButton = Instantiate(ItemButton);
            itemButton.name = item.ID;
            itemButton.transform.SetParent(parent);
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
                    var newItemButton = AddItemButton(item, buttonIndex, ParentTransfrom);
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

            NormalizeButtonIndexes(buttonList);

            RefreshEquippedItems();
        }

        private static void NormalizeButtonIndexes(List<GameObject> buttonList)
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

        private static void SetText(string Text, GameObject button)
        {
            foreach (var buttonText in button.GetComponentsInChildren<Text>())
            {
                if (buttonText.name == "Text")
                    buttonText.text = Text;
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
