using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ButtonSelectedHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        public Sprite SelectedSprite;
        public int Index;

        public void OnSelect(BaseEventData eventData)
        {
            GetComponent<Button>().image.overrideSprite = SelectedSprite;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            GetComponent<Button>().image.overrideSprite = null;
        }
    }
}
