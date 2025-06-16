using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    #region Variables
    public Rigidbody2D body;
    public float gravForce = 981f;
    public float forceMagnitude = 10f;
    public Vector2 gravity = Vector2.down;
    public Vector2 currentVelocity;
    public GameData GameData;
    public int ballValue;
    #region Upgradable Variables
    public float addToBounce = 0f;
    #endregion
    #endregion
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        // Initialize ballValue using GameData (if available)
        if (GameData.Instance != null)
        {
            ballValue = GameData.Instance.defaultBallValue;
        }
        else
        {
            ballValue = 1; // Fallback default if GameData isn't ready
            Debug.LogWarning("GameData.Instance is null, using fallback value for ballValue.");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(body != null)
        body.AddForce(gravity * gravForce * Time.deltaTime);
        
    }
    void FixedUpdate()
    {
        // Continuously update the current velocity each physics frame
        currentVelocity = body.linearVelocity;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Peg"))
        {
            // Base direction
            Vector2 direction = ((Vector2)transform.position - (Vector2)collision.transform.position).normalized;

            // Add a small random angle variation (in degrees)
            float angleVariation = Random.Range(-5f, 5f); // Very slight random angle
            direction = Quaternion.Euler(0, 0, angleVariation) * direction;

            // Optionally randomize force magnitude slightly
            float randomForce = forceMagnitude + Random.Range(-0.2f, 0.2f);

            // Add force with optional randomness
            body.AddForce(direction * (randomForce + addToBounce), ForceMode2D.Impulse);

            ballValue += Mathf.RoundToInt(GameData.Instance != null ? GameData.Instance.defaultBallValue : 1);
            forceMagnitude -= 2;
        }
    }
}
