using System;
using UnityEngine;
using TMPro;
using UnityEditor;

public class InventoryToolButton : MonoBehaviour
{
    [SerializeField] private ToolsType toolType;
    [SerializeField] private Inventory inventory;
    [SerializeField] private TMP_Text myText;
    
    private int countItems = 0;

    public void UpdateTextCount()
    {
        countItems = inventory.GetCountToolByType(toolType);
        myText.text = Convert.ToString(countItems);
    }
}
