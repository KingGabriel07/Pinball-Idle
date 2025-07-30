using System.Collections.Generic;
using UnityEngine;

public class HandleItemPositions : MonoBehaviour
{
    #region Variables
    public Transform[] slots = new Transform[9];
    private Dictionary<int, GameObject> slotItems = new();
    private List<GameObject> activeItems = new();

    public GameObject itemPrefab;

    private GameObject heldItem = null;
    private Vector3 offset;
    private Vector3 originalItemPosition;
    #endregion
    void Start()
    {
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
            mouseWorldPos.z = 0f;
            heldItem.transform.position = mouseWorldPos + offset;

            if (Input.GetMouseButtonUp(0))
            {
                TryPlaceItem(heldItem);
                heldItem = null;
            }
        }

        // Example: spawn item on key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateNewItem();
        }
    }
    #region Item Position Management
    public void PickUpItem(GameObject item)
    {
        heldItem = item;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        offset = item.transform.position - mouseWorldPos;
        offset.z = -1f;

        originalItemPosition = item.transform.position;

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

                    Vector3 pos = slots[i].position;
                    pos.z = -1f;
                    item.transform.position = pos;
                }
                return;
            }
        }

        Vector3 fallback = originalItemPosition;
        fallback.z = -0.1f;
        item.transform.position = fallback;
    }

    public void UpdateAllItemPositions()
    {
        foreach (var pair in slotItems)
        {
            int slotIndex = pair.Key;
            GameObject item = pair.Value;

            if (item != null && slots[slotIndex] != null)
            {
                Vector3 slotPos = slots[slotIndex].position;
                slotPos.z = -1f; // Ensure proper z-depth
                item.transform.position = slotPos;
            }
        }
    }
    #endregion
    #region Item Creation / Deletion
    public void CreateNewItem()
    {
        GameObject newItem = Instantiate(itemPrefab, new Vector3(0, 0, -0.1f), Quaternion.identity);
        newItem.GetComponent<ItemScript>().inventory = this;
        activeItems.Add(newItem);
    }

    public void UnregisterItem(GameObject item)
    {
        if (activeItems.Contains(item))
        {
            activeItems.Remove(item);
        }

        foreach (var key in slotItems.Keys)
        {
            if (slotItems[key] == item)
            {
                slotItems[key] = null;
                break;
            }
        }
    }
    #endregion
}