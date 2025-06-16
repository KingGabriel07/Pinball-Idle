using UnityEngine;

public class BallsHandler : MonoBehaviour
{
    public GameObject ballPrefab;   // Assign the Ball prefab in Inspector
    private Transform ballsParent;  // Parent object named "Balls"
    private Vector2 spawnScale = new Vector2(0.4f, 0.4f);

    void Start()
    {
        // Find the "Balls" GameObject in the scene
        GameObject parentObj = GameObject.Find("Balls");
        if (parentObj != null)
        {
            ballsParent = parentObj.transform;
        }
        else
        {
            Debug.LogWarning("GameObject named 'Balls' not found in the scene.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; // Ensure we're in 2D space

            // Check if the cursor is over SpawnArea
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.name == "SpawnArea")
            {
                if (ballPrefab != null && ballsParent != null)
                {
                    GameObject ball = Instantiate(ballPrefab, mouseWorldPos, Quaternion.identity, ballsParent);
                    ball.transform.localScale = new Vector3(spawnScale.x, spawnScale.y, 1f); // Apply custom size
                }
            }
        }
    }
}

