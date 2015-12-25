using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Utility;

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
            m_ContentRectTransform = m_ScrollRect.content;
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

            float positionTop = m_SelectedRectTransform.localPosition.y;
            float positionBot = (m_SelectedRectTransform.localPosition.y - m_SelectedRectTransform.rect.height);
            positionTop += m_ScrollRect.content.localPosition.y;
            positionBot += m_ScrollRect.content.localPosition.y;

            int count = m_RectTransform.gameObject.GetComponentsInChildren<ButtonSelectedHandler>().Length;
            float offsetPerItem = 1f / count;

            if (positionTop > 0)
            {
                Utility.Logger.Log("Im Above screen");
                m_ScrollRect.normalizedPosition = new Vector2(0, m_ScrollRect.normalizedPosition.y + offsetPerItem);
            }
            else if (positionBot < -m_RectTransform.rect.height)
            {
                Utility.Logger.Log("Im below the screen");
                m_ScrollRect.normalizedPosition = new Vector2(0, m_ScrollRect.normalizedPosition.y - offsetPerItem);
            }

            // math stuff
            //Vector3 selectedDifference = m_RectTransform.localPosition - m_SelectedRectTransform.localPosition;
            //float contentHeightDifference = (m_ContentRectTransform.rect.height - m_RectTransform.rect.height);

            //float selectedPosition = (m_ContentRectTransform.rect.height - selectedDifference.y);
            //float currentScrollRectPosition = m_ScrollRect.normalizedPosition.y * contentHeightDifference;
            //float above = currentScrollRectPosition - (m_SelectedRectTransform.rect.height) + m_RectTransform.rect.height;
            //float below = currentScrollRectPosition + (m_SelectedRectTransform.rect.height);

            //// check if selected is out of bounds
            //if (selectedPosition > above)
            //{
            //    float step = selectedPosition - above;
            //    float newY = currentScrollRectPosition + step;
            //    float newNormalizedY = newY / contentHeightDifference;
            //    //m_ScrollRect.normalizedPosition = Vector2.Lerp(m_ScrollRect.normalizedPosition, new Vector2(0, newNormalizedY), scrollSpeed * Time.deltaTime);
            //}
            //else if (selectedPosition < below)
            //{
            //    float step = selectedPosition - below;
            //    float newY = currentScrollRectPosition + step;
            //    float newNormalizedY = newY / contentHeightDifference;
            //    m_ScrollRect.normalizedPosition = new Vector2(0, m_ScrollRect.normalizedPosition.y - offsetPerItem);
            //    // m_ScrollRect.normalizedPosition = Vector2.Lerp(m_ScrollRect.normalizedPosition, new Vector2(0, newNormalizedY), scrollSpeed * Time.deltaTime);
            //}

        }

    }
}