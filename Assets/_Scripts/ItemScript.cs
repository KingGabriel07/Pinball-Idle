using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public string itemName;
    public int itemID;
    public HandleItemPositions inventory;

    private void OnMouseDown()
    {
        inventory.PickUpItem(gameObject);
    }

    private void OnDestroy()
    {
        inventory?.UnregisterItem(gameObject); // Safety check
    }
}