using UnityEngine;

public class SafeAreaController : MonoBehaviour
{
    private Rect lastSafeArea;

    private void Update()
    {
        if (Screen.safeArea == lastSafeArea) return;
        lastSafeArea = Screen.safeArea;

        var rt = GetComponent<RectTransform>();
        Vector2 min = lastSafeArea.position;
        Vector2 max = lastSafeArea.position + lastSafeArea.size;

        rt.anchorMin = new Vector2(min.x / Screen.width, min.y / Screen.height);
        rt.anchorMax = new Vector2(max.x / Screen.width, max.y / Screen.height);

        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }
}
