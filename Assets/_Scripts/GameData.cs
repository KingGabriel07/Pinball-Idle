using UnityEngine;

public class GameData : MonoBehaviour
{
    #region Variables
    [Header("Player values")]
    public int score = 0;

    public int defaultBallValue = 1;
    public int ballValueLevel = 1;
    public int ballValueUpgradePrice = 10;

    [Header("References")]
    private TextUpdater updater; // Cached internally

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
        updater.SetText("ScoreText", "$" + score);
    }
    #endregion

    #region Upgrade stats
    public void UpgradeBallValue()
    {
        if(score >= ballValueUpgradePrice)
        {
            ballValueLevel++;
            defaultBallValue = Mathf.RoundToInt(ballValueLevel * 1.3f);
            score -= ballValueUpgradePrice;
            ballValueUpgradePrice = Mathf.RoundToInt(10f * Mathf.Pow(1.6f, ballValueLevel));
            UpdateAllText();
        }
        
    }
    #endregion

}
