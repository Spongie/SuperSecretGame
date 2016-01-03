using Assets.Scripts.Character;
using Assets.Scripts.Environment.ResourceManagers;
using Assets.Scripts.Items;
using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

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
        private Item SelectedEquip;
        private List<GameObject> equipButtons;
        private List<GameObject> spellButtons;
        private bool skipFrame = false;

        void Awake()
        {
            equipButtons = new List<GameObject>();
            spellButtons = new List<GameObject>();
            Show();
        }

        public void Show()
        {
            int index = 0;
            bool first = true;
            foreach (Item item in Player.Controller.PlayerInventory.Items.Values)
            {
                GameObject itemButton = AddItemButton(item, index, ParentTransfrom, false);

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

            equipButtons.Clear();

            AddEquippedItems();
        }

        private void AddEquippedItems()
        {
            int index = 0;

            foreach (Item item in Player.Controller.PlayerInventory.GetEqippedItems())
            {
                GameObject equipButton = AddItemButton(item, index, EquipParentTransform, true);
                equipButtons.Add(equipButton);
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

            //spellButton.GetComponent<Button>().onClick.AddListener(() => EquipSelectedItem(spellButton.name));

            SetIcon(spellButton, icon);
            SetText(spellButton.name, spellButton);
            return spellButton;
        }

        void Update()
        {
            if(skipFrame)
            {
                skipFrame = false;
                return;
            }

            if (currentPanel == CurrentPanel.Equipped)
            {
                if (Input.GetButtonDown("A"))
                {
                    if (SelectedEquip.Slot == SelectedItem.Slot)
                    {
                        EquipSelectedItem(SelectedItem.ID, EventSystem.current.currentSelectedGameObject.name);
                        EndEquip();
                    }
                }
                else if (Input.GetButtonDown("B"))
                {
                    EndEquip();
                }
            }
            else
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
        }

        private void EndEquip()
        {
            SelectedEquip = null;
            currentPanel = CurrentPanel.Items;
            EventSystem.current.SetSelectedGameObject(firstItemButton);
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

        private GameObject AddItemButton(Item item, int index, Transform parent, bool isEquipButton)
        {
            var itemButton = Instantiate(ItemButton);
            itemButton.name = item.ID;
            itemButton.transform.SetParent(parent);
            Sprite icon = ItemIconManager.Icons[item.IconName];


            if (isEquipButton)
                setButtonSelectHandlerEquip(item, index, itemButton);
            else
            {
                itemButton.GetComponent<Button>().onClick.AddListener(() => StartEquippingItem(itemButton.name));
                setButtonSelectHandler(item, index, itemButton);
            }

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

        private void setButtonSelectHandlerEquip(Item item, int index, GameObject itemButton)
        {
            ButtonSelectedHandler selectHandler = itemButton.GetComponent<ButtonSelectedHandler>();
            selectHandler.Index = index;
            selectHandler.SourceObject = item;
            selectHandler.onButtonSelected += SelectHandler_onButtonSelectedEquip;
        }

        private void SelectHandler_onButtonSelectedEquip(object sender, System.EventArgs e)
        {
            Item item = sender as Item;

            if (item != null)
            {
                SelectedEquip = item;
            }
        }

        private void SelectHandler_onButtonSelected(object sender, System.EventArgs e)
        {
            Item item = sender as Item;

            if(item != null)
            {
                SelectedItem = item;
            }
        }

        public void StartEquippingItem(string piItemId)
        {
            if (SelectedItem.IsSingleSlotItem())
                EquipSelectedItem(piItemId, null);
            else
            {
                if (Player.Controller.PlayerInventory.HasFreeSlotForItem(SelectedItem))
                {
                    EquipSelectedItem(piItemId, null);
                }
                else
                {
                    Item itemToEquip = Player.Controller.PlayerInventory.Items.Values.First(item => item.ID == piItemId);
                    Item itemToSelect = Player.Controller.PlayerInventory.GetEqippedItemAtSlot(itemToEquip.Slot).First();

                    EventSystem.current.SetSelectedGameObject(equipButtons.First(button => button.name == itemToSelect.ID));
                    currentPanel = CurrentPanel.Equipped;
                    skipFrame = true;
                }
            }
        }

        public void EquipSelectedItem(string piItemID, string piItemIdToReplace)
        {
            Utility.Logger.Log(string.Format("Equpping item with id {0}", piItemID));

            Player.Controller.PlayerInventory.EquipItem(piItemID, piItemIdToReplace);

            RefreshEquippedItems();

            StartCoroutine(DoEquipItem(piItemID, piItemIdToReplace));
        }

        IEnumerator DoEquipItem(string piItemID, string piItemIdToReplace)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            var buttonList = new List<GameObject>();

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
                    var newItemButton = AddItemButton(item, buttonIndex, ParentTransfrom, false);
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

                    if (first)
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
