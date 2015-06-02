using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class Logger
    {
        public static void Log(string message)
        {
            if(Debug.isDebugBuild)
                Debug.Log(message);
        }
    }
}
