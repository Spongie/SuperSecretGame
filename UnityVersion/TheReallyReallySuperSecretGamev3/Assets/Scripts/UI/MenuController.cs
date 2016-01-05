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
using Assets.Scripts.Attacks;
using Assets.Scripts.Spells;

namespace Assets.Scripts.UI
{
    public enum CurrentPanel
    {
        Items,
        EquippedSelect,
        Spells,
        EquippedSpells,
        EquippedInspect
    }

    public class MenuController : MonoBehaviour
    {
        public Player Player;
        public GameObject ItemButton;
        public GameObject SpellButton;
        public GameObject ItemEquipButton;
        public Transform ParentTransfrom;
        public Transform EquipParentTransform;
        public Transform SpellParentTransform;
        public Transform SpellEquipParentTransform;
        public IconManager ItemIconManager;
        public GameObject firstItemButton;
        public GameObject firstSpellButton;
        private CurrentPanel currentPanel;
        private Item SelectedItem;
        private Item SelectedEquip;
        private List<GameObject> equipButtons;
        private List<GameObject> spellButtons;
        private bool skipFrame = false;
        private SpellInfo selectedSpell;
        private bool hadAnyItems = false;
        private string spellToEquip;
        public GameObject SpellPrompt;

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
                hadAnyItems = true;
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
            AddSpellButtons();
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

            foreach (Item item in Player.Controller.PlayerInventory.GetEquippedItemsMenu())
            {
                GameObject equipButton = AddItemButton(item, index, EquipParentTransform, true);
                equipButtons.Add(equipButton);
                index++;
            }
        }

        private void AddSpellButtons()
        {
            bool first = true;

            foreach (var spell in Player.Controller.SpellController.GetAvailableSpells())
            {
                GameObject spellButton = AddSpellButton(spell.name, false, SpellParentTransform);

                //setSpellButtonSelectHandler(spellButton.GetComponent<SpellInfo>(), 0, spellButton);

                if (first)
                {
                    if (!hadAnyItems)
                        EventSystem.current.SetSelectedGameObject(spellButton);

                    firstSpellButton = spellButton;
                    first = false;
                }
            }

            AddEquippedSpells();
        }

        private void AddEquippedSpells()
        {
            var equippedSpells = Player.Controller.SpellController.GetEquippedSpells();
            spellButtons.Clear();

            foreach (var equippedSpell in equippedSpells.OrderBy(spell => (int)spell.Key))
            {
                var spellButton = AddSpellButton(equippedSpell.Value, true, SpellEquipParentTransform);
                spellButtons.Add(spellButton);
            }
        }

        private void RefreshEquippedSpellbuttons()
        {
            foreach (var button in spellButtons)
            {
                Destroy(button);
            }

            AddEquippedSpells();
        }

        private GameObject AddSpellButton(string spell, bool isEquip, Transform parentTransform)
        {
            GameObject spellButton = Instantiate(SpellButton);
            spellButton.name = spell;
            spellButton.transform.SetParent(parentTransform);
            Sprite icon = ItemIconManager.Icons[spell];

            if(isEquip)
            {
                //spellButtons.Add(spellButton);
            }
            else
            {
                spellButton.GetComponent<Button>().onClick.AddListener(() => StartEquippingSpell(spell));
            }


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

            if (currentPanel == CurrentPanel.EquippedSelect)
            {
                if (Input.GetButtonDown("A"))
                {
                    if (SelectedEquip.Slot == SelectedItem.Slot)
                    {
                        EquipSelectedItem(SelectedItem.ID, EventSystem.current.currentSelectedGameObject.name);
                        EndEquip(false);
                    }
                }
                else if (Input.GetButtonDown("B"))
                {
                    EndEquip(false);
                }
            }
            else if (currentPanel == CurrentPanel.EquippedSpells)
            {
                if(Input.GetButtonDown("A"))
                {
                    EquipSpell(SpellSlot.Normal);
                }
                var inputY = Input.GetAxisRaw("Vertical");

                if(inputY < 0)
                {
                    EquipSpell(SpellSlot.Down);
                }
                else if (inputY > 0)
                {
                    EquipSpell(SpellSlot.Up);
                }

                var inputX = Input.GetAxisRaw("Horizontal");

                if( inputX != 0)
                {
                    EquipSpell(SpellSlot.Forward);
                }

            }
            else if (currentPanel == CurrentPanel.EquippedInspect)
            {
                UpdatePanelSwap(); 
            }
            else
            {
                UpdatePanelSwap();
            }
        }

        private void UpdatePanelSwap()
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
                    SelectButton(equipButtons.First());
                    currentPanel = CurrentPanel.EquippedInspect;
                }
                else
                {
                    SelectButton(firstSpellButton);
                    currentPanel = CurrentPanel.Spells;
                }
            }
            else if (Input.GetButtonDown("LB"))
            {
                if (IsItemButtonSelected())
                {
                    SelectButton(firstSpellButton);
                    currentPanel = CurrentPanel.Spells;
                }
                else if (IsSpellButtonSelected())
                {
                    SelectButton(equipButtons.First());
                    currentPanel = CurrentPanel.EquippedInspect;
                }
                else
                {
                    SelectButton(firstItemButton);
                    currentPanel = CurrentPanel.Items;
                }
            }
            else if (Input.GetButtonDown("B"))
            {
                Time.timeScale = 1;
                gameObject.SetActive(false);
                GlobalState.CurrentState = GlobalGameState.Playing;
            }
        }

        private void EquipSpell(SpellSlot slot)
        {
            Player.Controller.SpellController.EquipSpell(slot, spellToEquip);
            EndEquip(true);
            RefreshEquippedSpellbuttons();
        }

        private void EndEquip(bool equippedSpell)
        {
            SelectedEquip = null;
            currentPanel = CurrentPanel.Items;

            if (equippedSpell)
            {
                EventSystem.current.SetSelectedGameObject(firstSpellButton);
                SpellPrompt.SetActive(false);
            }
            else
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
            GameObject buttonToSpawn = isEquipButton ? ItemEquipButton : ItemButton;

            var itemButton = Instantiate(buttonToSpawn);
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

        private void setSpellButtonSelectHandler(SpellInfo spell, int index, GameObject spellButton)
        {
            ButtonSelectedHandler selectHandler = spellButton.GetComponent<ButtonSelectedHandler>();
            selectHandler.Index = index;
            selectHandler.SourceObject = spell;
            selectHandler.onButtonSelected += SelectHandler_onSpellButtonSelected;
        }

        private void SelectHandler_onSpellButtonSelected(object sender, System.EventArgs e)
        {
            SpellInfo info = sender as SpellInfo;

            if (info != null)
            {
                selectedSpell = info;
            }
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

        public void StartEquippingSpell(string spell)
        {
            currentPanel = CurrentPanel.EquippedSpells;
            skipFrame = true;
            spellToEquip = EventSystem.current.currentSelectedGameObject.name;
            EventSystem.current.SetSelectedGameObject(null);
            SpellPrompt.SetActive(true);
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
                    currentPanel = CurrentPanel.EquippedSelect;
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
                        firstItemButton = itemButton;
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
