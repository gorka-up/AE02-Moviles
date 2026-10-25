using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ViewManager view;

    private GameLogic gameLogic;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        gameLogic = new GameLogic(new SaveScore(), new Vibrate());
        gameLogic.Won += () => view.ShowWin(true, gameLogic.TimePased);
    }

    private void OnEnable() => view.Target.Tapped += TargetTapped;
    private void OnDisable() => view.Target.Tapped -= TargetTapped;

    private void Start()
    {
        view.ShowShop(false);
        view.ShowPause(false);
        view.ShowWin(false);
        view.MoveTargetRandom();
    }

    private void Update()
    {
        gameLogic.Tick(Time.deltaTime);
        view.Render(gameLogic);
    }

    //Evento de target siendo tapeado
    private void TargetTapped()
    {
        gameLogic.Tap();
        view.MoveTargetRandom();
    }

    public void OnOpenShop()
    {
        view.ShowShop(true);
    }

    public void OnCloseShop()
    {
        view.ShowShop(false);
    }

    public void OnBuyTapUpgrade()
    {
        gameLogic.BuyTapUpgrade();
    }

    public void OnBuyAutoTapUpgrade()
    {
        gameLogic.BuyAutoTapUpgrade();
    }

    public void OnPause()
    {
        gameLogic.SetPaused(true);
        view.ShowPause(true);
    }

    public void OnResume()
    {
        gameLogic.SetPaused(false);
        view.ShowPause(false);
    }

    public void OnRestart()
    {
        gameLogic.Restart();
        view.ShowWin(false);
        view.ShowShop(false);
        view.MoveTargetRandom();
    }
}
