namespace Helpers
{
    public static class NSBLogger
    {
        public static void Log(string message)
        {
            if(UnityEngine.Debug.isDebugBuild)
                UnityEngine.Debug.Log(message);
        }
    }
}