
public static class Logging
{
    static public void Log(object message)
    {
#if UNITY_EDITOR
        UnityEngine.Debug.Log(message);
#endif
    }
}