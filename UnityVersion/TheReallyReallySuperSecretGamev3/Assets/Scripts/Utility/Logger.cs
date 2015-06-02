using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class Logger
    {
        public static void Log(object message)
        {
            if(Debug.isDebugBuild)
                Debug.Log(message);
        }
    }
}
