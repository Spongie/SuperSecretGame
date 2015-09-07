using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class ButtonSelectedHandler : MonoBehaviour, ISelectHandler
    {
        public int Index;

        public void OnSelect(BaseEventData eventData)
        {
            Logger.Log(string.Format("Select {0} from the list", gameObject.transform.parent.name));
        }
    }
}
