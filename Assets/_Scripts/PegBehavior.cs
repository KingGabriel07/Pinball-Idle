using UnityEngine;

public class PegBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    #region Variables
    public Vector2 pegScale = new Vector2(0.75f, 0.75f);
    #endregion
    void Start()
    {
        transform.localScale = new Vector3(pegScale.x, pegScale.y, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
