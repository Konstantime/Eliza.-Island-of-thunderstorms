using System;
using UnityEngine;
using TMPro;


public class InventoryResourceButton : MonoBehaviour
{
    [SerializeField] private ResourcesType resourcesType;
    [SerializeField] private Inventory inventory;
    [SerializeField] private TMP_Text myText;

    private int countItems = 0;

    public void UpdateCount()
    {
        countItems = inventory.GetCountResourcesByType(resourcesType);
        myText.text = Convert.ToString(countItems);
    }
}
