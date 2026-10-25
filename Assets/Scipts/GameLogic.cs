using System;

public class GameLogic
{
    public const int Goal = 1000;

    private ISaveScore save;
    private IVibrate vibrations;

    //Game
    public int Points { get; private set; }
    public int TotalPoints { get; private set; }
    public float TimePased { get; private set; }
    public bool Finished { get; private set; }
    public bool Paused { get; private set; }

    //Shop
    public int TapLevel { get; private set; }
    public int AutoTapLevel { get; private set; }
    public int PointsPerTap 
    {
        get { return 1 + TapLevel;  }
    }
    public int PointsPerSecond
    {
        get { return AutoTapLevel;  }
    }
    public int TapUpgradeCost
    {
        get { return (int)Math.Ceiling(10 * Math.Pow(1.5, TapLevel)); }
    }
    public int AutoTapUpgradeCost
    {
        get { return (int)Math.Ceiling(25 * Math.Pow(1.6, AutoTapLevel)); }
    }

    //High Score
    public float BestTime
    {
        get { return save.LoadBestTime(); }
    }

    private float sinceLastSecond;

    public event Action Won;

    public GameLogic(ISaveScore save, IVibrate vibration)
    {
        this.save = save;
        this.vibrations = vibration;
    }

    public void Tap()
    {
        if (Paused || Finished) return;
        AddPoints(PointsPerTap);
    }

    public void Tick(float deltaTime)
    {
        if (Paused || Finished) return;
        TimePased += deltaTime;

        if (PointsPerSecond <= 0) return;

        sinceLastSecond += deltaTime;
        if (sinceLastSecond >= 1f)
        {
            sinceLastSecond -= 1f;
            AddPoints((int)(PointsPerSecond));
        }
    }

    public bool BuyTapUpgrade()
    {
        if (!TrySpend(TapUpgradeCost)) return false;
        TapLevel++;
        return true;
    }

    public bool BuyAutoTapUpgrade()
    {
        if (!TrySpend(AutoTapUpgradeCost)) return false;
        AutoTapLevel++;
        return true;
    }

    public void SetPaused(bool paused)
    {
        Paused = paused;
    }

    public void Restart()
    {
        Points = 0;
        TotalPoints = 0;
        TimePased = 0;
        TapLevel = 0;
        AutoTapLevel = 0;
        Finished = false;
        Paused = false;
        sinceLastSecond = 0f;
    }

    private bool TrySpend(int cost)
    {
        if (Points < cost) return false;
        Points -= cost;
        vibrations.MakeVibrate();
        return true;
    }

    private void AddPoints(int amount)
    {
        Points += amount;
        TotalPoints += amount;
        if (TotalPoints >= Goal) Finish();
    }

    private void Finish()
    {
        Finished = true;
        if (BestTime <= 0 || TimePased < BestTime) save.SaveBestTime(TimePased);
        vibrations.MakeVibrate();
        Won.Invoke();
    }
}
