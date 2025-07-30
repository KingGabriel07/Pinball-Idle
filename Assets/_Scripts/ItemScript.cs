using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public HandleItemPositions inventory;

    private void OnMouseDown()
    {
        Debug.Log("Clicked on: " + gameObject.name);
        inventory.PickUpItem(this.gameObject);
    }
}
