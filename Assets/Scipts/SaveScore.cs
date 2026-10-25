using UnityEngine;

public class SaveScore : ISaveScore
{
    private const string Key = "BestTime";

    public float LoadBestTime()
    {
        return PlayerPrefs.GetFloat(Key, 0f);
    }

    public void SaveBestTime(float seconds)
    {
        PlayerPrefs.SetFloat(Key, seconds);
        PlayerPrefs.Save();
    }
}
