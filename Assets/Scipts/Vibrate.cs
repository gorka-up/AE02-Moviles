using UnityEngine;

public class Vibrate : IVibrate
{
    public void MakeVibrate()
    {
#if UNITY_EDITOR
        Debug.Log("Simulate vibration");
#elif UNITY_ANDROID
        Handheld.Vibrate();
#elif UNITY_IOS
        Handheld.Vibrate();
#endif
    }
}
