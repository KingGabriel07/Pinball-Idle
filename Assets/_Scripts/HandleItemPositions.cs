using System.Collections.Generic;
using UnityEngine;

public class HandleItemPositions : MonoBehaviour
{
    public Transform[] slots = new Transform[9]; // Assign these in the inspector
    private Dictionary<int, GameObject> slotItems = new Dictionary<int, GameObject>();

    private GameObject heldItem = null;
    private Vector3 offset;

    private Vector3 originalItemPosition;

    void Start()
    {
        // Initialize all slots as empty
        for (int i = 0; i < slots.Length; i++)
        {
            slotItems[i] = null;
        }
    }

    void Update()
    {
        if (heldItem != null)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; // Set to your world-space plane (e.g. z = 0 for 2D)
            heldItem.transform.position = mouseWorldPos + offset;

            if (Input.GetMouseButtonUp(0))
            {
                TryPlaceItem(heldItem);
                heldItem = null;
            }
        }
    }


    public void PickUpItem(GameObject item)
    {
        heldItem = item;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        offset = item.transform.position - mouseWorldPos;

        originalItemPosition = item.transform.position; // Store this for later

        // Remove item from any existing slot
        foreach (var pair in slotItems)
        {
            if (pair.Value == item)
            {
                slotItems[pair.Key] = null;
                break;
            }
        }
    }

    void TryPlaceItem(GameObject item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Vector2.Distance(item.transform.position, slots[i].position) < 1f)
            {
                if (slotItems[i] == null)
                {
                    slotItems[i] = item;

                    Vector3 newPosition = slots[i].position;
                    newPosition.z = -1f;
                    item.transform.position = newPosition;
                }
                else
                {
                    // Optional: handle collision or swap logic
                }
                return;
            }
        }

        // No valid slot found — return to original position
        Vector3 fallback = originalItemPosition;
        fallback.z = -1f;
        item.transform.position = fallback;
    }
}
