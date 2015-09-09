using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollToSelected : MonoBehaviour
    {
        public float scrollSpeed = 10f;

        ScrollRect m_ScrollRect;
        RectTransform m_RectTransform;
        RectTransform m_ContentRectTransform;
        RectTransform m_SelectedRectTransform;

        void Awake()
        {
            m_ScrollRect = GetComponent<ScrollRect>();
            m_RectTransform = GetComponent<RectTransform>();
            m_ContentRectTransform =  m_ScrollRect.content;
        }

        void Update()
        {
            UpdateScrollToSelected();
        }

        void UpdateScrollToSelected()
        {

            // grab the current selected from the eventsystem
            GameObject selected = EventSystem.current.currentSelectedGameObject;

            if (selected == null)
            {
                return;
            }
            if (selected.transform.parent != m_ContentRectTransform.transform)
            {
                return;
            }

            m_SelectedRectTransform = selected.GetComponent<RectTransform>();

            

        }

    }
}