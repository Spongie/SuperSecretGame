using Assets.Scripts.Character;
using Assets.Scripts.Environment.ResourceManagers;
using Assets.Scripts.Items;
using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class MenuController : MonoBehaviour
    {
        public Player Player;
        public GameObject ItemButton;
        public Transform ParentTransfrom;
        public IconManager ItemIconManager;

        void Awake()
        {
            Show();
        }

        public void Show()
        {
            bool first = true;
            foreach (Item item in Player.ivController.PlayerInventory.Items.Values)
            {
                var itemButton = Instantiate(ItemButton);
                itemButton.name = item.ID;
                itemButton.transform.SetParent(ParentTransfrom);
                Sprite icon = ItemIconManager.Icons[item.IconName];

                SetIcon(itemButton, icon);
                SetText(item, itemButton);

                if(first)
                {
                    EventSystem.current.SetSelectedGameObject(itemButton.GetComponentInChildren<Button>().gameObject);
                    first = false;
                }
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
