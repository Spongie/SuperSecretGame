using System.Collections.Generic;

namespace Assets.Scripts.Utility
{
    public class ButtonLock
    {
        private List<string> ivLockedButtons;
        private static ButtonLock ivInstance;

        public ButtonLock()
        {
            ivLockedButtons = new List<string>();
        }

        public static ButtonLock Instance
        {
            get
            {
                if (ivInstance == null)
                    ivInstance = new ButtonLock();

                return ivInstance;
            }
        }

        public void AddLock(string buttonID)
        {
            if(!ivLockedButtons.Contains(buttonID))
                ivLockedButtons.Add(buttonID);
        }

        public void ClearLock(string buttonId)
        {
            ivLockedButtons.Remove(buttonId);
        }

        public bool IsButtonLocked(string buttonId)
        {
            return ivLockedButtons.Contains(buttonId);
        }
    }
}
