using UnityEngine;

public class GameData : MonoBehaviour
{
    #region Variables
    [Header("Player values")]
    [Tooltip("Core resources")]
    public int score = 0;
    [Space(5)]
    [Tooltip("Ball stats")]
    public int defaultBallValue = 1;
    public int ballValueLevel = 1;
    public int ballValueUpgradePrice = 10;
    [Space(5)]
    [Tooltip("Multiplier stats")]
    public int multiplierValue = 1;
    public int multiplierLevel = 1;
    public int multiplierUpgradePrice = 200;
    [Space(5)]
    [Tooltip("Peg stats")]
    public int pegUpgradePrice = 5000;
    public int pegLevel = 0;
    [Header("References")]
    private TextUpdater updater; // Cached internally
    public DestroyBalls DestroyBalls;

    public static GameData Instance { get; private set; }
    #endregion

    #region Methods
    public void AddScore(int amount)
    {
        score += amount;
        UpdateAllText();
    }

    private void Awake()
    {
        // Singleton pattern setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Cache TextUpdater reference safely here
        updater = FindFirstObjectByType<TextUpdater>();
        if (updater != null)
        {
            updater.RegisterTextObject("ScoreText");
            updater.RegisterTextObject("BallValUpgText");
            updater.RegisterTextObject("MultiplierUpgText");
        }
        else
        {
            Debug.LogWarning("GameData: No TextUpdater found in the scene.");
        }

        UpdateAllText();
    }

    private void UpdateAllText()
    {
        updater.SetText("BallValUpgText", "Increase Ball Value: $" + ballValueUpgradePrice);
        updater.SetText("MultiplierUpgText", "Increase Multiplier Value: $" + multiplierUpgradePrice);
        updater.SetText("Peg Upgrade Text", "Increase Number of Pegs: $" + pegUpgradePrice);
        updater.SetText("ScoreText", "$" + score);
    }
    #endregion

    #region Upgrade stats
    public void UpgradeBallValue()
    {
        if (score >= ballValueUpgradePrice)
        {
            ballValueLevel++;
            defaultBallValue = Mathf.RoundToInt(ballValueLevel * 1.3f);
            score -= ballValueUpgradePrice;
            ballValueUpgradePrice = Mathf.RoundToInt(10f * Mathf.Pow(1.6f, ballValueLevel));
            UpdateAllText();
        }
    }

    public void UpgradeEndMult()
    {
        if (score >= multiplierUpgradePrice)
        {
            multiplierLevel++;
            multiplierValue = multiplierLevel;
            score -= multiplierUpgradePrice;
            multiplierUpgradePrice = Mathf.RoundToInt(Mathf.Pow(10f, multiplierLevel) * Mathf.RoundToInt(Mathf.Pow(2f, multiplierLevel)));
            UpdateAllText();
            DestroyBalls.UpdateMultLevel();
        }
    }

    public void UpgradePegNumber()
    {
        if (score >= pegUpgradePrice)
        {
            multiplierLevel++;
            multiplierValue = multiplierLevel;
            score -= multiplierUpgradePrice;
            multiplierUpgradePrice = Mathf.RoundToInt(Mathf.Pow(10f, multiplierLevel) * Mathf.RoundToInt(Mathf.Pow(2f, multiplierLevel)));
            UpdateAllText();
            DestroyBalls.UpdateMultLevel();
        }
    }
    #endregion

}
