using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ButtonSelectedHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        public Sprite SelectedSprite;
        public int Index;
        public object SourceObject;
        public event EventHandler onButtonSelected;

        public void OnSelect(BaseEventData eventData)
        {
            GetComponent<Button>().image.overrideSprite = SelectedSprite;

            if(onButtonSelected != null)
                onButtonSelected(SourceObject, new EventArgs());
        }

        public void OnDeselect(BaseEventData eventData)
        {
            GetComponent<Button>().image.overrideSprite = null;
        }
    }
}
