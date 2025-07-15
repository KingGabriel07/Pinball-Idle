using UnityEngine;

public class DestroyBalls : MonoBehaviour
{
    #region Variables
    public GameData GameData;
    public int destructionMultiplier = 1;
    #endregion
    void Start()
    {
        if (GameData.Instance != null)
        {
            destructionMultiplier = GameData.Instance.multiplierValue;
        }
        else
        {
            destructionMultiplier = 1; // Fallback default if GameData isn't ready
            Debug.LogWarning("GameData.Instance is null, using fallback value for ballValue.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        BallBehavior ball = collision.gameObject.GetComponent<BallBehavior>();

        if (ball != null)
        {
            int value = ball.ballValue;

            if (GameData.Instance != null)
            {
                GameData.Instance.AddScore(value * destructionMultiplier);
            }
            else
            {
                Debug.LogWarning("GameData.Instance is null!");
            }

            Destroy(collision.gameObject);
        }
        else
        {
            Debug.LogWarning("Collided object does not have a BallBehavior component!");
        }
    }
    public void UpdateMultLevel()
    {
        destructionMultiplier = GameData.Instance.multiplierValue;
    }
}
