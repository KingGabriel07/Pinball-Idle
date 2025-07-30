using System;
using System.Reflection;
using UnityEngine;

public class TabSwitcher : MonoBehaviour
{
    [Header("Tabs (Set these in the Inspector)")]
    public GameObject[] tabs = new GameObject[4];
    public GameObject[] uiTabs = new GameObject[4];

    [Header("Tab Positions")]
    public float activeXPosition = 0f;
    public float inactiveXPosition = 100f;
    public float inactiveUiPosition = 100f;


    /// <summary>
    /// Call this with the index (0–3) of the tab to activate.
    /// </summary>
    public void SwitchToTab(int index)
    {
        if (tabs == null || tabs.Length == 0 || uiTabs == null || uiTabs.Length == 0)
        {
            Debug.LogWarning("TabSwitcher: No tabs assigned.");
            return;
        }

        for (int i = 0; i < tabs.Length; i++)
        {
            if (tabs[i] != null)
            {
                Vector3 targetPos = tabs[i].transform.position;
                targetPos.x = (i == index) ? activeXPosition : inactiveXPosition;
                tabs[i].transform.position = targetPos;

                targetPos = uiTabs[i].transform.position;
                targetPos.x = (i == index) ? activeXPosition : inactiveUiPosition;
                uiTabs[i].transform.position = targetPos;
            }
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
            if (hit.collider != null && hit.collider.gameObject.name == "Game Tab")
            {
                SwitchToTab(0);
            }
            if (hit.collider != null && hit.collider.gameObject.name == "Shop Tab")
            {
                SwitchToTab(1);
            }
            if (hit.collider != null && hit.collider.gameObject.name == "Third Tab")
            {
                SwitchToTab(2);
            }
            if (hit.collider != null && hit.collider.gameObject.name == "Fourth Tab")
            {
                SwitchToTab(3);
            }
        }
    }

    
}
