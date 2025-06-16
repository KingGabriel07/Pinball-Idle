using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class TextUpdater : MonoBehaviour
{
    private Dictionary<string, TextMeshProUGUI> cachedTexts = new Dictionary<string, TextMeshProUGUI>();

    // Call this manually or from another script to register a TMP object by name
    public void RegisterTextObject(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);

        if (obj == null)
        {
            Debug.LogWarning($"TextUpdater: GameObject named '{objectName}' not found.");
            return;
        }

        TextMeshProUGUI tmp = obj.GetComponent<TextMeshProUGUI>();
        if (tmp == null)
        {
            Debug.LogWarning($"TextUpdater: GameObject '{objectName}' does not have a TextMeshProUGUI component.");
            return;
        }

        cachedTexts[objectName] = tmp;
    }

    // Call this to set the text after registration
    public void SetText(string objectName, string newText)
    {
        if (!cachedTexts.ContainsKey(objectName))
        {
            Debug.LogWarning($"TextUpdater: No cached reference for '{objectName}'. Attempting to register...");
            RegisterTextObject(objectName);
        }

        if (cachedTexts.TryGetValue(objectName, out TextMeshProUGUI tmp))
        {
            tmp.text = newText;
        }
    }
}
    