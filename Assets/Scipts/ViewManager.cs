using TMPro;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [Header("HUD")]
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text totalPointsText;
    [SerializeField] private TMP_Text timerText;

    [Header("TARGET")]
    [SerializeField] private TargetController target;

    [Header("SCREEN SAFE ZONE")]
    [Tooltip("Top margin")]
    [SerializeField, Range(0f, 0.4f)] private float marginTop = 0.22f;
    [Tooltip("Bottom margin")]
    [SerializeField, Range(0f, 0.4f)] private float marginBottom = 0.15f;
    [Tooltip("Side margin")]
    [SerializeField, Range(0f, 0.3f)] private float marginSides = 0.12f;

    [Header("Shop")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TMP_Text tapUpgradeText;
    [SerializeField] private TMP_Text autoTapUpgradeText;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pausePanel;
    [Header("Win Menu")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TMP_Text winTimeText;

    public TargetController Target => target;

    public void Render(GameLogic gameLogic)
    {
        pointsText.text = $"Points: {gameLogic.Points.ToString()}";
        totalPointsText.text = $"{gameLogic.TotalPoints.ToString()}/{GameLogic.Goal}";
        timerText.text = $"{gameLogic.TimePased:0.0}s";
        tapUpgradeText.text = $"COST: {gameLogic.TapUpgradeCost}";
        autoTapUpgradeText.text = $"COST: {gameLogic.AutoTapUpgradeCost}";
    }

    public void MoveTargetRandom()
    {
        Rect safeArea = Screen.safeArea;
        Vector3 oldPos = target.transform.position;
        Vector3 nextPos = oldPos;

        float x = Random.Range(safeArea.xMin + safeArea.width * marginSides, safeArea.xMax - safeArea.width * marginSides);
        float y = Random.Range(safeArea.yMin + safeArea.height * marginBottom, safeArea.yMax - safeArea.height * marginTop);
        nextPos = ScreenToWorld(x, y);

        target.transform.position = nextPos;
    }

    public void ShowShop(bool show)
    {
        shopPanel.SetActive(show);
    }
    public void ShowPause(bool show)
    {
        pausePanel.SetActive(show);
    }

    public void ShowWin(bool show, float time = 0f)
    {
        winPanel.SetActive(show);
        winTimeText.text = $"Time: {time:0.0}s";
    }

    private Vector3 ScreenToWorld(float x, float y)
    {
        Vector3 pos = cam.ScreenToWorldPoint(new Vector3(x, y, -cam.transform.position.z));
        pos.z = 0f;
        return pos;
    }

    private void OnDrawGizmos()
    {
        Rect safeZone = new Rect(0f, 0f, 1f, 1f);

        float xMin = safeZone.xMin + safeZone.width * marginSides;
        float xMax = safeZone.xMax - safeZone.width * marginSides;
        float yMin = safeZone.yMin + safeZone.height * marginBottom;
        float yMax = safeZone.yMax - safeZone.height * marginTop;

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(xMin, yMin, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(xMax, yMax, 0));
        Vector3 center = (bottomLeft + topRight) / 2f;
        Vector3 size = topRight - bottomLeft;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);
    }
}
