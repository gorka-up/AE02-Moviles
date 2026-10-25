using UnityEngine;

public interface ISaveScore
{
    float LoadBestTime();
    void SaveBestTime(float seconds);
}
